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
    abstract class GameState
    {
        #region Attributes - 2D Arrays
        // 2D arrays for map information
        // Static to ensure only one copy is called
        public static Rectangle[,] tiles;
        public static int[,] textures;
        #endregion

        #region Attributes - Texture2D
        // Path tiles
        public Texture2D eraser_Tile; // ERASER
        public Texture2D pathDL_Tile;
        public Texture2D pathDR_Tile;
        public Texture2D pathUL_Tile;
        public Texture2D pathUR_Tile;
        public Texture2D pathLeftRight_Tile;
        public Texture2D pathUpDown_Tile;

        // Collidable tiles
        public Texture2D obj_Boulder;
        public Texture2D obj_Tree;

        // Spawn / Goal Tiles
        public Texture2D spawn;
        public Texture2D goal;

        // Background Images
        public Texture2D bg_MapEditMainMenu;
        public Texture2D bg_Desert;
        public Texture2D bg_Grasslands;
        public Texture2D bg_Tundra;

        // Game
        public Texture2D game_GameBorder_Txtr;

        // Main Menu - Textures for buttons / interface
        public Texture2D mainMenu_Exit_Txtr;        // This texture also used for map editor Exit button
        public Texture2D mainMenu_ExitHover_Txtr;   // This texture also used for map editor Exit button
        public Texture2D mainMenu_ExitClick_Txtr;   // This texture also used for map editor Exit button
        public Texture2D mainMenu_Load_Txtr;
        public Texture2D mainMenu_LoadHover_Txtr;
        public Texture2D mainMenu_LoadClick_Txtr;
        public Texture2D mainMenu_New_Txtr;
        public Texture2D mainMenu_NewHover_Txtr;
        public Texture2D mainMenu_NewClick_Txtr;

        // Map Editor - Textures for buttons / interface
        public Texture2D mapEdit_Tiles_Txtr;
        public Texture2D mapEdit_TilesHover_Txtr;
        public Texture2D mapEdit_TilesClick_Txtr;
        public Texture2D mapEdit_Back_Txtr;
        public Texture2D mapEdit_BackHover_Txtr;
        public Texture2D mapEdit_BackClick_Txtr;
        public Texture2D mapEdit_Menu_Txtr;
        public Texture2D mapEdit_MenuHover_Txtr;
        public Texture2D mapEdit_MenuClick_Txtr;
        public Texture2D mapEdit_Save_Txtr;
        public Texture2D mapEdit_SaveHover_Txtr;
        public Texture2D mapEdit_SaveClick_Txtr;
        public Texture2D mapEdit_SideBar_Txtr;
        public Texture2D mapEdit_BackSelect_Txtr;
        public Texture2D mapEdit_BackSelectHover_Txtr;
        public Texture2D mapEdit_BackSelectClick_Txtr;
        public Texture2D mapEdit_PathSelect_Txtr;
        public Texture2D mapEdit_PathSelectHover_Txtr;
        public Texture2D mapEdit_PathSelectClick_Txtr;

        // Save or Load Menu Font
        public SpriteFont font;
        #endregion

        #region Attributes - Mouse and Keyboard States
        // Keyboard and mouse states
        public KeyboardState kState;
        public KeyboardState prevKState;
        public MouseState mState;
        #endregion

        #region Attributes - Bools for Save/Load
        // Not assigned here but used in GG_MM_LoadMenu and GS_MM_SaveMenu
        // DO NOT DELETE
        public bool loadMenu;
        public bool saveMenu;
        #endregion

        #region Attributes - Texture Selection Booleans
        // Bools for keystates
        // Path selection
        public bool tf_Eraser;
        public bool tf_PathDL;
        public bool tf_PathDR;
        public bool tf_PathUL;
        public bool tf_PathUR;
        public bool tf_PathLeftRight;
        public bool tf_PathUpDown;

        // Object select
        public bool tf_Bolder;
        public bool tf_Tree;

        // Background select
        public bool tf_Grasslands;
        public bool tf_Desert;
        public bool tf_Tundra;

        // Change between background select and path select
        public bool tf_BackgroundSelect = false;
        public bool tf_PathSelect = false;
        #endregion

        #region Attributes - Rectangles
        // Mouse position Rectangle
        public Rectangle mousePos;

        // Retangles for tile selection options -- PALLETTE CLASS
        public Rectangle select_Eraser; 
        public Rectangle select_PathDL; // Corner from left to bottom
        public Rectangle select_PathDR; // Corner from right to bottom
        public Rectangle select_PathUL; // Corner from left to top
        public Rectangle select_PathUR; // Corner from right to top
        public Rectangle select_PathLeftRight;  // Path left to right
        public Rectangle select_PathUpDown;     // Path from top to bottom

        // Rectangle to show currently selected tile
        public Rectangle paintBrush;

        // Rectangles for Menus / Buttons - MENU CLASS - GOOGLE: STATE DESIGN PATTERN
        // Main Menu
        public Rectangle mainMenu_ExitRec;
        public Rectangle mainMenu_LoadRec;
        public Rectangle mainMenu_NewRec;

        // Game
        public Rectangle game_GameBorderRec;
        public Rectangle game_BackgroundRec;

        // Map Editor
        public Rectangle mapEdit_TilesRec;
        public Rectangle mapEdit_BackRec;
        public Rectangle mapEdit_MainBackground;
        public Rectangle mapEdit_MenuRec;
        public Rectangle mapEdit_ExitRec;
        public Rectangle mapEdit_SaveRec;
        public Rectangle mapEdit_BackSelectRec;
        public Rectangle mapEdit_PathTilesRec;  // Button displays only path tiles to paint
        public Rectangle sideBarBG;
        #endregion

        public static int saveLoadBackground;

        // Properties:
        #region Property - Get Rectangle 2D Array
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

        // Constructor
        public GameState()
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
                    newTile.X = (45 * x) + 10;
                    newTile.Y = (45 * i) + 10;
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
        // Update will be called during update phase of Game1
        abstract public GameProcesses.GameStateEnum Update(GraphicsDeviceManager graphics, Game1 game1);

        // Draw will be called during draw phase of Game1 
        abstract public void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics);

        // Load Content will be called on each GameState change
        abstract public void LoadContent(ContentManager Content);

        // Mouse Select Tiles - Detects collision between mouse rectangle and tile rectangles
        public void MouseTileSelect()
        {
            // Update mouse position at each call of this method in update in Game1
            mState = Mouse.GetState();
            // Detect collisions
            #region Eraser Select
            if (mousePos.Intersects(select_Eraser) && mState.LeftButton == ButtonState.Pressed)
            {
                tf_PathDL = false;
                tf_PathDR = false;
                tf_PathUL = false;
                tf_PathUR = false;
                tf_PathLeftRight = false;
                tf_PathUpDown = false;
                tf_Bolder = false;
                tf_Tree = false;
                tf_Eraser = true;
            }
            #endregion

            #region Tile Select - Paths
            if (mousePos.Intersects(select_PathDL) && mState.LeftButton == ButtonState.Pressed)
            {
                if (!tf_BackgroundSelect && tf_PathSelect)
                {
                    tf_PathDL = true;
                    tf_PathDR = false;
                    tf_PathUL = false;
                    tf_PathUR = false;
                    tf_PathLeftRight = false;
                    tf_PathUpDown = false;
                    tf_Bolder = false;
                    tf_Tree = false;
                    tf_Eraser = false;
                }
            }
            if (mousePos.Intersects(select_PathDR) && mState.LeftButton == ButtonState.Pressed && !tf_BackgroundSelect && tf_PathSelect)
            {
                if (!tf_BackgroundSelect && tf_PathSelect)
                {
                    tf_PathDL = false;
                    tf_PathDR = true;
                    tf_PathUL = false;
                    tf_PathUR = false;
                    tf_PathLeftRight = false;
                    tf_PathUpDown = false;
                    tf_Bolder = false;
                    tf_Tree = false;
                    tf_Eraser = false;
                }
            }
            if (mousePos.Intersects(select_PathUL) && mState.LeftButton == ButtonState.Pressed && !tf_BackgroundSelect && tf_PathSelect)
            {
                if (!tf_BackgroundSelect && tf_PathSelect)
                {
                    tf_PathDL = false;
                    tf_PathDR = false;
                    tf_PathUL = true;
                    tf_PathUR = false;
                    tf_PathLeftRight = false;
                    tf_PathUpDown = false;
                    tf_Bolder = false;
                    tf_Tree = false;
                    tf_Eraser = false;
                }
            }
            if (mousePos.Intersects(select_PathUR) && mState.LeftButton == ButtonState.Pressed && !tf_BackgroundSelect && tf_PathSelect)
            {
                if (!tf_BackgroundSelect && tf_PathSelect)
                {
                    tf_PathDL = false;
                    tf_PathDR = false;
                    tf_PathUL = false;
                    tf_PathUR = true;
                    tf_PathLeftRight = false;
                    tf_PathUpDown = false;
                    tf_Bolder = false;
                    tf_Tree = false;
                    tf_Eraser = false;
                }
            }
            if (mousePos.Intersects(select_PathLeftRight) && mState.LeftButton == ButtonState.Pressed && !tf_BackgroundSelect && tf_PathSelect)
            {
                if (!tf_BackgroundSelect && tf_PathSelect)
                {
                    tf_PathDL = false;
                    tf_PathDR = false;
                    tf_PathUL = false;
                    tf_PathUR = false;
                    tf_PathLeftRight = true;
                    tf_PathUpDown = false;
                    tf_Bolder = false;
                    tf_Tree = false;
                    tf_Eraser = false;
                }
            }
            if (mousePos.Intersects(select_PathUpDown) && mState.LeftButton == ButtonState.Pressed && !tf_BackgroundSelect && tf_PathSelect)
            {
                if (!tf_BackgroundSelect && tf_PathSelect)
                {
                    tf_PathDL = false;
                    tf_PathDR = false;
                    tf_PathUL = false;
                    tf_PathUR = false;
                    tf_PathLeftRight = false;
                    tf_PathUpDown = true;
                    tf_Bolder = false;
                    tf_Tree = false;
                    tf_Eraser = false;
                }
            }
            #endregion

            #region Tile Select - Objects
            if (mousePos.Intersects(select_PathDL) && mState.LeftButton == ButtonState.Pressed && !tf_BackgroundSelect && !tf_PathSelect)
            {
                if (!tf_BackgroundSelect && !tf_PathSelect)
                {
                    tf_PathDL = false;
                    tf_PathDR = false;
                    tf_PathUL = false;
                    tf_PathUR = false;
                    tf_PathLeftRight = false;
                    tf_PathUpDown = false;
                    tf_Bolder = true;
                    tf_Tree = false;
                    tf_Eraser = false;
                }
            }
            if (mousePos.Intersects(select_PathDR) && mState.LeftButton == ButtonState.Pressed && !tf_BackgroundSelect && !tf_PathSelect)
            {
                if (!tf_BackgroundSelect && !tf_PathSelect)
                {
                    tf_PathDL = false;
                    tf_PathDR = false;
                    tf_PathUL = false;
                    tf_PathUR = false;
                    tf_PathLeftRight = false;
                    tf_PathUpDown = false;
                    tf_Bolder = false;
                    tf_Tree = true;
                    tf_Eraser = false;
                }
            }
            #endregion

            #region Background Select
            if (mousePos.Intersects(select_PathDL) && mState.LeftButton == ButtonState.Pressed && tf_BackgroundSelect && !tf_PathSelect)
            {
                if (tf_BackgroundSelect && !tf_PathSelect)
                {
                    tf_Grasslands = true;
                    tf_Desert = false;
                    tf_Tundra = false;
                    saveLoadBackground = 1;
                }
            }
            if (mousePos.Intersects(select_PathDR) && mState.LeftButton == ButtonState.Pressed && tf_BackgroundSelect && !tf_PathSelect)
            {
                if (tf_BackgroundSelect && !tf_PathSelect)
                {
                    tf_Grasslands = false;
                    tf_Desert = true;
                    tf_Tundra = false;
                    saveLoadBackground = 2;
                }
            }
            if (mousePos.Intersects(select_PathUL) && mState.LeftButton == ButtonState.Pressed && tf_BackgroundSelect && !tf_PathSelect)
            {
                if (tf_BackgroundSelect && !tf_PathSelect)
                {
                    tf_Grasslands = false;
                    tf_Desert = false;
                    tf_Tundra = true; 
                    saveLoadBackground = 3;
                }
            }
            #endregion

            #region Change between Object, Background and Path selections
            if (mousePos.Intersects(mapEdit_BackSelectRec) && mState.LeftButton == ButtonState.Pressed)
            {
                tf_BackgroundSelect = true;
                tf_PathSelect = false;
            }
            if (mousePos.Intersects(mapEdit_PathTilesRec) && mState.LeftButton == ButtonState.Pressed)
            {
                tf_BackgroundSelect = false;
                tf_PathSelect = true;
            }
            if (mousePos.Intersects(mapEdit_TilesRec) && mState.LeftButton == ButtonState.Pressed)
            {
                tf_BackgroundSelect = false;
                tf_PathSelect = false;
            }
            #endregion

        }
    }
}
