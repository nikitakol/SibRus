namespace SibRus.Equipment
{
    public class HandEquipment : Core.Equipment
    {
        public static HandEquipment None()
        {
            return new HandEquipment { Name = "None" };
        }

        public static HandEquipment Shiv()
        {
            return new HandEquipment
            {
                Attack = 1,
                AttackChance = 10,
                Name = "Shiv",
                Speed = -2
            };
        }

        public static HandEquipment Machete()
        {
            return new HandEquipment
            {
                Attack = 1,
                AttackChance = 20,
                Name = "Machete"
            };
        }

        public static HandEquipment Axe()
        {
            return new HandEquipment
            {
                Attack = 2,
                AttackChance = 15,
                Name = "Axe",
                Speed = 1
            };
        }

        public static HandEquipment DualDaggers()
        {
            return new HandEquipment
            {
                Attack = 3,
                AttackChance = 30,
                Name = "Dual Daggers",
                Speed = 3
            };
        }
    }
}
