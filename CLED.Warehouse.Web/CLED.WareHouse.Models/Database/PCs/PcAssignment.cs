namespace CLED.WareHouse.Models.Database.PCs;

public class PcAssignment
{
    public int Id { get; set; }
    public int PcId { get; set; }
    public int StudentId { get; set; }
    public DateTime AssignmentDate { get; set; } = DateTime.Now;
    public int AssignmentReasonId { get; set; }
    public bool IsReturned { get; set; } = false;
    public DateTime ForecastedReturnDate { get; set; }
    public DateTime? ActualReturnDate { get; set; }
    public int? ReturnReasonId { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
    public string RegistrationUser { get; set; } = default!;
    public DateTime? DeletedDate { get; set; }
    public string? DeletedUser { get; set; }
}