using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SibRus.Core
{
    public class Player : Actor
    {
        public Player()
        {
            
        }

        public void DrawStats( RLConsole statConsole)
        {
            statConsole.Print(1, 1, $"Name:    {Name}", Colors.Text);
            statConsole.Print(1, 3, $"Health:  {Health}/{MaxHealth}", Colors.Text);
            statConsole.Print(1, 5, $"Attack:  {Attack} ({AttackChance}%)", Colors.Text);
            statConsole.Print(1, 7, $"Defense: {Defense} ({DefenseChance}%)", Colors.Text);
            statConsole.Print(1, 9, $"Gold:    {Gold}", Colors.Gold);
        }
    }
}
