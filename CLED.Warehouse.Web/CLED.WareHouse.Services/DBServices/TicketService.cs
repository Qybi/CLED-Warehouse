using CLED.WareHouse.Models.Database;
using CLED.WareHouse.Services.DBServices.Interfaces;

namespace CLED.WareHouse.Services.DBServices;

public class TicketService : IService<Ticket>
{

    public Task<Ticket> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Task<Ticket>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Insert(Ticket ticket)
    {
        throw new NotImplementedException();
    }

    public Task Update(Ticket ticket)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }
}