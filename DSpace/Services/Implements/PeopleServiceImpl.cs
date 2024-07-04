using Application.Peoples;
using Application.Responses;
using AutoMapper;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DSpace.Services.Implements
{
   public class PeopleServiceImpl : PeopleService
   {
      private IMapper _mapper;
      private readonly DSpaceDbContext _context;
      private UserManager<User> _userManager;
      private UserService _userService;


      protected ResponseDTO _response;

      public PeopleServiceImpl(IMapper mapper, DSpaceDbContext context, UserManager<User> userManager, UserService userService)
      {
         _context = context;
         _response = new ResponseDTO();
         _mapper = mapper;
         _userManager = userManager;
         _userService = userService;
      }

      public async Task<ResponseDTO> CreatePeople(PeopleDTOForCreateUpdate peopleDTO, int userCreateId)
      {
         try
         {
            var userCheck = await _userManager.Users.Include(x => x.People).FirstOrDefaultAsync(x => x.Email.Equals(peopleDTO.Email));
            if (userCheck == null)
            {
               var userNew = _userService.CreateInstanceUser();
               userNew.Email = peopleDTO.Email;
               userNew.isActive = true;
               userNew.FirstName = peopleDTO.FirstName;
               userNew.LastName = peopleDTO.LastName;
               userNew.UserName = peopleDTO.Email.Split('@')[0];
               var result = await _userService.CreateUser(userNew);
               if (result == false)
               {
                  throw new Exception();
               }
               userCheck = userNew;
            }
            else
            {
               userCheck.Email = peopleDTO.Email;
               userCheck.isActive = true;
               userCheck.FirstName = peopleDTO.FirstName;
               userCheck.LastName = peopleDTO.LastName;
               userCheck.UserName = peopleDTO.Email.Split('@')[0];
               var result = await _userService.UpdateUser(userCheck);
               if (result == false)
               {
                  throw new Exception();
               }

            }
            var peopleCheck = userCheck.People;
            if (userCheck.People == null)
            {
               People peopleNew = new People();
               peopleNew.Address = peopleDTO.Address;
               peopleNew.PhoneNumber = peopleDTO.PhoneNumber;
               peopleNew.CreatedDate = DateTime.Now;
               peopleNew.LastModifiedDate = DateTime.Now;
               peopleNew.UserId = userCheck.Id;
               peopleNew.CreatedBy = userCreateId;
               await _context.Peoples.AddAsync(peopleNew);
               peopleCheck = peopleNew;
            }
            else
            {
               peopleCheck.Address = peopleDTO.Address;
               peopleCheck.PhoneNumber = peopleDTO.PhoneNumber;
               peopleCheck.CreatedDate = DateTime.Now;
               peopleCheck.LastModifiedDate = DateTime.Now;

            }
            await _userService.AssignRole(userCheck, peopleDTO.rolename);
            await _context.SaveChangesAsync();
            _response.IsSuccess = true;
            _response.Message = "Add people success";
            var peopleDTOForSelect = _mapper.Map<PeopleDTOForSelect>(peopleCheck);
            //var peopleDTO = _mapper.Map<PeopleDTOForSelect>(people);
            var user = await _userService.getUserByEmail(peopleDTO.Email);
            peopleDTOForSelect.Role = await _userService.GetRole(user);
            _response.ObjectResponse = peopleDTO;
         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }

      public async Task<ResponseDTO> DeletePeople(int id)
      {
         try
         {
            var peopleDelete = await _context.Peoples.Include(x => x.PeopleParent).Include(x => x.GroupPeoples).Include(x => x.Collection).Include(x => x.Community).Include(x => x.ListPeopleCreated).Include(x=>x.User).SingleOrDefaultAsync(x => x.PeopleId == id);
            if (peopleDelete == null)
            {
               _response.IsSuccess = false;
               _response.Message = "People with " + id + "does not exist";
            }
            else
            {
               if (peopleDelete.GroupPeoples.Count != 0 || peopleDelete.Community.Count != 0 || peopleDelete.Collection.Count != 0 || peopleDelete.ListPeopleCreated.Count != 0)
               {
                  _response.IsSuccess = false;
                  _response.Message = "People with " + id + " is relative with collections or communities or list people created or people is exist in a group, please delete all it if you want delete this people  ";
               }
               else
               {
                  var user =  peopleDelete.User;
                  var rolesForUser = await _userManager.GetRolesAsync(user);
                  if (rolesForUser.Count() > 0)
                  {
                     foreach (var item in rolesForUser.ToList())
                     {
                        // item should be the name of the role
                        var result = await _userManager.RemoveFromRoleAsync(user, item);
                     }
                  }
                  _context.Peoples.Remove(peopleDelete);
                  await _userManager.DeleteAsync(user);
                  await _context.SaveChangesAsync();
                  _response.IsSuccess = true;
                  _response.Message = "Delete people with " + id + "success";
               }
            }
         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }
      public async Task<ResponseDTO> GetPeopleByID(int id)
      {
         try
         {
            var people = await _context.Peoples
               .Include(x => x.PeopleParent.User)
               .Include(x => x.User)
               .ThenInclude(x => x.UserRoles)
               .ThenInclude(x => x.Role)

               .SingleOrDefaultAsync(x => x.PeopleId == id);
            var peopleDTO = _mapper.Map<PeopleDTOForSelect>(people);
            var user = await _userService.getUserByEmail(peopleDTO.Email);
            peopleDTO.Role = await _userService.GetRole(user);
            if (people == null)
            {
               _response.IsSuccess = true;
               _response.Message = "people with id " + id + " does not exist";
            }
            else
            {
               _response.IsSuccess = true;
               _response.ObjectResponse = peopleDTO;
            }
         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }

         return _response;
      }

      public async Task<ResponseDTO> GetPeopleByName(string name)
      {
         try
         {
            IEnumerable<People> objList = await _context.Peoples
               .Include(p => p.User)
               .Where(x => (x.User.FirstName).Contains(name) || (x.User.LastName).Contains(name))
               .Include(x => x.User)
               .ThenInclude(x => x.UserRoles)
               .ThenInclude(x => x.Role)
               .ToListAsync();

            var peopleDTOMapper = _mapper.Map<List<PeopleDTOForSelect>>(objList);
            foreach (var people in peopleDTOMapper)
            {
               var user = await _userService.getUserByEmail(people.Email);
               people.Role = await _userService.GetRole(user);
            }
            _response.IsSuccess = true;
            _response.ObjectResponse = peopleDTOMapper;

         }
         catch (Exception ex)
         {
            _response.Message = ex.Message;
            _response.IsSuccess = false;
         }
         return _response;
      }

      public async Task<ResponseDTO> UpdatePeople(int id, PeopleDTOForCreateUpdate peopleDTO, int userUpdateId)
      {
         try
         {
            var check = await _context.Peoples.FindAsync(id);
            if (check == null)
            {
               _response.IsSuccess = true;
               _response.Message = "people with id " + id + " does not exist";
            }

            else
            {
               var userCheck = await _userService.getUserById(check.UserId);

               check.Address = peopleDTO.Address;
               check.PhoneNumber = peopleDTO.PhoneNumber;
               check.LastModifiedBy = userUpdateId;
               check.LastModifiedDate = DateTime.Now;
               //check.UserId = peopleDTO.UserId;
               userCheck.FirstName = peopleDTO.FirstName;
               userCheck.LastName = peopleDTO.LastName;
               userCheck.Email = peopleDTO.Email;
               userCheck.UserName = peopleDTO.Email.Split('@')[0];
               await _userService.AssignRole(userCheck, peopleDTO.rolename);
               var updateResult = await _userService.UpdateUser(userCheck);
               if (!updateResult)
               {
                  throw new Exception();
               }


               _context.Peoples.Update(check);
               await _context.SaveChangesAsync();
               var peopleDTOForSelect = _mapper.Map<PeopleDTOForSelect>(check);
               //var peopleDTO = _mapper.Map<PeopleDTOForSelect>(people);
               var user = await _userService.getUserByEmail(peopleDTO.Email);
               peopleDTOForSelect.Role = await _userService.GetRole(user);
               _response.ObjectResponse = peopleDTO;
               _response.IsSuccess = true;
            }
         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }
      public async Task<ResponseDTO> GetAllPeople()
      {
         try
         {
            IEnumerable<People> objList = await _context.Peoples.Include(x => x.User).ThenInclude(x => x.UserRoles).ThenInclude(x => x.Role).ToListAsync();

            var peopleDTOMapper = _mapper.Map<List<PeopleDTOForSelect>>(objList);
            foreach (var people in peopleDTOMapper)
            {
               var user = await _userService.getUserByEmail(people.Email);
               people.Role = await _userService.GetRole(user);
            }
            _response.IsSuccess = true;
            _response.ObjectResponse = peopleDTOMapper;

         }
         catch (Exception ex)
         {
            _response.Message = ex.Message;
            _response.IsSuccess = false;
         }
         return _response;
      }

      public async Task<ResponseDTO> GetPeopleByEmail(string email)
      {
         try
         {
            var peoples = await _context.Peoples.Where(x => x.User.Email.Contains(email)).Include(x => x.User).ThenInclude(x => x.UserRoles).ToListAsync();

            if (peoples == null)
            {
               _response.IsSuccess = true;
               _response.Message = "people with " + email + " does not exist";
               _response.ObjectResponse = null;
            }
            else
            {
               var peopleDTOMapper = _mapper.Map<List<PeopleDTOForSelect>>(peoples);
               foreach (var people in peopleDTOMapper)
               {
                  var user = await _userService.getUserByEmail(people.Email);
                  people.Role = await _userService.GetRole(user);
               }
               _response.IsSuccess = true;
               _response.Message = "success";
               _response.ObjectResponse = peopleDTOMapper;
            }
         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }
   }
}