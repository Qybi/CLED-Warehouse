using System;
using System.Collections.Generic;

namespace CLED.Warehouse.Models.DB;

public partial class ReasonsAssignment
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<AccessoriesAssignment> AccessoriesAssignments { get; set; } = new List<AccessoriesAssignment>();

    public virtual ICollection<PcAssignment> Pcassignments { get; set; } = new List<PcAssignment>();
}
