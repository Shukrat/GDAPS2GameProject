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
            eraser_Tile = Content.Load<Texture2D>("Tiles/Eraser");
            pathDL_Tile = Content.Load<Texture2D>("Tiles/Tiles - Path Art/pathcornerDL");
            pathDR_Tile = Content.Load<Texture2D>("Tiles/Tiles - Path Art/pathcornerDR");
            pathUL_Tile = Content.Load<Texture2D>("Tiles/Tiles - Path Art/pathcornerUL");
            pathUR_Tile = Content.Load<Texture2D>("Tiles/Tiles - Path Art/pathcornerUR");
            pathLeftRight_Tile = Content.Load<Texture2D>("Tiles/Tiles - Path Art/pathleftright");
            pathUpDown_Tile = Content.Load<Texture2D>("Tiles/Tiles - Path Art/pathupdown");


            // MAP EDITOR / INTERFACE
            mapEdit_SideBar_Txtr = Content.Load<Texture2D>("Interface/Interface - Noninteractive/Sidebar");
            mapEdit_Save_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Save/SaveButton");
            mapEdit_SaveHover_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Save/SaveHoverButton");
            mapEdit_SaveClick_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Save/SaveClickButton");
            mapEdit_BackSelect_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Backdrop Select/BGButton");
            mapEdit_BackSelectHover_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Backdrop Select/BGHoverButton");
            mapEdit_BackSelectClick_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Backdrop Select/BGClickButton");
            mapEdit_PathSelect_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Path Select/PathsButton");
            mapEdit_PathSelectHover_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Path Select/PathsHoverButton");
            mapEdit_PathSelectClick_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Path Select/PathsClickButton");
            mainMenu_Exit_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Exit/exitButton");
            mainMenu_ExitHover_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Exit/exitButtonHover");
            mainMenu_ExitClick_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Exit/exitButtonClick");
            mapEdit_Menu_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Menu/MenuButton");
            mapEdit_MenuHover_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Menu/MenuHoverButton");
            mapEdit_MenuClick_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Menu/MenuClickButton");
            mapEdit_Tiles_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Tiles/TilesButton");
            mapEdit_TilesHover_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Tiles/TilesHoverButton");
            mapEdit_TilesClick_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Tiles/TilesClickButton");

            // GAME / GAME BORDER
            game_GameBorder_Txtr = Content.Load<Texture2D>("Interface/Interface - Noninteractive/GameFrame");

            // OBJECTS
            obj_Boulder = Content.Load<Texture2D>("Tiles/Tiles - Object Art/Collidables/Boulder");
            obj_Tree = Content.Load<Texture2D>("Tiles/Tiles - Object Art/Collidables/Tree");

            // SPAWN / GOAL
            spawn = Content.Load<Texture2D>("Tiles/Tiles - Object Art/SpawnGoal/Portal");
            goal = Content.Load<Texture2D>("Tiles/Tiles - Object Art/SpawnGoal/Gate");

            // BACKGROUNDS
            bg_Desert = Content.Load<Texture2D>("Tiles/Tiles - Backsplashes/Desert");
            bg_Grasslands = Content.Load<Texture2D>("Tiles/Tiles - Backsplashes/Grasslands");
            bg_Tundra = Content.Load<Texture2D>("Tiles/Tiles - Backsplashes/Tundra");
        }

        // Returns an enum to GameProcessess when certani criteria are met
        // Otherwise, runs code for update for Map Editor
        public override GameProcesses.GameStateEnum Update(GraphicsDeviceManager graphics, Game1 game1)
        {
            #region Update - Mouse Location
            // Update mouse state location - assign details to mouse rectangle
            MouseStateGet();

            // Mouse position rectangle
            mousePos = new Rectangle();
            mousePos.Width = 1;
            mousePos.Height = 1;
            mousePos.X = mState.X;
            mousePos.Y = mState.Y;
            #endregion

            // Tile Selection Display Rectangle
            paintBrush = new Rectangle();
            paintBrush.Width = 45;
            paintBrush.Height = 45;
            paintBrush.X = graphics.PreferredBackBufferWidth - 65;
            paintBrush.Y = 610;

            #region Interface - Noninteractable
            // Game Border
            game_GameBorderRec = new Rectangle();
            game_GameBorderRec.Width = 1150;
            game_GameBorderRec.Height = 920;
            game_GameBorderRec.X = 0;
            game_GameBorderRec.Y = 0;

            // Sidebar background
            sideBarBG = new Rectangle();
            sideBarBG.Width = 230;
            sideBarBG.Height = 900;
            sideBarBG.X = graphics.PreferredBackBufferWidth - 240;
            sideBarBG.Y = 10;

            // Background
            game_BackgroundRec = new Rectangle();
            game_BackgroundRec.Width = 900;
            game_BackgroundRec.Height = 900;
            game_BackgroundRec.X = 10;
            game_BackgroundRec.Y = 10;
            #endregion

            #region Buttons
            // Menu Button
            mapEdit_MenuRec = new Rectangle();
            mapEdit_MenuRec.Width = 100;
            mapEdit_MenuRec.Height = 75;
            mapEdit_MenuRec.X = graphics.PreferredBackBufferWidth - 120;
            mapEdit_MenuRec.Y = 665;

            // Tiles Button
            mapEdit_TilesRec = new Rectangle();
            mapEdit_TilesRec.Width = 100;
            mapEdit_TilesRec.Height = 75;
            mapEdit_TilesRec.X = graphics.PreferredBackBufferWidth - 230;
            mapEdit_TilesRec.Y = 665;

            // Exit Button
            mapEdit_ExitRec = new Rectangle();
            mapEdit_ExitRec.Width = 100;
            mapEdit_ExitRec.Height = 75;
            mapEdit_ExitRec.X = graphics.PreferredBackBufferWidth - 120;
            mapEdit_ExitRec.Y = 835;

            // Save Button
            mapEdit_SaveRec = new Rectangle();
            mapEdit_SaveRec.Width = 100;
            mapEdit_SaveRec.Height = 75;
            mapEdit_SaveRec.X = graphics.PreferredBackBufferWidth - 120;
            mapEdit_SaveRec.Y = 750;

            // Background Selection Button
            mapEdit_BackSelectRec = new Rectangle();
            mapEdit_BackSelectRec.Width = 100;
            mapEdit_BackSelectRec.Height = 75;
            mapEdit_BackSelectRec.X = graphics.PreferredBackBufferWidth - 230;
            mapEdit_BackSelectRec.Y = 835;

            // Path Selection Button
            mapEdit_PathTilesRec = new Rectangle();
            mapEdit_PathTilesRec.Width = 100;
            mapEdit_PathTilesRec.Height = 75;
            mapEdit_PathTilesRec.X = graphics.PreferredBackBufferWidth - 230;
            mapEdit_PathTilesRec.Y = 750;
            #endregion

            #region Tile Selection
            // PATH TILE SELECTION OPTIONS RECTANGLES
            // Eraser
            select_Eraser = new Rectangle();
            select_Eraser.Width = 45;
            select_Eraser.Height = 45;
            select_Eraser.X = graphics.PreferredBackBufferWidth - 230;
            select_Eraser.Y = 610;

            // From Left to Bottom
            select_PathDL = new Rectangle();
            select_PathDL.Width = 45;
            select_PathDL.Height = 45;
            select_PathDL.X = graphics.PreferredBackBufferWidth - 230;
            select_PathDL.Y = 20;
            // From Right to Bottom
            select_PathDR = new Rectangle();
            select_PathDR.Width = 45;
            select_PathDR.Height = 45;
            select_PathDR.X = graphics.PreferredBackBufferWidth - 175;
            select_PathDR.Y = 20;
            // From Left to Top
            select_PathUL = new Rectangle();
            select_PathUL.Width = 45;
            select_PathUL.Height = 45;
            select_PathUL.X = graphics.PreferredBackBufferWidth - 120;
            select_PathUL.Y = 20;
            // From Right to Top
            select_PathUR = new Rectangle();
            select_PathUR.Width = 45;
            select_PathUR.Height = 45;
            select_PathUR.X = graphics.PreferredBackBufferWidth - 65;
            select_PathUR.Y = 20;
            // From Left to Right
            select_PathLeftRight = new Rectangle();
            select_PathLeftRight.Width = 45;
            select_PathLeftRight.Height = 45;
            select_PathLeftRight.X = graphics.PreferredBackBufferWidth - 230;
            select_PathLeftRight.Y = 75;
            // From Up to Down
            select_PathUpDown = new Rectangle();
            select_PathUpDown.Width = 45;
            select_PathUpDown.Height = 45;
            select_PathUpDown.X = graphics.PreferredBackBufferWidth - 175;
            select_PathUpDown.Y = 75;

            #endregion

            #region Mouse/Keyboard Tile Paint Selection Update
            // Check Mouse for new tile selection
            MouseTileSelect();
            #endregion

            #region Button Functionality - Save and Exit
            // Detects mouse rectangle and button collisions
            if (prevMState.LeftButton != ButtonState.Pressed)
            {
                if (mousePos.Intersects(mapEdit_SaveRec) && mState.LeftButton == ButtonState.Pressed)
                {
                    // Sets which "window" is currently "open," or being updated in Update method
                    // And drawn in Draw method
                    GameProcesses.saveLoad.Textures = textures;
                    prevMState = Mouse.GetState();
                    return GameProcesses.GameStateEnum.mapEdit_SaveMenu;
                }
                if (mousePos.Intersects(mapEdit_MenuRec) && mState.LeftButton == ButtonState.Pressed)
                {
                    // Sets which "window" is currently "open," or being updated in Update method
                    // And drawn in Draw method
                    prevMState = Mouse.GetState();
                    return GameProcesses.GameStateEnum.mapEdit_MainMenu;
                }
                if (mousePos.Intersects(mapEdit_ExitRec) && mState.LeftButton == ButtonState.Pressed)
                {
                    game1.Exit();
                }
            }


            return GameProcesses.GameStateEnum.mapEdit_MapEditor;
            #endregion
        }

        // Draws objects for this GameState
        public override void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            #region Background Switch
            switch (saveLoadBackground)
            {
                case 1:
                    { spriteBatch.Draw(bg_Grasslands, game_BackgroundRec, Color.Gray); break; }
                case 2:
                    { spriteBatch.Draw(bg_Desert, game_BackgroundRec, Color.Gray); break; }
                case 3:
                    { spriteBatch.Draw(bg_Tundra, game_BackgroundRec, Color.LightGray); break; }
                default:
                    break;
            }
            #endregion

            #region Draw Map Editor Buttons
            // Draws all textures for sidebar, tile selection options, and buttons

            spriteBatch.Draw(mapEdit_SideBar_Txtr, sideBarBG, Color.White);                 // Sidebar background
            spriteBatch.Draw(mapEdit_Save_Txtr, mapEdit_SaveRec, Color.White);              // Save Map Button
            spriteBatch.Draw(mainMenu_Exit_Txtr, mapEdit_ExitRec, Color.White);             // Exit Map Button
            spriteBatch.Draw(mapEdit_PathSelect_Txtr, mapEdit_PathTilesRec, Color.White);   // Path Tiles Selection Button
            spriteBatch.Draw(mapEdit_BackSelect_Txtr, mapEdit_BackSelectRec, Color.White);  // Background Selection Button
            spriteBatch.Draw(mapEdit_Menu_Txtr, mapEdit_MenuRec, Color.White);
            spriteBatch.Draw(mapEdit_Tiles_Txtr, mapEdit_TilesRec, Color.White);
            spriteBatch.Draw(game_GameBorder_Txtr, game_GameBorderRec, Color.White);
            spriteBatch.Draw(eraser_Tile, select_Eraser, Color.White);

            // Menu Button Hover and Click
            if (mousePos.Intersects(mapEdit_MenuRec))
            {
                spriteBatch.Draw(mapEdit_MenuHover_Txtr, mapEdit_MenuRec, Color.White);
                if (mState.LeftButton == ButtonState.Pressed)
                {
                    spriteBatch.Draw(mapEdit_MenuClick_Txtr, mapEdit_MenuRec, Color.White);
                }
            }

            // Tiles Button Hover and Click
            if (mousePos.Intersects(mapEdit_TilesRec))
            {
                spriteBatch.Draw(mapEdit_TilesHover_Txtr, mapEdit_TilesRec, Color.White);
                if (mState.LeftButton == ButtonState.Pressed)
                {
                    spriteBatch.Draw(mapEdit_TilesClick_Txtr, mapEdit_TilesRec, Color.White);
                }
            }

            // Exit Button Hover and Click
            if (mousePos.Intersects(mapEdit_ExitRec))
            {
                spriteBatch.Draw(mainMenu_ExitHover_Txtr, mapEdit_ExitRec, Color.White);
                if (mState.LeftButton == ButtonState.Pressed)
                {
                    spriteBatch.Draw(mainMenu_ExitClick_Txtr, mapEdit_ExitRec, Color.White);
                }
            }

            // Save Button Hover and Click
            if (mousePos.Intersects(mapEdit_SaveRec))
            {
                spriteBatch.Draw(mapEdit_SaveHover_Txtr, mapEdit_SaveRec, Color.White);
                if (mState.LeftButton == ButtonState.Pressed)
                {
                    spriteBatch.Draw(mapEdit_SaveClick_Txtr, mapEdit_SaveRec, Color.White);
                }
            }

            // Path Button Hover and Click
            if (mousePos.Intersects(mapEdit_PathTilesRec))
            {
                spriteBatch.Draw(mapEdit_PathSelectHover_Txtr, mapEdit_PathTilesRec, Color.White);
                if (mState.LeftButton == ButtonState.Pressed)
                {
                    spriteBatch.Draw(mapEdit_PathSelectClick_Txtr, mapEdit_PathTilesRec, Color.White);
                }
            }

            // Background Button Hover and Click
            if (mousePos.Intersects(mapEdit_BackSelectRec))
            {
                spriteBatch.Draw(mapEdit_BackSelectHover_Txtr, mapEdit_BackSelectRec, Color.White);
                if (mState.LeftButton == ButtonState.Pressed)
                {
                    spriteBatch.Draw(mapEdit_BackSelectClick_Txtr, mapEdit_BackSelectRec, Color.White);
                }
            }

            #endregion

            #region Paintbrush Switch
            if (tf_Eraser) { spriteBatch.Draw(eraser_Tile, paintBrush, Color.White); }
            if (tf_PathDL) { spriteBatch.Draw(pathDL_Tile, paintBrush, Color.White); }
            if (tf_PathDR) { spriteBatch.Draw(pathDR_Tile, paintBrush, Color.White); }
            if (tf_PathUL) { spriteBatch.Draw(pathUL_Tile, paintBrush, Color.White); }
            if (tf_PathUR) { spriteBatch.Draw(pathUR_Tile, paintBrush, Color.White); }
            if (tf_PathLeftRight) { spriteBatch.Draw(pathLeftRight_Tile, paintBrush, Color.White); }
            if (tf_PathUpDown) { spriteBatch.Draw(pathUpDown_Tile, paintBrush, Color.White); }
            if (tf_Bolder) { spriteBatch.Draw(obj_Boulder, paintBrush, Color.White); }
            if (tf_Tree) { spriteBatch.Draw(obj_Tree, paintBrush, Color.White); }
            if (tf_Spawn) { spriteBatch.Draw(spawn, paintBrush, Color.White); }
            if (tf_Goal) { spriteBatch.Draw(goal, paintBrush, Color.White); }
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
                        if (tf_Eraser) { textures[x, y] = 0; }
                        if (tf_PathDL) { textures[x, y] = 1; }
                        if (tf_PathDR) { textures[x, y] = 2; }
                        if (tf_PathUL) { textures[x, y] = 3; }
                        if (tf_PathUR) { textures[x, y] = 4; }
                        if (tf_PathLeftRight) { textures[x, y] = 5; }
                        if (tf_PathUpDown) { textures[x, y] = 6; }
                        if (tf_Bolder) { textures[x, y] = 7; }
                        if (tf_Tree) { textures[x, y] = 8; }
                        if (tf_Spawn) { textures[x, y] = 9; }
                        if (tf_Goal) { textures[x, y] = 10; }
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
                            spriteBatch.Draw(pathDL_Tile, tiles[x, y], Color.White);
                            break;
                        case 2:
                            spriteBatch.Draw(pathDR_Tile, tiles[x, y], Color.White);
                            break;
                        case 3:
                            spriteBatch.Draw(pathUL_Tile, tiles[x, y], Color.White);
                            break;
                        case 4:
                            spriteBatch.Draw(pathUR_Tile, tiles[x, y], Color.White);
                            break;
                        case 5:
                            spriteBatch.Draw(pathLeftRight_Tile, tiles[x, y], Color.White);
                            break;
                        case 6:
                            spriteBatch.Draw(pathUpDown_Tile, tiles[x, y], Color.White);
                            break;
                        case 7:
                            spriteBatch.Draw(obj_Boulder, tiles[x, y], Color.White);
                            break;
                        case 8:
                            spriteBatch.Draw(obj_Tree, tiles[x, y], Color.White);
                            break;
                        case 9:
                            spriteBatch.Draw(spawn, tiles[x, y], Color.White);
                            break;
                        case 10:
                            spriteBatch.Draw(goal, tiles[x, y], Color.White);
                            break;
                        default:
                            break;
                    }
                }
            }
            #endregion

            #region Draw Tile Selection Menu
            if (tf_PathSelect)
            {
                spriteBatch.Draw(pathDL_Tile, select_PathDL, Color.White);
                spriteBatch.Draw(pathDR_Tile, select_PathDR, Color.White);
                spriteBatch.Draw(pathUL_Tile, select_PathUL, Color.White);
                spriteBatch.Draw(pathUR_Tile, select_PathUR, Color.White);
                spriteBatch.Draw(pathLeftRight_Tile, select_PathLeftRight, Color.White);
                spriteBatch.Draw(pathUpDown_Tile, select_PathUpDown, Color.White);
            }

            if (tf_BackgroundSelect)
            {
                spriteBatch.Draw(bg_Grasslands, select_PathDL, Color.LightGray);
                spriteBatch.Draw(bg_Desert, select_PathDR, Color.White);
                spriteBatch.Draw(bg_Tundra, select_PathUL, Color.White);
            }

            if (!tf_BackgroundSelect && !tf_PathSelect)
            {
                spriteBatch.Draw(obj_Boulder, select_PathDL, Color.White);
                spriteBatch.Draw(obj_Tree, select_PathDR, Color.White);
                spriteBatch.Draw(spawn, select_PathUL, Color.White);
                spriteBatch.Draw(goal, select_PathUR, Color.White);
            }
            #endregion
        }
    }
}
