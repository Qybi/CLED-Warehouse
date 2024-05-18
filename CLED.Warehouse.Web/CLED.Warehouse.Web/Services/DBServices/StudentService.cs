using CLED.Warehouse.Authentication.Utils.Abstractions;
using CLED.Warehouse.Models.DB;
using CLED.WareHouse.Models.FileUpload;
using CLED.Warehouse.Web;
using CLED.WareHouse.Services.DBServices.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
using CLED.WareHouse.Models.Views;
using Npgsql;

namespace CLED.WareHouse.Services.DBServices;

public class StudentService : IService<Student>
{
    private readonly string _connectionString;
	private readonly WarehouseContext _context;
	private readonly IHashingUtils _hashingUtils;


	public StudentService(IConfiguration? configuration, WarehouseContext context, IHashingUtils hashingUtils)
	{
		_connectionString = configuration.GetConnectionString("db");
		_context = context;
		_hashingUtils = hashingUtils;
	}

	public async Task<Student> GetById(int studentId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       SELECT "Id", 
                              "SchoolIdentifier", 
                              "Email", 
                              "Surname", 
                              "Name", 
                              "CourseId", 
                              "DateOfBirth", 
                              "FiscalCode", 
                              "Gender", 
                              "RegistrationDate", 
                              "RegistrationUser", 
                              "DeletedDate", 
                              "DeletedUser"
                       FROM "Students"
                       WHERE "Id" = @id;
                       """;
        
        return await connection.QueryFirstOrDefaultAsync<Student>(query, new { id = studentId });
    }

    public async Task<IEnumerable<Student>> GetAll()
    {
        return await _context.Students.Include(x => x.Course).ToListAsync();
	}

    public async Task<StudentDetails> GetStudentDetails(int id)
    {
        var student =  await _context.Students
            .Include(x => x.Course)
            .Include(x => x.AccessoriesAssignments)
                .ThenInclude(x => x.Accessory)
			.Include(x => x.PcAssignments)
                .ThenInclude(x => x.Pc)
                    .ThenInclude(x => x.Stock)
			.FirstOrDefaultAsync(x => x.Id == id);

        return new StudentDetails
		{
			Name = student.Name,
			Surname = student.Surname,
			DateOfBirth = student.DateOfBirth,
			FiscalCode = student.FiscalCode,
			BirthCountry = student.BirthNation,
			BirthCity = student.BirthCity,
			ResidenceCountry = student.ResidenceNation,
			ResidenceCity = student.ResidenceCity,
			SchoolIdentifier = student.SchoolIdentifierId,
			EmailUser = student.EmailUser,
			Course = student.Course,
			PcAssignments = student.PcAssignments.Where(x => !x.IsReturned),
			AccessoriesAssignments = student.AccessoriesAssignments.Where(x => !x.IsReturned),
            Status = student.Status
		};
	}

    public async Task Insert(Student student)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        
        string query = """
                       INSERT INTO "Students" ("Id", "SchoolIdentifier", "Email", "Surname", "Name", "CourseId", "DateOfBirth", "FiscalCode", "Gender", "RegistrationDate", "RegistrationUser", "DeletedDate", "DeletedUser")
                       VALUES (@Id, @SchoolIdentifier, @Email, @Surname, @Name, @CourseId, @DateOfBirth, @FiscalCode, @Gender, @RegistrationDate, @RegistrationUser, @DeletedDate, @DeletedUser);
                       """;
        
        await connection.ExecuteAsync(query, student);
    }

    public async Task Update(Student student)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       UPDATE "Students" SET
                           "Id" = @Id,
                           "SchoolIdentifier" = @SchoolIdentifier,
                           "Email" = @Email,
                           "Surname" = @Surname,
                           "Name" = @Name,
                           "DateOfBirth" = @DateOfBirth,
                           "FiscalCode" = @FiscalCode,
                           "Gender" = @Gender,
                           "UserClaimClose" = @UserClaimClose,
                           "RegistrationDate" = @RegistrationDate,
                           "RegistrationUser" = @RegistrationUser,
                           "DeletedDate" = @DeletedDate,
                           "DeletedUser" = @DeletedUser
                       WHERE "Id" = @Id;
                       """;
        
        await connection.ExecuteAsync(query, student);
    }

    public async Task Delete(int studentId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       DELETE FROM "Students" WHERE "Id" = @id;
                       """;
        
        await connection.ExecuteAsync(query, new {id = studentId});
    }
    
    public async Task UploadStudentsData(IEnumerable<JsonStudentModel> uploadFile)
    {
	    foreach (var item in uploadFile)
	    {
		    var hash = _hashingUtils.GetHashedPassword(item.Password);
		    var newStudent = new Student()
		    {
			    FiscalCode = item.CodiceFiscale,
			    Surname = item.Cognome,
			    Name = item.Nome,
			    EmailUser = item.EmailUser,
			    PhoneNumber = item.Tel,
			    ResidenceCity = item.ComuneResidenza,
			    ResidenceProvince = item.ProvinciaResidenza,
			    Status = item.StatoAllievo,
			    DateOfBirth = item.DataNascita,
			    BirthCity = item.ComuneNascita,
			    BirthProvince = item.ProvinciaNascita,
			    ResignationDate = item.DataDimissioni,
			    Gender = item.Genere,
			    BirthNation = item.NazioneNascita,
			    CourseId = (await _context.Courses.FirstOrDefaultAsync(x => x.ShortName == item.SiglaCorso)).Id,
			    User = new User()
			    {
				    Enabled = true,
				    PasswordHash = hash.PasswordHash,
				    PasswordSalt = hash.Salt,
				    Username = item.Username,
				    RegistrationDate = DateTime.Now,
				    RegistrationUser = -1,
				    Roles = ["USER"]
			    }
		    };

		    _context.Students.Add(newStudent);
	    }

	    await _context.SaveChangesAsync();
    }

}