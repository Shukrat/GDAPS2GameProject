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
    // Morrgan Sweeney-Charlton; places markers on the map that tell enemies where to go
    class PathMarker
    {
        public Rectangle marker;
        int control;

        public PathMarker(int xPos, int yPos, int ctrl)
        {
            control = ctrl;
            
            switch (ctrl)
            {
                case 1:
                    {
                        marker = new Rectangle(xPos + 45, yPos, 1, 1);
                        control = 6;
                        break;
                    }
                case 2:
                    {
                        marker = new Rectangle(xPos, yPos, 1, 1);
                        control = 3;
                        break;
                    }
                case 3:
                    {
                        marker = new Rectangle(xPos + 45, yPos + 46, 1, 1);
                        control = 4;
                        break;
                    }
                case 4:
                    {
                        marker = new Rectangle(xPos, yPos + 46, 1, 1);
                        control = 2;
                        break;
                    }
                case 5:
                    {
                        marker = new Rectangle(xPos, yPos, 45, 45);
                        control = 5;
                        break;
                    }
            }
            
        }

        public int Control
        {
            get { return control; }
        }

        public Rectangle Marker
        {
            get { return marker; }
        }
    }
}
