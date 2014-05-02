using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace _4D13TowerDefenseGame
{
    // Morrgan Sweeney-Charlton; casts spells with various effects in the game
    class Spell
    {
        // attributes
        Rectangle areaOfEffect; // size where the spell hits
        string effect; // effect the spell has on the enemy

        // properties
        public Rectangle AreaOfEffect
        {
            get { return areaOfEffect; }
        }
        public string Effect
        {
            get { return effect; }
        }

        public Spell(string eft, int xPos, int yPos)
        {
            effect = eft;
            areaOfEffect = new Rectangle(xPos, yPos, 135, 135);
        }
    }
}
