using CLED.Warehouse.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLED.WareHouse.Models.Views;

public class StudentDetails
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string FiscalCode { get; set; }
    public string BirthCountry { get; set; }
    public string BirthCity { get; set; }
	public string ResidenceCountry { get; set; }
    public string ResidenceCity { get; set; }
    public string SchoolIdentifier { get; set; }
    public string EmailUser { get; set; }
    public string Status { get; set; }
    public Course Course { get; set; }
    public IEnumerable<PcAssignment> PcAssignments { get; set; }
    public IEnumerable<AccessoriesAssignment> AccessoriesAssignments { get; set; }
}
