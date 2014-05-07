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
    // Morrgan Sweeney-Charlton; class for handling any and everything that enemies can do
    class Enemy:MovableGamePiece
    {
        // attributes
        int moraleDamage; // ammount of morale the player loses if this enemy reaches the other side of the screen
        int armor; // amount of damage removed from incoming attacks
        int droppedCurrency;
        bool immune; // whether or not the enemy is immune to status effects (like slow); if true, then it is immune
        bool canAttack; // whether or not the enemy can attack the player's towers; if true, then it can
        bool alive; // whether or not the enemy is alive; if false, enemy will cease to exist immediately
        int direction; // direction the enemy should move in, 1 for up, 2 for right, 3 for down, 4 for up
        bool isVisible; // determines whether or not the enemy is drawn/can move and be attacked
        bool slowed; // determines if the enemy is being slowed down by a spell
        bool slowControl; // controls the use of the slow spell
        int slowCount; // controls the use of the slow spell
        Projectile shot; // projectiles fired at towers
        Rectangle hitBox;

        // RNG
        Random rng = new Random();

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
        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }
        public bool Slowed
        {
            get { return slowed; }
            set { slowed = value; }
        }
        public Rectangle HitBox
        {
            get { return hitBox; }
        }
        public Projectile Shot
        {
            get { return shot; }
            set { shot = value; }
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

            droppedCurrency = rng.Next(49);

            direction = 3;


            isVisible = false;
            slowed = false;
            slowControl = false;
            slowCount = 0;
            hitBox = new Rectangle((pieceShape.X - pieceShape.Width), (pieceShape.Y - pieceShape.Height), (pieceShape.Width * 3), (pieceShape.Height * 3));
        }

        // method stub for attacking towers
        public void AttackTower(Tower twr)
        {
            if (shot == null)
            {
                shot = new Projectile(1, attack, (this.PieceShape.X + (this.PieceShape.Width / 4)), (this.PieceShape.Y + (this.PieceShape.Height / 4)), 22, 22, "", 1, "");
            }
            if (shot.Active == false)
            {
                shot.Active = true;
                shot.Move(this.pieceShape.X, this.pieceShape.Y, twr.PieceShape.X, twr.PieceShape.Y, twr, this);
            }
            else if (shot.Active == true)
            {
                shot.Move(this.pieceShape.X, this.pieceShape.Y, twr.PieceShape.X, twr.PieceShape.Y, twr, this);
                if (shot.Active == false)
                {
                    shot = null;
                }
            }
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
                GameVariables.Currency = GameVariables.Currency + droppedCurrency;
            }
        }

        // method stub for reducing the player's morale
        public void MoraleAttack()
        {
            GameVariables.Morale -= moraleDamage;
        }

        public override void Move(/*int xDimension, int yDimension*/)
        {
            if (isVisible == true)
            {
                if (slowed == false)
                {
                    switch (direction)
                    {
                        case 1:
                            {
                                this.pieceShape.Y -= moveSpeed;
                                break;
                            }
                        case 2:
                            {
                                this.pieceShape.X += moveSpeed;
                                break;
                            }
                        case 3:
                            {
                                this.pieceShape.Y += moveSpeed;
                                break;
                            }
                        case 4:
                            {
                                this.pieceShape.X -= moveSpeed;
                                break;
                            }
                        case 5:
                            {
                                this.MoraleAttack();
                                alive = false;
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    foreach (PathMarker pm in GameVariables.Markers)
                    {
                        if (this.pieceShape.Intersects(pm.Marker))
                        {
                            direction = pm.Control;
                        }
                    }
                    
                }
                else if (slowed == true)
                {
                    if (slowControl == false)
                    {
                        switch (direction)
                        {
                            case 1:
                                {
                                    this.pieceShape.Y -= moveSpeed;
                                    break;
                                }
                            case 2:
                                {
                                    this.pieceShape.X += moveSpeed;
                                    break;
                                }
                            case 3:
                                {
                                    this.pieceShape.Y += moveSpeed;
                                    break;
                                }
                            case 4:
                                {
                                    this.pieceShape.X -= moveSpeed;
                                    break;
                                }
                            case 5:
                                {
                                    this.MoraleAttack();
                                    alive = false;
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        slowControl = true;
                    }
                    else
                    {
                        if (slowCount == 6)
                        {
                            slowControl = false;
                            slowCount = 0;
                        }
                        slowCount++;
                    }
                    
                    foreach (PathMarker pm in GameVariables.Markers)
                    {
                        if (this.pieceShape.Intersects(pm.Marker))
                        {
                            direction = pm.Control;
                        }
                    }
                    
                }
                
            }
        }
    }
}
