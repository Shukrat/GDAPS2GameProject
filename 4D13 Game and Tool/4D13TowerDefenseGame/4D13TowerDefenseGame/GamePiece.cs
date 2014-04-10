#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace _4D13TowerDefenseGame
{
    // Morrgan Sweeney-Charlton; represents methods, attributes and properties common to objects placed on the game screen
    // cannot be instantiated
    abstract class GamePiece
    {
        // attributes
        protected int health; // number of hits object can take before being destroyed
        protected int attack; // ammount of damage object does in a single hit
        protected int xPos; // x and y values for use in creating a vector 2 in the game class
        protected int yPos;
        string imageStr; // string for name of file loaded into image
        protected Rectangle pieceShape; // rectangle for various attributes

        // properties
        public int Health
        {
            get { return health; }
            set { health = value; }
        }
        public int Attack
        {
            get { return attack; }
        }
        public string ImageStr
        {
            get { return imageStr; }
        }
        public Rectangle PieceShape
        {
            get { return pieceShape; }
        }

        // parameterized constructor
        public GamePiece(int hlt, int atk, int x, int y, int w, int h, String imgStr)
        {
            health = hlt;
            attack = atk;
            xPos = x;
            yPos = y;
            imageStr = imgStr;
            pieceShape = new Rectangle(xPos, yPos, w, h); // 50x50 is a placeholder value; sprites should be of a similar size
            // but maybe it should vary
        }
    }
}
