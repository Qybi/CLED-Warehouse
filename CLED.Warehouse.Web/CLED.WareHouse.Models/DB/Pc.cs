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

    public int RegistrationUser { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedUser { get; set; }

    public virtual ICollection<Pcassignment> Pcassignments { get; set; } = new List<Pcassignment>();

    public virtual PcmodelStock Stock { get; set; } = null!;
}
