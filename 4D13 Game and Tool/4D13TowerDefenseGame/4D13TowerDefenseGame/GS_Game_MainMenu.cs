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
    class GS_Game_MainMenu : GameState
    {
        public override void LoadContent(ContentManager Content)
        {
            bg_MapEditMainMenu = Content.Load<Texture2D>("Splash");

            mainMenu_Exit_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Exit/exitButton");
            mainMenu_ExitHover_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Exit/exitButtonHover");
            mainMenu_ExitClick_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Exit/exitButtonClick");

            mainMenu_Load_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Load Custom/LCButton");
            mainMenu_LoadHover_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Load Custom/LCHoverButton");
            mainMenu_LoadClick_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Load Custom/LCClickButton");

            mainMenu_New_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - New Map/NewButton");
            mainMenu_NewHover_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - New Map/NewHoverButton");
            mainMenu_NewClick_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - New Map/NewClickButton");

            mainMenu_MapEditor_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Map Editor/MEButton");
            mainMenu_MapEditorHover_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Map Editor/MEHoverButton");
            mainMenu_MapEditorClick_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Map Editor/MEClickButton");

            mainMenu_Settings_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Settings/settingsButton");
            mainMenu_SettingsHover_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Settings/settingsButtonHover");
            mainMenu_SettingsClick_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Settings/settingsButtonClick");
        }

        public override GameProcesses.GameStateEnum Update(GraphicsDeviceManager graphics, Game1 game1)
        {

            #region Update - Mouse Location
            // Update mouse state location - assign details to mouse rectangle
            MouseStateGet();
            mousePos.X = mState.X;
            mousePos.Y = mState.Y;
            #endregion

            #region Update - Interface
            // Interface creation for map editor
            // Create Exit Button
            mainMenu_ExitRec = new Rectangle();
            mainMenu_ExitRec.Width = 200;
            mainMenu_ExitRec.Height = 88;
            mainMenu_ExitRec.X = 475;
            mainMenu_ExitRec.Y = 750;

            // Create Load Map Button
            mainMenu_LoadRec = new Rectangle();
            mainMenu_LoadRec.Width = 200;
            mainMenu_LoadRec.Height = 88;
            mainMenu_LoadRec.X = 475;
            mainMenu_LoadRec.Y = 554;

            // Create New Map Button
            mainMenu_NewRec = new Rectangle();
            mainMenu_NewRec.Width = 200;
            mainMenu_NewRec.Height = 88;
            mainMenu_NewRec.X = 475;
            mainMenu_NewRec.Y = 446;

            // Map editor button
            mainMenu_MapEditorRec = new Rectangle();
            mainMenu_MapEditorRec.Width = 200;
            mainMenu_MapEditorRec.Height = 88;
            mainMenu_MapEditorRec.X = 475;
            mainMenu_MapEditorRec.Y = 652;

            // Settings Button
            mainMenu_SettingsRec = new Rectangle();
            mainMenu_SettingsRec.Width = 150;
            mainMenu_SettingsRec.Height = 66;
            mainMenu_SettingsRec.X = 980;
            mainMenu_SettingsRec.Y = 834;

            #endregion

            mapEdit_MainBackground = new Rectangle();
            mapEdit_MainBackground.Width = 1150;
            mapEdit_MainBackground.Height = 920;
            mapEdit_MainBackground.X = 0;
            mapEdit_MainBackground.Y = 0;

            #region Update - Main Menu
            // Main menu update programming
            // Detects collisions between mouse rectangle and buttons
            // and if button is pressed

            // New Map Button collision
            // Goes to Update - Map Maker in this Update method
            if (prevMState.LeftButton != ButtonState.Pressed)
            {
                if (mousePos.Intersects(mainMenu_NewRec) && mState.LeftButton == ButtonState.Pressed)
                {
                    prevMState = Mouse.GetState();
                    return GameProcesses.GameStateEnum.main_MapFirst;
                }
                // Load Map Button collision
                // Changes GameState to gs_LoadMenu
                if (mousePos.Intersects(mainMenu_LoadRec) && mState.LeftButton == ButtonState.Pressed)
                {
                    prevMState = Mouse.GetState();
                    return GameProcesses.GameStateEnum.main_LoadScreen;
                }
                if (mousePos.Intersects(mainMenu_MapEditorRec) && mState.LeftButton == ButtonState.Pressed)
                {
                    prevMState = Mouse.GetState();
                    return GameProcesses.GameStateEnum.mapEdit_MainMenu;
                }
                if (mousePos.Intersects(mainMenu_SettingsRec) && mState.LeftButton == ButtonState.Pressed)
                {
                    prevMState = Mouse.GetState();
                    return GameProcesses.GameStateEnum.main_Settings;
                }
                // Exit Map Button collision
                if (mousePos.Intersects(mainMenu_ExitRec) && mState.LeftButton == ButtonState.Pressed)
                {
                    game1.Exit();
                }
            }
            prevMState = Mouse.GetState();
            return GameProcesses.GameStateEnum.main_MainMenu;
            #endregion
        }

        public override void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            #region Draw - Main Menu
            // Draws main buttons: New, Load, Exit
            spriteBatch.Draw(bg_MapEditMainMenu, mapEdit_MainBackground, Color.White);
            spriteBatch.Draw(mainMenu_New_Txtr, mainMenu_NewRec, Color.White);
            spriteBatch.Draw(mainMenu_Load_Txtr, mainMenu_LoadRec, Color.White);
            spriteBatch.Draw(mainMenu_Exit_Txtr, mainMenu_ExitRec, Color.White);
            spriteBatch.Draw(mainMenu_MapEditor_Txtr, mainMenu_MapEditorRec, Color.White);
            spriteBatch.Draw(mainMenu_Settings_Txtr, mainMenu_SettingsRec, Color.White);
            //spriteBatch.Draw(mainMenu_MapEditor_Txtr, mainMenu_MapEditorRec, Color.White);

            // New Button Hover and Click
            if (mousePos.Intersects(mainMenu_NewRec))
            {
                spriteBatch.Draw(mainMenu_NewHover_Txtr, mainMenu_NewRec, Color.White);
                if (mState.LeftButton == ButtonState.Pressed)
                {
                    spriteBatch.Draw(mainMenu_NewClick_Txtr, mainMenu_NewRec, Color.White);
                }
            }

            // Load Button Hover and Click
            if (mousePos.Intersects(mainMenu_LoadRec))
            {
                spriteBatch.Draw(mainMenu_LoadHover_Txtr, mainMenu_LoadRec, Color.White);
                if (mState.LeftButton == ButtonState.Pressed)
                {
                    spriteBatch.Draw(mainMenu_LoadClick_Txtr, mainMenu_LoadRec, Color.White);
                }
            }

            // Exit Button Hover and Click
            if (mousePos.Intersects(mainMenu_ExitRec))
            {
                spriteBatch.Draw(mainMenu_ExitHover_Txtr, mainMenu_ExitRec, Color.White);
                if (mState.LeftButton == ButtonState.Pressed)
                {
                    spriteBatch.Draw(mainMenu_ExitClick_Txtr, mainMenu_ExitRec, Color.White);
                }
            }

            // Map Editor Button Hover and Click
            if (mousePos.Intersects(mainMenu_MapEditorRec))
            {
                spriteBatch.Draw(mainMenu_MapEditorHover_Txtr, mainMenu_MapEditorRec, Color.White);
                if (mState.LeftButton == ButtonState.Pressed)
                {
                    spriteBatch.Draw(mainMenu_MapEditorClick_Txtr, mainMenu_MapEditorRec, Color.White);
                }
            }

            // Settings Button Hover and Click
            if (mousePos.Intersects(mainMenu_SettingsRec))
            {
                spriteBatch.Draw(mainMenu_SettingsHover_Txtr, mainMenu_SettingsRec, Color.White);
                if (mState.LeftButton == ButtonState.Pressed)
                {
                    spriteBatch.Draw(mainMenu_SettingsClick_Txtr, mainMenu_SettingsRec, Color.White);
                }
            }
            #endregion
        }
    }
}
