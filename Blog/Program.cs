using Microsoft.AspNetCore.Authentication.JwtBearer;
using BusinessLogicLayer.IServices;
using BusinessLogicLayer.Service;
using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using DataAccessLayer.UnitOfWorkFolder;
using DomainLayer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using BusinessLogicLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Configure Database Connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Inject UnitOfWork containing all repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// 🔹 Inject AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// 🔹 Inject Services
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ILikeService, LikeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFollowerService, FollowerService>();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();

// 🔹 Configure Identity
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// 🔹 Configure JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"] ?? throw new ArgumentNullException("JWT Key is missing."));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"]
    };
});

// 🔹 Configure Authorization
builder.Services.AddAuthorization(options =>
{
    // Only require authentication when explicitly requested
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
        
    // No fallback policy means no implicit authorization
    options.FallbackPolicy = null;
});

// 🔹 Add Controllers
builder.Services.AddControllers();

// 🔹 Configure Swagger with JWT Authentication
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JewelBlog API",
        Version = "v1",
        Description = "API documentation for JewelBlog API."
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by your JWT token."
    });

    // Use operation filter to only apply security to endpoints with [Authorize]
    c.OperationFilter<SwaggerAuthorizeOperationFilter>();
});

// 🔹 Identity Credentials Configuration
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = false;

    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

var app = builder.Build();

// 🔹 Configure Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "JewelBlog API v1");
    });
}

app.UseHttpsRedirection();

// Authentication middleware comes before Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();

// Fixed operation filter with proper null checking
public class SwaggerAuthorizeOperationFilter : Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter
{
    public void Apply(OpenApiOperation operation, Swashbuckle.AspNetCore.SwaggerGen.OperationFilterContext context)
    {
        // Check for authorize attribute on the method and its containing class
        var hasAuthorize = false;
        
        // Check method attributes if method info is available
        if (context.MethodInfo != null)
        {
            hasAuthorize = context.MethodInfo.GetCustomAttributes<AuthorizeAttribute>(true).Any();
            
            // If no authorize on method, check the controller
            if (!hasAuthorize && context.MethodInfo.DeclaringType != null)
            {
                hasAuthorize = context.MethodInfo.DeclaringType.GetCustomAttributes<AuthorizeAttribute>(true).Any();
            }
        }

        if (hasAuthorize)
        {
            // Add JWT bearer token requirement
            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                }
            };
        }
    }
}