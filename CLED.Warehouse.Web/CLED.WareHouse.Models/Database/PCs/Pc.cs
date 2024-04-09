namespace CLED.WareHouse.Models.Database.PCs;

public class Pc
{
    public int Id { get; set; }
    public int StockId { get; set; }
    public string Serial { get; set; } = default!;
    public string PropertySticker { get; set; } = default!; // cespite
    public bool IsMuletto { get; set; } = false; // values in_repair, in_use, out_of_order, in_warehouse, expired
    public string? Status { get; set; } = default!;
    public int UseCycle { get; set; } // 1 - 2 | > 2 significa "fuori uso/muletto"
    public string? Notes { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
    public DateTime RegistrationUser { get; set; } = default!;
    public DateTime? DeletedDate { get; set; }
    public string? DeletedUser { get; set; }
}