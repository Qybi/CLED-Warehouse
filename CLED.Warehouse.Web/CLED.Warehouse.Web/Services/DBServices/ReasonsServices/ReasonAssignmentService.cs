using CLED.Warehouse.Models.DB;
using CLED.Warehouse.Web;
using CLED.WareHouse.Services.DBServices.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CLED.WareHouse.Services.DBServices.ReasonsServices;

public class ReasonAssignmentService : IService<ReasonsAssignment>
{
    private readonly string _connectionString;
	private readonly WarehouseContext _context;

	public ReasonAssignmentService(IConfiguration? configuration, WarehouseContext context)
	{
		_connectionString = configuration.GetConnectionString("db");
		_context = context;
	}
	public async Task<ReasonsAssignment> GetById(int reasonAssignmentId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       SELECT "Id",
                              "Name"
                       FROM "ReasonsAssignment"
                       WHERE "Id" = @id;
                       """;
        
        return await connection.QueryFirstOrDefaultAsync<ReasonsAssignment>(query, new { id = reasonAssignmentId });
    }

    public async Task<IEnumerable<ReasonsAssignment>> GetAll()
    {
        return await _context.ReasonsAssignments.ToListAsync();
	}

    public async Task Insert(ReasonsAssignment reasonsAssignment)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        
        string query = """
                       INSERT INTO "ReasonsAssignment" ("Id", "Name")
                       VALUES (@Id, @Name);
                       """;
        
        await connection.ExecuteAsync(query, reasonsAssignment);
    }

    public async Task Update(ReasonsAssignment reasonsAssignment)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       UPDATE "ReasonsAssignment" SET
                           "Id" = @Id,
                           "Name" = @Name
                       WHERE "Id" = @Id;
                       """;
        
        await connection.ExecuteAsync(query, reasonsAssignment);
    }

    public async Task Delete(int reasonAssignmentId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       DELETE FROM "ReasonsAssignment" WHERE "Id" = @id;
                       """;
        
        await connection.ExecuteAsync(query, new {id = reasonAssignmentId});
    }
}