﻿using System;
using System.Collections.Generic;

namespace CLED.Warehouse.Models.DB;

public partial class PcModelStock
{
    public int Id { get; set; }

    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string Cpu { get; set; } = null!;

    public string Ram { get; set; } = null!;

    public string Storage { get; set; } = null!;

    public DateTime PurchaseDate { get; set; }

    public int TotalQuantity { get; set; }

    public DateTime RegistrationDate { get; set; }

    public int RegistrationUser { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedUser { get; set; }

    public virtual ICollection<Accessory> Accessories { get; set; } = new List<Accessory>();

    public virtual ICollection<Pc> Pcs { get; set; } = new List<Pc>();
}
