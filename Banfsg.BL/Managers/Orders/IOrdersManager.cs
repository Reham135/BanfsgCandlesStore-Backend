﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.BL;

public interface IOrdersManager
{
    void AddNewOrder(string uID, int addressId);
}
