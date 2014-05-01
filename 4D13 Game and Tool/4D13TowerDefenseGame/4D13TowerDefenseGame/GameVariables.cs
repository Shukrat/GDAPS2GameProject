﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _4D13TowerDefenseGame
{
    // Morrgan Sweeney-Charlton; class of static variables, such as the player's health
    static class GameVariables
    {
        static int morale = 100;
        static int currency = 1000;
        static List<Enemy> enemies;
        static List<Tower> towers;

        static public int Morale
        {
            get { return morale; }
            set { morale = value; }
        }

        static public List<Enemy> Enemies
        {
            get { return enemies; }
            set { enemies = value; }
        }

        static public List<Tower> Towers
        {
            get { return towers; }
            set { towers = value; }
        }

        static public int Currency
        {
            get { return currency; }
            set { currency = value; }
        }
    }
}
