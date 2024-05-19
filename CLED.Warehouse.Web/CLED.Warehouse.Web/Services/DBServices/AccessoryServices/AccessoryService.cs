using CLED.Warehouse.Models.DB;
using CLED.Warehouse.Web;
using CLED.WareHouse.Services.DBServices.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CLED.WareHouse.Services.DBServices.AccessoryServices;

public class AccessoryService : IService<Accessory>
{
    
    private readonly string _connectionString;
    private readonly WarehouseContext _context;

	public AccessoryService(IConfiguration? configuration, WarehouseContext context)
	{
		_connectionString = configuration.GetConnectionString("db");
		_context = context;
	}

	public async Task<Accessory> GetById(int accessoryId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       SELECT 
                           "Id",
                           "StockId",
                           "Name",
                           "Description",
                           "RegistrationDate", 
                           "RegistrationUser",
                           "DeletedDate",
                           "DeletedUser"
                       FROM "Accessories"
                       WHERE "Id" = @id;
                       """;
        
        return await connection.QueryFirstOrDefaultAsync<Accessory>(query, new { id = accessoryId });
    }

    public async Task<IEnumerable<Accessory>> GetAll()
    {
        return await _context.Accessories.Include(x => x.Stock).ToListAsync();
	}

    public async Task Insert(Accessory accessory)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       INSERT INTO "Accessories" ("Id", "StockId", "Name", "Description", "RegistrationDate", "RegistrationUser", "DeletedDate", "DeletedUser")
                       VALUES (@Id, @StockId, @Name, @Description, @RegistrationDate, @RegistrationUser, @DeletedDate, @DeletedUser);
                       """;
        
        await connection.ExecuteAsync(query, accessory);
    }

    public async Task Update(Accessory accessory)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       UPDATE "Accessories" SET
                            "Id" = @Id,
                            "StockId" = @StockId,
                            "Name" = @Name,
                            "Description" = @Description, 
                            "RegistrationDate" = @RegistrationDate,
                            "RegistrationUser" = @RegistrationUser,
                            "DeletedDate" = @DeletedDate,
                            "DeletedUser" = @DeletedUser
                       WHERE "Id" = @Id;
                       """;
        
        await connection.ExecuteAsync(query, accessory);
    }

    public async Task Delete(int accessoryId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       DELETE FROM "Accessories" WHERE "Id" = @id;
                       """;
        
        await connection.ExecuteAsync(query, new {id = accessoryId});
    }
}