using CLED.Warehouse.Models.DB;

namespace CLED.Warehouse.Models.DB;

public partial class AccessoriesAssignment : IIdentifiable<int> { }
public partial class Accessory : IIdentifiable<int>;
public partial class Course : IIdentifiable<int>;
public partial class PcAssignment : IIdentifiable<int> { }
public partial class Pc : IIdentifiable<int> { }
public partial class PcModelStock : IIdentifiable<int> { }
public partial class ReasonsAssignment : IIdentifiable<int> { }
public partial class ReasonsReturn : IIdentifiable<int> { }
public partial class Student : IIdentifiable<int> { }
public partial class User : IIdentifiable<int> { }
public partial class Ticket : IIdentifiable<int> { }

