namespace CLED.WareHouse.Models.FileUpload;

public class UploadedComputerJson
{
    public JsonPcModel[] RowsJsonPcModels { get; set; }
}

public class JsonPcModel
{
    public string Seriale { get; set; } = default!;
    public string Marca { get; set; } = default!;
    public string Modello { get; set; } = default!;
    public string CPU { get; set; } = default!;
    public string RAM { get; set; } = default!;
    public string Storage { get; set; } = default!;
    public string DataAcquisto { get; set; } = default!;
}

