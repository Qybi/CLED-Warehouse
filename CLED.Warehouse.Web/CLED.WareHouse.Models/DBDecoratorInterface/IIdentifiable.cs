﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLED.Warehouse.Models.DB;

public interface IIdentifiable<T>
{
	T Id { get; }
}
