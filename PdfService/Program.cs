
using Microsoft.Extensions.DependencyInjection;
using PdfService.Db;
using PdfService.Services;
using PdfService.Services.Interfaces;

namespace PdfService
{
    public class Program
    {
        public static void Main(string[] args)
        {

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);


            var builder = WebApplication.CreateBuilder(args);

           

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Services

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

            builder.Services.AddTransient<IPdfEditingService, PdfEditingService>();

            // DbContext Service
            builder.Services.AddDbContext<PdfServiceDbContext>();


            //// Config Service
            //var conf = builder.Configuration;

            //builder.Services.AddSingleton<IConfiguration>(conf);



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Cors

            app.UseCors("AllowAllOrigins");



            app.MapGet("/", (IConfiguration config) =>
            {
                var appName = config["AppSettings:AppName"];
                return $"Application Name: {appName}";
            });



            app.MapControllers();

            app.Run();
        }
    }
}
