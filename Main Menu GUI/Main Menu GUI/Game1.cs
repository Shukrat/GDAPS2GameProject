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

namespace Main_Menu_GUI
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Attributes
        #region Attributes - Texture2D
        Texture2D exit;
        Texture2D exitHover;
        Texture2D exitClick;
        Texture2D settings;
        Texture2D settingsHover;
        Texture2D settingsClick;
        Texture2D start;
        Texture2D startHover;
        Texture2D startClick;
        #endregion

        #region Attributes - Rectangles
        Rectangle exitRec;
        Rectangle settingsRec;
        Rectangle startRec;
        Rectangle mousePos;
        #endregion

        #region Attributes - Mouse and Keyboard State
        KeyboardState kState;
        MouseState mState;
        #endregion

        #region Attributes - Draw Booleans
        bool startBool;
        bool settingsBool;
        #endregion
        
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
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;

            #region Rectangle Initialization
            exitRec = new Rectangle();
            exitRec.Width = 200;
            exitRec.Height = 88;
            exitRec.X = graphics.PreferredBackBufferWidth - exitRec.Width - 10;
            exitRec.Y = graphics.PreferredBackBufferHeight - exitRec.Height - 10;

            settingsRec = new Rectangle();
            settingsRec.Width = 200;
            settingsRec.Height = 88;
            settingsRec.X = (int)(graphics.PreferredBackBufferWidth - settingsRec.Width*2.5);
            settingsRec.Y = graphics.PreferredBackBufferHeight - settingsRec.Height - 10;

            startRec = new Rectangle();
            startRec.Width = 200;
            startRec.Height = 88;
            startRec.X = 10;
            startRec.Y = graphics.PreferredBackBufferHeight - startRec.Height - 10;

            mousePos = new Rectangle();
            mousePos.Width = 1;
            mousePos.Height = 1;
            mousePos.X = 0;
            mousePos.Y = 0;
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
            #region Content - Button Art
            exit = Content.Load<Texture2D>("exitButton");
            exitHover = Content.Load<Texture2D>("exitButtonHover");
            exitClick = Content.Load<Texture2D>("exitButtonClick");
            settings = Content.Load<Texture2D>("settingsButton");
            settingsHover = Content.Load<Texture2D>("settingsButtonHover");
            settingsClick = Content.Load<Texture2D>("settingsButtonClick");
            start = Content.Load<Texture2D>("startButton");
            startHover = Content.Load<Texture2D>("startButtonHover");
            startClick = Content.Load<Texture2D>("startButtonClick");
            #endregion

            // TODO: use this.Content to load your game content here
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

            #region Mouse Location Update
            // Update mouse state and set paint brush to location
            mState = Mouse.GetState();
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
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(exit, exitRec, Color.White);
            spriteBatch.Draw(settings, settingsRec, Color.White);
            spriteBatch.Draw(start, startRec, Color.White);

            if (mousePos.Intersects(exitRec))
            {
                spriteBatch.Draw(exitHover, exitRec, Color.White);
            }
            if (mousePos.Intersects(exitRec) && mState.LeftButton == ButtonState.Pressed)
            {
                spriteBatch.Draw(exitClick, exitRec, Color.White);
            }

            if (mousePos.Intersects(settingsRec))
            {
                spriteBatch.Draw(settingsHover, settingsRec, Color.White);
            }
            if (mousePos.Intersects(settingsRec) && mState.LeftButton == ButtonState.Pressed)
            {
                spriteBatch.Draw(settingsClick, settingsRec, Color.White);
            }

            if (mousePos.Intersects(startRec))
            {
                spriteBatch.Draw(startHover, startRec, Color.White);
            }
            if (mousePos.Intersects(startRec) && mState.LeftButton == ButtonState.Pressed)
            {
                spriteBatch.Draw(startClick, startRec, Color.White);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
