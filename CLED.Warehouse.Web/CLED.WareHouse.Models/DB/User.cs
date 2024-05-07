using System;
using System.Collections.Generic;

namespace CLED.Warehouse.Web;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public List<string> Roles { get; set; } = null!;

    public bool Enabled { get; set; }

    public int? StudentId { get; set; }

    public DateTime RegistrationDate { get; set; }

    public string RegistrationUser { get; set; } = null!;

    public DateTime? DeletedDate { get; set; }

    public string? DeletedUser { get; set; }

    public virtual Student? Student { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
