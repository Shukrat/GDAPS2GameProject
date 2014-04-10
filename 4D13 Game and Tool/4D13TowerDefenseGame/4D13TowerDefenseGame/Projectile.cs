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
    // Morrgan Sweeney-Charlton; class for handling any and everything projectiles can do
    class Projectile:MovableGamePiece
    {
        // attributes
        string effect; // string representing status ailments projectile inflicts
        bool active; // boolean determining if the projectile is in use

        // properties
        public string Effect
        {
            get { return effect; }
        }
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        // parametrized constructor
        public Projectile(int hlt, int atk, int x, int y, int w, int h, String imgStr, int mvSp, string eft)
            : base(hlt, atk, x, y, w, h, imgStr, mvSp)
        {
            effect = eft;
            active = false;
        }

        // move the projectile toward the enemy
        public override void Move(Enemy en)
        {
            /*
            while (active == true)
            {
                this.pieceShape.X = this.pieceShape.X + (this.moveSpeed - en.PieceShape.X);
                this.pieceShape.Y = this.pieceShape.Y + (this.moveSpeed - en.PieceShape.Y);
                if (this.pieceShape.Intersects(en.PieceShape) == true)
                {
                    active = false;
                }
            }
              */
        }
    }
}
