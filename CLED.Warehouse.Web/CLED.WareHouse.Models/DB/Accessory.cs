﻿using System;
using System.Collections.Generic;

namespace CLED.Warehouse.Models.DB;

public partial class Accessory
{
    public int Id { get; set; }

    public int StockId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime RegistrationDate { get; set; }

    public int RegistrationUser { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedUser { get; set; }

    public int? Qty { get; set; }

    public virtual ICollection<AccessoriesAssignment> AccessoriesAssignments { get; set; } = new List<AccessoriesAssignment>();

    public virtual PcModelStock Stock { get; set; } = null!;
}
