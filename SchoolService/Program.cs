
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


            builder.Services.AddTransient<SchoolServiceDbContext>();


            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // Generic


            builder.Services.AddTransient<ITeacherRepository, TeacherRepository>();

            builder.Services.AddTransient<ITeacherService, TeacherService>();
            
            builder.Services.AddTransient<IBranchRepository, BranchRepository>();

            builder.Services.AddTransient<IUniversityRepository, UniversityRepository>();
            builder.Services.AddTransient<IUniversityService, UniversityService>();




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
