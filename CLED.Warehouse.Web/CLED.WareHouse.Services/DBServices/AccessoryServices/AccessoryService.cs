using CLED.WareHouse.Models.Database.Accessories;
using CLED.WareHouse.Services.DBServices.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CLED.WareHouse.Services.DBServices.AccessoryServices;

public class AccessoryService : IService<Accessory>
{
    
    private readonly string _connectionString;

    public AccessoryService(IConfiguration? configuration)
    {
        _connectionString = configuration.GetConnectionString("db");
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
                       FROM "Accessories";
                       """;
        
        return await connection.QueryAsync<Accessory>(query);
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