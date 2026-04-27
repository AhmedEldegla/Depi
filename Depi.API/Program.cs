using DEPI.Application.UseCases.Projects.CreateProject;
using DEPI.Infrastructure.DependencyInjection;
namespace Depi.API

{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           
            builder.Services.AddMediatR(cfg =>
               cfg.RegisterServicesFromAssembly(typeof(CreateProjectCommand).Assembly));      

            // Add services to the container.

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("DefaultConnection is missing in appsettings.json");
            }

            builder.Services.AddInfrastructure(connectionString);


            builder.Services.AddControllers();


            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            
           
            

            app.MapControllers();

            app.Run();
        }
    }
}
