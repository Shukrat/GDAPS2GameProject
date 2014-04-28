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
    class GameProcesses
    {
        // Enums to set which game state is running
        // Add more here to add more game states for the game
        public enum GameStateEnum
        {
            // Map editor enums
            mapEdit_MainMenu, // USED TO TRANSITION TO MAPEDITOR
            mapEdit_MapEditor,
            mapEdit_LoadMenu,
            mapEdit_SaveMenu,

            // Main game enums
            main_MainMenu,
            main_LoadScreen,
            main_LoadMap,
            main_Settings,
            main_MapFirst,
            main_MapSecond,
            main_MapThird,            
        }

        // Static SaveLoad class call - Needed to reliably save/load texture 2D array info
        // Static allows for all classes to call this method by refering to GameProcessess first
        public static SaveLoad saveLoad = new SaveLoad();

        // GameStateEnum variable to change when child classes of GameState change it
        public GameStateEnum gameStateStatus;

        // Attributes:
        #region Attributes - Class Calls
        // Create child classes as needed
        // ADD CHILD CLASSES OF GameState HERE AND INTIALIZE IN METHOD BELOW
        // MAP EDITOR GAMESTATE CREATION
        // Map Editor Menu gamestates
        public GS_MM_MainMenu gs_mapEdit_MainMenu;
        public GS_MM_LoadMenu gs_mapEdit_LoadMenu;
        public GS_MM_SaveMenu gs_mapEdit_SaveMenu;
        // Map Editor gamestate
        public GS_MM_MapEditor gs_mapEdit_MapEditor;

        // MAIN GAME GAMESTATE CREATION
        // Menu gamestates
        public GS_Game_MainMenu gs_Game_MainMenu;
        public GS_Game_Settings gs_Game_Settings;
        public GS_Game_LoadMap gs_Game_LoadMap;
        public GS_Game_LoadScreen gs_Game_LoadScreen;
        // Gameplay gamestates
        public GS_Game_MapFirst gs_Game_MapFirst;
        public GS_Game_MapSecond gs_Game_MapSecond;
        public GS_Game_MapThird gs_Game_MapThird;

        #endregion

        // Initialization - to be used in Initilize in Game1
        public void Initialize(GraphicsDeviceManager graphics)
        {
            // Set width and height of window
            graphics.PreferredBackBufferHeight = 920;
            graphics.PreferredBackBufferWidth = 1150;

            // MAP EDITOR GAME STATES
            gs_mapEdit_MainMenu = new GS_MM_MainMenu();
            gs_mapEdit_LoadMenu = new GS_MM_LoadMenu();
            gs_mapEdit_SaveMenu = new GS_MM_SaveMenu();
            gs_mapEdit_MapEditor = new GS_MM_MapEditor();

            // MAIN GAME GAME STATES
            gs_Game_MainMenu = new GS_Game_MainMenu();
            gs_Game_Settings = new GS_Game_Settings();
            gs_Game_LoadScreen = new GS_Game_LoadScreen();  // Load menu (asks what map to load)
            gs_Game_LoadMap = new GS_Game_LoadMap();        // Actual loaded map
            gs_Game_MapFirst = new GS_Game_MapFirst();
            gs_Game_MapSecond = new GS_Game_MapSecond();
            gs_Game_MapThird = new GS_Game_MapThird();
            

            // Set default game state - SET THIS TO THE FIRST SCREEN YOU WANT
            gameStateStatus = GameStateEnum.main_MainMenu;

        }

        // Load Content - to be used in LoadContent in Game1
        public void LoadContent(ContentManager Content)
        {
            // MAP EDITOR CONTENT
            gs_mapEdit_MainMenu.LoadContent(Content);
            gs_mapEdit_LoadMenu.LoadContent(Content);
            gs_mapEdit_MapEditor.LoadContent(Content);
            gs_mapEdit_SaveMenu.LoadContent(Content);

            gs_Game_MainMenu.LoadContent(Content);
            gs_Game_Settings.LoadContent(Content);
            gs_Game_MapFirst.LoadContent(Content);
            gs_Game_MapSecond.LoadContent(Content);
            gs_Game_MapThird.LoadContent(Content);
            gs_Game_LoadScreen.LoadContent(Content);
            gs_Game_LoadMap.LoadContent(Content);
        }

        // Update - to be used in Update in Game1
        public void Update(GraphicsDeviceManager graphics, Game1 game1, GameProcesses gameProcesses)
        {
            // Change which update method is called from which child class
            // When gameStateStatus (enum) is changed
            switch (gameStateStatus)
            {
                // ----- CASES FOR MAP EDITOR FUNCTION
                case GameStateEnum.mapEdit_MainMenu:
                    {
                        gameStateStatus = gs_mapEdit_MainMenu.Update(graphics, game1);
                        break;
                    }
                case GameStateEnum.mapEdit_MapEditor:
                    {
                        gameStateStatus = gs_mapEdit_MapEditor.Update(graphics, game1);
                        break;
                    }
                case GameStateEnum.mapEdit_LoadMenu:
                    {
                        gameStateStatus = gs_mapEdit_LoadMenu.Update(graphics, game1);
                        break;
                    }
                case GameStateEnum.mapEdit_SaveMenu:
                    {
                        gameStateStatus = gs_mapEdit_SaveMenu.Update(graphics, game1);
                        break;
                    }
                // END CASES FOR MAP EDITOR FUNCTION

                // ----- CASES FOR MAIN GAME
                case GameStateEnum.main_MainMenu:
                    {
                        gameStateStatus = gs_Game_MainMenu.Update(graphics, game1);
                        break;
                    }
                case GameStateEnum.main_LoadScreen:
                    {
                        gameStateStatus = gs_Game_LoadScreen.Update(graphics, game1);
                        break;
                    }
                case GameStateEnum.main_LoadMap:
                    {
                        gameStateStatus = gs_Game_LoadMap.Update(graphics, game1);
                        break;
                    }
                case GameStateEnum.main_MapFirst:
                    {
                        gameStateStatus = gs_Game_MapFirst.Update(graphics, game1);
                        break;
                    }
                case GameStateEnum.main_MapSecond:
                    {
                        break;
                    }
                case GameStateEnum.main_MapThird:
                    {
                        break;
                    }
                case GameStateEnum.main_Settings:
                    {
                        gameStateStatus = gs_Game_Settings.Update(graphics, game1);
                        break;
                    }
                // END CASES FOR MAIN GAME
                default:
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            // Change which draw method is called form which child class
            // When gameStateStatus (enum) is changed
            switch (gameStateStatus)
            {
                // ----- CASES FOR MAP EDITOR FUNCTION
                case GameStateEnum.mapEdit_MainMenu:
                    {
                        gs_mapEdit_MainMenu.Draw(spriteBatch, graphics);
                        break;
                    }
                case GameStateEnum.mapEdit_MapEditor:
                    {
                        gs_mapEdit_MapEditor.Draw(spriteBatch, graphics);
                        break;
                    }
                case GameStateEnum.mapEdit_LoadMenu:
                    {
                        gs_mapEdit_LoadMenu.Draw(spriteBatch, graphics);
                        break;
                    }
                case GameStateEnum.mapEdit_SaveMenu:
                    {
                        gs_mapEdit_SaveMenu.Draw(spriteBatch, graphics);
                        break;
                    }
                // END CASES FOR MAP EDITOR FUNCTION

                // ----- CASES FOR MAIN GAME
                case GameStateEnum.main_MainMenu:
                    {
                        gs_Game_MainMenu.Draw(spriteBatch, graphics);
                        break;
                    }
                case GameStateEnum.main_LoadScreen:
                    {
                        gs_Game_LoadScreen.Draw(spriteBatch, graphics);
                        break;
                    }
                case GameStateEnum.main_LoadMap:
                    {
                        gs_Game_LoadMap.Draw(spriteBatch, graphics);
                        break;
                    }
                case GameStateEnum.main_MapFirst:
                    {
                        gs_Game_MapFirst.Draw(spriteBatch, graphics);
                        break;
                    }
                case GameStateEnum.main_MapSecond:
                    {
                        break;
                    }
                case GameStateEnum.main_MapThird:
                    {
                        break;
                    }
                case GameStateEnum.main_Settings:
                    {
                        gs_Game_Settings.Draw(spriteBatch, graphics);
                        break;
                    }
                // END CASES FOR MAIN GAME
                default:
                    break;
            }
        }
    }
}
