using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace Map_Editor
{
    class SaveLoad
    {
        // Attributes
        private int[,] textures;
        private Rectangle[,] tiles;
        private string fileName;

        // Constructor
        public SaveLoad(int[,] textures, Rectangle[,] tiles, string fileName)
        {
            this.textures = textures;
            this.tiles = tiles;
            this.fileName = fileName;
        }

        public void Save()
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
                        output.Write(textures[x, y]);
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

        public void Load()
        {
            BinaryReader input = null;
            try
            {
                input = new BinaryReader(File.OpenRead(fileName + ".dat"));

                for (int x = 0; x < 20; x++)
                {
                    for (int y = 0; y < 20; y++)
                    {
                        textures[x, y] = input.ReadInt32();
                    }
                }

                input.Close();
            }
            catch (IOException ioe)
            {
                Console.WriteLine("Message: " + ioe.Message);
            }
        }
    }
}
