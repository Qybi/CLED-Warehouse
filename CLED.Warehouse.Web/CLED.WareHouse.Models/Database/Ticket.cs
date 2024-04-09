namespace CLED.WareHouse.Models.Database;

public class Ticket
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string TicketType { get; set; } = default!; // {necessità: rotto, smarrito, malfunzionamento}
    public string TicketBody { get; set; } = default!;
    public string Status { get; set; } = default!;
    public DateTime DateOpen { get; set; } = DateTime.Now;
    public DateTime? DateClose { get; set; }
    public string UserClaimOpen { get; set; } = default!;
    public string? UserClaimClose { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
    public DateTime? DeletedDate { get; set; }
    public string? DeletedUser { get; set; }
}