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
    // Morrgan Sweeney-Charlton; represents methods, attributes and properties common to objects placed on the game screen that can also move
    // cannot be instantiated
    abstract class MovableGamePiece:GamePiece
    {
        // attributes
        protected int moveSpeed; // speed at which the piece moves across the screen

        // properties
        public int MoveSpeed
        {
            get { return moveSpeed; }
        }

        // parameterized contstructor
        public MovableGamePiece(int hlt, int atk, int x, int y, int w, int h, String imgStr, int mvSp)
            :base(hlt, atk, x, y, w, h, imgStr)
        {
            moveSpeed = mvSp;
        }

        // method stub for moving pieces on the screen
        // will add more once I get a feel for what it needs to do in its entirety
        // is virtual so that child classes can override it as needed
        public virtual void Move()
        {
        }

        // method stub for moving pieces on the screen
        // will add more once I get a feel for what it needs to do in its entirety
        // is virtual so that child classes can override it as needed
        // overloaded so certain classes can use it
        public virtual void Move(int targetx, int targety, int towerx, int towery, Enemy en, Tower t)
        {
        }

        public virtual void Move(int xDim, int yDim)
        {
        }
    }
}
