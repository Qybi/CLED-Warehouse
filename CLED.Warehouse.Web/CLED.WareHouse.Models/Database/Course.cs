using System.Runtime.InteropServices.JavaScript;

namespace CLED.WareHouse.Models.Database;

public class Course
{
    public int Id { get; set; }
    public string Code { get; set; } = default!; // lunghezza di 30 char {es CLED, SOQU}
    public string FullName { get; set; } = default!;
    public DateTime DateStart { get; set; } = default!;
    public DateTime DateEnd { get; set; } = default!; //{massimo due anni di durata del corso}
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
    public DateTime RegistrationUser { get; set; } = default!;
    public DateTime? DeletedDate { get; set; }
    public string? DeletedUser { get; set; }        
}