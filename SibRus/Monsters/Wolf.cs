using RogueSharp.DiceNotation;
using SibRus.Core;

namespace SibRus.Monsters
{
    class Wolf : Monster
    {
        public static Wolf Create(int level)
        {
            int health = Dice.Roll("2D5");
            return new Wolf
            {
                Attack = Dice.Roll("1D3") + level / 3,
                AttackChance = Dice.Roll("25D3"),
                Awareness = 8,
                Color = Colors.WolfColor,
                Defense = Dice.Roll("1D3") + level / 3,
                DefenseChance = Dice.Roll("10D4"),
                Gold = Dice.Roll("5D5"),
                Health = health,
                MaxHealth = health,
                Name = "Wolf",
                Speed = 14,
                Symbol = 'w'
            };
        }
    }
}
