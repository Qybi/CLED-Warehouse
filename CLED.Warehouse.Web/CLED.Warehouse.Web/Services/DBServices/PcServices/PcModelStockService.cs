using CLED.Warehouse.Models.DB;
using CLED.WareHouse.Services.DBServices.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CLED.WareHouse.Services.DBServices.PcServices;

public class PcModelStockService : IService<PcModelStock>
{
    private readonly string _connectionString;

    public PcModelStockService(IConfiguration? configuration)
    {
        _connectionString = configuration.GetConnectionString("db");
    }
    
    public async Task<PcModelStock> GetById(int pcModelStockId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       SELECT "Id",
                              "Brand",
                              "Model",
                              "CPU",
                              "RAM",
                              "Storage",
                              "PurchaseDate",
                              "TotalQuantity",
                              "RegistrationDate",
                              "RegistrationUser",
                              "DeletedDate",
                              "DeletedUser"
                       FROM "PCModelStock"
                       WHERE "Id" = @id;
                       """;
        
        return await connection.QueryFirstOrDefaultAsync<PcModelStock>(query, new { id = pcModelStockId });
    }

    public async Task<IEnumerable<PcModelStock>> GetAll()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       SELECT "Id",
                              "Brand",
                              "Model",
                              "CPU",
                              "RAM",
                              "Storage",
                              "PurchaseDate",
                              "TotalQuantity",
                              "RegistrationDate",
                              "RegistrationUser",
                              "DeletedDate",
                              "DeletedUser"
                       FROM "PCModelStock"
                       """;
        
        return await connection.QueryAsync<PcModelStock>(query);
    }

    public async Task Insert(PcModelStock pcModelStock)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        
        string query = """
                       INSERT INTO "PCModelStock"("Id", "Brand", "Model", "CPU", "RAM", "Storage", "PurchaseDate", "TotalQuantity", "RegistrationDate", "RegistrationUser", "DeletedDate", "DeletedUser")
                       VALUES (@Id, @Brand, @Model, @Cpu, @Ram, @Storage, @PurchaseDate, @TotalQuantity, @RegistrationDate, @RegistrationUser, @DeletedDate, @DeletedUser);
                       """;
    }

    public async Task Update(PcModelStock pcModelStock)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        
        string query = """
                       UPDATE "PCModelStock" SET
                            "Id" = @Id,
                            "Brand" = @Brand,
                            "Model" = @Model,
                            "CPU" = @Cpu,
                            "RAM" = @Ram,
                            "Storage" = @Storage,
                            "PurchaseDate" = @PurchaseDate, 
                            "TotalQuantity" = @TotalQuantity, 
                            "RegistrationDate" = @RegistrationDate,
                            "RegistrationUser" =@RegistrationUser, 
                            "DeletedDate" = @DeletedDate,
                            "DeletedUser" = @DeletedUser
                       WHERE "Id" = @Id;
                       """;
        await connection.ExecuteAsync(query, pcModelStock);
    }

    public async Task Delete(int pcModelStockId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       DELETE FROM "PcmodelStock" WHERE "Id" = @id
                       """;
        
        await connection.ExecuteAsync(query, new {id = pcModelStockId});
    }
}