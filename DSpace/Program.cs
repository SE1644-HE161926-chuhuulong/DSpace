using Application.Extensions;
using Domain;
using DSpace.Configurations;
using DSpace.Services;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddDatabaseModule<DSpaceDbContext>(builder.Configuration);

builder.Services.AddIdentity<User, Role>(
        options => {
            // options.SignIn.RequireConfirmedEmail = true;
            // options.SignIn.RequireConfirmedAccount = true;
        }
    )
    .AddEntityFrameworkStores<DSpaceDbContext>()
    .AddUserStore<UserStore<User, Role, DSpaceDbContext, string, IdentityUserClaim<string>,
        UserRole, IdentityUserLogin<string>, IdentityUserToken<string>, IdentityRoleClaim<string>>>()
    .AddRoleStore<RoleStore<Role, DSpaceDbContext, string, UserRole, IdentityRoleClaim<string>>>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
    {
        
        //options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
       options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
       options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
       options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddCookie()
    .AddGoogle(options =>
    {
        IConfigurationSection googleAuthNSection = builder.Configuration.GetSection("Authentication:Google");
        options.ClientId = googleAuthNSection["ClientId"];
        options.ClientSecret = googleAuthNSection["ClientSecret"];
        options.CallbackPath = "/signin-google";
    }).AddJwtBearer(options =>
    {
       options.SaveToken = true;
       options.RequireHttpsMetadata = false;
       options.TokenValidationParameters = new TokenValidationParameters()
       {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ValidAudience = builder.Configuration["JWT:ValidAudience"],
          ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
       };
    });
builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("https://localhost:7200", "https://localhost:7200", "http://localhost:50060")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScopedCollection();
builder.Services.AddExtensions(builder.Configuration);
builder.Services.Configure<FormOptions>(options =>
{
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartBodyLengthLimit = int.MaxValue;
    options.MultipartHeadersLengthLimit = int.MaxValue;
});
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = int.MaxValue;
});
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo() { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();
IServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
app.UseApplicationDatabase<DSpaceDbContext>(serviceProvider, app.Environment);
app.UseApplicationIdentity(serviceProvider);
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors(
    builder => builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();