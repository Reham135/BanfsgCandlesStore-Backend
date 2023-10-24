﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.DAL;

public interface IOrderRepo : IGenericRepo<Order>
{
    int GetIdOfLastOrderByUID(string uID);
}
