﻿using System;
using System.Collections.Generic;

namespace CLED.Warehouse.Models.DB;

public partial class PcAssignment
{
    public int Id { get; set; }

    public int PcId { get; set; }

    public int StudentId { get; set; }

    public DateTime AssignmentDate { get; set; }

    public int AssignmentReasonId { get; set; }

    public bool IsReturned { get; set; }

    public DateTime ForecastedReturnDate { get; set; }

    public DateTime? ActualReturnDate { get; set; }

    public int? ReturnReasonId { get; set; }

    public DateTime RegistrationDate { get; set; }

    public int RegistrationUser { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedUser { get; set; }

    public string? PropertySticker { get; set; }

    public virtual ReasonsAssignment AssignmentReason { get; set; } = null!;

    public virtual Pc Pc { get; set; } = null!;

    public virtual ReasonsReturn? ReturnReason { get; set; }

    public virtual Student Student { get; set; } = null!;
}
