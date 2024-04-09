namespace CLED.WareHouse.Models.Database.Accessories;

public class Accessory
{
    public int Id { get; set; }
    public int StockId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
    public string RegistrationUser { get; set; } = default!;
    public DateTime? DeletedDate { get; set; }
    public string? DeletedUser { get; set; }
}