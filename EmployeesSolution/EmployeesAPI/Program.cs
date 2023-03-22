using AutoMapper;
using EmployeesApi;
using EmployeesApi.Controllers.Domain;
using EmployeesAPI.AutomapperProfiles;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace EmployeesAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IDepartmentLookup, DepartmentLookup>();
            builder.Services.AddScoped<ILookupEmployees, EntityFrameworkEmployeeLookup>();
            builder.Services.AddScoped<IManageEmployees, EntityFrameworkEmployeeLookup>();

            var sqlConnectionString = builder.Configuration.GetConnectionString("employees");
            Console.WriteLine("Using this connection string " + sqlConnectionString);

            if (sqlConnectionString == null)
            {
                throw new Exception("Don't start this api! Can't connect to a database");
            }
            
            builder.Services.AddDbContext<EmployeeDataContext>(options =>
            {
                options.UseSqlServer(sqlConnectionString);
            });

            var mapperConfig = new MapperConfiguration(options =>
            {
                options.AddProfile<Departments>();
                options.AddProfile<Employees>();
            }); 
            var mapper = mapperConfig.CreateMapper(); 
            builder.Services.AddSingleton<MapperConfiguration>(mapperConfig);
            builder.Services.AddSingleton<IMapper>(mapper);

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