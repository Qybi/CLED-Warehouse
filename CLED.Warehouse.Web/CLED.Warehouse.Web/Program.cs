using CLED.WareHouse.Services.DBServices;
using CLED.WareHouse.Services.DBServices.AccessoryServices;
using CLED.WareHouse.Services.DBServices.PcServices;
using CLED.WareHouse.Services.DBServices.ReasonsServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;

namespace CLED.Warehouse.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
			builder.Services.AddAuthorization();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddScoped<PcService>();
			builder.Services.AddScoped<PcModelStockService>();
			builder.Services.AddScoped<PcAssignmentService>();
			builder.Services.AddScoped<AccessoryAssignmentService>();
			builder.Services.AddScoped<AccessoryService>();
			builder.Services.AddScoped<ReasonAssignmentService>();
			builder.Services.AddScoped<ReasonReturnService>();
			builder.Services.AddScoped<CourseService>();
			builder.Services.AddScoped<StudentService>();
			builder.Services.AddScoped<TicketService>();
			

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();
			app.MapGet("/", () => "Hello World");
			
			//app.MapPcEndpoints();     // mapping endpoints for REST API
			
			app.Run();
		}
	}
}
