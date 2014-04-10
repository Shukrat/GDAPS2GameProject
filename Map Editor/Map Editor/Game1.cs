#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace Map_Editor
{
    // ------------------------------TO DO NOTES------------------------------
    // - ADD MORE TILE SELECTIONS ON SIDEBAR
    // - ADD SAVE FUNCTION
    // - ADD LOAD FUNCTION
    // - ADD CLOSE FUNCTION - ASKS IF YOU WANT TO SAVE
    // - ADD ERASE FUNCTION
    // - PERFECT WINDOW SIZE
    // - ADD WALKING PATH 2D ARRAY?
    // - ADD BUILDABLE LOCATION 2D ARRAY?
    // - MORE?
    // ------------------------------END TO DO NOTES------------------------------


    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Attributes:
        #region Attributes - Texture2D
        // Tiles
        Texture2D grayTile;
        Texture2D blueTile;
        Texture2D greenTile;
        Texture2D sideBar;
        Texture2D pathTile;
        #endregion
        #region Attributes - Texture Selection Booleans
        // Bools for keystates
        bool tfGrayTile;
        bool tfBlueTile;
        bool tfGreenTile;
        bool tfPathTile;
        #endregion
        #region Attributes - Rectangles
        // Retangles for tile selection options
        Rectangle sideBarBG;
        Rectangle selectGray;
        Rectangle selectBlue;
        Rectangle selectGreen;
        Rectangle selectPath;
        Rectangle paintBrush;
        Rectangle mousePos;
        #endregion
        #region Attributes - 2D Arrays
        // 2D arrays for map information
        Rectangle[,] tiles;
        int[,] textures;
        string fileName;
        #endregion
        #region Attribute - Mouse and Keyboard States
        // Keyboard and mouse states
        KeyboardState kState;
        MouseState mState;
        #endregion

        // Custom Methods
        #region Custom Methods
        // CUSTOM METHODS - NOT PART OF DEFAULT MONOGAME METHODS
        // Keyboard Shortcuts to Select Tiles
        public void KeyboardSC()
        {
            kState = Keyboard.GetState();
            // Select gray tile
            if (kState.IsKeyDown(Keys.D1))
            {
                tfGrayTile = true;
                tfBlueTile = false;
                tfGreenTile = false;
                tfPathTile = false;
            }
            // Select blue tile
            if (kState.IsKeyDown(Keys.D2))
            {
                tfGrayTile = false;
                tfBlueTile = true;
                tfGreenTile = false;
                tfPathTile = false;
            }
            // Select green tile
            if (kState.IsKeyDown(Keys.D3))
            {
                tfGrayTile = false;
                tfBlueTile = false;
                tfGreenTile = true;
                tfPathTile = false;
            }
            if (kState.IsKeyDown(Keys.D4))
            {
                tfGrayTile = false;
                tfBlueTile = false;
                tfGreenTile = false;
                tfPathTile = true;
            }
        }

        // Mouse Select Tiles
        public void MouseTileSelect()
        {
            mState = Mouse.GetState();
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

        #endregion

        // Constructor - MONOGAME DEFAULT
        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            SaveLoad saveLoad = new SaveLoad(textures, tiles, fileName);
            graphics.PreferredBackBufferHeight = 900;
            graphics.PreferredBackBufferWidth = 1210;

            #region Initialize Arrays
            // Initilize 2D Rectangle Array
            // Used to store map information
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
            // Used to establish textures assigned in tiles 2D matrix above
            textures = new int[20, 20];
            for (int i = 0; i < 20; i++)
            {
                for (int x = 0; x < 20; x++)
                {
                    textures[i, x] = 0;
                    // IFORMATION FOR TEXTURES 2D ARRAY:
                    // 0 = No texture;
                    // 1 = Gray;
                    // 2 = Blue;
                    // 3 = Green;
                }
            }
            #endregion

            #region Interface
            // ----------Setup interface----------
            // Sidebar background
            sideBarBG = new Rectangle();
            sideBarBG.Width = 210;
            sideBarBG.Height = 800;
            sideBarBG.X = graphics.PreferredBackBufferWidth - 210;
            sideBarBG.Y = 0;

            // Mouse position rectangle
            mousePos = new Rectangle();
            mousePos.Width = 1;
            mousePos.Height = 1;
            mousePos.X = 0;
            mousePos.Y = 0;

            // Tile Selection Display Rectangle
            paintBrush = new Rectangle();
            paintBrush.Width = 50;
            paintBrush.Height = 50;
            paintBrush.X = mState.X;
            paintBrush.Y = mState.Y;
            #endregion

            #region Image Rectangles
            // Gray
            selectGray = new Rectangle();
            selectGray.Width = 50;
            selectGray.Height = 50;
            selectGray.X = graphics.PreferredBackBufferWidth - 195;
            selectGray.Y = 20;
            // Blue
            selectBlue = new Rectangle();
            selectBlue.Width = 50;
            selectBlue.Height = 50;
            selectBlue.X = graphics.PreferredBackBufferWidth - 130;
            selectBlue.Y = 20;
            // Green
            selectGreen = new Rectangle();
            selectGreen.Width = 50;
            selectGreen.Height = 50;
            selectGreen.X = graphics.PreferredBackBufferWidth - 65;
            selectGreen.Y = 20;
            //Path
            selectPath = new Rectangle();
            selectPath.Width = 50;
            selectPath.Height = 50;
            selectPath.X = graphics.PreferredBackBufferWidth - 195;
            selectPath.Y = 90;
            #endregion

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            #region Texture2D File Load
            grayTile = Content.Load<Texture2D>("Gray Tile");
            blueTile = Content.Load<Texture2D>("Blue Tile");
            greenTile = Content.Load<Texture2D>("Green Tile");
            pathTile = Content.Load<Texture2D>("path");
            sideBar = Content.Load<Texture2D>("Sidebar");
            #endregion
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            #region Mouse/Keyboard Tile Paint Selection Update
            // Check Keyboard or Mouse for new tile selection
            KeyboardSC();
            MouseTileSelect();
            #endregion

            #region Mouse Location Update
            // Update mouse state and set paint brush to location
            mState = Mouse.GetState();
            paintBrush.X = mState.X;
            paintBrush.Y = mState.Y;
            mousePos.X = mState.X;
            mousePos.Y = mState.Y;
            #endregion

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            #region Draw Interface
            // Draw interface
            spriteBatch.Draw(sideBar, sideBarBG, Color.White); // Sidebar background
            spriteBatch.Draw(grayTile, selectGray, Color.White); // 1 - Gray Tile
            spriteBatch.Draw(blueTile, selectBlue, Color.White); // 2 - Blue Tile
            spriteBatch.Draw(greenTile, selectGreen, Color.White); // 3 - Green Tile
            spriteBatch.Draw(pathTile, selectPath, Color.White); // 4 - Path Tile
            #endregion

            #region Rectangle Texture Assignment Detection
            // Draw rectangle matrix
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    // If mouse button is pressed, and the mouse position is in the rectangle 2x2 array:
                    // Draw the current tile selected
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
                    }
                }
            }
            #endregion
            // ----------------------------UPDATE THIS SECTION TO UPDATE SELECTION RECTANGLE IN SIDEBAR
            // Draw paint brush selection
            //if (tfBlueTile)
            //{
            //    spriteBatch.Draw(blueTile, paintBrush, Color.White);
            //}
            //if (tfGrayTile)
            //{
            //    spriteBatch.Draw(grayTile, paintBrush, Color.White);
            //}
            //if (tfGreenTile)
            //{
            //    spriteBatch.Draw(greenTile, paintBrush, Color.White);
            //}
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
