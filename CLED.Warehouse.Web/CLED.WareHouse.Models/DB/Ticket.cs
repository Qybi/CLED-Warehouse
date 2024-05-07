using System;
using System.Collections.Generic;

namespace CLED.Warehouse.Models.DB;

public partial class Ticket
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public string TicketType { get; set; } = null!;

    public string TicketBody { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime DateOpen { get; set; }

    public DateTime? DateClose { get; set; }

    public string UserClaimOpen { get; set; } = null!;

    public string? UserClaimClose { get; set; }

    public DateTime RegistrationDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string? DeletedUser { get; set; }

    public virtual Student Student { get; set; } = null!;
}
