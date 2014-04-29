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
    class GS_Game_LoadMap : GameState
    {
        public override void LoadContent(ContentManager Content)
        {
            #region Load Interface Content
            // LOAD INTERFACE CONTENT
            // Buttons
            mapEdit_SideBar_Txtr = Content.Load<Texture2D>("Interface/Interface - Noninteractive/Sidebar");
            mainMenu_Exit_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Exit/exitButton");
            mainMenu_ExitHover_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Exit/exitButtonHover");
            mainMenu_ExitClick_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Exit/exitButtonClick");
            mapEdit_Menu_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Menu/MenuButton");
            mapEdit_MenuHover_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Menu/MenuHoverButton");
            mapEdit_MenuClick_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Menu/MenuClickButton");
            // Towers
            twr_Catapult_Txtr = Content.Load<Texture2D>("Tiles/Tiles - Tower Art/catapult");
            twr_Trebuchet_Txtr = Content.Load<Texture2D>("Tiles/Tiles - Tower Art/Tower2");
            // Spells
            spell_Fireball_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/Game - In Game/Buttons/Spells/Fireball");
            spell_Heal_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/Game - In Game/Buttons/Spells/Heal");
            spell_Rage_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/Game - In Game/Buttons/Spells/Rage");
            spell_Slow_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/Game - In Game/Buttons/Spells/Slow");
            // Healthbar
            game_HealthBar_Txtr = Content.Load<Texture2D>("Interface/Interface - Noninteractive/HealthBarFrame");
            // Speed controls
            game_Play_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/Game - In Game/Buttons/Speed Control/Play");
            game_Pause_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/Game - In Game/Buttons/Speed Control/Pause");
            game_FF_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/Game - In Game/Buttons/Speed Control/FF");
            game_FFF_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/Game - In Game/Buttons/Speed Control/FFF");
            #endregion

            #region Load Tile Content
            // LOAD TILE CONTENT
            // Paths
            pathDL_Tile = Content.Load<Texture2D>("Tiles/Tiles - Path Art/pathcornerDL");
            pathDR_Tile = Content.Load<Texture2D>("Tiles/Tiles - Path Art/pathcornerDR");
            pathUL_Tile = Content.Load<Texture2D>("Tiles/Tiles - Path Art/pathcornerUL");
            pathUR_Tile = Content.Load<Texture2D>("Tiles/Tiles - Path Art/pathcornerUR");
            pathLeftRight_Tile = Content.Load<Texture2D>("Tiles/Tiles - Path Art/pathleftright");
            pathUpDown_Tile = Content.Load<Texture2D>("Tiles/Tiles - Path Art/pathupdown");
            // Objects
            obj_Boulder = Content.Load<Texture2D>("Tiles/Tiles - Object Art/Collidables/Boulder");
            obj_Tree = Content.Load<Texture2D>("Tiles/Tiles - Object Art/Collidables/Tree");
            obj_Monster = Content.Load<Texture2D>("Tiles/Tiles - Object Art/Monsters/Monster");
            // Spawn/Goal
            spawn = Content.Load<Texture2D>("Tiles/Tiles - Object Art/SpawnGoal/Portal");
            goal = Content.Load<Texture2D>("Tiles/Tiles - Object Art/SpawnGoal/Gate");

            // BACKGROUNDS
            bg_Desert = Content.Load<Texture2D>("Tiles/Tiles - Backsplashes/Desert");
            bg_Grasslands = Content.Load<Texture2D>("Tiles/Tiles - Backsplashes/Grasslands");
            bg_Tundra = Content.Load<Texture2D>("Tiles/Tiles - Backsplashes/Tundra");

            // GAME / GAME BORDER
            game_GameBorder_Txtr = Content.Load<Texture2D>("Interface/Interface - Noninteractive/GameFrame");
            #endregion

        }

        public override GameProcesses.GameStateEnum Update(GraphicsDeviceManager graphics, Game1 game1)
        {
            // POSSIBLY UNNEEDED - BUTTONS AND INTERFACE STILL NEED EDITING 4/28
            #region Mouse/Keyboard Tile Paint Selection Update
            // Check Mouse for new tile selection
            MouseTileSelect();
            #endregion

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

            // BUTTONS
            #region Button Rectangles
            // Menu Button
            mapEdit_MenuRec = new Rectangle();
            mapEdit_MenuRec.Width = 100;
            mapEdit_MenuRec.Height = 75;
            mapEdit_MenuRec.X = graphics.PreferredBackBufferWidth - 230;
            mapEdit_MenuRec.Y = 835;

            // Exit Button
            mapEdit_ExitRec = new Rectangle();
            mapEdit_ExitRec.Width = 100;
            mapEdit_ExitRec.Height = 75;
            mapEdit_ExitRec.X = graphics.PreferredBackBufferWidth - 120;
            mapEdit_ExitRec.Y = 835;
            #endregion

            // HEALTH / MANA BARS
            #region Health / Mana Bar Rectangles
            // HEALTH BAR
            game_HealthBarRec = new Rectangle();
            game_HealthBarRec.Width = 215;
            game_HealthBarRec.Height = 46;
            game_HealthBarRec.X = graphics.PreferredBackBufferWidth - 225;
            game_HealthBarRec.Y = 20;

            // MANA BAR
            game_ManaBarRec = new Rectangle();
            game_ManaBarRec.Width = 215;
            game_ManaBarRec.Height = 46;
            game_ManaBarRec.X = graphics.PreferredBackBufferWidth - 225;
            game_ManaBarRec.Y = 86;
            #endregion

            // SPELLS
            #region Spell Rectangles
            // Fireball
            spell_FireballRec = new Rectangle();
            spell_FireballRec.Width = 45;
            spell_FireballRec.Height = 45;
            spell_FireballRec.X = graphics.PreferredBackBufferWidth - 230;
            spell_FireballRec.Y = 147;

            // Heal
            spell_HealRec = new Rectangle();
            spell_HealRec.Width = 45;
            spell_HealRec.Height = 45;
            spell_HealRec.X = graphics.PreferredBackBufferWidth - 175;
            spell_HealRec.Y = 147;

            // Rage
            spell_RageRec = new Rectangle();
            spell_RageRec.Width = 45;
            spell_RageRec.Height = 45;
            spell_RageRec.X = graphics.PreferredBackBufferWidth - 120;
            spell_RageRec.Y = 147;

            // Slow
            spell_SlowRec = new Rectangle();
            spell_SlowRec.Width = 45;
            spell_SlowRec.Height = 45;
            spell_SlowRec.X = graphics.PreferredBackBufferWidth - 65;
            spell_SlowRec.Y = 147;
            #endregion

            // TOWERS
            #region Tower Rectangles
            // Catapult
            twr_CatapultRec = new Rectangle();
            twr_CatapultRec.Width = 45;
            twr_CatapultRec.Height = 45;
            twr_CatapultRec.X = graphics.PreferredBackBufferWidth - 230;
            twr_CatapultRec.Y = 222;

            // Trebuchet
            twr_TrebuchetRec = new Rectangle();
            twr_TrebuchetRec.Width = 45;
            twr_TrebuchetRec.Height = 45;
            twr_TrebuchetRec.X = graphics.PreferredBackBufferWidth - 175;
            twr_TrebuchetRec.Y = 222;
            #endregion

            // PLAY / PAUSE / FF / FFF
            #region Game Control Rectangles
            // Play
            game_PlayRec = new Rectangle();
            game_PlayRec.Width = 45;
            game_PlayRec.Height = 45;
            game_PlayRec.X = graphics.PreferredBackBufferWidth - 175;
            game_PlayRec.Y = 775;

            // Pause
            game_PauseRec = new Rectangle();
            game_PauseRec.Width = 45;
            game_PauseRec.Height = 45;
            game_PauseRec.X = graphics.PreferredBackBufferWidth - 230;
            game_PauseRec.Y = 775;

            // Fast Forward
            game_FFRec = new Rectangle();
            game_FFRec.Width = 45;
            game_FFRec.Height = 45;
            game_FFRec.X = graphics.PreferredBackBufferWidth - 120;
            game_FFRec.Y = 775;

            // 3x Fast Forward
            game_FFFRec = new Rectangle();
            game_FFFRec.Width = 45;
            game_FFFRec.Height = 45;
            game_FFFRec.X = graphics.PreferredBackBufferWidth - 65;
            game_FFFRec.Y = 775;
            #endregion

            #region Button Functionality - Save and Exit
            if (prevMState.LeftButton != ButtonState.Pressed)
            {
                if (mousePos.Intersects(mapEdit_MenuRec) && mState.LeftButton == ButtonState.Pressed)
                {
                    // Sets which "window" is currently "open," or being updated in Update method
                    // And drawn in Draw method
                    prevMState = Mouse.GetState();
                    return GameProcesses.GameStateEnum.main_MainMenu;
                }
                if (mousePos.Intersects(mapEdit_ExitRec) && mState.LeftButton == ButtonState.Pressed)
                {
                    game1.Exit();
                }
            }


            
            #endregion

            /// <summary>
            /// INSERT GAME CODE HERE
            /// ...
            /// </summary>

            for (int i = 0; i < GameVariables.Enemies.Count; i++)
            {
                foreach (Tower t in GameVariables.Towers)
                {
                    if (GameVariables.Enemies[i] != null)
                    {

                        if (t.HitBox.Intersects(GameVariables.Enemies[i].PieceShape))
                        {
                            t.AttackEnemy(GameVariables.Enemies[i]);
                        }
                        if (GameVariables.Enemies[i].Alive == false)
                        {
                            GameVariables.Enemies[i] = null;
                            t.shot = null;
                        }

                    }
                    if (GameVariables.Enemies[i] != null)
                    {
                        for (int x = 0; x < 20; x++)
                        {
                            for (int y = 0; y < 20; y++)
                            {
                                if (GameVariables.Enemies[i].PieceShape.Intersects(GameState.tiles[x, y]))
                                {
                                    if (GameState.textures[x, y] == 10)
                                    {
                                        GameVariables.Enemies[i].MoraleAttack();
                                        GameVariables.Enemies[i] = null;
                                    }
                                    else
                                    {
                                        GameVariables.Enemies[i].Move(x, y);
                                    }

                                }
                            }
                        }
                        if (GameVariables.Enemies[i].PieceShape.X > 800)
                        {
                            GameVariables.Enemies[i].MoraleAttack();
                            GameVariables.Enemies[i] = null;
                            t.shot = null;
                        }
                    }
                }
            }






            return GameProcesses.GameStateEnum.main_LoadMap;
        }

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
            spriteBatch.Draw(mainMenu_Exit_Txtr, mapEdit_ExitRec, Color.White);             // Exit Map Button
            spriteBatch.Draw(mapEdit_Menu_Txtr, mapEdit_MenuRec, Color.White);
            spriteBatch.Draw(game_GameBorder_Txtr, game_GameBorderRec, Color.White);

            // Game Speed Controls
            spriteBatch.Draw(game_Play_Txtr, game_PlayRec, Color.White);
            spriteBatch.Draw(game_Pause_Txtr, game_PauseRec, Color.White);
            spriteBatch.Draw(game_FF_Txtr, game_FFRec, Color.White);
            spriteBatch.Draw(game_FFF_Txtr, game_FFFRec, Color.White);

            // Spells
            spriteBatch.Draw(spell_Fireball_Txtr, spell_FireballRec, Color.White);
            spriteBatch.Draw(spell_Heal_Txtr, spell_HealRec, Color.White);
            spriteBatch.Draw(spell_Rage_Txtr, spell_RageRec, Color.White);
            spriteBatch.Draw(spell_Slow_Txtr, spell_SlowRec, Color.White);
            
            // Towers
            spriteBatch.Draw(twr_Catapult_Txtr, twr_CatapultRec, Color.White);
            spriteBatch.Draw(twr_Trebuchet_Txtr, twr_TrebuchetRec, Color.White);

            // Health / Mana Bars
            spriteBatch.Draw(game_HealthBar_Txtr, game_HealthBarRec, Color.White);
            spriteBatch.Draw(game_HealthBar_Txtr, game_ManaBarRec, Color.White);

            // Menu Button Hover and Click
            if (mousePos.Intersects(mapEdit_MenuRec))
            {
                spriteBatch.Draw(mapEdit_MenuHover_Txtr, mapEdit_MenuRec, Color.White);
                if (mState.LeftButton == ButtonState.Pressed)
                {
                    spriteBatch.Draw(mapEdit_MenuClick_Txtr, mapEdit_MenuRec, Color.White);
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


            /// <summary>
            /// INSERT GAME ANIMATION CODE HERE
            /// ...
            /// </summary>
            foreach (Tower t in GameVariables.Towers)
            {
                spriteBatch.Draw(twr_Catapult_Txtr, t.PieceShape, Color.White);
            }
            foreach (Enemy e in GameVariables.Enemies)
            {
                if (e != null)
                {
                    spriteBatch.Draw(obj_Monster, e.PieceShape, Color.White);
                }
            }
        }
    }
}
