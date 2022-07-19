using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SibRus.Interfaces
{
    public interface IItem
    {
        string Name { get; }
        int RemainingUSes { get; }

        bool Use();
    }
}
