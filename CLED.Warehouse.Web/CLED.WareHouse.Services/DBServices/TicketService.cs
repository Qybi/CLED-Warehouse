using CLED.WareHouse.Models.Database;
using CLED.WareHouse.Services.DBServices.Interfaces;

namespace CLED.WareHouse.Services.DBServices;

public class TicketService : IService<Ticket>
{

    public async Task<Ticket> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Ticket>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task Insert(Ticket ticket)
    {
        throw new NotImplementedException();
    }

    public async Task Update(Ticket ticket)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(int id)
    {
        throw new NotImplementedException();
    }
}