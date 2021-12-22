using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonCrawler
{
    class Monster
    {
        private Vector2 monPosition;
        private Vector2 monTileCord;
        private Vector2 oldMonPosition;
        private Dir direction = Dir.Down;
        public SpriteAnimation anim;
        public SpriteAnimation[] monAnimations = new SpriteAnimation[4];
        Random random = new Random();
        int min = 1;
        int max = 15;

        public int speed = 128;
        private int velocity = 2;

        private int stepsX = 0;
        private int stepsY = 0;

        bool isMoving = false;
        private bool arrived = false;

        private int monsterLevel = -1;
        public int monsterRound = 2;

        Player player = new Player();

        public int MonsterLevel
        {
            get { return monsterLevel; }
            set { monsterLevel = value; }
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
            get { return stepsY; }
            set { stepsY = value; }
        }


        public void monLevel (int currentRound, Player player, int currentLevel)
        {
            
            if (monsterRound <= currentRound)
            {
                monsterRound++;

                for (int i = 0; i < player.Velocity; i++)
                {

                monsterLevel = random.Next(min, max);
                    if(monsterLevel == currentLevel)
                    {
                        
                        break;
                    }
                }

            }
        }

        public void MovePosition(float dt)
        {
            isMoving = false;

            if(Vector2.Distance(monTileCord, monPosition) < 10)
            {
                if (stepsY == 0)
                {
                    stepsX = 0;
                    if(arrived == false)
                    {
                        monsterRound++;
                        arrived = true;
                    }
                }
                if (stepsX == 0)
                {
                    stepsY = 0;
                }
                monPosition = monTileCord;
            }

            if (stepsX <= velocity && stepsX > 0)
            {
                direction = Dir.Right;
                isMoving = true;
            }
            if (stepsX >= -velocity && stepsX < 0)
            {
                direction = Dir.Left;
                isMoving = true;
            }
            if (stepsY >= -velocity && stepsY < 0)
            {
                direction = Dir.Up;
                isMoving = true;
            }
            if (stepsY <= velocity && stepsY > 0)
            {
                direction = Dir.Down;
                isMoving = true;
            }
            if (isMoving)
            {
                Vector2 moveDistance = monTileCord - monPosition;
                moveDistance.Normalize();
                switch (direction)
                {
                    case Dir.Down:
                        monPosition.Y += moveDistance.Y * (speed * dt);
                        break;
                    case Dir.Up:
                        monPosition.Y += moveDistance.Y * (speed * dt);

                        break;
                    case Dir.Left:
                        monPosition.X += moveDistance.X * (speed * dt);

                        break;
                    case Dir.Right:
                        monPosition.X += moveDistance.X * (speed * dt);

                        break;
                }
            }
        }

        public void AnimateMovement(GameTime gametime)
        {
            anim = monAnimations[(int)direction];
            anim.Position = new Vector2(monPosition.X - 64, monPosition.Y - 164);
            if (isMoving)
            {
                anim.Update(gametime);
            }
            else
            {
                anim.setFrame(0);
            }
        }

        public void UpdateMonster(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            MovePosition(dt);
            AnimateMovement(gameTime);
        }

        public void MoveMonster(int currentLevel, Vector2 monDoor, int currentRound, GameTime gameTime)

        {
            // if(random.Next(min, max) == currentLevel)

            if (monsterLevel == currentLevel)
            {

                if (currentRound >= monsterRound)
                {
                    
                    monsterRound++;
                    UpdateMonster(gameTime);

                }
                //animating movement
                if (monDoor.X == 128 * 8 + 64)
                {
                    direction = Dir.Left;
                    monPosition = monDoor;
                }
                if (monDoor.X == 128 * 1 + 64)
                {
                    direction = Dir.Up;
                    monPosition = monDoor;
                }
                if (monDoor.Y == 128 * 4 + 64)
                {
                    direction = Dir.Right;
                    monPosition = monDoor;
                }
                if (monDoor.Y == 128 * 1 + 64)
                {
                    direction = Dir.Down;
                    monPosition = monDoor;
                }
                

            }
        }

    }


}
