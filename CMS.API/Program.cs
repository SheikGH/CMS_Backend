using AutoMapper;
using CMS.API.Extensions;
using CMS.API.Middlewares;
using CMS.Application.DTOs;
using CMS.Application.Interfaces;
using CMS.Application.Mappings;
using CMS.Application.Security;
using CMS.Application.Services;
using CMS.Core.Entities;
using CMS.Core.Interfaces;
using CMS.Infrastructure;
using CMS.Infrastructure.Data;
using CMS.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Get database connection string and password from Environemnt Variable
//var sqlConn = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("dbCMS"));
//sqlConn.Password = builder.Configuration.GetSection("DBPassword").Value;
//var connString = sqlConn.ConnectionString;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
//options.UseSqlServer(connString));
options.UseSqlServer(builder.Configuration.GetConnectionString("dbCMS")));

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddCors();

// Auto Mapper Configurations
builder.Services.AddAutoMapper(typeof(CustomerProfile));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

//move some dependency injection as Application and Infrastructure projects
//builder.Services.AddApplication();
//builder.Services.AddInfrastructure(builder.Configuration);
//builder.Services.AddScoped<JwtService>();

//Implement CQRS by using MediatR 
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());


var secretKey = builder.Configuration.GetSection("Jwt:Key").Value;
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            //ValidIssuer = builder.Configuration["Jwt:Issuer"],
            //ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Implement Cache to Improve Performance
//builder.Services.AddOutputCache();
//builder.Services.AddOutputCache(options => {
//    options.DefaultExpireTimeSpan = TimeSpan.FromSeconds(20);
//});
builder.Services.AddResponseCaching(x => x.MaximumBodySize = 1024);

var app = builder.Build();

//Comment default exception handling and create an customer exception handler by using MiddleWare
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
//else
//{
//    app.UseExceptionHandler(
//        options =>
//        {
//            options.Run(
//                async context =>
//                {
//                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
//                    var ex = context.Features.Get<IExceptionHandlerFeature>();
//                    if (ex != null)
//                    {
//                        await context.Response.WriteAsync(ex.Error.Message);
//                    }
//                }
//             );
//        }
//    );
//}

//MiddleWare to handle exception - Builtin - exception handling
//app.ConfigureBuiltinExceptionHandler();
//MiddleWare to handle exception - Custom - exception handling
app.ConfigureExceptionHandler();

app.UseHsts();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(m => m.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

//Implement Cache to Improve Performance
//app.UseOutputCache();
app.UseResponseCaching();
app.Use(async (context, next) =>
{
    context.Response.GetTypedHeaders().CacheControl =
   new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
   {
       Public = true,
       MaxAge = TimeSpan.FromSeconds(10)
   };
    await next();
});
app.Run();
