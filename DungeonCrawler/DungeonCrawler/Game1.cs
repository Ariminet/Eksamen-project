using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DungeonCrawler
{
    enum Dir
    {
        Down,
        Up,
        Left,
        Right
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D playerSprite;
        Texture2D walkDown;
        Texture2D walkUp;
        Texture2D walkRight;
        Texture2D walkLeft;

        /// <summary>
        /// Array of Textures which will be walls
        /// </summary>
        Texture2D[] textures = new Texture2D[9];

        /// <summary>
        /// Variable for floor tile image
        /// </summary>
        Texture2D floor;
     
        /// <summary>
        /// Variable for door image
        /// </summary>
        Texture2D door;

        /// <summary>
        /// Keeps track of gameState 
        /// and what level player is at
        /// </summary>
        public bool gameStarted = false;
        private int currentLevel = 0;

        
        SpriteFont gameFont;

       

        Player player = new Player();


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 768;
            _graphics.ApplyChanges();


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            
            //Player Image Loaded
            playerSprite = Content.Load<Texture2D>("./assets/player/hero");
            walkDown = Content.Load<Texture2D>("./assets/player/heroDown");
            walkRight = Content.Load<Texture2D>("./assets/player/heroRight");
            walkLeft = Content.Load<Texture2D>("./assets/player/heroLeft");
            walkUp = Content.Load<Texture2D>("./assets/player/heroUp");


            
            //Game Text Font Loaded
            gameFont = Content.Load<SpriteFont>("./assets/font/Fonten");


            //Game Images Loaded
            // Floor image (Tile) Loaded
            floor = Content.Load<Texture2D>("./assets/gameImages/floorSmall");

            //Adds walls to array of textures 
            for (int i = 1; i < textures.Length; i++)
            {
                textures[i] = Content.Load<Texture2D>("./assets/gameImages/walls/wall-" + i);
            }
            // Loads door image to door variable
            door = Content.Load<Texture2D>("./assets/gameImages/door/door");


            player.animations[0] = new SpriteAnimation(walkDown, 3, 8);
            player.animations[1] = new SpriteAnimation(walkUp, 3, 8);
            player.animations[2] = new SpriteAnimation(walkLeft, 3, 8);
            player.animations[3] = new SpriteAnimation(walkRight, 3, 8);

            player.anim = player.animations[0];
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            player.Update(gameTime);


            //Checks if game is started
            if (!gameStarted)
            {
                // if game not started it will create 15 levels and add them to a list in class of Maps
                for (int i = 0; i < 15; i++)
                {
                    //Creates List of tiles (Map Layout)
                    Tile.CreateMap(gameTime, _graphics, floor, textures,door, i);
                    //Adds current generated List of tiles to Maps so we have a list array of Maps which indicates which map(lvl) should be rendered
                    Maps.AddMap(i, Tile.tiles);


                }
                //returns gameStarted true  and then if game is true it keeps track of player turn
                gameStarted = true;
            }
            if(gameStarted)
            {

                   
                    

                if (player.enterDoor)
                {
                    foreach (Tile t in Maps.mapsList[currentLevel].Map)
                    {

                        if (Vector2.Distance(t.Position, player.Position) < 164 && t.DoorLevel > -1 )
                        {

                            

                            if (player.TileCoord == new Vector2(1,2) && player.OldTileCoord == new Vector2(1, 2))
                            {
                                
                                player.TileCoord = new Vector2(8, player.TileCoord.Y);
                                player.Position = new Vector2(128 * 8 + 64, player.Position.Y);
                                player.TilePosition = player.Position;
                                player.pickedPos = player.Position;
                                player.Direction = Dir.Left;
                                currentLevel = t.DoorLevel;
                                player.enterDoor = false;
                            }
                            if (player.TileCoord == new Vector2(8, 2) && player.OldTileCoord == new Vector2(8, 2))
                            {
                                
                                player.TileCoord = new Vector2(1, player.TileCoord.Y);
                                player.Position = new Vector2(128 * 1 + 64, player.Position.Y);
                                player.TilePosition = player.Position;
                                player.pickedPos = player.Position;
                                player.Direction = Dir.Right;
                                currentLevel = t.DoorLevel;
                                player.enterDoor = false;
                            }
                            if (player.TileCoord == new Vector2(4, 1) && player.OldTileCoord == new Vector2(4, 1))
                            {
                                
                                player.TileCoord = new Vector2(player.TileCoord.X, 4);
                                player.Position = new Vector2(player.Position.X, 128 * 4 + 64);
                                player.TilePosition = player.Position;
                                player.pickedPos = player.Position;
                                player.Direction = Dir.Up;
                                currentLevel = t.DoorLevel;
                                player.enterDoor = false;
                            }
                            if (player.TileCoord == new Vector2(4, 4) && player.OldTileCoord == new Vector2(4, 4))
                            {
                                
                                player.TileCoord = new Vector2(player.TileCoord.X, 1);
                                player.Position = new Vector2(player.Position.X, 128 * 1 + 64);
                                player.TilePosition = player.Position;
                                player.pickedPos = player.Position;
                                player.Direction = Dir.Down;
                                currentLevel = t.DoorLevel;
                                player.enterDoor = false;
                            }
                            
                            

                        }
                        
                        else
                        {
                            player.enterDoor = false;
                        }
                    }


                    
                }

                
                    
                
                // checks if player is at current tile  if he has picked a new tile it will render calculations on which tile 
                // player picked and if tile picked is within players velocity(steps 1 step == 1 tile) and moves appropriately
                if(currentLevel < 15)
                {
                    Maps.PlayerMoves(player, currentLevel);
                }
            }  



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            ///Maps.DrawMap(_spriteBatch, player, Maps.mapsList[10].Map);

            // Draws map according to current map level so picks Maps.mapList[CurrentLevel].Map 
            // mapList == List of Maps which indicates array of tiles == mapLayout 
            // mapList[index] == level to be rendered
            //Map == List of tiles within the class Maps
            if(currentLevel < 15)
            {
                Maps.DrawMap(_spriteBatch, player, currentLevel, gameFont);
            }else
            {
                Maps.DrawMap(_spriteBatch, player, 14, gameFont);
                _spriteBatch.DrawString(gameFont, "Congratulations you've won the game", new Vector2(_graphics.GraphicsDevice.Viewport.Width / 2, _graphics.GraphicsDevice.Viewport.Height / 2), Color.White);
            }
            


            /*
              _spriteBatch.DrawString(gameFont, "Player pos: " + player.Position, new Vector2(100, 200), Color.White);
              _spriteBatch.DrawString(gameFont, "Tile pos: " + Tile.tiles[15].Position, new Vector2(100, 400), Color.White);
              _spriteBatch.DrawString(gameFont, "Player tileCoord: " + player.TileCoord, new Vector2(100, 300), Color.White);
              _spriteBatch.DrawString(gameFont, "Tile coord: " + Tile.tiles[15].TileCoord, new Vector2(100, 500), Color.White);
            */



            //for (int i = 0; i < Maps.mapsList.Count; i++)
            //{
            //  _spriteBatch.DrawString(gameFont, "GameLevel: " + Maps.mapsList[i].MapLevel, new Vector2(200, 100 * i), Color.White);

            //}

            // render if instructions on how to enter door if player is at new picked position and within the range of a door
            /*
            if (Vector2.Distance(player.TilePosition, player.Position) < 10) { 
                foreach (Tile  t in Maps.mapsList[currentLevel].Map)
            {
                if (Vector2.Distance(t.Position, player.Position) < 128 && t.tileTexture.ToString() == "./assets/gameImages/door/door")
                {
                    _spriteBatch.DrawString(gameFont, "Press E to enter the door", player.Position - new Vector2(164,128), Color.White);
                }
            }
            }
           
            */

            //_spriteBatch.DrawString(gameFont, "GameStarted: " + gameStarted, new Vector2(300, 300 ), Color.White);

            //_spriteBatch.Draw(playerSprite, new Vector2(player.Position.X - floor.Width / 2, player.Position.Y  - floor.Height / 2), Color.White);

            player.anim.Draw(_spriteBatch);
            if(currentLevel < 15)
            {
                _spriteBatch.DrawString(gameFont, "GameLevel: " + Maps.mapsList[currentLevel].MapLevel, new Vector2(100, 0), Color.White);
            }
            _spriteBatch.DrawString(gameFont, "Player Coordinate" + player.TileCoord, new Vector2(100, 100), Color.White);
            _spriteBatch.DrawString(gameFont, " Old Player Coordinate" + player.OldTileCoord, new Vector2(100, 200), Color.White);
            _spriteBatch.DrawString(gameFont, "currentTile" + player.Position, new Vector2(100, 300), Color.White);
            _spriteBatch.DrawString(gameFont, "Enter Door  " + player.enterDoor, new Vector2(500, 400), Color.White);

            if(currentLevel < 15)
            {
                foreach (Tile t in Maps.mapsList[currentLevel].Map)
                {
                    if (Vector2.Distance(t.Position, player.Position) < 164 && t.DoorLevel >= 0)
                    {
                        _spriteBatch.DrawString(gameFont, "closest door lvl " + t.DoorLevel, new Vector2(100, 500), Color.White);
                    }

                }
            }
            
                /*
                 _spriteBatch.DrawString(gameFont, "Picked coordinate" + player.pickedPos, new Vector2(100, 100), Color.White);
                _spriteBatch.DrawString(gameFont, "Picked coordinate DISTANCE  " + Vector2.Distance(player.Position,player.pickedPos), new Vector2(100, 200), Color.White);

                _spriteBatch.DrawString(gameFont, "Moving" + player.IsMoving, new Vector2(500, 100), Color.White);

                _spriteBatch.DrawString(gameFont, "Go to Tile" + player.TilePosition, new Vector2(500, 400), Color.White);
                _spriteBatch.DrawString(gameFont, "My Dier" + player.Direction, new Vector2(500, 500), Color.White);
                _spriteBatch.DrawString(gameFont, "StepsX " + player.StepsX + "StepsY " + player.StepsY, new Vector2(500, 600), Color.White);
                */
                _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
