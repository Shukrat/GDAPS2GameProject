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
        static int currency = 1000;
        static List<Enemy> enemies;
        static List<Tower> towers;
        static List<PathMarker> markers;
        static List<Spell> magic;
        static int spawnLocationX;
        static int spawnLocationY;

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

        static public List<PathMarker> Markers
        {
            get { return markers; }
            set { markers = value; }
        }

        static public List<Spell> Magic
        {
            get { return magic; }
            set { magic = value; }
        }

        static public int SpawnLocationX
        {
            get { return spawnLocationX; }
            set { spawnLocationX = value; }
        }
        static public int SpawnLocationY
        {
            get { return spawnLocationY; }
            set { spawnLocationY = value; }
        }
    }
}
