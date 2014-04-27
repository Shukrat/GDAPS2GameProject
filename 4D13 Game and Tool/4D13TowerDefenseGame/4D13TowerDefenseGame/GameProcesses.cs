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
            mapEdit_MainMenu,
            mapEdit_MapEditor,
            mapEdit_LoadMenu,
            mapEdit_SaveMenu,

            // Main game enums
            main_NewGame,
            main_MapEditor,
            main_LoadCustom,
            main_Settings
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
        public GS_MM_MainMenu gs_mapEdit_MainMenu;
        public GS_MM_LoadMenu gs_mapEdit_LoadMenu;
        public GS_MM_SaveMenu gs_mapEdit_SaveMenu;
        public GS_MM_MapEditor gs_mapEdit_MapEditor;
        #endregion

        // Initialization - to be used in Initilize in Game1
        public void Initialize(GraphicsDeviceManager graphics)
        {
            // Set width and height of window
            graphics.PreferredBackBufferHeight = 900;
            graphics.PreferredBackBufferWidth = 1130;

            // MAP EDITOR GAME STATES
            gameStateStatus = GameStateEnum.mapEdit_MainMenu;
            gs_mapEdit_MainMenu = new GS_MM_MainMenu();
            gs_mapEdit_LoadMenu = new GS_MM_LoadMenu();
            gs_mapEdit_SaveMenu = new GS_MM_SaveMenu();
            gs_mapEdit_MapEditor = new GS_MM_MapEditor();

            // Set default game state - SET THIS TO THE FIRST SCREEN YOU WANT
            gameStateStatus = GameStateEnum.mapEdit_MainMenu;

        }

        // Load Content - to be used in LoadContent in Game1
        public void LoadContent(ContentManager Content)
        {
            // MAP EDITOR CONTENT
            gs_mapEdit_MainMenu.LoadContent(Content);
            gs_mapEdit_LoadMenu.LoadContent(Content);
            gs_mapEdit_MapEditor.LoadContent(Content);
            gs_mapEdit_SaveMenu.LoadContent(Content);

        }

        // Update - to be used in Update in Game1
        public void Update(GraphicsDeviceManager graphics, Game1 game1, GameProcesses gameProcesses)
        {
            // Change which update method is called from which child class
            // When gameStateStatus (enum) is changed
            switch (gameStateStatus)
            {
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
                default:
                    break;
            }
        }
    }
}
