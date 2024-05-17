using CLED.Warehouse.Models.DB;
using CLED.Warehouse.Web;
using CLED.WareHouse.Services.DBServices.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CLED.WareHouse.Services.DBServices.PcServices;

public class PcService : IService<Pc>
{
    private readonly string _connectionString;
	private readonly WarehouseContext _context;

	public PcService(IConfiguration? configuration, WarehouseContext context)
	{
		_connectionString = configuration.GetConnectionString("db");
		_context = context;
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
        return await _context.Pcs.ToListAsync();
	}

    public async Task Insert(Pc pc)
    {
		try
		{
			pc.RegistrationUser = -1;
			pc.RegistrationDate = DateTime.Now;
			_context.Pcs.Add(pc);
			await _context.SaveChangesAsync();

		}
		catch (Exception ex)
		{

			throw;
		}
	}

    public async Task Update(Pc pc)
    {
		try
		{
			var pcToUpdate = await _context.Pcs.FindAsync(pc.Id);

			pcToUpdate.StockId = pc.StockId;
			pcToUpdate.Serial = pc.Serial;
			pcToUpdate.PropertySticker = pc.PropertySticker;
			pcToUpdate.IsMuletto = pc.IsMuletto;
			pcToUpdate.Status = pc.Status;
			pcToUpdate.UseCycle = pc.UseCycle;
			pcToUpdate.Notes = pc.Notes;

			_context.Pcs.Update(pcToUpdate);
			await _context.SaveChangesAsync();

		}
		catch (Exception ex)
		{

			throw;
		}
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
}