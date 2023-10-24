
using Banfsg.BL;
using Banfsg.DAL;
using Banfsg.DAL.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.RateLimiting;

namespace Banfsg.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        #region default services
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        #endregion

        #region Database

        var connectionString = builder.Configuration.GetConnectionString("Banfsg_Connection_string");//get connection string from secrets

        builder.Services.AddDbContext<BanfsgContext>(options => options.UseSqlServer(connectionString));
        #endregion

        builder.Services.Configure<MailSetting>(builder.Configuration.GetSection("MailSetting"));//to read from the appJsonSections
  

        #region Identity  //configure the manager             
        //to use the identity functionalities after install its packages and userManager(which user and which context)
        builder.Services.AddIdentity<User, IdentityRole>(options =>
        {
            options.Password.RequiredUniqueChars = 3;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 3;
            options.Password.RequireDigit = false;
            options.User.RequireUniqueEmail = true;


        }
        )
            .AddEntityFrameworkStores<BanfsgContext>()
            .AddDefaultTokenProviders();//// This line registers the default token providers, including the two-factor token provider(nessecary for generate the password reset token

        builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
        options.TokenLifespan = TimeSpan.FromHours(10));//make the reset pass token valid for 10 hours
        #endregion

        #region Authentication
        //we should tell it,which secret key it will use to authenticate the request, we should set 3 things(authentication schema,challenge schema
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "BanfsgAuthentication";
            options.DefaultChallengeScheme = "BanfsgAuthentication";



        }).AddJwtBearer("BanfsgAuthentication", options =>
        {
            var keyString = builder.Configuration.GetValue<string>("SecretKey");
            var KeyByteArray = Encoding.ASCII.GetBytes(keyString!);
            var keySecurityKey = new SymmetricSecurityKey(KeyByteArray);
            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = keySecurityKey,
                ValidateIssuer = false,
                ValidateAudience = false
            };


        });//which authentication scheme we will use and its options,when you authenticate the request with BanfsgAuth,use this secretkey
        #endregion

        #region Authorization
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("ForCustomersOnly", policy =>
            policy.RequireClaim(ClaimTypes.Role, "Customer")
            );
            options.AddPolicy("ForAdminsOnly", policy =>
            policy.RequireClaim(ClaimTypes.Role, "Admin")
            );
        });
        #endregion

        #region Repos & Managers Services
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddTransient<IEmailManager, EmailManager>();
        builder.Services.AddScoped<IUserRepo, UserRepo>();
        builder.Services.AddScoped<IUsersManager, UsersManager>();
        builder.Services.AddScoped<IUserAddressesManager, UserAddressesManager>();
        builder.Services.AddScoped<IUserAddressesRepo, UserAddressesRepo>();
        builder.Services.AddScoped<IUserProductCartRepo, UserProductCartRepo>();
        builder.Services.AddScoped<IUserProductCartManager, UserProductCartManager>();
        builder.Services.AddScoped<IOrderRepo, OrderRepo>();
        builder.Services.AddScoped<IOrdersManager, OrdersManager>();
        builder.Services.AddScoped<IOrderDetailsRepo, OrderDetailsRepo>();
        builder.Services.AddScoped<IUserProfileRepo, UserProfileRepo>();
        builder.Services.AddScoped<IUserProfileManager, UserProfileManager>();
        builder.Services.AddScoped<IProductRepo, ProductRepo>();
        builder.Services.AddScoped<IProductManager, ProductManager>();
        builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
        builder.Services.AddScoped<ICategoryManager, CategoryManager>();


        #endregion

        #region Rate limiting service
        builder.Services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;//This specifies the HTTP status code that will be returned when a rate limit is exceeded
            options.AddFixedWindowLimiter("fixed", options =>
            {
                options.Window = TimeSpan.FromSeconds(10); //the rate limit will apply within a 10 - second window.
                options.PermitLimit = 3;    //maximum number of requests allowed to send in 10 seconds
                options.QueueLimit = 3;    // the maximum number of requests that can be queued when the rate limit is reached
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;// the order in which requests in the queue will be processed the oldest requests in the queue will be processed first when permits become available.
            });
        });
        #endregion

      

       

        var app = builder.Build();

        #region MiddleWares

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseRateLimiter(); //come after authentication and authorization

        app.MapControllers();
        #endregion

        app.Run();
    }
}