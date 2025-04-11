
using Entity.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserService.Db;
using UserService.Repository;
using UserService.Repository.Interfaces;
using UserService.Service;
using UserService.Service.Interfaces;

namespace UserService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var jwtSettings = builder.Configuration.GetSection("Jwt"); // Burada appsettins.json dosyas�nda bulunan Jwt objesine eri�tim

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // Hangi de�erlerin do�rulanaca��:
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    // Ne ile Do�rulanaca��: (Yani buras� gelen token da olmas� gereknleri belirtti�imiz alan gibi)
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
                };
            });


            builder.Services.AddDbContext<UserServiceDbContext>();

            builder.Services.AddTransient<IUserRepository, UserRepository>();

            builder.Services.AddTransient<IUserService, UserManager>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Uygulamaya kimlik do�rulama ve yetki sisteminin dahil edilmesi
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
