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

namespace _4D13TowerDefenseGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 


    // TO ADD
    // Lose screen

    // Game functions:
    // Monster movement - somewhat works? Needs polish.
    // Monster attack - not implemented
    // Tower attack - needs to test heal and rage spell    

    // Things the NEED to be done
    // fix pathing ((can't go up along paths and starts are forced down))
    // fix spell timer ((very quick but works))
    // lose ((boots to main menu))
    // enemy speed




    public class Game1 : Game
    {
        // Object Creation

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameProcesses gameProcesses = new GameProcesses();

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
            // Logic Intialization
            gameProcesses.Initialize(graphics);

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
            gameProcesses.LoadContent(Content);

            // TODO: use this.Content to load your game content here
            // tower = Content.Load<Texture2D>(t.ImageStr);
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
            gameProcesses.Update(graphics, this, gameProcesses);


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            gameProcesses.Draw(spriteBatch, graphics);
            

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
