using CLED.Warehouse.Models.DB;
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


	public StudentService(IConfiguration? configuration, WarehouseContext context)
	{
		_connectionString = configuration.GetConnectionString("db");
		_context = context;
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
			.Include(x => x.Pcassignments)
                .ThenInclude(x => x.Pc)
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
			ResidencyCity = student.ResidenceCity,
			SchoolIdentifier = student.SchoolIdentifierId,
			EmailUser = student.EmailUser,
			Course = student.Course,
			PcAssignments = student.Pcassignments,
			AccessoriesAssignments = student.AccessoriesAssignments
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
}