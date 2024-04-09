namespace CLED.WareHouse.Models.Database.PCs;

public class PcModelStock
{
    public int Id { get; set; }
    public string Brand { get; set; } = default!;
    public string Model { get; set; } = default!;
    public string Cpu { get; set; } = default!;
    public string Ram { get; set; } = default!;
    public string Storage { get; set; } = default!;
    public DateTime PurchaseDate { get; set; } = default!;
    public int TotalQuantity { get; set; } = 0;
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
    public string RegistrationUser { get; set; } = default!;
    public DateTime? DeletedDate { get; set; }
    public string? DeletedUser { get; set; }
}