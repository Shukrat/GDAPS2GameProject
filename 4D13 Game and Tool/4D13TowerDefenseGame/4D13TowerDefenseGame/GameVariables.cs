using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _4D13TowerDefenseGame
{
    // Morrgan Sweeney-Charlton; class of static variables, such as the player's health
    static class GameVariables
    {
        static int morale = 100;

        static public int Morale
        {
            get { return morale; }
            set { morale = value; }
        }
    }
}
