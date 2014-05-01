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
    // Morrgan Sweeney-Charlton; class for handling any and everything towers can do/have done to them
    class Tower:GamePiece
    {
        // attributes
        int cost; // ammount of resources player has to pay to build the tower
        int saleValue; // ammount of resources the player receives for selling the tower
        public Projectile shot = null; // projectile created by the Tower
        int shotSpeed; // speed of projectiles created by this tower
        string effect; // effects done by this tower to enemies
        string shotString; // image of the projectile used by the tower
        Rectangle hitbox; // area from which the tower can attack; placeholder because the circle class doesn't want to run

        // properties
        public int Cost
        {
            get { return cost; }
        }

        public Rectangle HitBox
        {
            get { return hitbox; }
        }

        // parameterized constructor
        public Tower(int hlt, int atk, int x, int y, int w, int h, String imgStr, String stStr, int cst, int stSpd, string eft)
            :base(hlt, atk, x, y, w, h, imgStr)
        {
            cost = cst;
            cost = 100;
            shotSpeed = stSpd;
            effect = eft;
            shotString = stStr;
            // construct this tower's projectile in the center of the tower sprite
            
            saleValue = cost / 2;
            hitbox = new Rectangle((pieceShape.X - pieceShape.Width), (pieceShape.Y - pieceShape.Height), (pieceShape.Width * 3), (pieceShape.Height * 3));
        }  

        // method for attacking enemies
        public void AttackEnemy(Enemy en)
        {
            if (shot == null)
            {
                shot = new Projectile(1, attack, (this.PieceShape.X + (this.PieceShape.Width / 4)), (this.PieceShape.Y + (this.PieceShape.Height / 4)), 22, 22, shotString, shotSpeed, effect);
            }
            if (shot.Active == false)
            {
                shot.Active = true;
                shot.Move(en.PieceShape.X, en.PieceShape.Y, this.pieceShape.X, this.pieceShape.Y, en, this);
            }
            else if (shot.Active == true)
            {
                shot.Move(en.PieceShape.X, en.PieceShape.Y, this.pieceShape.X, this.pieceShape.Y, en, this);
                if (shot.Active == false)
                {
                    shot = null;
                }
            }
        }

        // method stub for building the tower in a given position
        public void Build()
        {
        }

        // method stub for damaging the tower
        public void TakeDamage(Enemy en)
        {
            // the this. is to ensure that the Health called is the tower's health
            this.Health = this.Health - en.Attack;
        }
    }
}
