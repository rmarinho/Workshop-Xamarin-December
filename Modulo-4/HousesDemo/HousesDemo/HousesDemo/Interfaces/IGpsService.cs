﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HousesDemo
{
    public interface IGpsService
    {
        Task<Location> GetLocation();
    }
}
