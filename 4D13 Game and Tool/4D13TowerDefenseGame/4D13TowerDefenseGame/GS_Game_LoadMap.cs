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
        int enemySpawner;
        int frameCount;

        public override void LoadContent(ContentManager Content)
        {
            #region Load Interface Content
            // LOAD INTERFACE CONTENT
            // Font
            font = Content.Load<SpriteFont>("Fonts/mainFont");
            // Buttons
            mapEdit_SideBar_Txtr = Content.Load<Texture2D>("Interface/Interface - Noninteractive/Sidebar");
            mainMenu_Exit_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Exit/exitButton");
            mainMenu_ExitHover_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Exit/exitButtonHover");
            mainMenu_ExitClick_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Exit/exitButtonClick");
            mapEdit_Menu_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Menu/MenuButton");
            mapEdit_MenuHover_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Menu/MenuHoverButton");
            mapEdit_MenuClick_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/All Buttons/Button - Menu/MenuClickButton");
            // Towers
            twr_Catapult_Txtr = Content.Load<Texture2D>("Tiles/Tiles - Tower Art/Tower1");
            twr_Trebuchet_Txtr = Content.Load<Texture2D>("Tiles/Tiles - Tower Art/Tower2");

            // projectiles
            projectiles_Ball_Txtr = Content.Load<Texture2D>("Projectiles/Projectile");

            // Spells
            spell_Fireball_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/Game - In Game/Buttons/Spells/Fireball");
            spell_Heal_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/Game - In Game/Buttons/Spells/Heal");
            spell_Rage_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/Game - In Game/Buttons/Spells/Rage");
            spell_Slow_Txtr = Content.Load<Texture2D>("Interface/Interface - Interactive/Game - In Game/Buttons/Spells/Slow");
            // Healthbar
            game_HealthBar_Txtr = Content.Load<Texture2D>("Interface/Interface - Noninteractive/HealthBarFrame");
            game_Health_Txtr = Content.Load<Texture2D>("Interface/Interface - Noninteractive/Health Bar");

            // Currecny/Mana bar
            game_Currency_Txtr = Content.Load<Texture2D>("Interface/Interface - Noninteractive/Currency Bar");
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

            enemySpawner = 0;
            frameCount = -10;
            // Reset/set gold
            GameVariables.Currency = 1000;
            GameVariables.Morale = 100;
            GameVariables.Magic = new List<Spell>();
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
            #region Value Checks

            if (GameVariables.Currency <= 0)
            {
                GameVariables.Currency = 0;
            }

            if (GameVariables.Morale <= 0)
            {
                return GameProcesses.GameStateEnum.main_MainMenu;
            }
            #endregion

            #region enemy/tower checks
            // checks against all enemies
            for (int i = 0; i < GameVariables.Enemies.Count; i++)
            {
                for (int t = 0; t < GameVariables.Towers.Count; t++)
                {
                    foreach (Spell st in GameVariables.Magic)
                    {
                        // See if towers are under spell effect
                        if (GameVariables.Towers[t].PieceShape.Intersects(st.AreaOfEffect))
                        {
                            switch (st.Effect)
                            {
                                case "berserk":
                                    {

                                        GameVariables.Towers[t].Berserked = true;
                                        break;
                                    }

                                default:
                                    {
                                        if (GameVariables.Towers[t].shot != null)
                                        {
                                            GameVariables.Towers[t].shot.MoveSpeed = 10;

                                        }
                                        GameVariables.Towers[t].Berserked = false;
                                        break;
                                    }
                            }
                        }
                    }

                    // sets towers to attack enemies.
                    if (GameVariables.Enemies[i] != null)
                    {

                        if (GameVariables.Towers[t].HitBox.Intersects(GameVariables.Enemies[i].PieceShape) && GameVariables.Enemies[i].IsVisible == true)
                        {
                            GameVariables.Towers[t].AttackEnemy(GameVariables.Enemies[i]);
                        }
                        if (GameVariables.Enemies[i].Alive == false)
                        {
                            GameVariables.Enemies[i] = null;
                            GameVariables.Towers[t].shot = null;
                        }
                    }

                    // sets spells to work
                    if (GameVariables.Enemies[i] != null)
                    {
                        if (GameVariables.Enemies[i].Immune == false && GameVariables.Enemies[i].IsVisible == true)
                        {
                            foreach (Spell s in GameVariables.Magic)
                            {
                                if (GameVariables.Enemies[i].PieceShape.Intersects(s.AreaOfEffect))
                                {
                                    switch (s.Effect)
                                    {
                                        case "slow":
                                            {

                                                GameVariables.Enemies[i].Slowed = true;
                                                break;
                                            }
                                        case "fire":
                                            {

                                                GameVariables.Enemies[i].Health -= 5;
                                                break;
                                            }
                                        default:
                                            {
                                                GameVariables.Enemies[i].Slowed = false;
                                                break;
                                            }
                                    }
                                }
                            }
                        }

                        GameVariables.Enemies[i].Move();



                        // if enemies go out of bounds attack
                        if (GameVariables.Enemies[i].PieceShape.X > 800)
                        {
                            GameVariables.Enemies[i].MoraleAttack();
                            GameVariables.Enemies[i] = null;
                            GameVariables.Towers[t].shot = null;
                        }
                    }
                }
            }
            #endregion

            #region Game Start Tower Check
            if (GameVariables.Towers.Count > 0)
            {
                if (frameCount == 179)
                {

                    for (int k = 0; k <= enemySpawner; k++)
                    {
                        if (k < GameVariables.Enemies.Count && GameVariables.Enemies[k] != null)
                        {
                            GameVariables.Enemies[k].IsVisible = true;
                        }
                    }
                    enemySpawner++;
                    // add spell despawn around
                    GameVariables.Magic.Clear();
                    frameCount = 0;
                }
                frameCount++;
            }
            #endregion

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
            spriteBatch.Draw(mapEdit_Menu_Txtr, mapEdit_MenuRec, Color.White);              // Menu Button
            spriteBatch.Draw(game_GameBorder_Txtr, game_GameBorderRec, Color.White);        // Game Border

            // Spells
            spriteBatch.Draw(spell_Fireball_Txtr, spell_FireballRec, Color.White);          // FireBall icon
            spriteBatch.Draw(spell_Heal_Txtr, spell_HealRec, Color.White);                  // Heal Icon
            spriteBatch.Draw(spell_Rage_Txtr, spell_RageRec, Color.White);                  // Rage Icon
            spriteBatch.Draw(spell_Slow_Txtr, spell_SlowRec, Color.White);                  // Slow Icon

            // Towers
            spriteBatch.Draw(twr_Catapult_Txtr, twr_CatapultRec, Color.White);              // Catapault Icon
            spriteBatch.Draw(twr_Trebuchet_Txtr, twr_TrebuchetRec, Color.White);            // Trebuchet Icon

            // Health / Mana Bars
            spriteBatch.Draw(game_HealthBar_Txtr, game_HealthBarRec, Color.White);          // Health Bar underlay
            spriteBatch.Draw(game_HealthBar_Txtr, game_ManaBarRec, Color.White);            // Mana Bar Underlay.

            

            #endregion

            #region Heal Check
            if (mousePos.Intersects(spell_HealRec))
            {
                if (mState.LeftButton == ButtonState.Pressed)
                {
                    if (GameVariables.Morale < 100)
                    {
                        if (GameVariables.Currency >= 50)
                        {
                            GameVariables.Currency = GameVariables.Currency - 10;
                            GameVariables.Morale = GameVariables.Morale + 1;
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
            #endregion // tower drawing           

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

                        if (tf_Catapult)
                        {
                            if (textures[x, y] == 0)
                            {
                                if (GameVariables.Currency >= 100)
                                {
                                    //create tower
                                    textures[x, y] = 11;
                                    GameVariables.Towers.Add(new Tower(1, 50, GameState.tiles[x, y].X, GameState.tiles[x, y].Y, 45, 45, "Tiles/Tiles - Tower Art/Tower1", "Projectiles/Projectile", 5, 10, "", 10));
                                    GameVariables.Currency = GameVariables.Currency - 100;
                                }
                            }
                        }
                        if (tf_Trebuchet)
                        {
                            if (textures[x, y] == 0)
                            {
                                if (GameVariables.Currency >= 200)
                                {
                                    //create tower
                                    textures[x, y] = 12;

                                    GameVariables.Towers.Add(new Tower(1, 100, GameState.tiles[x, y].X, GameState.tiles[x, y].Y, 45, 45, "Tiles/Tiles - Tower Art/Tower2", "Projectiles/Projectile", 1, 5, "", 40));
                                    GameVariables.Currency = GameVariables.Currency - 200;
                                }
                            }
                        }
                        if (tf_Fire)
                        {
                            if (textures[x, y] != 0 || textures[x, y] == 0)
                            {
                                if (GameVariables.Currency >= 100)
                                {
                                    //create magic
                                    textures[x, y] = 13;

                                    GameVariables.Magic.Add(new Spell("fire", GameState.tiles[x, y].X - 45, GameState.tiles[x, y].Y - 45));
                                    GameVariables.Currency = GameVariables.Currency - 50;
                                    if (GameVariables.Currency <= 0)
                                    {
                                        GameVariables.Currency = 0;
                                    }
                                }
                            }
                        }

                        if (tf_Rage)
                        {
                            if (textures[x, y] != 0 || textures[x, y] == 0)
                            {
                                if (GameVariables.Currency >= 100)
                                {
                                    //create magic
                                    textures[x, y] = 15;
                                    GameVariables.Magic.Add(new Spell("speed", GameState.tiles[x, y].X - 45, GameState.tiles[x, y].Y - 45));
                                    GameVariables.Currency = GameVariables.Currency - 50;
                                    if (GameVariables.Currency <= 0)
                                    {
                                        GameVariables.Currency = 0;
                                    }
                                }
                            }
                        }
                        if (tf_Slow)
                        {
                            if (textures[x, y] != 0 || textures[x, y] == 0)
                            {
                                if (GameVariables.Currency >= 100)
                                {
                                    //create magic
                                    textures[x, y] = 16;
                                    GameVariables.Magic.Add(new Spell("slow", GameState.tiles[x, y].X - 45, GameState.tiles[x, y].Y - 45));
                                    GameVariables.Currency = GameVariables.Currency - 50;
                                    if (GameVariables.Currency <= 0)
                                    {
                                        GameVariables.Currency = 0;
                                    }
                                }
                            }
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
                        case 11:
                            spriteBatch.Draw(twr_Catapult_Txtr, tiles[x, y], Color.White);
                            break;
                        case 12:
                            spriteBatch.Draw(twr_Trebuchet_Txtr, tiles[x, y], Color.White);
                            break;
                        case 13:
                            spriteBatch.Draw(spell_Fireball_Txtr, tiles[x, y], Color.White);
                            break;
                        case 15:
                            spriteBatch.Draw(spell_Rage_Txtr, tiles[x, y], Color.White);
                            break;
                        case 16:
                            spriteBatch.Draw(spell_Slow_Txtr, tiles[x, y], Color.White);
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

            #region Enemy and tower animation
            // animates the bullets
            foreach (Tower t in GameVariables.Towers)
            {
                if (t != null)
                {
                    spriteBatch.Draw(twr_Catapult_Txtr, t.PieceShape, Color.White);
                    if (t.Fired != t.CoolDown)
                    {
                        t.Fired++;
                    }
                    if (t.shot != null)
                    {
                        spriteBatch.Draw(projectiles_Ball_Txtr, t.shot.PieceShape, Color.White);
                    }
                }
            }
            // draw the enemies
            foreach (Enemy e in GameVariables.Enemies)
            {
                if (e != null && e.IsVisible == true)
                {
                    spriteBatch.Draw(obj_Monster, e.PieceShape, Color.White);
                    if (e.Shot != null)
                    {
                        spriteBatch.Draw(projectiles_Ball_Txtr, e.Shot.PieceShape, Color.White);
                    }
                }
            }
            #endregion
        }
    }
}
