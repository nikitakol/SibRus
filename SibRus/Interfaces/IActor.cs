﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SibRus.Interfaces
{
    public interface IActor
    {
        string Name { get; set; }
        int Awareness { get; set; }
    }
}
