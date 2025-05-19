
using Microsoft.EntityFrameworkCore;
using SchoolService.Db;
using SchoolService.Mapper;
using SchoolService.Repositories;
using SchoolService.Repositories.Interfaces;
using SchoolService.Services;
using SchoolService.Services.Interfaces;

namespace SchoolService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // String olarak gelen enum deðerlerini dönüþtürmeyi saðlar
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // CORS politikasý ekleyin
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()  // Herhangi bir kaynaða izin verir
                          .AllowAnyMethod()  // Tüm HTTP yöntemlerine izin verir (GET, POST, PUT, DELETE vb.)
                          .AllowAnyHeader(); // Tüm baþlýklara izin verir
                });
            });

            #region Services - DI


            // mapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // DbContext
            builder.Services.AddDbContext<SchoolServiceDbContext>(opt => { opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")); });

            builder.Services.AddTransient<SchoolServiceDbContext>();


            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // Generic


            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IUserService, UserService>();


            builder.Services.AddTransient<IUniversityRepository, UniversityRepository>();
            builder.Services.AddTransient<IUniversityService, UniversityService>();


            builder.Services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddTransient<IDepartmentService, DepartmentService>();


            builder.Services.AddTransient<ILessonRepository, LessonRepository>();
            builder.Services.AddTransient<ILessonService, LessonService>();

            #endregion



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Cors

            app.UseCors("AllowAllOrigins");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
