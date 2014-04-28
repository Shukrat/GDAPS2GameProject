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
    class GS_Game_Settings : GameState
    {
        public override void LoadContent(ContentManager Content)
        {
            bg_MapEditMainMenu = Content.Load<Texture2D>("Splash");

            mapEdit_Back_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Back/BackButton");
            mapEdit_BackHover_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Back/BackHoverButton");
            mapEdit_BackClick_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Back/BackClickButton");
        }

        public override GameProcesses.GameStateEnum Update(GraphicsDeviceManager graphics, Game1 game1)
        {
            #region Update - Mouse Location
            // Update mouse state location - assign details to mouse rectangle
            MouseStateGet();
            mousePos.X = mState.X;
            mousePos.Y = mState.Y;
            #endregion

            #region Create Button Rectangles
            mainMenu_ExitRec = new Rectangle();
            mainMenu_ExitRec.Width = 200;
            mainMenu_ExitRec.Height = 88;
            mainMenu_ExitRec.X = 475;
            mainMenu_ExitRec.Y = 750;

            mapEdit_MainBackground = new Rectangle();
            mapEdit_MainBackground.Width = 1150;
            mapEdit_MainBackground.Height = 920;
            mapEdit_MainBackground.X = 0;
            mapEdit_MainBackground.Y = 0;
            #endregion

            if (prevMState.LeftButton != ButtonState.Pressed)
            {
                // Back Button collision
                if (mousePos.Intersects(mainMenu_ExitRec) && mState.LeftButton == ButtonState.Pressed)
                {
                    prevMState = Mouse.GetState();
                    return GameProcesses.GameStateEnum.main_MainMenu;
                }
            }
            return GameProcesses.GameStateEnum.main_Settings;
        }

        public override void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            spriteBatch.Draw(bg_MapEditMainMenu, mapEdit_MainBackground, Color.White);
            spriteBatch.Draw(mapEdit_Back_Txtr, mainMenu_ExitRec, Color.White);

            // Back Button Hover and Click
            if (mousePos.Intersects(mainMenu_ExitRec))
            {
                spriteBatch.Draw(mapEdit_BackHover_Txtr, mainMenu_ExitRec, Color.White);
                if (mState.LeftButton == ButtonState.Pressed)
                {
                    spriteBatch.Draw(mapEdit_BackClick_Txtr, mainMenu_ExitRec, Color.White);
                }
            }
            
        }
    }
}
