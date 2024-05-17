using CLED.Warehouse.Models.DB;
using CLED.WareHouse.Services.DBServices.Interfaces;
using CLED.Warehouse.Web;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CLED.WareHouse.Services.DBServices.PcServices;

public class PcService : IService<Pc>
{
    private readonly string _connectionString;
    private readonly WarehouseContext _context;

    public PcService(IConfiguration? configuration, WarehouseContext context)
    {
        _context = context;
        _connectionString = configuration.GetConnectionString("db");
    }
    
    public async Task<Pc> GetById(int pcId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       SELECT
                           "Id",
                           "StockId",
                           "Serial",
                           "PropertySticker",
                           "IsMuletto",
                           "Status",
                           "UseCycle",
                           "Notes",
                           "RegistrationDate",
                           "RegistrationUser",
                           "DeletedDate",
                           "DeletedUser"
                       FROM "Pcs"
                       WHERE "Id" = @id;
                       """;
        
        return await connection.QueryFirstOrDefaultAsync<Pc>(query, new { id = pcId });
    }

    public async Task<IEnumerable<Pc>> GetAll()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       SELECT
                           "Id",
                           "StockId",
                           "Serial",
                           "PropertySticker",
                           "IsMuletto",
                           "Status",
                           "UseCycle",
                           "Notes",
                           "RegistrationDate",
                           "RegistrationUser",
                           "DeletedDate",
                           "DeletedUser"
                       FROM "Pcs"
                       """;
        
        return await connection.QueryAsync<Pc>(query);
    }

    public async Task Insert(Pc pc)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        
        string query = """
                       INSERT INTO "Pcs" ("Id", "StockId", "Serial", "PropertySticker", "IsMuletto", "Status", "UseCycle", "Notes", "RegistrationDate", "RegistrationUser", "DeletedDate", "DeletedUser")
                       VALUES (@Id, @StockId, @Serial, @PropertySticker, @IsMuletto, @Status, @UseCycle, @Notes, @RegistrationDate, @RegistrationUser, @DeletedDate, @DeletedUser);
                       """;
        
        await connection.ExecuteAsync(query, pc);
    }

    public async Task Update(Pc pc)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       UPDATE "Pcs" SET
                           "Id" = @Id,
                           "StockId" = @StockId,
                           "Serial" = @Serial,
                           "PropertySticker" = @PropertySticker,
                           "IsMuletto" = @IsMuletto,
                           "Status" = @Status,
                           "UseCycle" = @UseCycle,
                           "Notes" = @Notes,
                           "RegistrationDate" = @RegistrationDate,
                          "RegistrationUser" = @RegistrationUser,
                          "DeletedDate" = @DeletedDate,
                           "DeletedUser" = @DeletedUser
                       WHERE "Id" = @Id;
                       """;
        
        await connection.ExecuteAsync(query, pc);
    }

    public async Task Delete(int pcId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       DELETE FROM "Pcs" WHERE "Id" = @id;
                       """;
        
        await connection.ExecuteAsync(query, new {id = pcId});
    }

    public async Task InsertFromNewSerial(Pc pc)
    {
        /* prendo il pc da front end, gli mancano un po' di dati:
            - data di registrazione (DateTime.Now)
            - registrationUser
        */

        var newPc = new Pc()
        {
            StockId = pc.StockId,
            Serial = pc.Serial,
            IsMuletto = pc.IsMuletto,
            UseCycle = pc.UseCycle,
            RegistrationDate = DateTime.Now,
            RegistrationUser = -1
        };

        _context.Pcs.Add(newPc);
        await _context.SaveChangesAsync();
    }
}