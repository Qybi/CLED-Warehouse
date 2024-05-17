using System.Text.Json.Serialization;

namespace CLED.WareHouse.Models.FileUpload;

// public class UploadedStudentsJson
// {
//     public JsonStudentModel[] Students { get; set; }
// }

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
    public DateTime AnnoCorso { get; set; } = default!;
    public DateOnly DataNascita { get; set; } = default!;
    public string ComuneNascita { get; set; } = default!;
    public string ProvinciaNascita { get; set; } = default!;
    public DateOnly? DataDimissioni { get; set; }
    public string Genere { get; set; } = default!;
    public string NazioneNascita { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    
}