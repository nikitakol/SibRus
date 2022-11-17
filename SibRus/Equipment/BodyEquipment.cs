namespace SibRus.Equipment
{
    public class BodyEquipment : Core.Equipment
    {
        public static BodyEquipment None()
        {
            return new BodyEquipment{ Name = "None" };
        }

        public static BodyEquipment Leather()
        {
            return new BodyEquipment()
            {
                Defense = 1,
                DefenseChance = 10,
                Name = "Leather"
            };
        }

        public static BodyEquipment Vest()
        {
            return new BodyEquipment()
            {
                Defense = 2,
                DefenseChance = 5,
                Name = "Vest"
            };
        }

        public static BodyEquipment Plate()
        {
            return new BodyEquipment()
            {
                Defense = 2,
                DefenseChance = 10,
                Name = "Plate"
            };
        }
    }
}
