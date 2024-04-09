namespace CLED.WareHouse.Models.Database;

public record Student
{
    public int Id { get; set; }
    public string SchoolIdentifier { get; set; } = default!;  // matricola
    public string Email { get; set; } = default!;
    public string Surname { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int CourseId { get; set; }
    public DateOnly DateOfBirth { get; set; } = default!;
    public string FiscalCode { get; set; } = default!; // lunghezza 50 char
    public string Gender { get; set; } = default!;
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
    public string RegistrationUser { get; set; } = default!;
    public DateTime? DeletedDate { get; set; }
    public string? DeletedUser { get; set; }
}