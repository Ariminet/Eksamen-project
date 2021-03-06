using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace DungeonCrawler
{
    class Maps
    {
        public static List<Maps> mapsList = new List<Maps>();

        // den bruges ikke da foreach i DrawMap skal renders fra mapslist list<tile>
        private List<Tile> map;
        private int mapLevel = 0;
        

        public int MapLevel
        {
            get { return mapLevel; }
            set { mapLevel = value; }
        }
       
        public List<Tile> Map
        {
            get { return map; }
            set { map = value; }
        }
        public Maps(int newMapLevel, List<Tile> newMap)
        {
            // Laver en ny liste med Tile for hver class Map der instancieres
            map = new List<Tile>(newMap);
            mapLevel = newMapLevel;
        }

        public static void AddMap(int mapLevel, List<Tile> newMap)
        {
            mapsList.Add(new Maps(mapLevel, newMap));
        }


        public static void PlayerMoves(Player player , int newMap)
        {
            foreach (Tile t in mapsList[newMap].map)
            {
                int sum = 64;
                if(t.TileCoord != player.TileCoord) { 
                if (Vector2.Distance(player.TilePosition, player.Position) < 20)
                {
                    if (Vector2.Distance(player.pickedPos, t.Position) < sum)
                    {

                        int moveX = (int)t.TileCoord.X - (int)player.TileCoord.X;
                        int moveY = (int)t.TileCoord.Y - (int)player.TileCoord.Y;
                        if (moveX <= player.Velocity && moveX >= -player.Velocity && player.TileCoord.Y == t.TileCoord.Y)
                        {


                            player.TilePosition = t.Position;
                            player.StepsX = moveX;
                            player.TileCoord = t.TileCoord;


                        }
                        if (moveY <= player.Velocity && moveY >= -player.Velocity && player.TileCoord.X == t.TileCoord.X)
                        {

                            player.TilePosition = t.Position;
                            player.StepsY = moveY;
                            player.TileCoord = t.TileCoord;

                        }

                    }

                }
            }
            }
        }

        /// <summary>
        /// Draws Current Level on the screen
        /// </summary>
        /// <param name="_spriteBatch">Monogame Drawing ability</param>
        /// <param name="player">Player Values for position and coordinates</param>
        /// <param name="newMap">newMap = currentmap that needs to be rendered</param>
        /// <param name="font">font is the game text style used for drawing information</param>
        public static void DrawMap(SpriteBatch _spriteBatch, Player player, int newMap, SpriteFont font)
        {
            foreach (Tile t in mapsList[newMap].map)
            {
              
                
                if (t.tileTexture.ToString() == "./assets/gameImages/floorSmall" || t.tileTexture.ToString() == "./assets/gameImages/door/door") {
                    int canMoveX = (int)t.TileCoord.X - (int)player.TileCoord.X;
                    int canMoveY = (int)t.TileCoord.Y - (int)player.TileCoord.Y;

                    if (Vector2.Distance(player.TilePosition, player.Position) < 10)
                    {

                        
                       
                        if (canMoveY <= player.Velocity && canMoveY >= -player.Velocity && player.TileCoord.X == t.TileCoord.X && player.TileCoord != t.TileCoord)
                        {
                            _spriteBatch.Draw(t.tileTexture, new Vector2(t.Position.X - t.tileTexture.Width / 2, t.Position.Y - t.tileTexture.Height / 2), Color.Red);
                        }
                        else if (canMoveX <= player.Velocity && canMoveX >= -player.Velocity && player.TileCoord.Y == t.TileCoord.Y && player.TileCoord != t.TileCoord)
                        {
                            _spriteBatch.Draw(t.tileTexture, new Vector2(t.Position.X - t.tileTexture.Width / 2, t.Position.Y - t.tileTexture.Height / 2), Color.Red);
                        }else if ( Vector2.Distance(t.Position, player.Position) < 164 && t.tileTexture.ToString() == "./assets/gameImages/door/door")
                        {
                            // renders door green if player pos is within 164px of a door
                            _spriteBatch.Draw(t.tileTexture, new Vector2(t.Position.X - t.tileTexture.Width / 2, t.Position.Y - t.tileTexture.Height / 2), Color.Green);

                        }else if (t.MagicDoor)
                        {
                            _spriteBatch.Draw(t.tileTexture, new Vector2(t.Position.X - t.tileTexture.Width / 2, t.Position.Y - t.tileTexture.Height / 2), t.TextureColor);
                        }
                        else
                        {
                            _spriteBatch.Draw(t.tileTexture, new Vector2(t.Position.X - t.tileTexture.Width / 2, t.Position.Y - t.tileTexture.Height / 2), Color.White);
                        }

                        
                    }
                    else
                    {
                         if (t.MagicDoor)
                        {
                            _spriteBatch.Draw(t.tileTexture, new Vector2(t.Position.X - t.tileTexture.Width / 2, t.Position.Y - t.tileTexture.Height / 2), t.TextureColor);
                        }else
                        {
                            _spriteBatch.Draw(t.tileTexture, new Vector2(t.Position.X - t.tileTexture.Width / 2, t.Position.Y - t.tileTexture.Height / 2), Color.White);
                        }
                        
                    }
                }
                else
                {

                    _spriteBatch.Draw(t.tileTexture, new Vector2(t.Position.X - t.tileTexture.Width / 2, t.Position.Y - t.tileTexture.Height / 2), Color.White);


                }

                
            }


        }
    }
}
