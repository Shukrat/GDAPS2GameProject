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
using System.Threading;

namespace Map_Editor
{
    class GS_MM_LoadMenu : GameState
    {
        // Load content needed for this GameState
        public override void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("Fonts/mainFont");
        }

        // Returns an enum to GameProcessess when certani criteria are met
        // Otherwise, runs code for update for Map Editor Load Menu
        public override GameProcesses.GameStateEnum Update(GraphicsDeviceManager graphics, Game1 game1)
        {
            // SaveLoad Calls:
            // Set SaveLoad loadComplete bool to false in order to wait for user input
            // Set SaveLoad textures 2D array to this one
            // Send bool if on load screen or save screen
            // Call SaveLoad Update (user input)
            // Set this textures 2D array to SaveLoad's loaded 2D array
            GameProcesses.saveLoad.LoadComplete = false;
            GameProcesses.saveLoad.Textures = textures;
            GameProcesses.saveLoad.LoadMenu = true;
            GameProcesses.saveLoad.Update(kState, prevKState);
            textures = GameProcesses.saveLoad.Textures;

            // Waits for user hits enter in SaveLoad.Update method
            // moves to Map Maker Update Bool in this Update method
            if (GameProcesses.saveLoad.LoadComplete)
            {
                return GameProcesses.GameStateEnum.mapEdit_MapEditor;

            }

            return GameProcesses.GameStateEnum.mapEdit_LoadMenu;

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
