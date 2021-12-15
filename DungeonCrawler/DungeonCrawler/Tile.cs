using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DungeonCrawler
{
    class Tile
    {
        //Tile Variables
        //List of Tiles to be rendered
        public static List<Tile> tiles;
        // position of each tile
        private Vector2 position;
        ///radius of each tile
        public static int radius = 64;
        //current coordinate for each tile
        private Vector2 tileCoord = new Vector2(0, 0);
        //if tile is door what level it takes you too
        private int doorLevel = -1;
        //Texture of tile door / floor / wall
        public Texture2D tileTexture;


        private static Random rNum = new Random();

        private Color textureColor;
        private bool magicDoor = false;

        //Tile Constructor
        public Tile(Vector2 newPos, Vector2 newTileCoord, Texture2D texture)
        {
            position = newPos;
            tileCoord = newTileCoord;
            tileTexture = texture;
            
        }
        public Tile(Vector2 newPos, Vector2 newTileCoord, Texture2D texture, int doorLvl)
        {
            position = newPos;
            tileCoord = newTileCoord;
            tileTexture = texture;
            doorLevel = doorLvl;

        }
        public Tile(Vector2 newPos, Vector2 newTileCoord, Texture2D texture, int doorLvl, Color tColor, bool isMagic)
        {
            position = newPos;
            tileCoord = newTileCoord;
            tileTexture = texture;
            doorLevel = doorLvl;
            textureColor = tColor;
            magicDoor = isMagic;

        }

        // position property
        public Vector2 Position
        {
            get { return position; }
        }

        //Tile coordinate property
        public Vector2 TileCoord 
        { 
            get { return tileCoord; }
        }

      public int DoorLevel
        {
            get { return doorLevel; }
        }

        public bool MagicDoor
        {
            get { return magicDoor; }
            set { magicDoor = value; }
        }

        public Color TextureColor
        {
            get { return textureColor; }
        }

        /// <summary>
        /// Generate List of Tiles and aquires tile coorinates while adding to List of tiles
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="_graphics">to aquire game size for loops</param>
        /// <param name="floorTexture">floor texture</param>
        /// <param name="textures">wall textures </param>
        /// <param name="door">door texture</param>
        /// <param name="newMap">newMap = nextlevel variable</param>
        public static void CreateMap(GameTime gameTime, GraphicsDeviceManager _graphics,Texture2D floorTexture, Texture2D[] textures, Texture2D door, int newMap)
        {
            int gameWidth = _graphics.PreferredBackBufferWidth - radius;
            int gameHeight = _graphics.PreferredBackBufferHeight - radius;
            // Laver en ny liste med tiles for hver class Tile der instancieres
            tiles = new List<Tile>();

            int maybeDoor;



            for (int y = 64; y <= gameHeight; y += 128)
            {
                for (int x = 64; x <= gameWidth; x += 128)
                {
                    //addes door 
                    
                    
                        
                    //loops thru walls and places them around the tiles
                    if (x == 64 || x == 1216 )
                    {
                        //Rotate texture on both sides
                        if(x == 64 && y == 64)
                        {
                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(-1, -1), textures[5]));

                        }else if (x == 64 && y != 64) {
                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(-1, -1), textures[3]));
                        }else if (x == 1216 && y == 64)
                        {
                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(-1, -1), textures[6]));
                        }else if (x == 1216 && y != 64)
                        {
                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(-1, -1), textures[4]));
                        }


                    }
                    if (y == 64 || y == 704)
                    {
                        // rotate texture at bottom 704
                        
                        if(y == 64 && x != 64 && x != 1216)
                        {
                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(-1, -1), textures[1]));
                        }
                        else if(y == 704 && x == 64)
                        {
                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(-1, -1), textures[7]));
                        }
                        else if (y == 704 && x == 1216)
                        {
                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(-1, -1), textures[8]));
                        }
                        else if (y == 704 && x != 64)
                        {
                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(-1, -1), textures[2]));
                        }
                        


                    }
                    // door creation for each level
                    if (x == 576 || y == 320)
                    {
                        if (newMap < 15)
                        {
                            switch (newMap)
                            {
                                case 0:
                                    maybeDoor = rNum.Next(0, 3);
                                    if (y == 320 && x == 1216)
                                    {
                                        tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, newMap + 1));
                                    }
                                    
                                    if (maybeDoor == 1 && x == 576 && y == 704)
                                    {
                                        tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, rNum.Next(0, 14), Color.Purple, true));
                                    }

                                    break;
                                case 1:
                                    if (y == 320 || x == 576)
                                    {
                                        maybeDoor = rNum.Next(0, 3);
                                        if (y == 320 && x == 64)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, newMap - 1));
                                        }
                                        if (y == 320 && x == 1216)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, newMap + 1));
                                        }
                                        if (maybeDoor == 1 && x == 576 && y == 704)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, rNum.Next(0, 14), Color.Purple, true));
                                        }

                                    }

                                    break;
                                case 2:
                                    if (y == 320 || x == 576)
                                    {
                                        if (y == 320 && x == 64)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, newMap - 1));
                                        }
                                        if (x == 576 && y == 704)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, newMap + 1));
                                        }

                                    }
                                    break;
                                case 3:
                                    if (y == 320 || x == 576)
                                    {
                                        maybeDoor = rNum.Next(0, 3);
                                        if (y == 320 && x == 64)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, newMap + 1));
                                        }
                                        if (maybeDoor == 1 && x == 576 && y == 704)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, rNum.Next(0, 14), Color.Purple, true));
                                        }
                                        if (y == 64 && x == 576)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, newMap - 1));
                                        }


                                    }
                                    break;
                                case 4 :
                                    if (y == 320 || x == 576)
                                    {
                                        maybeDoor = rNum.Next(0, 3);
                                        // center left door
                                        if (y == 320 && x == 64)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, newMap + 1));
                                        }
                                        // center right door
                                        if (y == 320 && x == 1216)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, newMap - 1));
                                        }
                                        // bottom center door
                                        if (maybeDoor == 1 && x == 576 && y == 704)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, rNum.Next(0, 14), Color.Purple, true));
                                        }
                                        // top center door

                                        if (maybeDoor == 1 && x == 576 && y == 64)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, rNum.Next(0, 14), Color.Purple, true));
                                        }
                                        


                                    }
                                    break;
                                case 5:
                                    if (y == 320 || x == 576)
                                    {
                                        maybeDoor = rNum.Next(0, 3);
                                        // Center Right door
                                        if (y == 320 && x == 1216)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, newMap - 1));
                                        }
                                        // bottom center door
                                        if (y == 704 && x == 576)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, newMap + 1));
                                        }
                                        // top center door
                                        if (maybeDoor == 1 && x == 576 && y == 64)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, rNum.Next(0, 14), Color.Purple, true));
                                        }


                                    }
                                    break;
                                case 6:
                                    if (y == 320 || x == 576)
                                    {
                                        
                                        // center right door
                                        if (y == 320 && x == 1216)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, newMap + 1));
                                        }
                                      
                                        // top center door
                                        if (y == 64 && x == 576)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, newMap - 1));
                                        }

                                        // random door bottom center
                                        maybeDoor = rNum.Next(0, 3);
                                        if (maybeDoor == 1 && x == 576 && y == 704)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, rNum.Next(0, 14), Color.Purple, true));
                                        }


                                    }
                                    break;
                                case 7:
                                    if (y == 320 || x == 576)
                                    {
                                        maybeDoor = rNum.Next(0, 3);
                                        // center left door
                                        if (y == 320 && x == 64)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, newMap - 1));
                                        }
                                        // center right door
                                        if (y == 320 && x == 1216)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, newMap + 1));
                                        }
                                        // bottom center door
                                        
                                        if (maybeDoor == 1 && x == 576 && y == 704)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, rNum.Next(0, 14), Color.Purple, true));
                                        }
                                        // top center door
                                        if (maybeDoor == 1 && x == 576 && y == 64)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, rNum.Next(0, 14)));
                                        }



                                    }
                                    break;
                                case 8:
                                    if (y == 320 || x == 576)
                                    {
                                        maybeDoor = rNum.Next(0, 3);
                                        // center left door
                                        if (y == 320 && x == 64)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, newMap - 1));
                                        }
                                     
                                        // bottom center door
                                        if (y == 704 && x == 576)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, newMap + 1));
                                        }
                                        
                                        // top center door
                                        if (maybeDoor == 1 && x == 576 && y == 64)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, rNum.Next(0, 14), Color.Purple, true));
                                        }



                                    }
                                    break;
                                case 9:
                                    if (y == 320 || x == 576)
                                    {
                                        maybeDoor = rNum.Next(0, 3);
                                        // center left door
                                        if (y == 320 && x == 64)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, newMap + 1));
                                        }
                                        
                                        
                                        // top center door
                                        if (y == 64 && x == 576)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, newMap - 1));
                                        }

                                        // random door bottom center
                                        
                                        // bottom center door

                                        if (maybeDoor == 1 && x == 576 && y == 704)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, rNum.Next(0, 14), Color.Purple, true));
                                        }


                                    }
                                    break;
                                case 10:
                                    if (y == 320 || x == 576)
                                    {
                                        maybeDoor = rNum.Next(0, 3);
                                        // center left door
                                        if (y == 320 && x == 64)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, newMap + 1));
                                        }
                                        // center right door
                                        if (y == 320 && x == 704)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, newMap - 1));
                                        }
                                        // bottom center door

                                        if (maybeDoor == 1 && x == 576 && y == 704)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, rNum.Next(0, 14), Color.Purple, true));
                                        }
                                        // top center door
                                        if (maybeDoor == 1 && x == 576 && y == 64)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, rNum.Next(0, 14), Color.Purple, true));
                                        }


                                    }
                                    break;
                                case 11:
                                    if (y == 320 || x == 576)
                                    {
                                        maybeDoor = rNum.Next(0, 3);
                                        // center right door
                                        if (y == 320 && x == 1216)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, newMap - 1));
                                        }
                                        // bottom center door
                                        if (y == 704 && x == 576)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, newMap - 5));
                                        }
                                        // top center door
                                        if (maybeDoor == 1 && x == 576 && y == 64)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, rNum.Next(0, 14), Color.Purple, true));
                                        }




                                    }
                                    break;
                                case 12:
                                    if (y == 320 || x == 576)
                                    {
                                        maybeDoor = rNum.Next(0, 3);
                                        // center right door
                                        if (y == 320 && x == 1216)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, newMap + 1));
                                        }
                                        
                                        // top center door
                                        if (y == 64 && x == 576)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, newMap - 1));
                                        }
                                        // bottom center door

                                        if (maybeDoor == 1 && x == 576 && y == 704)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, rNum.Next(0, 14), Color.Purple, true));
                                        }
                                        




                                    }
                                    break;
                                case 13:
                                    if (y == 320 || x == 576)
                                    {
                                        maybeDoor = rNum.Next(0, 3);
                                        // center left door
                                        if (y == 320 && x == 64)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, newMap - 1));
                                        }
                                        // center right door
                                        if (y == 320 && x == 1216)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, newMap + 1));
                                        }

                                        // bottom center door
                                        if (maybeDoor == 1 && x == 576 && y == 704)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, rNum.Next(0, 14), Color.Purple, true));
                                        }
                                        // top center door
                                        if (maybeDoor == 1 && x == 576 && y == 64)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, rNum.Next(0, 14), Color.Purple, true));
                                        }




                                    }
                                    break;
                                case 14:
                                    if (y == 320 || x == 576)
                                    {
                                        
                                        // center right door
                                        if (y == 320 && x == 1216)
                                        {
                                            tiles.Add(new Tile(new Vector2(x, y), new Vector2(100, 100), door, newMap + 1));
                                        }
                                       


                                    }
                                    break;
                            }

                        }

                    }

                }

            }
            // creates map of tiles (floor)
            int coordY = 0;
            for (int y = 192; y < gameHeight; y += 128)
            {
                int coordX = 0;
                coordY++;
                for (int x = 192; x < gameWidth; x += 128)
                {
                    coordX++;
                    tiles.Add(new Tile(new Vector2(x, y), new Vector2(coordX, coordY), floorTexture));


                }

            }
            
        }

       
        
        public static void Update(GameTime gameTime, GraphicsDeviceManager _graphics, Texture2D floor)
        {
            
        }
    }
}
