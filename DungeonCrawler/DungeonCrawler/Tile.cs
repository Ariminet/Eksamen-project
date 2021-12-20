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
        public static List<Tile> tiles;
        private Vector2 position;
        public static int radius = 64;
        private Vector2 tileCoord = new Vector2(0, 0);
        private int doorLevel;
        public Texture2D tileTexture;

        public Tile(Vector2 newPos, Vector2 newTileCoord, Texture2D texture)
        {
            position = newPos;
            tileCoord = newTileCoord;
            tileTexture = texture;
            
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public Vector2 TileCoord 
        { 
            get { return tileCoord; }
        }

      


        public static void CreateMap(GameTime gameTime, GraphicsDeviceManager _graphics,Texture2D floorTexture, Texture2D[] textures,int newMap)
        {
            int gameWidth = _graphics.PreferredBackBufferWidth - radius;
            int gameHeight = _graphics.PreferredBackBufferHeight - radius;
            // Laver en ny liste med tiles for hver class Tile der instancieres
            tiles = new List<Tile>();


            for (int y = 64; y <= gameHeight; y += 128)
            {
                for (int x = 64; x <= gameWidth; x += 128)
                {
                    if(x == 64 || x == 1216 )
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


                }

            }

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
