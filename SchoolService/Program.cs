
using SchoolService.Db;
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

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // CORS politikas� ekleyin
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()  // Herhangi bir kayna�a izin verir
                          .AllowAnyMethod()  // T�m HTTP y�ntemlerine izin verir (GET, POST, PUT, DELETE vb.)
                          .AllowAnyHeader(); // T�m ba�l�klara izin verir
                });
            });

            #region Services - DI

            builder.Services.AddTransient<SchoolServiceDbContext>();


            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // Generic


            builder.Services.AddTransient<ITeacherRepository, TeacherRepository>();
            builder.Services.AddTransient<ITeacherService, TeacherService>();


            builder.Services.AddTransient<IBranchRepository, BranchRepository>();
            builder.Services.AddTransient<IBranchService, BranchService>();


            builder.Services.AddTransient<IUniversityRepository, UniversityRepository>();
            builder.Services.AddTransient<IUniversityService, UniversityService>();


            builder.Services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddTransient<IDepartmentService, DepartmentService>();


            builder.Services.AddTransient<IBaseLessonRepository, BaseLessonRepository>();
            builder.Services.AddTransient<IBaseLessonService, BaseLessonService>();

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
