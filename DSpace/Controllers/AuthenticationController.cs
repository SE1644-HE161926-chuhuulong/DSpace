﻿using Application.Users;
using AutoMapper;
using Domain;
using DSpace.Services;
using Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Security.Claims;
using System.Text;

namespace DSpace.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : Controller
{
   private readonly IMapper _mapper;
   private readonly DSpaceDbContext _context;
   private readonly SignInManager<User> _signInManager;
   private readonly UserManager<User> _userManager;
   private readonly IHttpContextAccessor _httpContextAccessor;
   private IConfiguration _configuration;
   private UserService _userService;
   private JwtTokenService _jwtTokenService;

   private string? ThisProvider { get; set; }
   public AuthenticationProperties? ThisProperties { get; set; }
   public AuthenticationController(
       IMapper mapper,
       DSpaceDbContext context,
       SignInManager<User> signInManager,
       UserManager<User> userManager,
       IHttpContextAccessor httpContextAccessor,
       IConfiguration configuration,
       JwtTokenService jwtTokenService, UserService userService)
   {
      _mapper = mapper;
      _context = context;
      _signInManager = signInManager;
      _userManager = userManager;
      _httpContextAccessor = httpContextAccessor;
      _configuration = configuration;
      _jwtTokenService = jwtTokenService;
      _userService = userService;
   }

   public class RegisterModel
   {
      [Required]
      [EmailAddress]
      [Display(Name = "Email")]
      public string Email { get; set; }

      [Required]
      [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
      [DataType(DataType.Password)]
      [Display(Name = "Password")]
      public string Password { get; set; }

      [DataType(DataType.Password)]
      [Display(Name = "Confirm password")]
      [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
      public string ConfirmPassword { get; set; }
   }

   public class LoginModel
   {
      [Required]
      [Display(Name = "Username")]
      public string Username { get; set; }

      [Required]
      [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
      [DataType(DataType.Password)]
      [Display(Name = "Password")]
      public string Password { get; set; }

      [Display(Name = "Remember me?")]
      public bool RememberMe { get; set; }
   }
   
   public class LoginModel2
   {
      public string Email { get; set; }
      public string Given_Name { get; set; }
      public string Family_Name { get; set; }
   }
   
   
   [EnableCors("AllowSpecificOrigin")]
   [HttpPost("register-with-password")]
   public async Task<IActionResult> RegisterWithPassword(RegisterModel inputModel)
   {
      var externalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
      var user = CreateUser();
      user.UserName = RemoveDiacritics(inputModel.Email.Split("@")[0]);
      user.FirstName = "can be any first name";
      user.LastName = "can be any last name";

      user.Email = inputModel.Email;
      var result = await _userManager.CreateAsync(user, inputModel.Password);
      if (result.Succeeded)
      {
         result = await _userManager.AddToRoleAsync(user, RolesConstants.STAFF);
         if (result.Succeeded)
         {
            return Redirect("http://localhost:3000/");
         }
      }
      return NotFound();
   }

   [EnableCors("AllowSpecificOrigin")]
   [HttpPost("login-with-password")]
   public async Task<IActionResult> SignInPassword(LoginModel inputModel)
   {
      var externalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
      var result = await _signInManager.PasswordSignInAsync(inputModel.Username, inputModel.Password,
          inputModel.RememberMe, lockoutOnFailure: false);
      if (result.Succeeded)
      {
         return Redirect("http://localhost:3000/homepageAdmin");
      }
      else
      {
         return Unauthorized("Cannot sign in");
      }
   }

   [HttpPost("AssignRole")]
   public async Task<IActionResult> AssignRole(UserDTOForAssignRole request)
   {
      var user = await _userService.getUserByEmail(request.Email);
      if (user == null)
      {
         return BadRequest("nguoi dung chua tung dang nhap vao he thong");
      }
     
      var result = await _userService.AssignRole(user, request.Role);
      return Ok(await _userService.GetRole(user));
   }


   private User CreateUser()
   {
      try
      {
         return Activator.CreateInstance<User>();
      }
      catch
      {
         throw new InvalidOperationException($"Can't create an instance of '{nameof(User)}'. ");
      }
   }

   private string RemoveDiacritics(string text)
   {
      var normalizedString = text.Normalize(NormalizationForm.FormD);
      var stringBuilder = new StringBuilder();

      foreach (char c in normalizedString)
      {
         var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
         if (unicodeCategory != UnicodeCategory.NonSpacingMark && unicodeCategory != UnicodeCategory.SpaceSeparator)
         {
            stringBuilder.Append(c);
         }
      }

      return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
   }

   [HttpGet("getCurrentUser")]
   [Authorize]
   public IActionResult GetCurrentUser()
   {
      //var identity = User.Identity as ClaimsIdentity;
      var identity = HttpContext.User.Identity as ClaimsIdentity;

      if (identity != null)
      {
         var userClaims = identity.Claims;
         var id = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier).Value;
         var role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value;
         var email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email).Value;

         return Ok(new
         {
            id,
            email,
            role
         });
      }
      return Unauthorized();
   }
   
   [AllowAnonymous]
   [HttpPost("Login")]
   public async Task<IActionResult> LoginWithGoogle(LoginModel2 request) {
      
      string email = request.Email;
      //if(email.Contains("@fpt.edu.vn"))
      //{
      //   return Unauthorized();
      //}
      var userCheck = await _userService.getUserByEmail(email);
      if (userCheck == null)
      {
         var userNew = _userService.CreateInstanceUser(); //CreateUser();
         userNew.Email = email;
         userNew.FirstName =  request.Family_Name;
         userNew.LastName = request.Given_Name;
         
         userNew.isActive = true;
         userNew.UserName = email;

         var createUserResult = await _userService.CreateUser(userNew);
         if (createUserResult)
         {
            await _userService.AssignRole(userNew, RolesConstants.STUDENT);

         }
      }
      var user = await _userService.getUserByEmail(email);

      var userRole = await _userService.GetRole(user);

      var authClaims = _userService.getUserClaim(user, userRole);

      var token = _jwtTokenService.GenerateToken(authClaims);

      return Ok(new
      {
         token,
         role = userRole,
         name = request.Given_Name + " " + request.Family_Name
      });
   }
}