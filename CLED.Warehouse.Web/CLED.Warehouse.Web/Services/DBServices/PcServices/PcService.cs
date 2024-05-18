using CLED.Warehouse.Models.DB;
using CLED.WareHouse.Services.DBServices.Interfaces;
using CLED.Warehouse.Web;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using CLED.WareHouse.Models.Constants;

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

    public async Task<Pc> Insert(Pc pc)
    {
		try
		{
			pc.RegistrationUser = -1;
			pc.RegistrationDate = DateTime.Now;
			_context.Pcs.Add(pc);
			await _context.SaveChangesAsync();

			return pc;
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
    
    public async Task<bool> CheckSerial(string serial)
    {
        try
        {
            return await _context.Pcs.AnyAsync(x => x.Serial == serial); //anyasync come ifExists, fa select ifexists where condition
        }
        catch (Exception ex)
        {
    
            throw;
        }
    }

    public async Task InsertFromNewSerial(Pc pc)
    {
        /* prendo il pc da front end, gli mancano un po' di dati:
            - data di registrazione (DateTime.Now)
            - registrationUser
        */
        try
        {
	        var newPc = new Pc()
	        {
		        StockId = pc.StockId,
		        Serial = pc.Serial,
		        IsMuletto = pc.IsMuletto,
		        UseCycle = pc.UseCycle,
		        RegistrationDate = DateTime.Now,
		        PropertySticker = pc.PropertySticker,
		        RegistrationUser = -1
	        };

	        _context.Pcs.Add(newPc);
	        await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
	        Console.WriteLine(ex);
	        throw;
        }

    }
    
    public async Task ImportBulk(IEnumerable<Pc> bulk)
    {
	    try
	    {
		    foreach (var item in bulk)
		    {
			    item.Stock = null;
		    }

		    _context.Pcs.AddRange(bulk);
		    await _context.SaveChangesAsync();
	    }
	    catch (Exception ex)
	    {

		    throw;
	    }
    }

    public async Task<Pc> GetPcFromSerial(string serial)
    {
	    try
	    {
		    return (await _context.Pcs.FirstOrDefaultAsync(x => x.Serial == serial))!;
	    }
	    catch (Exception ex)
	    {
		    Console.WriteLine(ex);
		    throw;
	    }   
    }

	// this is ass to have the double method, but I have no time to put generics in all services atm.
	// this will be the same for accessories
	Task IService<Pc>.Insert(Pc obj)
	{
		throw new NotImplementedException();
	}
}