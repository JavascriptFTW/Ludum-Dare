using Controller;

namespace Model
{
    public class Coin : Entity
    {
        private Vector speed;

        public Coin(float x, float y, float w, float h)
        {
            // TODO (Joshua): Make seperate textures for each state
            spritePath = "CoinTexture";
            layer = "Items";

            position.X = x;
            position.Y = y;
            size.X = w;
            size.Y = h;

            speed = new Vector(25, 0);
        }

        override public void Update()
        {
            base.Update();
            position.X -= speed.X * GameController.deltaTime * GameModel.difficulty;
            kill = kill || (position.X + size.X / 2 + 20 < 0);
        }

        override public void OnCollision(Entity ent)
        {
            if (ent.Type == "Player")
            {
                var player = ent as Player;
                player.Score += 1;
                kill = true;
            }
        }
    }
}
