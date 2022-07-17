using RogueSharp.DiceNotation;
using SibRus.Core;

namespace SibRus.Monsters
{
    class Bear : Monster
    {
        public static Bear Create(int level)
        {
            int health = Dice.Roll("2D5");
            return new Bear
            {
                Attack = Dice.Roll("1D3") + level / 3,
                AttackChance = Dice.Roll("25D3"),
                Awareness = 7,
                Color = Colors.BearColor,
                Defense = Dice.Roll("1D3") + level / 3,
                DefenseChance = Dice.Roll("10D4"),
                Gold = Dice.Roll("5D5"),
                Health = health,
                MaxHealth = health,
                Name = "Bear",
                Speed = 20,
                Symbol = 'B'
            };
        }
    }
}
