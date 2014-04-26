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
using System.Globalization;
using System.Reflection;
using System.IO;

namespace Map_Editor
{
    class SaveLoad
    {
        // Attributes:
        #region Attributes - Fonts
        private SpriteFont font;
        #endregion

        #region Attributes - Strings
        private string fileName;
        private string lineBuffer;
        #endregion

        #region Attributes - Bools
        private bool lineDelay;
        private bool saveMenu;
        private bool saveComplete = false;
        private bool loadMenu;
        private bool loadComplete = false;
        #endregion

        #region Attributes - 2D Arrays
        // int 2D array
        private Rectangle[,] tiles;
        private int[,] textures;
        #endregion

        // Properties:
        #region Property - ConsoleFont
        public SpriteFont ConsoleFont
        {
            get { return font; }
            set { font = value ?? font; }
        }
        #endregion

        #region Property - Get/Set Tiles Array
        public Rectangle[,] Tiles
        {
            get { return tiles; }
            set { tiles = value; }
        }
        #endregion

        #region Property - Get/Set Textures Array
        public int[,] Textures
        {
            get { return textures; }
            set { textures = value; }
        }
        #endregion

        #region Property - Get/Set LoadMenu bool
        public bool LoadMenu
        {
            get { return loadMenu; }
            set { loadMenu = value; }
        }
        #endregion

        #region Property - Get/Set SaveMenu bool
        public bool SaveMenu
        {
            get { return saveMenu; }
            set { saveMenu = value; }
        }
        #endregion

        #region Property - Get/Set SaveComplete Bool
        public bool SaveComplete
        {
            get { return saveComplete; }
            set { saveComplete = value; }
        }
        #endregion

        #region Property - Get/Set LoadComplete bool
        public bool LoadComplete
        {
            get { return loadComplete; }
            set { loadComplete = value; }
        }
        #endregion

        // Constructor
        public SaveLoad()
        {
            lineDelay = false;
        }

        // Methods - Save, Load, Update, Draw
        #region Method - Save
        // Save - Takes tiles 2D array and saves to binary file
        // fileName is loaded from SaveLoad.Update()
        public void Save(string fileName)
        {
            Stream str = null;
            BinaryWriter output = null;
            try
            {
                str = File.OpenWrite(fileName + ".dat");

                output = new BinaryWriter(str);

                for (int x = 0; x < 20; x++)
                {
                    for (int y = 0; y < 20; y++)
                    {
                        // Take value out of array and then write value to file
                        Int32 value = textures[x,y];
                        output.Write(value);
                    }
                }
                output.Close();
                str.Close();
            }
            catch (IOException ioe)
            {
                Console.WriteLine("Message: " + ioe.Message);
            }
        }
        #endregion

        #region Method - Load
        // Load - Takes binary file requested, assigns data to tiles 2D array
        // fileName is loaded from SaveLoad.Update()
        public void Load(string fileName)
        {
            BinaryReader input = null;
            try
            {
                input = new BinaryReader(File.OpenRead(fileName + ".dat"));

                for (int x = 0; x < 20; x++)
                {
                    for (int y = 0; y < 20; y++)
                    {
                        Int32 value = input.ReadInt32();
                        textures[x, y] = value;
                    }
                }

                input.Close();
            }
            catch (IOException ioe)
            {
                Console.WriteLine("Message: " + ioe.Message);
            }
        }
        #endregion

        #region Method - Update
        // Method is meant to read keyboard state, add it to lineBuffer string
        // And save upon hitting enter
        public void Update(KeyboardState kState, KeyboardState prevKState)
        {
            // Detect if the current or previous keystates were the Enter key
            // If NOT, then...
            if (!(kState.IsKeyDown(Keys.Enter) || prevKState.IsKeyDown(Keys.Enter)))
            {
                // Create array to store pressed keys during this time
                Keys[] pressedKeys = kState.GetPressedKeys();

                // Loop through the length of the above created array,
                // Apply
                for (int i = 0; i < pressedKeys.Length; i++)
                {
                    if (kState.IsKeyDown(pressedKeys[i]) && !prevKState.IsKeyDown(pressedKeys[i]))
                    {
                        switch (pressedKeys[i])
                        {
                            #region CASE - DO NOTHING KEYS
                            // These keys do nothing, and simply just break the switch, returning to loop
                            case Keys.Apps:
                            case Keys.Attn:
                            case Keys.BrowserBack:
                            case Keys.BrowserFavorites:
                            case Keys.BrowserForward:
                            case Keys.BrowserHome:
                            case Keys.BrowserRefresh:
                            case Keys.BrowserSearch:
                            case Keys.BrowserStop:
                            case Keys.ChatPadGreen:
                            case Keys.ChatPadOrange:
                            case Keys.Crsel:
                            case Keys.End:
                            case Keys.EraseEof:
                            case Keys.Escape:
                            case Keys.Execute:
                            case Keys.Exsel:
                            case Keys.F1:
                            case Keys.F10:
                            case Keys.F11:
                            case Keys.F12:
                            case Keys.F13:
                            case Keys.F14:
                            case Keys.F15:
                            case Keys.F16:
                            case Keys.F17:
                            case Keys.F18:
                            case Keys.F19:
                            case Keys.F2:
                            case Keys.F20:
                            case Keys.F21:
                            case Keys.F22:
                            case Keys.F23:
                            case Keys.F24:
                            case Keys.F3:
                            case Keys.F4:
                            case Keys.F5:
                            case Keys.F6:
                            case Keys.F7:
                            case Keys.F8:
                            case Keys.F9:
                            case Keys.Help:
                            case Keys.Home:
                            case Keys.ImeConvert:
                            case Keys.ImeNoConvert:
                            case Keys.Insert:
                            case Keys.Kana:
                            case Keys.Kanji:
                            case Keys.LaunchApplication1:
                            case Keys.LaunchApplication2:
                            case Keys.LaunchMail:
                            case Keys.Left:
                            case Keys.LeftAlt:
                            case Keys.LeftControl:
                            case Keys.LeftShift:
                            case Keys.LeftWindows:
                            case Keys.MediaNextTrack:
                            case Keys.MediaPlayPause:
                            case Keys.MediaPreviousTrack:
                            case Keys.MediaStop:
                            case Keys.None:
                            case Keys.NumLock:
                            case Keys.Oem8:
                            case Keys.OemAuto:
                            case Keys.OemClear:
                            case Keys.OemCopy:
                            case Keys.OemEnlW:
                            case Keys.OemTilde:
                            case Keys.Pa1:
                            case Keys.PageDown:
                            case Keys.PageUp:
                            case Keys.Pause:
                            case Keys.Play:
                            case Keys.Print:
                            case Keys.PrintScreen:
                            case Keys.ProcessKey:
                            case Keys.Right:
                            case Keys.RightAlt:
                            case Keys.RightControl:
                            case Keys.RightShift:
                            case Keys.RightWindows:
                            case Keys.Scroll:
                            case Keys.Select:
                            case Keys.SelectMedia:
                            case Keys.Separator:
                            case Keys.Sleep:
                            case Keys.OemSemicolon:
                            case Keys.VolumeDown:
                            case Keys.VolumeMute:
                            case Keys.VolumeUp:
                            case Keys.Zoom:
                            // ::::From here::::
                            // These keys may be placed back in other cases
                            // to write additional symbols, or add functionality
                            case Keys.Tab:
                            case Keys.OemOpenBrackets:
                            case Keys.OemCloseBrackets:
                            case Keys.OemPipe:
                            case Keys.OemPlus:
                            case Keys.Add:
                            case Keys.Multiply:
                            case Keys.Divide:
                            case Keys.Decimal:
                            case Keys.OemPeriod:
                            case Keys.OemComma:
                            case Keys.Up:
                            case Keys.Down:
                            // ::::To here::::
                                break;
                            #endregion

                            #region CASE - DO SOMETHING KEYS
                            // These keys are functional for typing, including letters, some symbols,
                            // and numbers.
                            // These keys are customized for file saving, so no slashes, etc may be stored in file name
                            case Keys.OemMinus:
                            case Keys.Subtract:
                                lineBuffer += "-";
                                break;
                            case Keys.D0:
                                lineBuffer += "0";
                                break;
                            case Keys.D1:
                                lineBuffer += "1";
                                break;
                            case Keys.D2:
                                lineBuffer += "2";
                                break;
                            case Keys.D3:
                                lineBuffer += "3";
                                break;
                            case Keys.D4:
                                lineBuffer += "4";
                                break;
                            case Keys.D5:
                                lineBuffer += "5";
                                break;
                            case Keys.D6:
                                lineBuffer += "6";
                                break;
                            case Keys.D7:
                                lineBuffer += "7";
                                break;
                            case Keys.D8:
                                lineBuffer += "8";
                                break;
                            case Keys.D9:
                                lineBuffer += "9";
                                break;
                            case Keys.NumPad0:
                                lineBuffer += "0";
                                break;
                            case Keys.NumPad1:
                                lineBuffer += "1";
                                break;
                            case Keys.NumPad2:
                                lineBuffer += "2";
                                break;
                            case Keys.NumPad3:
                                lineBuffer += "3";
                                break;
                            case Keys.NumPad4:
                                lineBuffer += "4";
                                break;
                            case Keys.NumPad5:
                                lineBuffer += "5";
                                break;
                            case Keys.NumPad6:
                                lineBuffer += "6";
                                break;
                            case Keys.NumPad7:
                                lineBuffer += "7";
                                break;
                            case Keys.NumPad8:
                                lineBuffer += "8";
                                break;
                            case Keys.NumPad9:
                                lineBuffer += "9";
                                break;
                            case Keys.Space:
                                lineBuffer += " ";
                                break;
                            case Keys.Delete:
                            case Keys.Back:
                                {
                                    if (lineBuffer.Length > 0)
                                    {
                                        lineBuffer = lineBuffer.Remove(lineBuffer.Length - 1);
                                    }
                                    break;
                                }
                            default:
                                {
                                    lineBuffer += (pressedKeys[i].ToString().ToLower() ?? string.Empty);
                                    break;
                                }
                            #endregion
                        }
                    }
                }

                lineDelay = false;
            }
                // Once the user hits enter, this code runs:
            else if (!lineDelay)
            {
                // saveMenu value is passed from MapProcesses class
                if (saveMenu)
                {
                    // Sets the fileName string to the above detected string
                    // Calls save method using string
                    lineDelay = true;
                    fileName = lineBuffer;
                    Save(fileName);

                    // Once method runs, sets saveComplete bool to true
                    // Moves MapProcesses Update method from Update - Save Menu
                    // to Update - Map Maker in Update method
                    saveComplete = true;
                    saveMenu = false;
                }

                // loadMenu value is passed from MapProcesses class
                // Sets the fileName string to the above detected string
                // Calls load method using string
                if (loadMenu)
                {
                    lineDelay = true;
                    fileName = lineBuffer;
                    Load(fileName);

                    // Once method runs, sets loadComplete bool to true
                    // Moves MapProcesses Update method from Update - Load Menu
                    // to Update - Map Maker in Update method
                    loadComplete = true;
                    loadMenu = false;
                }
            }
        }
        #endregion

        #region Method - Draw
        // Draws the text detected in SaveLoad.Update()
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Enter desired map file name: " + lineBuffer, Vector2.Zero, Color.Black);
        }
        #endregion
    }
}