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
            bg_MapEditMainMenu = Content.Load<Texture2D>("SplashEditor");

            mapEdit_Back_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Back/BackButton");
            mapEdit_BackHover_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Back/BackHoverButton");
            mapEdit_BackClick_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Back/BackClickButton");

            mainMenu_Exit_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Exit/exitButton");
            mainMenu_ExitHover_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Exit/exitButtonHover");
            mainMenu_ExitClick_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Exit/exitButtonClick");

            mainMenu_Load_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Load/LoadButton");
            mainMenu_LoadHover_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Load/LoadHoverButton");
            mainMenu_LoadClick_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Load/LoadClickButton");

            mainMenu_New_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - New Map/NewButton");
            mainMenu_NewHover_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - New Map/NewHoverButton");
            mainMenu_NewClick_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - New Map/NewClickButton");
        }

        // Returns an enum to GameProcessess when certani criteria are met
        // Otherwise, runs code for update for Map Editor Main Menu
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
            mainMenu_ExitRec = new Rectangle();     // FUNCTIONS AS BACK BUTTON TO MAIN MENU
            mainMenu_ExitRec.Width = 200;           // FUNCTIONS AS BACK BUTTON TO MAIN MENU
            mainMenu_ExitRec.Height = 88;           // FUNCTIONS AS BACK BUTTON TO MAIN MENU
            mainMenu_ExitRec.X = 475;               // FUNCTIONS AS BACK BUTTON TO MAIN MENU
            mainMenu_ExitRec.Y = 750;               // FUNCTIONS AS BACK BUTTON TO MAIN MENU

            // Create Load Map Button
            mainMenu_LoadRec = new Rectangle();
            mainMenu_LoadRec.Width = 200;
            mainMenu_LoadRec.Height = 88;
            mainMenu_LoadRec.X = 475;
            mainMenu_LoadRec.Y = 652;

            // Create New Map Button
            mainMenu_NewRec = new Rectangle();
            mainMenu_NewRec.Width = 200;
            mainMenu_NewRec.Height = 88;
            mainMenu_NewRec.X = 475;
            mainMenu_NewRec.Y = 554;

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
                    #region Initialize Arrays
                    // Initilize 2D Rectangle Array
                    // Used to detect mouse "paint brush" collision information
                    tiles = new Rectangle[20, 20];
                    for (int i = 0; i < 20; i++)
                    {
                        for (int x = 0; x < 20; x++)
                        {
                            Rectangle newTile = new Rectangle();
                            newTile.Width = 45;
                            newTile.Height = 45;
                            newTile.X = (45 * x) + 10;
                            newTile.Y = (45 * i) + 10;
                            tiles[i, x] = newTile;
                        }
                    }

                    // Initialize 2D int array
                    // Used to establish textures assigned in tiles 2D Rectangle matrix above
                    textures = new int[20, 20];
                    for (int i = 0; i < 20; i++)
                    {
                        for (int x = 0; x < 20; x++)
                        {
                            textures[i, x] = 0;
                            // IFORMATION FOR TEXTURES 2D ARRAY:
                            // 0 = No texture - Buildable location;
                            // 1 = Gray;
                            // 2 = Blue;
                            // 3 = Green;
                        }
                    }
                    #endregion

                    saveLoadBackground = 0;
                    prevMState = Mouse.GetState();
                    return GameProcesses.GameStateEnum.mapEdit_MapEditor;
                }
                // Load Map Button collision
                // Changes GameState to gs_LoadMenu
                if (mousePos.Intersects(mainMenu_LoadRec) && mState.LeftButton == ButtonState.Pressed)
                {
                    #region Initialize Arrays
                    // Initilize 2D Rectangle Array
                    // Used to detect mouse "paint brush" collision information
                    tiles = new Rectangle[20, 20];
                    for (int i = 0; i < 20; i++)
                    {
                        for (int x = 0; x < 20; x++)
                        {
                            Rectangle newTile = new Rectangle();
                            newTile.Width = 45;
                            newTile.Height = 45;
                            newTile.X = (45 * x) + 10;
                            newTile.Y = (45 * i) + 10;
                            tiles[i, x] = newTile;
                        }
                    }

                    // Initialize 2D int array
                    // Used to establish textures assigned in tiles 2D Rectangle matrix above
                    textures = new int[20, 20];
                    for (int i = 0; i < 20; i++)
                    {
                        for (int x = 0; x < 20; x++)
                        {
                            textures[i, x] = 0;
                            // IFORMATION FOR TEXTURES 2D ARRAY:
                            // 0 = No texture - Buildable location;
                            // 1 = Gray;
                            // 2 = Blue;
                            // 3 = Green;
                        }
                    }
                    #endregion
                    prevMState = Mouse.GetState();
                    return GameProcesses.GameStateEnum.mapEdit_LoadMenu;
                }
                // Exit Map Button collision
                if (mousePos.Intersects(mainMenu_ExitRec) && mState.LeftButton == ButtonState.Pressed)
                {
                    prevMState = Mouse.GetState();
                    return GameProcesses.GameStateEnum.main_MainMenu;
                }
            }

            
            return GameProcesses.GameStateEnum.mapEdit_MainMenu;
            #endregion
        }

        // Draws objects for this GameState
        public override void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            #region Draw - Main Menu
            // Draws main buttons: New, Load, Exit
            spriteBatch.Draw(bg_MapEditMainMenu, mapEdit_MainBackground, Color.White);
            spriteBatch.Draw(mapEdit_Back_Txtr, mainMenu_ExitRec, Color.White);
            spriteBatch.Draw(mainMenu_New_Txtr, mainMenu_NewRec, Color.White);
            spriteBatch.Draw(mainMenu_Load_Txtr, mainMenu_LoadRec, Color.White);

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

            // Back Button Hover and Click
            if (mousePos.Intersects(mainMenu_ExitRec))
            {
                spriteBatch.Draw(mapEdit_BackHover_Txtr, mainMenu_ExitRec, Color.White);
                if (mState.LeftButton == ButtonState.Pressed)
                {
                    spriteBatch.Draw(mapEdit_BackClick_Txtr, mainMenu_ExitRec, Color.White);
                }
            }
            #endregion
        }
    }
}
