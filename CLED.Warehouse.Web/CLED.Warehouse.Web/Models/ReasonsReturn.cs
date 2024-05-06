using System;
using System.Collections.Generic;

namespace CLED.Warehouse.Web.Models;

public partial class ReasonsReturn
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<AccessoriesAssignment> AccessoriesAssignments { get; set; } = new List<AccessoriesAssignment>();

    public virtual ICollection<Pcassignment> Pcassignments { get; set; } = new List<Pcassignment>();
}
