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
    public class Game1 : Game
    {
        // Object Creation

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
       // Tower t = new Tower(50, 20, 400, 300, 50, 50, "Image", "Image", 100, 5, "");
       // Texture2D tower;
       //// Enemy e = new Enemy(50, 20, 200, 200, 50, 50, "Image", 100, 100, 100, false, false);
       // List<Enemy> enemies;
       // Random rgen = new Random();


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

            //enemies = new List<Enemy>();

            //for (int i = 0; i < 20; i++)
            //{
            //    enemies.Add(new Enemy(50, 20, 0, (i * 40), 50, 50, "Image", 1, 100, 10, false, false));
            //}

            

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
            // Object Update
            //for (int i = 0; i < enemies.Count; i++)
            //{
            //    if (enemies[i] != null)
            //    {
            //        if (t.HitBox.Intersects(enemies[i].PieceShape))
            //        {
            //            t.AttackEnemy(enemies[i]);
            //        }
            //        if (enemies[i].Alive == false)
            //        {
            //            enemies[i] = null;
            //            t.shot = null;
            //        }
            //    }
            //    if (enemies[i] != null)
            //    {
            //        enemies[i].Move();
            //        if (enemies[i].PieceShape.X > 800)
            //        {
            //            enemies[i].MoraleAttack();
            //            enemies[i] = null;
            //            t.shot = null;
            //        }
            //    }
            //    else if (enemies[i] == null)
            //    {
            //        enemies[i] = new Enemy(50, 20, 0, rgen.Next(0, 601), 50, 50, "Image", 1, 100, 10, false, false);
            //    }
            //}


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);
            //gameState.ChangeState(1);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            gameProcesses.Draw(spriteBatch, graphics);
            

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
