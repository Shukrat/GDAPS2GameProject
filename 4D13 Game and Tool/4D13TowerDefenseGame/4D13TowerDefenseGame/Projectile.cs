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
        public override void Move(int targetx, int targety, int towerx, int towery, Enemy en, Tower t)
        {
            if (this.pieceShape.Intersects(t.HitBox))
            {
                int vectorX = (targetx - towerx);
                int vectorY = (targety - towery);

                double vectorMagnitude = (Math.Sqrt((vectorX * vectorX) + (vectorY * vectorY)));

                double unitVectorX = (vectorX / vectorMagnitude);
                double unitVectorY = (vectorY / vectorMagnitude);

                double finalVectorX = (unitVectorX * moveSpeed);
                double finalVectorY = (unitVectorY * moveSpeed);



                this.pieceShape.X += (int)finalVectorX;
                this.pieceShape.Y += (int)finalVectorY;

                if (this.pieceShape.Intersects(en.PieceShape))
                {
                    active = false;
                    en.TakeDamage(this);

                }
                else if (this.pieceShape.X < 0 || this.pieceShape.Y < 0 || this.pieceShape.X > 800 || this.pieceShape.Y > 600)
                {
                    active = false;
                }
            }
            else
            {
                active = false;
            }
        }

        // overload for enemies that can attack towers
        public override void Move(int targetx, int targety, int towerx, int towery, Tower t, Enemy en)
        {
            if (this.pieceShape.Intersects(t.HitBox))
            {
                int vectorX = (targetx - towerx);
                int vectorY = (targety - towery);

                double vectorMagnitude = (Math.Sqrt((vectorX * vectorX) + (vectorY * vectorY)));

                double unitVectorX = (vectorX / vectorMagnitude);
                double unitVectorY = (vectorY / vectorMagnitude);

                double finalVectorX = (unitVectorX * moveSpeed);
                double finalVectorY = (unitVectorY * moveSpeed);



                this.pieceShape.X += (int)finalVectorX;
                this.pieceShape.Y += (int)finalVectorY;

                if (this.pieceShape.Intersects(t.PieceShape))
                {
                    active = false;
                    t.TakeDamage(this);

                }
                else if (this.pieceShape.X < 0 || this.pieceShape.Y < 0 || this.pieceShape.X > 800 || this.pieceShape.Y > 600)
                {
                    active = false;
                }
            }
            else
            {
                active = false;
            }
        }
    }
}
