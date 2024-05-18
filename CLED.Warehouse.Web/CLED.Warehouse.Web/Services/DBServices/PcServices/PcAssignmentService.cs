using CLED.Warehouse.Models.DB;
using CLED.WareHouse.Services.DBServices.Interfaces;
using CLED.Warehouse.Web;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using CLED.WareHouse.Models.Constants;

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
		return await _context.Pcassignments
			.Include(x => x.Pc)
				.ThenInclude(x => x.Stock)
			.Where(x => x.StudentId == studentId && !x.IsReturned)
			.ToListAsync();

	}
	public async Task Insert(PcAssignment pcAssignment)
	{
		try
		{
			pcAssignment.RegistrationUser = -1;
			pcAssignment.RegistrationDate = DateTime.Now;
			if (pcAssignment.ForecastedReturnDate < DateTime.Now.Date.AddYears(-10))
				pcAssignment.ForecastedReturnDate = pcAssignment.AssignmentDate.AddYears(2);

			_context.Pcassignments.Add(pcAssignment);

			var pc = await _context.Pcs.FindAsync(pcAssignment.PcId);
			pc.Status = PCStatus.Assigned;

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

	public async Task<bool> ReturnPc(int pcId, DateTime returnDate, int returnReasonId)
	{
		try
		{
			var pcAssignment = await _context.Pcassignments.FindAsync(pcId);
			var pc = await _context.Pcs.FindAsync(pcAssignment.PcId);

			pcAssignment.IsReturned = true;
			pcAssignment.ActualReturnDate = returnDate;
			pcAssignment.ReturnReasonId = returnReasonId;

			switch ((ReturnReasons)returnReasonId)
			{
				case ReturnReasons.Dimissioni:
					pc.Status = PCStatus.Warehouse;
					break;
				case ReturnReasons.FineCorso:
					pc.UseCycle++;
					pc.Status = PCStatus.Warehouse;
					if (pc.UseCycle > 2) pc.IsMuletto = true;
					break;
				case ReturnReasons.Riparazione:
					pc.Status = PCStatus.OutOfOrder;
					break;
				case ReturnReasons.Sostituzione:
					pc.Status = PCStatus.Warehouse;
					break;
				default:
					break;
			}

			await _context.SaveChangesAsync();

			return true;
		}
		catch (Exception ex)
		{
			throw;
		}
	}
}