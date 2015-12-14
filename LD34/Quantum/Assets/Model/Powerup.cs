using System.Collections.Generic;

using Controller;

namespace Model
{
    public class Powerup : QuantumObject
    {
        public int Timeout
        {
            get;
            set;
        }
        public bool collected = false;
        // private string name;
        private Vector speed = new Vector(5, 0);

        /* public string Name
        {
            get
            {
                return name;
            }
        } */

        protected Powerup()
        {
            size.X = GameModel.ViewHeight / 30;
            size.Y = GameModel.ViewHeight / 30;
            position.X = GameModel.ViewWidth + 50;
            //position.Y = (float)(GameModel.rng.Next(0, 80) / 80.0) * GameModel.ViewHeight;
        }

        override public void Update()
        {
            // Check for collision with player
            if (IsColliding(GameModel.player))
            {
                OnCollision(GameModel.player);
            }
            // Fix this later to do stuff with Time.deltaTime and junk so we don't have jittery movement
            position.X -= speed.X * GameController.deltaTime * GameModel.difficulty;
            kill = (position.X + size.X / 2 + 20 < 0);
            if (IsColliding(GameModel.player))
            {
                OnCollision(GameModel.player);
            }
        }

        public virtual void Activate()
        {
            // Will be implemented by individual powerup types
        }

        public void OnCollision(Player o2)
        {
            /* collected = true;
            GameModel.player.CollectPowerup(this); */
        }
        
    }
}
