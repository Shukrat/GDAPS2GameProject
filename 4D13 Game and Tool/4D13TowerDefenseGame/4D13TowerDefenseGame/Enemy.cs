using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _4D13TowerDefenseGame
{
    // Morrgan Sweeney-Charlton; class for handling any and everything that enemies can do
    class Enemy:MovableGamePiece
    {
        // attributes
        int moraleDamage; // ammount of morale the player loses if this enemy reaches the other side of the screen
        int armor; // amount of damage removed from incoming attacks
        bool immune; // whether or not the enemy is immune to status effects (like slow); if true, then it is immune
        bool canAttack; // whether or not the enemy can attack the player's towers; if true, then it can
        bool alive; // whether or not the enemy is alive; if false, enemy will cease to exist immediately
        //GameVariables gv = new GameVariables(); // accesss to stat

        // properties
        public int MoraleDamage
        {
            get { return moraleDamage; }
        }
        public int Armor
        {
            get { return armor; }
        }
        public bool Immune
        {
            get { return immune; }
        }
        public bool CanAttack
        {
            get { return canAttack; }
        }

        public bool Alive
        {
            get { return alive; }
        }
        // parameterized constructor
        public Enemy(int hlt, int atk, int x, int y, int w, int h, string imgStr, int mvSp, int mrlDmg, int amr, bool imu, bool cnAtk)
            :base(hlt, atk, x, y, w, h, imgStr, mvSp)
        {
            moraleDamage = mrlDmg;
            armor = amr;
            immune = imu;
            canAttack = cnAtk;
            alive = true;
        }

        // method stub for attacking towers
        public void AttackTower(Tower twr)
        {
        }

        // method stub for being injured by towers
        public void TakeDamage(Projectile prj)
        {
            // subtract the attack of the projectile from the enemy's health
            this.health = this.health - (prj.Attack - armor);

            // test if enemy is immune to status ailements, and inflict them inf not
            if (immune == false)
            {
                switch (prj.Effect)
                {
                    // need to come up with a series of status effects for this to work
                    case "":
                        {
                            break;
                        }
                }
            }

            if (this.health <= 0)
            {
                this.alive = false;
            }
        }

        // method stub for reducing the player's morale
        public void MoraleAttack()
        {
            GameVariables.Morale -= moraleDamage;
        }

        public override void Move()
        {
            this.pieceShape.X += moveSpeed;
        }
    }
}
