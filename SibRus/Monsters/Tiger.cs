using RogueSharp.DiceNotation;
using SibRus.Core;

namespace SibRus.Monsters
{
    class Tiger : Monster
    {
        public static Tiger Create(int level)
        {
            int health = Dice.Roll("1D5");
            return new Tiger
            {
                Attack = Dice.Roll("1D2") + level / 3,
                AttackChance = Dice.Roll("10D5"),
                Awareness = 10,
                Color = Colors.TigerColor,
                Defense = Dice.Roll("1D2") + level / 3,
                DefenseChance = Dice.Roll("10D4"),
                Gold = Dice.Roll("5D5"),
                Health = health,
                MaxHealth = health,
                Name = "Siberian Tiger",
                Speed = 40,
                Symbol = 'T'
            };
        }
    }
}
