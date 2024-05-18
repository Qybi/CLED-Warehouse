using CLED.Warehouse.Models.DB;
using CLED.WareHouse.Services.DBServices.Interfaces;
using CLED.Warehouse.Web;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Npgsql;
using Microsoft.EntityFrameworkCore;

namespace CLED.WareHouse.Services.DBServices.PcServices;

public class PcAssignmentService : IService<PcAssignment>
{
	private readonly string _connectionString;
	private readonly WarehouseContext _context;

	public PcAssignmentService(IConfiguration? configuration, WarehouseContext context)
	{
		_connectionString = configuration.GetConnectionString("db");
		_context = context;
	}
	public async Task<PcAssignment> GetById(int pcAssignmentId)
	{
		await using var connection = new NpgsqlConnection(_connectionString);
		await connection.OpenAsync();

		string query = """
                       SELECT "Id", 
                              "PCId", 
                              "StudentId", 
                              "AssignmentDate", 
                              "AssignmentReasonId", 
                              "IsReturned", 
                              "ForecastedReturnDate", 
                              "ActualReturnDate", 
                              "ReturnReasonId", 
                              "RegistrationDate", 
                              "RegistrationUser", 
                              "DeletedDate", 
                              "DeletedUser"
                       FROM "PCAssignments"
                       WHERE "Id" = @id;
                       """;

		return await connection.QueryFirstOrDefaultAsync<PcAssignment>(query, new { id = pcAssignmentId });
	}

	public async Task<IEnumerable<PcAssignment>> GetAll()
	{
		await using var connection = new NpgsqlConnection(_connectionString);
		await connection.OpenAsync();

		string query = """
                       SELECT "Id", 
                              "PCId", 
                              "StudentId", 
                              "AssignmentDate", 
                              "AssignmentReasonId", 
                              "IsReturned", 
                              "ForecastedReturnDate", 
                              "ActualReturnDate", 
                              "ReturnReasonId", 
                              "RegistrationDate", 
                              "RegistrationUser", 
                              "DeletedDate", 
                              "DeletedUser"
                       FROM "PCAssignments"
                       """;

		return await connection.QueryAsync<PcAssignment>(query);
	}

	public async Task<IEnumerable<PcAssignment>> GetStudentAssignments(int studentId)
	{
		return await _context.Pcassignments.Include(x => x.Pc).Where(x => x.StudentId == studentId).ToListAsync();

	}
	public async Task Insert(PcAssignment pcAssignment)
	{
		try
		{
			var newAssignment = new PcAssignment()
			{
				Pcid = pcAssignment.Pcid,
				AssignmentDate = DateAndTime.Now,
				IsReturned = false,
				ForecastedReturnDate = pcAssignment.ForecastedReturnDate,
				StudentId = pcAssignment.StudentId,
				RegistrationUser = -1,
				RegistrationDate = DateTime.Now
			};


			_context.Pcassignments.Add(newAssignment);
			await _context.SaveChangesAsync();
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}

	}

	public async Task Update(PcAssignment pcAssignment)
	{
		await using var connection = new NpgsqlConnection(_connectionString);
		await connection.OpenAsync();

		string query = """
                       UPDATE "PCAssignments" SET
                           "Id" = @Id,
                           "PCId" = @PCId,
                           "StudentId" = @StudentId,
                           "AssignmentDate" = @AssignmentDate,
                           "AssignmentReasonId" = @AssignmentReasonId,
                           "IsReturned" = @IsReturned,
                           "ForecastedReturnDate" = @ForecastedReturnDate,
                           "ActualReturnDate" = @ActualReturnDate,
                           "ReturnReasonId" = @ReturnReasonId,
                           "RegistrationDate" = @RegistrationDate,
                           "RegistrationUser" = @RegistrationUser,
                           "DeletedDate" = @DeletedDate,
                           "DeletedUser" = @DeletedUser
                       WHERE "Id" = @Id;
                       """;

		await connection.ExecuteAsync(query, pcAssignment);
	}

	public async Task Delete(int pcAssignmentId)
	{
		await using var connection = new NpgsqlConnection(_connectionString);
		await connection.OpenAsync();

		string query = """
                       DELETE FROM "PCAssignments" WHERE "Id" = @id;
                       """;

		await connection.ExecuteAsync(query, new { id = pcAssignmentId });
	}
}