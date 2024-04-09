namespace CLED.WareHouse.Models.Database;

public class AccessoryAssignment
{
    public int Id { get; set; }
    public int AccessoryId { get; set; }
    public int StudentId { get; set; }
    public DateTime AssignmentDate { get; set; } = DateTime.Now;
    public int AssignmentReasonId { get; set; }
    public bool IsReturned { get; set; } = false;
    public DateTime ForecastReturnDate { get; set; } = default!;
    public DateTime? ActualReturnDate { get; set; }
    public int? ReturnReasonId { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
    public string RegistrationUser { get; set; } = default!;
    public DateTime? DeletedDate { get; set; }
    public string? DeletedUser { get; set; }
}