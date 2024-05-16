namespace CLED.WareHouse.Models.FileUpload;

/*public class UploadedStudentsJson
{
    public JsonStudentModel[] RowsJsonStudentModels { get; set; }
}*/

public class JsonStudentModel
{
    public string IdAllievoCorso { get; set; } = default!;
    public string CodiceFiscale { get; set; } = default!;
    public string SiglaCorso { get; set; } = default!;
    public string CodiceCorso { get; set; } = default!;
    public string NomeCorso { get; set; } = default!;
    public string Cognome { get; set; } = default!;
    public string Nome { get; set; } = default!;
    public string EmailUser { get; set; } = default!;
    public string Tel { get; set; } = default!;
    public string ComuneResidenza { get; set; } = default!;
    public string ProvinciaResidenza { get; set; } = default!;
    public string StatoAllievo { get; set; } = default!; 
    public string StatoCorso { get; set; } = default!;
    public string AnnoCorso { get; set; } = default!;
    public string DataNascita { get; set; } = default!;
    public string ComuneNascita { get; set; } = default!;
    public string ProvinciaNascita { get; set; } = default!;
    public string? DataDimissioni { get; set; }
    public string Genere { get; set; } = default!;
    public string NazioneNascita { get; set; } = default!;
}