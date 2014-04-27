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
    class GS_MM_MapEditor : GameState
    {
        // Load content needed for this GameState
        public override void LoadContent(ContentManager Content)
        {
            // TILES:
            grayTile = Content.Load<Texture2D>("TEST TILES - OBSOLETE/Gray Tile");
            blueTile = Content.Load<Texture2D>("TEST TILES - OBSOLETE/Blue Tile");
            greenTile = Content.Load<Texture2D>("TEST TILES - OBSOLETE/Green Tile");
            pathTile = Content.Load<Texture2D>("TEST TILES - OBSOLETE/path");

            // MAP EDITOR / INTERFACE
            mapEdit_SideBar_Txtr = Content.Load<Texture2D>("Interface/Interface - Noninteractive/Sidebar");
            mapEdit_Save_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/Map Editor Menu/Button - Save/saveButton");
            mapEdit_Exit_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/Map Editor Menu/Button - Exit/mapEditExitButton");
        }

        // Returns an enum to GameProcessess when certani criteria are met
        // Otherwise, runs code for update for Map Editor
        public override GameProcesses.GameStateEnum Update(GraphicsDeviceManager graphics, Game1 game1)
        {
            #region Update - Mouse Location
            // Update mouse state location - assign details to mouse rectangle
            mState = Mouse.GetState();
            mousePos.X = mState.X;
            mousePos.Y = mState.Y;
            #endregion

            #region Interface
            // Setup interface 
            // Creates sidebar, tiles selection icons
            // and buttons.
            // ONLY DRAWS INTERFACE ONCE
            if (!interfaceMade)
            {
                // Mouse position rectangle
                mousePos = new Rectangle();
                mousePos.Width = 1;
                mousePos.Height = 1;
                mousePos.X = 0;
                mousePos.Y = 0;

                // Sidebar background
                sideBarBG = new Rectangle();
                sideBarBG.Width = 230;
                sideBarBG.Height = 900;
                sideBarBG.X = graphics.PreferredBackBufferWidth - 230;
                sideBarBG.Y = 0;

                // Exit Button
                mapEdit_ExitRec = new Rectangle();
                mapEdit_ExitRec.Width = 100;
                mapEdit_ExitRec.Height = 75;
                mapEdit_ExitRec.X = graphics.PreferredBackBufferWidth - 110;
                mapEdit_ExitRec.Y = 805;

                // Save Button
                mapEdit_SaveRec = new Rectangle();
                mapEdit_SaveRec.Width = 100;
                mapEdit_SaveRec.Height = 75;
                mapEdit_SaveRec.X = graphics.PreferredBackBufferWidth - 220;
                mapEdit_SaveRec.Y = 805;

                // Tile Selection Display Rectangle
                paintBrush = new Rectangle();
                paintBrush.Width = 50;
                paintBrush.Height = 50;
                paintBrush.X = mState.X;
                paintBrush.Y = mState.Y;

                // TILE SELECTION OPTIONS RECTANGLES
                // Gray
                selectGray = new Rectangle();
                selectGray.Width = 45;
                selectGray.Height = 45;
                selectGray.X = graphics.PreferredBackBufferWidth - 220;
                selectGray.Y = 10;
                // Blue
                selectBlue = new Rectangle();
                selectBlue.Width = 45;
                selectBlue.Height = 45;
                selectBlue.X = graphics.PreferredBackBufferWidth - 165;
                selectBlue.Y = 10;
                // Green
                selectGreen = new Rectangle();
                selectGreen.Width = 45;
                selectGreen.Height = 45;
                selectGreen.X = graphics.PreferredBackBufferWidth - 110;
                selectGreen.Y = 10;
                //Path
                selectPath = new Rectangle();
                selectPath.Width = 45;
                selectPath.Height = 45;
                selectPath.X = graphics.PreferredBackBufferWidth - 55;
                selectPath.Y = 10;


                // End of setup, set interfaceMade to true
                interfaceMade = true;
            }
            #endregion

            #region Mouse/Keyboard Tile Paint Selection Update
            // Check Mouse for new tile selection
            MouseTileSelect();
            #endregion

            #region Button Functionality - Save and Exit
            // Detects mouse rectangle and button collisions
            if (mousePos.Intersects(mapEdit_SaveRec) && mState.LeftButton == ButtonState.Pressed)
            {
                // Sets which "window" is currently "open," or being updated in Update method
                // And drawn in Draw method
                GameProcesses.saveLoad.Textures = textures;
                return GameProcesses.GameStateEnum.mapEdit_SaveMenu;
            }
            if (mousePos.Intersects(mapEdit_ExitRec) && mState.LeftButton == ButtonState.Pressed)
            {
                game1.Exit();
                
            }

            return GameProcesses.GameStateEnum.mapEdit_MapEditor;
            #endregion
        }

        // Draws objects for this GameState
        public override void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            #region Draw Interface
            // Draws all textures for sidebar, tile selection options, and buttons
            spriteBatch.Draw(mapEdit_SideBar_Txtr, sideBarBG, Color.White); // Sidebar background
            spriteBatch.Draw(grayTile, selectGray, Color.White); // 1 - Gray Tile
            spriteBatch.Draw(blueTile, selectBlue, Color.White); // 2 - Blue Tile
            spriteBatch.Draw(greenTile, selectGreen, Color.White); // 3 - Green Tile
            spriteBatch.Draw(pathTile, selectPath, Color.White); // 4 - Path Tile
            spriteBatch.Draw(mapEdit_Save_Txtr, mapEdit_SaveRec, Color.White); // Save Map Button
            spriteBatch.Draw(mapEdit_Exit_Txtr, mapEdit_ExitRec, Color.White); // Exit Map Button
            #endregion
            
            #region Rectangle Texture Assignment Detection
            // Draw rectangle matrix
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    // If mouse button is pressed, and the mouse position is in the rectangle 2x2 array:
                    // Draw the current tile selected
                    // Assigns textures 2D array the int associated with currently selected tile
                    // THIS DOES NOT DRAW THE TEXTURES PERMANENTLY - DRAW TEXTURES ON RECTANGLES BELOW DOES
                    if (mousePos.Intersects(tiles[x, y]) && mState.LeftButton == ButtonState.Pressed)
                    {
                        if (tfGrayTile)
                        {
                            textures[x, y] = 1;
                        }
                        if (tfBlueTile)
                        {
                            textures[x, y] = 2;
                        }

                        if (tfGreenTile)
                        {
                            textures[x, y] = 3;
                        }
                        if (tfPathTile)
                        {
                            textures[x, y] = 4;
                        }
                    }
                }
            }
            #endregion
            
            #region Draw Textures on Rectangles
            // Draw textures on matrix if ints in textures array are specific numbers
            // THIS PERMANENTLY SHOWS TEXTURE'S ASSIGNED ON SCREEN
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    // At location x, y in int 2D array, draw texture according to int
                    // IFORMATION FOR TEXTURES 2D ARRAY:
                    // 0 = No texture;
                    // 1 = Gray;
                    // 2 = Blue;
                    // 3 = Green;
                    switch (textures[x, y])
                    {
                        case 0:
                            break;
                        case 1:
                            spriteBatch.Draw(grayTile, tiles[x, y], Color.White);
                            break;
                        case 2:
                            spriteBatch.Draw(blueTile, tiles[x, y], Color.White);
                            break;
                        case 3:
                            spriteBatch.Draw(greenTile, tiles[x, y], Color.White);
                            break;
                        case 4:
                            spriteBatch.Draw(pathTile, tiles[x, y], Color.White);
                            break;
                        default:
                            break;
                    }
                }
            }
            #endregion
        }
    }
}
