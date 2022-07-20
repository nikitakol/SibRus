using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SibRus.Equipment
{
    public class FeetEquipment : Core.Equipment
    {
        public static FeetEquipment None()
        {
            return new FeetEquipment { Name = "None" };
        }

        public static FeetEquipment Leather()
        {
            return new FeetEquipment()
            {
                Defense = 1,
                DefenseChance = 5,
                Name = "Leather"
            };
        }

        public static FeetEquipment Vest()
        {
            return new FeetEquipment()
            {
                Defense = 1,
                DefenseChance = 10,
                Name = "Chain"
            };
        }

        public static FeetEquipment Plate()
        {
            return new FeetEquipment()
            {
                Defense = 1,
                DefenseChance = 15,
                Name = "Plate"
            };
        }
    }
}
