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
    class GS_MM_MainMenu : GameState
    {
        // Load content needed for this GameState
        public override void LoadContent(ContentManager Content)
        {
            mainMenu_Exit_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/Main Menu/Button - Exit/exitButton");
            mainMenu_Load_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/Main Menu/Button - Load Map/loadMapButton");
            mainMenu_New_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/Main Menu/Button - New Map/newMapButton");
        }

        // Returns an enum to GameProcessess when certani criteria are met
        // Otherwise, runs code for update for Map Editor Main Menu
        public override GameProcesses.GameStateEnum Update(GraphicsDeviceManager graphics, Game1 game1)
        {
            #region Update - Mouse Location
            // Update mouse state location - assign details to mouse rectangle
            mState = Mouse.GetState();
            mousePos.X = mState.X;
            mousePos.Y = mState.Y;
            #endregion

            #region Update - Interface
            // Interface creation for map editor
            // Create Exit Button
            mainMenu_ExitRec = new Rectangle();
            mainMenu_ExitRec.Width = 200;
            mainMenu_ExitRec.Height = 88;
            mainMenu_ExitRec.X = graphics.PreferredBackBufferWidth - mainMenu_ExitRec.Width - 10;
            mainMenu_ExitRec.Y = graphics.PreferredBackBufferHeight - mainMenu_ExitRec.Height - 10;

            // Create Load Map Button
            mainMenu_LoadRec = new Rectangle();
            mainMenu_LoadRec.Width = 200;
            mainMenu_LoadRec.Height = 88;
            mainMenu_LoadRec.X = (int)(graphics.PreferredBackBufferWidth - mainMenu_LoadRec.Width * 2.5);
            mainMenu_LoadRec.Y = graphics.PreferredBackBufferHeight - mainMenu_LoadRec.Height - 10;

            // Create New Map Button
            mainMenu_NewRec = new Rectangle();
            mainMenu_NewRec.Width = 200;
            mainMenu_NewRec.Height = 88;
            mainMenu_NewRec.X = 10;
            mainMenu_NewRec.Y = graphics.PreferredBackBufferHeight - mainMenu_NewRec.Height - 10;
            #endregion

            #region Update - Main Menu
            // Main menu update programming
            // Detects collisions between mouse rectangle and buttons
            // and if button is pressed

            // New Map Button collision
            // Goes to Update - Map Maker in this Update method
            if (mousePos.Intersects(mainMenu_NewRec) && mState.LeftButton == ButtonState.Pressed)
            {
                return GameProcesses.GameStateEnum.mapEdit_MapEditor;
            }
            // Load Map Button collision
            // Changes GameState to gs_LoadMenu
            if (mousePos.Intersects(mainMenu_LoadRec) && mState.LeftButton == ButtonState.Pressed)
            {
                return GameProcesses.GameStateEnum.mapEdit_LoadMenu;
            }
            // Exit Map Button collision
            if (mousePos.Intersects(mainMenu_ExitRec) && mState.LeftButton == ButtonState.Pressed)
            {
                game1.Exit();
            }

            return GameProcesses.GameStateEnum.mapEdit_MainMenu;
            #endregion
        }

        // Draws objects for this GameState
        public override void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            #region Draw - Main Menu
            // Draws main buttons: New, Load, Exit
            spriteBatch.Draw(mainMenu_Exit_Txtr, mainMenu_ExitRec, Color.White);
            spriteBatch.Draw(mainMenu_New_Txtr, mainMenu_NewRec, Color.White);
            spriteBatch.Draw(mainMenu_Load_Txtr, mainMenu_LoadRec, Color.White);
            #endregion
        }
    }
}
