using CLED.Warehouse.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLED.Warehouse.Data.Abstractions;

public interface IUnitOfWork
{
	IAccessoryRepository Accessories { get; }
	IAccessoryAssignmentRepository AccessoryAssignments { get; }
	ICourseRepository Courses { get; }
	IPcRepository Pcs { get; }
	IPcAssignmentRepository PcAssignments { get; }
	IPcModelStockRepository PcModelsStock { get; }
	IReasonsAssignmentRepository ReasonsAssignment { get; }
	IReasonsReturnRepository ReasonsReturn { get; }
	IStudentRepository Students { get; }
	ITicketRepository Tickets { get; }
	//IUserRepository Users { get; }
	Task SaveAsync();
}
