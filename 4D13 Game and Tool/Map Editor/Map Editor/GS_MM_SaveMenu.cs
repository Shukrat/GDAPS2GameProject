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
    class GS_MM_SaveMenu : GameState
    {
        // Load content needed for this GameState
        public override void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("Fonts/mainFont");
        }

        // Returns an enum to GameProcessess when certani criteria are met
        // Otherwise, runs code for update for Map Editor Save Menu
        public override GameProcesses.GameStateEnum Update(GraphicsDeviceManager graphics, Game1 game1)
        {
            // SaveLoad Calls:
            // Set SaveLoad saveComplete bool to false in order to wait for user input
            // Send bool if on load screen or save screen
            // Set SaveLoad textures 2D array to this one
            // Call SaveLoad Update (user input)
            // Saves to binary file
            GameProcesses.saveLoad.SaveComplete = false;
            GameProcesses.saveLoad.SaveMenu = true;
            //saveLoad.Textures = textures;
            GameProcesses.saveLoad.Update(kState, prevKState);

            // Waits for user to hit enter in SaveLoad.Update method
            // moves to Update - Map Maker in this Update method
            if (GameProcesses.saveLoad.SaveComplete)
            {

                return GameProcesses.GameStateEnum.mapEdit_MapEditor;
            }

            return GameProcesses.GameStateEnum.mapEdit_SaveMenu;
        }

        // Draws objects for this GameState
        public override void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            // Update kState and prevKState before running SaveLoad update programming
            prevKState = kState;
            kState = Keyboard.GetState();

            // Set font, and draw the text.
            GameProcesses.saveLoad.ConsoleFont = font;
            GameProcesses.saveLoad.Draw(spriteBatch);
        }
    }
}
