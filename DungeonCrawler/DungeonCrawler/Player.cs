using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
namespace DungeonCrawler
{
    class Player
    {

        public Vector2 pickedPos;
        private Vector2 position = new Vector2(192, 192);
        public int speed = 128;
        private int velocity = 2;
        public bool potionBuff = false;
        private bool isSneaking = false;
        private bool isHit = false;
        private Vector2 tileCoord = new Vector2(1, 1);
        private Vector2 oldTileCoord = new Vector2(0, 0);
        private KeyboardState kStateOld = Keyboard.GetState();
        private Dir direction = Dir.Down;
        private bool isMoving = false;

        private int stepsX = 0;
        private int stepsY = 0;

        private Vector2 tilePosition = new Vector2(192, 192);


        public bool enterDoor = false;



        public SpriteAnimation anim;

        public SpriteAnimation[] animations = new SpriteAnimation[4];

        public Vector2 Position 
        { 
            get { return position; }
            set { position = value; }
        }
        public int Velocity
        { // benytter en ternary til at sætte potionbuffen på hvis den er true
            get { return velocity + (potionBuff ? 3 : 0); }
            set { velocity = value; }
        }
        public Vector2 TileCoord
        {
            get { return tileCoord; }
            set { tileCoord = value; }
        }
        public Vector2 OldTileCoord
        {
            get { return oldTileCoord; }
            set { oldTileCoord = value; }
        }

        public bool IsSneaking
        {
            get { return isSneaking; }
        }

        public Dir Direction
        {
            get { return direction; }
            set { direction = value; }
        }
        public bool IsMoving
        {
            get { return isMoving; }
            set { isMoving = value; }
        }

        public int StepsX
        {
            get { return stepsX; }
            set { stepsX = value; }
        }
        public int StepsY
        {
            get{ return stepsY; }
            set { stepsY = value; }
        }

        public Vector2 TilePosition
        {
            get { return tilePosition; }
            set { tilePosition = value; }
        }

        public KeyboardState KStateOld
        {
            get { return kStateOld; }
        }

        public void Running(KeyboardState kState)
        {
            if (kState.IsKeyDown(Keys.Q) && kStateOld.IsKeyUp(Keys.Q) && Vector2.Distance(tilePosition, position) <= 10)
            {
                isSneaking = !isSneaking;
                if (isSneaking)
                {
                    velocity = 5;
                    speed = speed * 2;
                }
                else
                {
                    velocity = 2;
                    speed = speed / 2;
                }
            }
        }

        public void EnterDoor(KeyboardState kState)
        {
                if (kState.IsKeyDown(Keys.E) && kStateOld.IsKeyUp(Keys.E))
                {
                    enterDoor = true;
                    oldTileCoord = tileCoord;
                }
        }
        public void PickNewPostion(MouseState mState)
        {
            if (mState.LeftButton == ButtonState.Pressed)
            {

                
                pickedPos = new Vector2(mState.X, mState.Y);

            }
          
        }
        public void MovePosition(KeyboardState kState, float dt)
        {
            kStateOld = kState;




            if (Vector2.Distance(tilePosition, position) < 10)


            {
                if (StepsY == 0)
                {
                    stepsX = 0;
                }
                if (stepsX == 0)
                {
                    stepsY = 0;
                }

                position = tilePosition;
            }

            if (stepsX <= Velocity && stepsX > 0)
            {
                direction = Dir.Right;
                isMoving = true;
            }
            else if (stepsX >= -Velocity && stepsX < 0)
            {
                direction = Dir.Left;
                isMoving = true;
            }
            else if (stepsY >= -Velocity && stepsY < 0)
            {
                direction = Dir.Up;
                isMoving = true;
            }
            else if (stepsY <= Velocity && stepsY > 0)
            {
                direction = Dir.Down;
                isMoving = true;
            }
            // når den har tjekket for alle spillerens bevægelsesfelter, skal isMoving sættes til false, men når bevægelsen er ovre er isMoving stadig true
            // derfor laves en else if der sætter isMoving til false samtidig med at potionBuffen sættes til false. Derved fjernes buffen igen. 
            else if (isMoving)
            {
                isMoving = false;
                potionBuff = false;
            }
            if (isMoving)
            {
                Vector2 moveDistance = tilePosition - position;
                moveDistance.Normalize();
                switch (direction)
                {
                    case Dir.Down:
                        position.Y += moveDistance.Y * (speed * dt);
                        break;
                    case Dir.Up:
                        position.Y += moveDistance.Y * (speed * dt);
                        
                        break;
                    case Dir.Left:
                        position.X += moveDistance.X * (speed * dt);
                        
                        break;
                    case Dir.Right:
                        position.X += moveDistance.X * (speed * dt);
                        
                        break;
                }
            }
            

        }

        public void AnimateMovement(GameTime gameTime)
        {
            anim = animations[(int)direction];
            // Position on start >= same as when picked new position find fix
            anim.Position = new Vector2(position.X - 36, position.Y -64);
            if (isMoving)
            {
                anim.Update(gameTime);
                
            }
            else
            {
                anim.setFrame(0);
            }
        }

        
        public void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();

            MouseState mState = Mouse.GetState();

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kState.IsKeyDown(Keys.B) )
            {
                kStateOld = kState;
            }
            //Pick location player wants to go to
            PickNewPostion(mState);
            //Check keyboard if Q is pressed then changed movement to new speed
            Running(kState);
            //Check keyboard if E is pressed then changed Level 
            EnterDoor(kState);
            //Calculated the amount of pixel to move over a period of time while locking movement features 
            MovePosition(kState, dt);
            
            //calculated which sprite in the spritestrip to show and when to change it to walking UP // Down // Left // Right
            //and when it stands still
            AnimateMovement(gameTime);


        }

    }
}
