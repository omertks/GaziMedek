using MessageService.Services.Abstracts;
using MessageService.Db;
using Microsoft.EntityFrameworkCore;
using MessageService.Services;

namespace MessageService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Swagger for development environment
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

            // Database connection
            builder.Services.AddDbContext<MessageServiceDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Dependency injection
            builder.Services.AddScoped<IChatService, ChatService>();
            builder.Services.AddScoped<IMessageService, MessageServiceC>();
            builder.Services.AddHttpClient<UserClient>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            // Cors

            app.UseCors("AllowAllOrigins");

            app.MapControllers();

            app.Run();
        }
    }
}
