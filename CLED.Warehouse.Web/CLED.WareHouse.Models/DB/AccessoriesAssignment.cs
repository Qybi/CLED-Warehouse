using System;
using System.Collections.Generic;

namespace CLED.Warehouse.Web;

public partial class AccessoriesAssignment
{
    public int Id { get; set; }

    public int AccessoryId { get; set; }

    public int StudentId { get; set; }

    public DateTime AssignmentDate { get; set; }

    public int AssignmentReasonId { get; set; }

    public bool IsReturned { get; set; }

    public DateTime ForecastedReturnDate { get; set; }

    public DateTime? ActualReturnDate { get; set; }

    public int? ReturnReasonId { get; set; }

    public DateTime RegistrationDate { get; set; }

    public string RegistrationUser { get; set; } = null!;

    public DateTime? DeletedDate { get; set; }

    public string? DeletedUser { get; set; }

    public virtual Accessory Accessory { get; set; } = null!;

    public virtual ReasonsAssignment AssignmentReason { get; set; } = null!;

    public virtual ReasonsReturn? ReturnReason { get; set; }

    public virtual Student Student { get; set; } = null!;
}
