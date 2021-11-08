using RogueSharp.DiceNotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SibRus.Core
{
    public class CommandSystem
    {
        public bool MovePlayer( Direction direction)
        {
            int x = Game.Player.X;
            int y = Game.Player.Y;

            switch (direction)
            {
                case Direction.Up:
                    {
                        y = Game.Player.Y - 1;
                        break;
                    }
                case Direction.Down:
                    {
                        y = Game.Player.Y + 1;
                        break;
                    }
                case Direction.Left:
                    {
                        x = Game.Player.X - 1;
                        break;
                    }
                case Direction.Right:
                    {
                        x = Game.Player.X + 1;
                        break;
                    }
                default:
                    {
                        return false;
                    }
            }

            if ( Game.DungeonMap.SetActorPosition( Game.Player, x, y))
            {
                return true;
            }

            Monster monster = Game.DungeonMap.GetMonsterAt(x, y);

            if (monster != null)
            {
                Attack(Game.Player, monster);
                return true;
            }

            return false;
        }

        public void Attack(Actor attacker, Actor defender)
        {
            StringBuilder attackMessage = new StringBuilder();
            StringBuilder defenseMessage = new StringBuilder();

            int hits = ResolveAttack(attacker, defender, attackMessage);

            int blocks = ResolveDefense(defender, hits, attackMessage, defenseMessage);

            Game.MessageLog.Add(attackMessage.ToString());
            if (!string.IsNullOrWhiteSpace(defenseMessage.ToString()))
            {
                Game.MessageLog.Add(defenseMessage.ToString());
            }

            int damage = hits - blocks;

            ResolveDamage(defender, damage);
        }

        public static int ResolveAttack(Actor attacker, Actor defender, StringBuilder attackMessage)
        {
            int hits = 0;

            attackMessage.AppendFormat("{0} attacks {1} and rolls: ", attacker.Name, defender.Name);

            DiceExpression attackDice = new DiceExpression().Dice(attacker.Attack, 100);
            DiceResult attackResult = attackDice.Roll();

            foreach( TermResult termResult in attackResult.Results)
            {
                attackMessage.Append(termResult.Value + ",");

                if( termResult.Value >=100 - attacker.AttackChance )
                {
                    hits++;
                }
            }
            return hits;
        }

        public static int ResolveDefense(Actor defender, int hits, StringBuilder attackMessage, StringBuilder defenseMessage)
        {
            int blocks = 0;

            if(hits > 0)
            {
                attackMessage.AppendFormat("scoring {0} hits.", hits);
                defenseMessage.AppendFormat("{0} defends and rolls: ", defender.Name);

                DiceExpression defenseDice = new DiceExpression().Dice(defender.Defense, 100);
                DiceResult defenseRoll = defenseDice.Roll();

                foreach (TermResult termResult in defenseRoll.Results)
                {
                    defenseMessage.Append(termResult.Value + ",");

                    if (termResult.Value >= 100 - defender.DefenseChance)
                    {
                        blocks++;
                    }
                }
                defenseMessage.AppendFormat("resulting in {0} blocks.", blocks);
            }

            else
            {
                attackMessage.Append("and misses completely.");
            }

            return blocks;
        }

        public static void ResolveDamage(Actor defender, int damage)
        {
            if( damage > 0)
            {
                defender.Health = defender.Health - damage;

                Game.MessageLog.Add( $"{defender.Name} blocked all damage" );

                if (defender.Health <= 0)
                {
                    ResolveDeath(defender);
                }
            }

            else
            {
                Game.MessageLog.Add( $"{defender.Name} blocked all damage");
            }
        }

        private static void ResolveDeath( Actor defender)
        {
            if (defender is Player)
            {
                Game.MessageLog.Add($"{defender.Name} was killed, back to Gulag.");
            }

            else if (defender is Monster)
            {
                Game.DungeonMap.RemoveMonster((Monster)defender);
                Game.MessageLog.Add($"{defender.Name} died and dropped {defender.Gold} gold");
            }
        }
    }
}
