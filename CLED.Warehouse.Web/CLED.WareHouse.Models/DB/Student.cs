using System;
using System.Collections.Generic;

namespace CLED.Warehouse.Models.DB;

public partial class Student
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string SchoolIdentifierId { get; set; } = null!;

    public string IalmanId { get; set; } = null!;

    public string EmailUser { get; set; } = null!;

    public string EmailPersonal { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int CourseId { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string FiscalCode { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string BirthCity { get; set; } = null!;

    public string BirthProvince { get; set; } = null!;

    public string BirthNation { get; set; } = null!;

    public string ResidenceCity { get; set; } = null!;

    public string ResidenceProvince { get; set; } = null!;

    public string ResidenceNation { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateOnly? AmmissionDate { get; set; }

    public DateOnly? ResignationDate { get; set; }

    public decimal? CourseStartAge { get; set; }

    public string? ExamOutcome { get; set; }

    public decimal? ExamScore { get; set; }

    public string? ExamHonors { get; set; }

    public string? SchoolDegreeTitle { get; set; }

    public string? SchoolDegree { get; set; }

    public string? ProfessionalStatus { get; set; }

    public bool IsInInternship { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<AccessoriesAssignment> AccessoriesAssignments { get; set; } = new List<AccessoriesAssignment>();

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<PcAssignment> Pcassignments { get; set; } = new List<PcAssignment>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual User User { get; set; } = null!;
}
