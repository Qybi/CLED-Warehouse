using System;
using System.Collections.Generic;

namespace CLED.Warehouse.Models.DB;

public partial class Pc
{
    public int Id { get; set; }

    public int StockId { get; set; }

    public string Serial { get; set; } = null!;

    public string? PropertySticker { get; set; }

    public bool IsMuletto { get; set; }

    public string? Status { get; set; }

    public int UseCycle { get; set; }

    public string? Notes { get; set; }

    public DateTime RegistrationDate { get; set; }

    public string RegistrationUser { get; set; } = null!;

    public DateTime? DeletedDate { get; set; }

    public string? DeletedUser { get; set; }

    public virtual ICollection<Pcassignment> Pcassignments { get; set; } = new List<Pcassignment>();

    public virtual PcmodelStock Stock { get; set; } = null!;
}
