using CLED.Warehouse.Models.DB;
using CLED.WareHouse.Services.DBServices;
using CLED.WareHouse.Services.DBServices.PcServices;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CLED.Warehouse.Web.EndPoints;

public static class CourseEndPoint
{
    public static IEndpointRouteBuilder MapCourseEndPoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/v1/course")
            // .RequireAuthorization()
            .WithTags("course");

        group.MapGet("/", GetAllCourseAsync)
            .WithName("GetCourse")
            .WithSummary("Get all Summary")
            .WithDescription("Return a list of all courses");
        
        group.MapGet("/{id:int}", GetCourseByIdAsync)
            .WithName("GetCourseById")
            .WithSummary("Get all Summary")
            .WithDescription("Return a single course selected by ID");

        group.MapPost("/", InsertcourseAsync)
            .WithName("InsertCourse")
            .WithSummary("Create a new summary")
            .WithDescription("Insert the new courses values inside json file");

        group.MapPut("/{id:int}", UpdateCourseAsync)
            .WithName("UpdateCourse")
            .WithSummary("Update the Course")
            .WithDescription("Change course values inside json file");

        group.MapDelete("/{id:int}", DeleteCourseAsync)
            .WithName("DeleteCourse")
            .WithSummary("Delete the Course");

        return builder;
    }

    private static async Task<Ok<IEnumerable<Course>>> GetAllCourseAsync(CourseService data)
    {
        var list = await data.GetAll();
        return TypedResults.Ok((list));
    }

    private static async Task<Results<Ok<Course>, NotFound>> GetCourseByIdAsync(int id, CourseService data)
    {
        var product =  await data.GetById(id);
        if (product == null)
            return TypedResults.NotFound();
    
        return TypedResults.Ok(product);
    }

    private static async Task<Created> InsertcourseAsync(Course course, CourseService data)
    {
        await data.Insert(course);
        return TypedResults.Created();
    }

    private static async Task<Results<NoContent, NotFound>> UpdateCourseAsync(int id, Course course, CourseService data)
    {
        var temp = await data.GetById(id);
        if (temp == null)
            return TypedResults.NotFound();

        course.Id = id;
        await data.Update(course);
        return TypedResults.NoContent();
    }

    private static async Task<Results<NoContent, NotFound>> DeleteCourseAsync(int id, CourseService data)
    {
        var temp = await data.GetById(id);
        if (temp == null)
            return TypedResults.NotFound();
        
        await data.Delete(id);
        return TypedResults.NoContent();
    }
}