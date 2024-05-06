using System;
using System.Collections.Generic;

namespace CLED.Warehouse.Web.Models;

public partial class Course
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string ShortName { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string Location { get; set; } = null!;

    public DateTime DateStart { get; set; }

    public DateTime DateEnd { get; set; }

    public DateTime RegistrationDate { get; set; }

    public string RegistrationUser { get; set; } = null!;

    public DateTime? DeletedDate { get; set; }

    public string? DeletedUser { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
