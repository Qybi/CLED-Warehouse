using CLED.Warehouse.Models.DB;

namespace CLED.Warehouse.Data.Abstractions;

public interface ITicketRepository : IRepository<int, Ticket>
{
}