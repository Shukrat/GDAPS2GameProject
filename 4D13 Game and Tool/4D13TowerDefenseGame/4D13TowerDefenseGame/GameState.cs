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

namespace _4D13TowerDefenseGame
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
        // Tiles
        // Path tiles
        public Texture2D pathDL_Tile;
        public Texture2D pathDR_Tile;
        public Texture2D pathUL_Tile;
        public Texture2D pathUR_Tile;
        public Texture2D pathET_Tile;
        public Texture2D pathLeftRight_Tile;
        public Texture2D pathNT_Tile;
        public Texture2D pathST_Tile;
        public Texture2D pathUpDown_Tile;
        public Texture2D pathWT_Tile;

        // Object tiles
        public Texture2D grayTile;     // OBSOLETE
        public Texture2D blueTile;     // OBSOLETE
        public Texture2D greenTile;    // OBSOLETE
        public Texture2D pathTile;     // OBSOLETE

        // Main Menu - Textures for buttons / interface
        public Texture2D mainMenu_Exit_Txtr;
        public Texture2D mainMenu_ExitHover_Txtr;
        public Texture2D mainMenu_ExitClick_Txtr;
        public Texture2D mainMenu_Load_Txtr;
        public Texture2D mainMenu_New_Txtr;

        // Map Editor - Textures for buttons / interface
        public Texture2D mapEdit_Save_Txtr;
        public Texture2D mapEdit_Exit_Txtr;
        public Texture2D mapEdit_SideBar_Txtr;

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
        public bool loadMenu;
        public bool saveMenu;
        #endregion

        #region Attributes - Other stuff
        // Used to draw interface on mapeditor once
        public bool interfaceMade = false;
        #endregion

        #region Attributes - Texture Selection Booleans
        // Bools for keystates
        public bool tfGrayTile;
        public bool tfBlueTile;
        public bool tfGreenTile;
        public bool tfPathTile;
        #endregion

        #region Attributes - Rectangles
        // Mouse position Rectangle
        public Rectangle mousePos;

        // Retangles for tile selection options -- PALLETTE CLASS
        public Rectangle selectGray;   // OBSOLETE
        public Rectangle selectBlue;   // OBSOLETE
        public Rectangle selectGreen;  // OBSOLETE
        public Rectangle selectPath;   // OBSOLETE

        public Rectangle select_PathDL; // Corner from left to bottom
        public Rectangle select_PathDR; // Corner from right to bottom
        public Rectangle select_PathUL; // Corner from left to top
        public Rectangle select_PathUR; // Corner from right to top
        public Rectangle select_PathET; // T-intersect from right
        public Rectangle select_PathLeftRight;  // Path left to right
        public Rectangle select_PathNT; // T-intersect from top
        public Rectangle select_PathST; // T-intersect from bottom
        public Rectangle select_PathUpDown;     // Path from top to bottom
        public Rectangle select_PathWT; // T-intersect from left

        // Rectangle to show currently selected tile
        public Rectangle paintBrush;

        // Rectangles for Menus / Buttons - MENU CLASS - GOOGLE: STATE DESIGN PATTERN
        // Main Menu
        public Rectangle mainMenu_ExitRec;
        public Rectangle mainMenu_LoadRec;
        public Rectangle mainMenu_NewRec;
        // Map Editor
        public Rectangle mapEdit_ExitRec;
        public Rectangle mapEdit_SaveRec;
        public Rectangle mapEdit_BackRec;
        public Rectangle mapEdit_PathTilesRec;  // Button displays only path tiles to paint
        public Rectangle sideBarBG;
        #endregion

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
                    newTile.X = 45 * x;
                    newTile.Y = 45 * i;
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
    }
}
