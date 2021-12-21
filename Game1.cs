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

        Texture2D[] textures = new Texture2D[9];

        Texture2D floor;
     
        Texture2D door;


        SpriteFont gameFont;

        public bool gameStarted = false;
        private int currentLevel = 0;

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
            //Game Images Loaded
            floor = Content.Load<Texture2D>("./assets/gameImages/floorSmall");

            

            //Player Image Loaded
            playerSprite = Content.Load<Texture2D>("./assets/player/hero");
            walkDown = Content.Load<Texture2D>("./assets/player/heroDown");
            walkRight = Content.Load<Texture2D>("./assets/player/heroRight");
            walkLeft = Content.Load<Texture2D>("./assets/player/heroLeft");
            walkUp = Content.Load<Texture2D>("./assets/player/heroUp");


            //Game Text Font Loaded
            gameFont = Content.Load<SpriteFont>("./assets/font/Fonten");

            for (int i = 1; i < textures.Length; i++)
            {
                textures[i] = Content.Load<Texture2D>("./assets/gameImages/walls/wall-" + i);
            }


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
            if (!gameStarted)
            {
                for (int i = 0; i < 15; i++)
                {
                    Tile.CreateMap(gameTime, _graphics, floor, textures, i);
                    Maps.AddMap(i, Tile.tiles);


                }
                gameStarted = true;
            }

            if(gameStarted)
            {
                Maps.PlayerMoves(player, currentLevel);
            }  



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            ///Maps.DrawMap(_spriteBatch, player, Maps.mapsList[10].Map);

            Maps.DrawMap(_spriteBatch, player, currentLevel);


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
            _spriteBatch.DrawString(gameFont, "GameLevel: " + Maps.mapsList[currentLevel].MapLevel, new Vector2(100, 0), Color.White);

            //_spriteBatch.DrawString(gameFont, "GameStarted: " + gameStarted, new Vector2(300, 300 ), Color.White);

            //_spriteBatch.Draw(playerSprite, new Vector2(player.Position.X - floor.Width / 2, player.Position.Y  - floor.Height / 2), Color.White);

            player.anim.Draw(_spriteBatch);
            

            /*
            _spriteBatch.DrawString(gameFont, "" + player.TileCoord, new Vector2(100, 100), Color.White);
            _spriteBatch.DrawString(gameFont, "Moving" + player.IsMoving, new Vector2(500, 100), Color.White);
            _spriteBatch.DrawString(gameFont, "currentTile" + player.Position, new Vector2(500, 200), Color.White);
            _spriteBatch.DrawString(gameFont, "Go to Tile" + player.TilePosition, new Vector2(500, 400), Color.White);
            _spriteBatch.DrawString(gameFont, "My Dier" + player.Direction, new Vector2(500, 500), Color.White);
            _spriteBatch.DrawString(gameFont, "StepsX " + player.StepsX + "StepsY " + player.StepsY, new Vector2(500, 600), Color.White);
            */
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
