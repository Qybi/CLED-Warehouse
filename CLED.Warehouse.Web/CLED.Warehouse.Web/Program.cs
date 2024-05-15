using CLED.WareHouse.Services.DBServices;
using CLED.WareHouse.Services.DBServices.AccessoryServices;
using CLED.WareHouse.Services.DBServices.PcServices;
using CLED.WareHouse.Services.DBServices.ReasonsServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CLED.Warehouse.Web.EndPoints;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CLED.Warehouse.Authentication;
using CLED.Warehouse.Authentication.Utils.Abstractions;
using CLED.Warehouse.Authentication.Utils;
using Microsoft.EntityFrameworkCore;
using CLED.WareHouse.Services.DBServices.Interfaces;
using CLED.Warehouse.Data.Abstractions;
using ISTES.Appointment.Data;

namespace CLED.Warehouse.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services
				.AddAuthentication(auth =>
				{
					auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(options =>
				{
					options.SaveToken = true;
					options.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuer = false,
						ValidateAudience = false,
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty))
					};
				});
			builder.Services.AddAuthorization();

			builder.Services.AddDbContext<WarehouseContext>(options =>
			{
				options.UseNpgsql(builder.Configuration.GetConnectionString("db"));
			});

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			//builder.Services.AddEndpointsApiExplorer();
			//builder.Services.AddSwaggerGen();

			builder.Services.AddScoped<IHashingUtils, HashingUtils>();
			builder.Services.AddScoped<IAuthManager, AuthManager>(opt => new AuthManager(
					opt.GetRequiredService<ILogger<AuthManager>>(),
					opt.GetRequiredService<IHashingUtils>(),
					builder.Configuration.GetConnectionString("db")
			)
			);

			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

			//builder.Services.AddScoped<PcService>();
			//builder.Services.AddScoped<PcModelStockService>();
			//builder.Services.AddScoped<PcAssignmentService>();
			//builder.Services.AddScoped<AccessoryAssignmentService>();
			//builder.Services.AddScoped<AccessoryService>();
			//builder.Services.AddScoped<ReasonAssignmentService>();
			//builder.Services.AddScoped<ReasonReturnService>();
			//builder.Services.AddScoped<CourseService>();
			//builder.Services.AddScoped<StudentService>();
			//builder.Services.AddScoped<TicketService>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			//if (app.Environment.IsDevelopment())
			//{
			//	app.UseSwagger();
			//	app.UseSwaggerUI();
			//}
			app.UseStaticFiles();
			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();

			// Mapping Application Endpoints
			app.MapPcEndPoints();
			app.MapPcModelStockEndPoints();
			app.MapPcAssignmentPoints();
			app.MapAccessoryEndPoints();
			app.MapAccessoryAssignmentEndPoint();
			app.MapReasonAssignmentEndPoints();
			app.MapReasonReturnEndPoints();
			app.MapCourseEndPoints();
			app.MapStudentEndPoints();
			app.MapTicketEndPoints();
			app.Run();

		}
	}
}
