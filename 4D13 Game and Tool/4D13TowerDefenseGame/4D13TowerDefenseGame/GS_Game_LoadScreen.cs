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
    class GS_Game_LoadScreen : GameState
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
        // Otherwise, runs code for update for Map Editor Load Menu
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
            // Set SaveLoad loadComplete bool to false in order to wait for user input
            // Set SaveLoad textures 2D array to this one
            // Send bool if on load screen or save screen
            // Call SaveLoad Update (user input)
            // Set this textures 2D array to SaveLoad's loaded 2D array
            GameProcesses.saveLoad.LoadComplete = false;
            GameProcesses.saveLoad.Textures = textures;
            GameProcesses.saveLoad.LoadMenu = true;
            GameProcesses.saveLoad.Update(kState, prevKState);
            textures = GameProcesses.saveLoad.Textures;
            saveLoadBackground = GameProcesses.saveLoad.saveLoadBackground;

            // Waits for user hits enter in SaveLoad.Update method
            // moves to Map Maker Update Bool in this Update method
            if (GameProcesses.saveLoad.LoadComplete)
            {
                #region Initalization
                // Sets the Health and mana
                GameVariables.Currency = 500;
                GameVariables.Morale = 100;        

                // Sets everything up the lists for use
                //GameVariables.Towers = null;
                //GameVariables.Enemies = null;
                GameVariables.Magic = new List<Spell>();
                //GameVariables.Markers = null;

                // set the list of enemies to exist
                GameVariables.Enemies = new List<Enemy>();
                GameVariables.Towers = new List<Tower>();
                GameVariables.Markers = new List<PathMarker>();

                // Spawn location
                GameVariables.SpawnLocationX = 0;
                GameVariables.SpawnLocationY = 0;
                #endregion

                #region Portal finding
                // Goes to find the portal
                for (int i = 0; i < 20; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        if (GameState.textures[i, j] == 9)
                        {
                            GameVariables.SpawnLocationX = GameState.tiles[i, j].X;
                            GameVariables.SpawnLocationY = GameState.tiles[i, j].Y;
                        }
                    }
                }
                #endregion

                #region Pathmarker adding
                // adds path markers on the path
                for (int i = 0; i < 20; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        switch (GameState.textures[i, j])
                        {
                            case 1:
                                {
                                    GameVariables.Markers.Add(new PathMarker(GameState.tiles[i, j].X, GameState.tiles[i, j].Y, 1));//3
                                    break;
                                }
                            case 2:
                                {
                                    GameVariables.Markers.Add(new PathMarker(GameState.tiles[i, j].X, GameState.tiles[i, j].Y, 2));//3
                                    break;
                                }
                            case 3:
                                {
                                    GameVariables.Markers.Add(new PathMarker(GameState.tiles[i, j].X, GameState.tiles[i, j].Y, 3));//4
                                    break;
                                }
                            case 4:
                                {
                                    GameVariables.Markers.Add(new PathMarker(GameState.tiles[i, j].X, GameState.tiles[i, j].Y, 4));//2
                                    break;
                                }
                            case 10:
                                {
                                    GameVariables.Markers.Add(new PathMarker(GameState.tiles[i, j].X, GameState.tiles[i, j].Y, 5));
                                    break;
                                }
                        }
                    }
                }
                #endregion

                #region Enemy spawning
                // spawn a list of 1000 enemies
                for (int i = 0; i < 1000; i++)
                {
                    GameVariables.Enemies.Add(new Enemy(10 * i, 20, GameVariables.SpawnLocationX, GameVariables.SpawnLocationY, 50, 50, "Monster", 1, (i/2), (i/4), false, false));
                }
                #endregion

                #region Draw tectures (X,Y)
                // Draw tectures at their XY cordiantes
                int xDim = 0;
                int yDim = 0;
                for (int i = 0; i < 20; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        if (GameState.textures[i, j] == 9)
                        {
                            xDim = GameState.tiles[i, j].X;
                            yDim = GameState.tiles[i, j].Y;
                        }
                    }
                }
                #endregion

                return GameProcesses.GameStateEnum.main_LoadMap;
            }

            if (prevMState.LeftButton != ButtonState.Pressed)
            {
                if (mousePos.Intersects(mainMenu_ExitRec) && mState.LeftButton == ButtonState.Pressed)
                {
                    prevMState = Mouse.GetState();
                    return GameProcesses.GameStateEnum.main_MainMenu;
                }
            }

            return GameProcesses.GameStateEnum.main_LoadScreen;

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
