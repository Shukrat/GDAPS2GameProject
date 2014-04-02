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

namespace Defense
{
    // Morrgan Sweeney-Charlton; class for handling any and everything towers can do/have done to them
    class Tower:GamePiece
    {
        // attributes
        int cost; // ammount of resources player has to pay to build the tower
        int saleValue; // ammount of resources the player receives for selling the tower
        public Projectile shot; // projectile created by the Tower
        int shotSpeed; // speed of projectiles created by this tower
        string effect; // effects done by this tower to enemies
        string shotString; // image of the projectile used by the tower

        // properties
        public int Cost
        {
            get { return cost; }
        }

        // parameterized constructor
        public Tower(int hlt, int atk, int x, int y, int w, int h, String imgStr, String stStr, int cst, int stSpd, string eft)
            :base(hlt, atk, x, y, w, h, imgStr)
        {
            cost = cst;
            shotSpeed = stSpd;
            effect = eft;
            shotString = stStr;
            // construct this tower's projectile in the center of the tower sprite
            shot = new Projectile(1, atk, (x + (this.PieceShape.Width / 2)), (y + (this.PieceShape.Height / 2)), 25, 25, shotString, shotSpeed, effect);
            saleValue = cost / 2;
        }

        // method stub for attacking enemies
        public void Attack(Enemy en)
        {
            if (shot.Active == false)
            {
                shot.Active = true;
                shot.Move(en);
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
