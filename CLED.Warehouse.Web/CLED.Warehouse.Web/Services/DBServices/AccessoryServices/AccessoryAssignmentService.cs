using CLED.Warehouse.Models.DB;
using CLED.Warehouse.Web;
using CLED.WareHouse.Services.DBServices.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CLED.WareHouse.Services.DBServices.AccessoryServices;

public class AccessoryAssignmentService : IService<AccessoriesAssignment>
{
    
    private readonly string _connectionString;
    private readonly WarehouseContext _context;

	public AccessoryAssignmentService(IConfiguration? configuration, WarehouseContext context)
	{
		_connectionString = configuration.GetConnectionString("db");
		_context = context;
	}
	public async Task<AccessoriesAssignment> GetById(int accessoryAssignmentId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();   
        
        string query = """
                       SELECT 
                           "Id",
                           "AccessoryId",
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
                       FROM "AccessoriesAssignments"
                       WHERE "Id" = @id;
                       """;
        
        return await connection.QueryFirstOrDefaultAsync<AccessoriesAssignment>(query, new { id = accessoryAssignmentId });
    }

	public async Task<IEnumerable<AccessoriesAssignment>> GetStudentAssignments(int studentId)
	{
		return await _context.AccessoriesAssignments.Include(x => x.Accessory).Where(x => x.StudentId == studentId).ToListAsync();
	}

	public async Task<IEnumerable<AccessoriesAssignment>> GetAll()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();   
        
        string query = """
                       SELECT
                           "Id",
                           "AccessoryId",
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
                       FROM "AccessoriesAssignments";
                       """;
        
        return await connection.QueryAsync<AccessoriesAssignment>(query);
    }

    public async Task Insert(AccessoriesAssignment accessoryAssignment)
    {
        try
        {
            _context.AccessoriesAssignments.Add(accessoryAssignment);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw;
        }
	}

    public async Task Update(AccessoriesAssignment accessoryAssignment)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       UPDATE "AccessoriesAssignments" SET 
                       "Id" = @Id,
                       "AccessoryId" = @AccessoryId,
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
                       WHERE 2 = @Id;
                       """;
        
        await connection.ExecuteAsync(query, accessoryAssignment);
    }

    public async Task Delete(int accessoryAssignmentId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       DELETE FROM "AccessoriesAssignments" WHERE "Id" = @id;
                       """;
        
        await connection.ExecuteAsync(query, new {id = accessoryAssignmentId});
    }

    public async Task<bool> ReturnAccessory(int assignmentId, DateTime returnDate, int returnReasonId)
    {
        try
        {
            var assignment = await _context.AccessoriesAssignments.FindAsync(assignmentId);
			assignment.IsReturned = true;
			assignment.ActualReturnDate = returnDate;
			assignment.ReturnReasonId = returnReasonId;

			await _context.SaveChangesAsync();

            return true;
		}
        catch (Exception ex)
        {
            throw;
        }
    }

	public async Task<IEnumerable<AccessoriesAssignment>> GetAssignmentsByAccessoryId(int accessoryId)
	{
		return await _context.AccessoriesAssignments
			.Include(x => x.Student)
			.Include(x => x.ReturnReason)
			.Where(x => x.AccessoryId == accessoryId)
			.ToListAsync();
	}
}