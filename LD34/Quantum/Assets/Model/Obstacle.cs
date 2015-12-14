using Controller;

namespace Model
{
    public class Obstacle : Entity
    {
        protected Vector speed;

        public Obstacle(float x, float y, float w, float h)
        {
            spritePath = "ObstacleTexture";
            layer = "Obstacles";
            type = "Obstacle";
            position.X = x;
            position.Y = y;
            size.X = w;
            size.Y = h;
            speed = new Vector(15, 0);
        }

        public Obstacle(float x, float y, float w, float h, int freq)
        {
            spritePath = "ObstacleTexture";
            layer = "Obstacles";
            type = "Obstacle";
            position.X = x;
            position.Y = y;
            size.X = w;
            size.Y = h;
            speed = new Vector(15, 0);
            frequency = freq;
        }

        override public void Update()
        {
            base.Update();
            // Fix this later to do stuff with Time.deltaTime and junk so we don't have jittery movement
            position.X -= speed.X * GameController.deltaTime * GameModel.difficulty;
            kill = kill || (position.X + size.X / 2 + 20 < 0);
        }

        override public void OnCollision(Entity o2)
        {
            base.OnCollision(o2);
            if (o2.Type == "Player")
            {
                var player = o2 as Player;
                player.Lives -= 1;
                player.ToggleFrequency();
                if (player.Dead)
                {
                    player.OnDeath();
                }
            }
        }
    }
}
