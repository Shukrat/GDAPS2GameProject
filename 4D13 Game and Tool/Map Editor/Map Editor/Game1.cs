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
    // - ADD ERASE FUNCTION - Blank tile set as 0;
    // - PERFECT WINDOW SIZE
    // - ADD WALKING PATH 2D ARRAY?
    // - ADD BUILDABLE LOCATION 2D ARRAY? - THIS WILL BE INT VALUE OF 0
    // - ADD BACKSPLAT SELECTION TO UPDATE TILE SELECTION OPTIONS
    // - UPDATE COMMENTS
    // - MORE?
    // ------------------------------END TO DO NOTES------------------------------


    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        

        // Create MapProcesses class
        // Class effectively moves code from Game1 to distinct class
        // Unclutters Game1
        GameProcesses mapProcesses = new GameProcesses();

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
            
            // Pass graphics to MapProcesses to run MapProcesses.Initialize() properly
            mapProcesses.Initialize(graphics);

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
            
            // Pass Content to MapProcesses to run MapProcesses.LoadContent() properly
            mapProcesses.LoadContent(Content);
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
            
            // Pass graphics and the Game1 class to MapProcesses to run MapProcesses.Update() properly
            mapProcesses.Update(graphics, this, mapProcesses);

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
            // Pass spritebatch and graphics to MapProcesses to run MapProcesses.Draw() properly
            mapProcesses.Draw(spriteBatch, graphics);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
