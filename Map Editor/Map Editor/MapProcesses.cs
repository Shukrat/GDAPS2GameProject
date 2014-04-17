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

namespace Map_Editor
{
    class MapProcesses
    {
        // Attributes:
        #region Attributes - Class Calls
        SaveLoad saveLoad = new SaveLoad();
        #endregion

        #region Attributes - Texture2D
        // Tiles
        Texture2D grayTile;
        Texture2D blueTile;
        Texture2D greenTile;
        Texture2D sideBar;
        Texture2D pathTile;

        // Images for Menus
        // Textures for Main Menu
        Texture2D exit;
        Texture2D exitHover;
        Texture2D exitClick;
        Texture2D loadMap;
        Texture2D newMap;

        // Textures for Map Editor
        Texture2D saveMap;
        Texture2D mapEditExitButton;
        
        // Font for save or load menu
        SpriteFont font;
        #endregion

        #region Attributes - Texture Selection Booleans
        // Bools for keystates
        bool tfGrayTile;
        bool tfBlueTile;
        bool tfGreenTile;
        bool tfPathTile;
        #endregion

        #region Attributes - Rectangles
        // Mouse position Rectangle
        Rectangle mousePos;

        // Retangles for tile selection options
        Rectangle selectGray;   // OBSOLETE
        Rectangle selectBlue;   // OBSOLETE
        Rectangle selectGreen;  // OBSOLETE
        Rectangle selectPath;   // OBSOLETE
        
        // Rectangle to show currently selected tile
        Rectangle paintBrush;

        // Rectangles for Menus / Buttons
        // Main Menu
        Rectangle mainMenuExitRec;
        Rectangle mainMenuLoadRec;
        Rectangle mainMenuNewRec;
        // Map Editor
        Rectangle mapEditExitRec;
        Rectangle mapEditSaveRec;
        Rectangle sideBarBG;
        #endregion

        #region Attributes - 2D Arrays
        // 2D arrays for map information
        Rectangle[,] tiles;
        int[,] textures;
        string fileName;
        #endregion

        #region Attributes - Mouse and Keyboard States
        // Keyboard and mouse states
        KeyboardState kState;
        KeyboardState prevKState;
        MouseState mState;
        #endregion

        #region Attributes - Bools for Game Window Transitions
        bool mainMenu = true;
        bool loadMenu;
        bool saveMenu;
        bool mapMaker;
        #endregion

        #region Attributes - Other stuff
        bool interfaceMade = false;
        #endregion

        // Properties:
        #region Property - Rectangle 2D Array
        // Get for tiles rectangle 2D array
        public Rectangle[,] Tiles
        {
            get { return tiles; }
        }
        #endregion

        #region Property - Get/Set int 2D Array
        // Get for textures int 2D array
        public int[,] Textures
        {
            get { return textures; }
            set { textures = value; }
        }
        #endregion

        #region Property - Get/Set FileName
        // Get for filename string
        public string FileName
        {
            get { return fileName; }
        }
        #endregion

        // Constructor
        public MapProcesses()
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
                    newTile.X = 45 * x;
                    newTile.Y = 45 * i;
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
        }

        // Mouse Select Tiles - Detects collision between mouse rectangle and tile rectangles
        public void MouseTileSelect()
        {
            // Update mouse position at each call of this method in update in Game1
            mState = Mouse.GetState();
            // Detect collisions
            if (mousePos.Intersects(selectGray) && mState.LeftButton == ButtonState.Pressed)
            {
                tfGrayTile = true;
                tfBlueTile = false;
                tfGreenTile = false;
                tfPathTile = false;
            }
            if (mousePos.Intersects(selectBlue) && mState.LeftButton == ButtonState.Pressed)
            {
                tfGrayTile = false;
                tfBlueTile = true;
                tfGreenTile = false;
                tfPathTile = false;
            }
            if (mousePos.Intersects(selectGreen) && mState.LeftButton == ButtonState.Pressed)
            {
                tfGrayTile = false;
                tfBlueTile = false;
                tfGreenTile = true;
                tfPathTile = false;
            }
            if (mousePos.Intersects(selectPath) && mState.LeftButton == ButtonState.Pressed)
            {
                tfGrayTile = false;
                tfBlueTile = false;
                tfGreenTile = false;
                tfPathTile = true;
            }
        }

        // Initialization - to be used in Initilize in Game1
        public void Initialize(GraphicsDeviceManager graphics)
        {
            // Set width and height of window
            graphics.PreferredBackBufferHeight = 900;
            graphics.PreferredBackBufferWidth = 1130;

            // Initialize main menu buttons - Only called on program start
            #region Rectangle Initialization
            // Create Exit Button
            mainMenuExitRec = new Rectangle();
            mainMenuExitRec.Width = 200;
            mainMenuExitRec.Height = 88;
            mainMenuExitRec.X = graphics.PreferredBackBufferWidth - mainMenuExitRec.Width - 10;
            mainMenuExitRec.Y = graphics.PreferredBackBufferHeight - mainMenuExitRec.Height - 10;

            // Create Load Map Button
            mainMenuLoadRec = new Rectangle();
            mainMenuLoadRec.Width = 200;
            mainMenuLoadRec.Height = 88;
            mainMenuLoadRec.X = (int)(graphics.PreferredBackBufferWidth - mainMenuLoadRec.Width * 2.5);
            mainMenuLoadRec.Y = graphics.PreferredBackBufferHeight - mainMenuLoadRec.Height - 10;

            // Create New Map Button
            mainMenuNewRec = new Rectangle();
            mainMenuNewRec.Width = 200;
            mainMenuNewRec.Height = 88;
            mainMenuNewRec.X = 10;
            mainMenuNewRec.Y = graphics.PreferredBackBufferHeight - mainMenuNewRec.Height - 10;
            #endregion
        }

        // Load Content - to be used in LoadContent in Game1
        public void LoadContent(ContentManager Content)
        {
            // Load All Textures
            #region Texture2D File Load
            grayTile = Content.Load<Texture2D>("Gray Tile");
            blueTile = Content.Load<Texture2D>("Blue Tile");
            greenTile = Content.Load<Texture2D>("Green Tile");
            pathTile = Content.Load<Texture2D>("path");
            sideBar = Content.Load<Texture2D>("Sidebar");
            exit = Content.Load<Texture2D>("exitButton");
            loadMap = Content.Load<Texture2D>("loadMapButton");
            newMap = Content.Load<Texture2D>("newMapButton");
            saveMap = Content.Load<Texture2D>("saveButton");
            mapEditExitButton = Content.Load<Texture2D>("mapEditExitButton");
            font = Content.Load<SpriteFont>("mainFont");
            #endregion
        }

        // Update - to be used in Update in Game1
        public void Update(GraphicsDeviceManager graphics, Game1 game1)
        {
            #region Update - Mouse Location
            // Update mouse state location - assign details to mouse rectangle
            mState = Mouse.GetState();
            mousePos.X = mState.X;
            mousePos.Y = mState.Y;
            #endregion

            #region Update - Main Menu
            // Main menu update programming
            // Detects collisions between mouse rectangle and buttons
            // and if button is pressed
            if (mainMenu)
            {
                // New Map Button collision
                // Goes to Update - Map Maker in this Update method
                if (mousePos.Intersects(mainMenuNewRec) && mState.LeftButton == ButtonState.Pressed)
                {
                    mainMenu = false;
                    mapMaker = true;
                    loadMenu = false;
                    saveMenu = false;
                }
                // Load Map Button collision
                // Goes to Update - Load Map in this Update method
                if (mousePos.Intersects(mainMenuLoadRec) && mState.LeftButton == ButtonState.Pressed)
                {
                    mainMenu = false;
                    mapMaker = false;
                    loadMenu = true;
                    saveMenu = false;
                }
                // Exit Map Button collision
                if (mousePos.Intersects(mainMenuExitRec) && mState.LeftButton == ButtonState.Pressed)
                {
                    game1.Exit();
                }
            }
            #endregion

            #region Update - Map Maker
            // Main map builder screen
            // Creates Interface, Updates Mouse Tile Selection
            // And detects button collision
            if (mapMaker)
            {
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
                    mapEditExitRec = new Rectangle();
                    mapEditExitRec.Width = 100;
                    mapEditExitRec.Height = 75;
                    mapEditExitRec.X = graphics.PreferredBackBufferWidth - 110;
                    mapEditExitRec.Y = 805;

                    // Save Button
                    mapEditSaveRec = new Rectangle();
                    mapEditSaveRec.Width = 100;
                    mapEditSaveRec.Height = 75;
                    mapEditSaveRec.X = graphics.PreferredBackBufferWidth - 220;
                    mapEditSaveRec.Y = 805;

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
                if (mousePos.Intersects(mapEditSaveRec) && mState.LeftButton == ButtonState.Pressed)
                {
                    // Sets which "window" is currently "open," or being updated in Update method
                    // And drawn in Draw method
                    mainMenu = false;
                    mapMaker = false;
                    loadMenu = false;
                    saveMenu = true;
                }
                if (mousePos.Intersects(mapEditExitRec) && mState.LeftButton == ButtonState.Pressed)
                {
                    game1.Exit();
                }
                #endregion
            }
            #endregion

            #region Update - Load Menu
            // If Load Map is selected from Main Menu
            // Update calls SaveLoad class
            // Requests users input
            // Loads file to textures 2D array
            // Goes to Map Maker Update Bool in this method
            if (loadMenu)
            {
                // SaveLoad Calls:
                // Set SaveLoad loadComplete bool to false in order to wait for user input
                // Set SaveLoad textures 2D array to this one
                // Send bool if on load screen or save screen
                // Call SaveLoad Update (user input)
                // Set this textures 2D array to SaveLoad's loaded 2D array
                saveLoad.LoadComplete = false;
                saveLoad.Textures = textures;
                saveLoad.LoadMenu = loadMenu;
                saveLoad.Update(kState, prevKState);
                textures = saveLoad.Textures;

                // Waits for user hits enter in SaveLoad.Update method
                // moves to Map Maker Update Bool in this Update method
                if (saveLoad.LoadComplete)
                {
                    loadMenu = false;
                    mapMaker = true;
                }
            }
            #endregion

            #region Update - Save Menu
            // If Save Map is selected from Map Maker
            // Update calls SaveLoad class
            // Requests users input
            // Saves file from textures 2D array
            // Goes to Update - Map Maker in this method
            if (saveMenu)
            {
                // SaveLoad Calls:
                // Set SaveLoad saveComplete bool to false in order to wait for user input
                // Send bool if on load screen or save screen
                // Set SaveLoad textures 2D array to this one
                // Call SaveLoad Update (user input)
                // Saves to binary file
                saveLoad.SaveComplete = false;
                saveLoad.SaveMenu = saveMenu;
                saveLoad.Textures = textures;
                saveLoad.Update(kState, prevKState);

                // Waits for user to hit enter in SaveLoad.Update method
                // moves to Update - Map Maker in this Update method
                if (saveLoad.SaveComplete)
                {
                    saveMenu = false;
                    mapMaker = true;
                    interfaceMade = false;
                }
            }
            #endregion
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            #region Draw - Main Menu
            // Draws main buttons: New, Load, Exit
            if (mainMenu)
            {
                spriteBatch.Draw(exit, mainMenuExitRec, Color.White);
                spriteBatch.Draw(newMap, mainMenuNewRec, Color.White);
                spriteBatch.Draw(loadMap, mainMenuLoadRec, Color.White);
            }
            #endregion

            #region Draw - Map Maker
            // Draws map maker interface, detects texture/tile assignment
            if (mapMaker)
            {
                #region Draw Interface
                // Draws all textures for sidebar, tile selection options, and buttons
                spriteBatch.Draw(sideBar, sideBarBG, Color.White); // Sidebar background
                spriteBatch.Draw(grayTile, selectGray, Color.White); // 1 - Gray Tile
                spriteBatch.Draw(blueTile, selectBlue, Color.White); // 2 - Blue Tile
                spriteBatch.Draw(greenTile, selectGreen, Color.White); // 3 - Green Tile
                spriteBatch.Draw(pathTile, selectPath, Color.White); // 4 - Path Tile
                spriteBatch.Draw(saveMap, mapEditSaveRec, Color.White); // Save Map Button
                spriteBatch.Draw(mapEditExitButton, mapEditExitRec, Color.White); // Exit Map Button
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
            #endregion

            #region Draw - Load Menu
            if (loadMenu)
            {
                // Update kState and prevKState before running SaveLoad update programming
                prevKState = kState;
                kState = Keyboard.GetState();

                // Set font, and draw the text.
                saveLoad.ConsoleFont = font;
                saveLoad.Draw(spriteBatch);
            }
            #endregion

            #region Draw - Save Menu
            // Draw Save Menu - Draws text on screen for user input for file saving
            if (saveMenu)
            {
                // Update kState and prevKState before running SaveLoad update programming
                prevKState = kState;
                kState = Keyboard.GetState();

                // Set font, and draw the text.
                saveLoad.ConsoleFont = font;
                saveLoad.Draw(spriteBatch);
            }
            #endregion
        }
    }
}
