using System;

using Controller;

namespace Model
{
    public class Player : Entity
    {

        private int lives = 3;
        private int score = 0;
        // private List<Powerup> powerupQueue = new List<Powerup>();

        public bool Dead
        {
            get
            {
                return lives <= 0;
            }
        }

        public int Lives
        {
            get
            {
                return lives;
            }
            set
            {
                if (lives == 0 || value < lives)
                {
                    lives = value;
                }
            }
        }

        public int Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
            }
        }

        private float speed = 200;

        public Player(float x, float y, float w, float h)
        {
            spritePath = "PlayerTexture";
            layer = "Player";
            type = "Player";
            position.X = x;
            position.Y = y;
            size.X = w;
            size.Y = h;
        }

        override public void Update()
        {
            base.Update();
            float moveDist = speed * GameController.deltaTime;
            float dist = position.Dist2D(GameController.mouse);

            if (dist > moveDist)
            {
                double ang = position.Angle2D(GameController.mouse) * Math.PI / 180.0;

                position.X += (float)Math.Sin(ang) * moveDist;
                position.Y -= (float)Math.Cos(ang) * moveDist;
            } else
            {
                position.X = GameController.mouse.X;
                position.Y = GameController.mouse.Y;
            }

            if (GameController.MouseButtonDown(0))
            {
                ToggleFrequency();
            }
        }

        override public void OnCollision(Entity o2)
        {
            base.OnCollision(o2);
        }

        public void OnDeath()
        {
            // TODO (Joshua): Display menus and stuff before we
            // reset (so we can have a leaderboard)
            GameController.Reset();
        }

        /* public void FirePowerup()
        {
            if (powerupQueue.Count > 0)
            {
                powerupQueue[powerupQueue.Count - 1].Activate();
            } 
        }

        public void CollectPowerup(Powerup p)
        {
            powerupQueue.Add(p);
        } */
    }
}
