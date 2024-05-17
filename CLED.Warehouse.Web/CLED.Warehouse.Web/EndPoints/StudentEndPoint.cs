using System.Text.Json.Nodes;
using CLED.Warehouse.Models.DB;
using CLED.WareHouse.Models.FileUpload;
using CLED.WareHouse.Models.Views;
using CLED.WareHouse.Services.DBServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CLED.Warehouse.Web.EndPoints;

public static class StudentEndPoint
{
    public static IEndpointRouteBuilder MapStudentEndPoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/v1/students")
            .RequireAuthorization()
            .WithTags("Student");

        group.MapGet("/", GetAllStudentAsync)
            .WithName("GetSudents")
            .WithSummary("Get all Summary")
            .WithDescription("Return a list of all pcs");
        
        group.MapGet("/{id:int}", GetStudentByIdAsync)
            .WithName("GetStudentById")
            .WithSummary("Get all Summary")
            .WithDescription("Return a single student selected by ID");

        group.MapGet("/details/{id:int}", GetStudentDetailsAsync);

        group.MapPost("/", InsertStudentAsync)
            .WithName("InsertStudent")
            .WithSummary("Create a new summary")
            .WithDescription("Insert the new students values inside json file");

        group.MapPut("/{id:int}", UpdateStudentAsync)
            .WithName("UpdateStudent")
            .WithSummary("Update the Student")
            .WithDescription("Change students values inside json file");

        group.MapDelete("/{id:int}", DeleteStudentAsync)
            .WithName("DeleteStudent")
            .WithSummary("Delete the Student");
        
        group.MapPost("/jsonImport", JsonUploadStudentsAsync);

        return builder;
    }

    private static async Task<Ok<IEnumerable<Student>>> GetAllStudentAsync(StudentService data)
    {
        var list = await data.GetAll();
        return TypedResults.Ok((list));
    }

    private static async Task<Results<Ok<Student>, NotFound>> GetStudentByIdAsync(int id, StudentService data)
    {
        var product =  await data.GetById(id);
        if (product == null)
            return TypedResults.NotFound();
    
        return TypedResults.Ok(product);
    }

    private static async Task<Results<Ok<StudentDetails>, NotFound>> GetStudentDetailsAsync(int id, StudentService data)
	{
        var student = await data.GetStudentDetails(id);
		if (student == null)
			return TypedResults.NotFound();

		return TypedResults.Ok(student);
	}

	private static async Task<Created> InsertStudentAsync(Student student, StudentService data)
    {
        await data.Insert(student);
        return TypedResults.Created();
    }

    private static async Task<Results<NoContent, NotFound>> UpdateStudentAsync(int id, Student student, StudentService data)
    {
        var temp = await data.GetById(id);
        if (temp == null)
            return TypedResults.NotFound();

        student.Id = id;
        await data.Update(student);
        return TypedResults.NoContent();
    }

    private static async Task<Results<NoContent, NotFound>> DeleteStudentAsync(int id, StudentService data)
    {
        var temp = await data.GetById(id);
        if (temp == null)
            return TypedResults.NotFound();
        
        await data.Delete(id);
        return TypedResults.NoContent();
    }
    
    private static async Task<Created> JsonUploadStudentsAsync([FromBody]string jsonStudents, StudentService data)
    {
        List<JsonStudentModel>? students = JsonConvert.DeserializeObject<List<JsonStudentModel>>(jsonStudents);

        await data.UploadStudentsData(students);

        return TypedResults.Created();
    }
}