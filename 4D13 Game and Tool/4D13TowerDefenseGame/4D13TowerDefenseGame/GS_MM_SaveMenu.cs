﻿using System;
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
    class GS_MM_SaveMenu : GameState
    {
        // Load content needed for this GameState
        public override void LoadContent(ContentManager Content)
        {
            mapEdit_Back_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Back/BackButton");
            mapEdit_BackHover_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Back/BackHoverButton");
            mapEdit_BackClick_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Back/BackClickButton");

            font = Content.Load<SpriteFont>("Fonts/mainFont");
        }

        // Returns an enum to GameProcessess when certani criteria are met
        // Otherwise, runs code for update for Map Editor Save Menu
        public override GameProcesses.GameStateEnum Update(GraphicsDeviceManager graphics, Game1 game1)
        {
            #region Update - Mouse Location
            // Update mouse state location - assign details to mouse rectangle
            MouseStateGet();
            mousePos.X = mState.X;
            mousePos.Y = mState.Y;
            #endregion

            mainMenu_ExitRec = new Rectangle();     // FUNCTIONS AS BACK BUTTON TO MAIN MENU
            mainMenu_ExitRec.Width = 200;           // FUNCTIONS AS BACK BUTTON TO MAIN MENU
            mainMenu_ExitRec.Height = 88;           // FUNCTIONS AS BACK BUTTON TO MAIN MENU
            mainMenu_ExitRec.X = 940;               // FUNCTIONS AS BACK BUTTON TO MAIN MENU
            mainMenu_ExitRec.Y = 822;               // FUNCTIONS AS BACK BUTTON TO MAIN MENU

            // SaveLoad Calls:
            // Set SaveLoad saveComplete bool to false in order to wait for user input
            // Send bool if on load screen or save screen
            // Set SaveLoad textures 2D array to this one
            // Call SaveLoad Update (user input)
            // Saves to binary file
            GameProcesses.saveLoad.SaveComplete = false;
            GameProcesses.saveLoad.SaveMenu = true;
            GameProcesses.saveLoad.saveLoadBackground = saveLoadBackground;
            //saveLoad.Textures = textures;
            GameProcesses.saveLoad.Update(kState, prevKState);

            // Waits for user to hit enter in SaveLoad.Update method
            // moves to Update - Map Maker in this Update method
            if (GameProcesses.saveLoad.SaveComplete)
            {
                return GameProcesses.GameStateEnum.mapEdit_MapEditor;
            }
            if (prevMState.LeftButton != ButtonState.Pressed)
            {
                if (mousePos.Intersects(mainMenu_ExitRec) && mState.LeftButton == ButtonState.Pressed)
                {
                    prevMState = Mouse.GetState();
                    return GameProcesses.GameStateEnum.mapEdit_MapEditor;
                }
            }

            return GameProcesses.GameStateEnum.mapEdit_SaveMenu;
        }

        // Draws objects for this GameState
        public override void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            // Update kState and prevKState before running SaveLoad update programming
            prevKState = kState;
            kState = Keyboard.GetState();

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

            // Set font, and draw the text.
            GameProcesses.saveLoad.ConsoleFont = font;
            GameProcesses.saveLoad.Draw(spriteBatch);
        }
    }
}
