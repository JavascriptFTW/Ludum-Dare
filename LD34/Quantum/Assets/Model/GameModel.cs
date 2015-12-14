using System;
using System.Collections.Generic;
using Controller;

namespace Model
{
    class GameModel
    {
        // Measured in seconds
        private static float Time = 0;
        public static float ViewWidth;
        public static float ViewHeight;
        private static int curFrequency = -1;
        private static int lastSecond = -1000000;
        private static List<ObstacleGroup> obstacleGroups = new List<ObstacleGroup>();
        private static float pGroupX = 0;

        public static List<Entity> Entities = new List<Entity>();

        public static float difficulty = 0;
        public static Player player;
        private static Random rng = new Random();

        public static void Start()
        {
            ViewWidth = GameController.Width;
            ViewHeight = GameController.Height;

            player = new Player(ViewWidth / 2, ViewHeight / 2,
                ViewHeight * 0.1f, ViewHeight * 0.1f);
            Entities.Add(player);
            
            obstacleGroups.Add(new ObstacleGroup(new Obstacle[] {
                new Obstacle(0, -ViewHeight / 2, ViewWidth, 50, 0),
                new Obstacle(0, ViewHeight / 2, ViewWidth, 50, 0)
            }));

            obstacleGroups.Add(new ObstacleGroup(new Obstacle[] {
                new Obstacle(0, -ViewHeight / 2, ViewWidth, 50, 0),
                new Obstacle(0, ViewHeight / 2, ViewWidth, 50, 0),
                new Obstacle(-ViewWidth * 2/9, 0, ViewWidth / 5, ViewHeight / 2),
                new Obstacle(ViewWidth  * 2/9, 0, ViewWidth / 5, ViewHeight / 2)
            }));

            obstacleGroups.Add(new ObstacleGroup(new Obstacle[] {
                new Obstacle(0, -ViewHeight / 2, ViewWidth, 50, 0),
                new Obstacle(0, ViewHeight / 2, ViewWidth, 50, 0),
                new Obstacle(-ViewWidth * 3/9, -ViewHeight * 1/6, ViewWidth / 12, ViewHeight * 2/3),
                new Obstacle(-ViewWidth * 1/9, ViewHeight * 1/6, ViewWidth / 12, ViewHeight * 2/3),
                new Obstacle(ViewWidth * 1/9, -ViewHeight * 1/6, ViewWidth / 12, ViewHeight * 2/3),
                new Obstacle(ViewWidth * 3/9, ViewHeight * 1/6, ViewWidth / 12, ViewHeight * 2/3)
            }));

            obstacleGroups.Add(new ObstacleGroup(new Obstacle[] {
                new Obstacle(0, -ViewHeight / 2, ViewWidth, 50, 0),
                new Obstacle(0, ViewHeight / 2, ViewWidth, 50, 0),
                new Obstacle(-ViewWidth / 6, -ViewHeight / 6, ViewWidth / 4, ViewHeight / 4),
                new Obstacle(ViewWidth / 6, -ViewHeight / 6, ViewWidth / 4, ViewHeight / 4),
                new Obstacle(ViewWidth / 6, ViewHeight / 6, ViewWidth / 4, ViewHeight / 4),
                new Obstacle(-ViewWidth / 6, ViewHeight / 6, ViewWidth / 4, ViewHeight / 4)
            }));

            obstacleGroups.Add(new ObstacleGroup(new Obstacle[]
            {
                new Obstacle(0, -ViewHeight / 2, ViewWidth, 50, 0),
                new Obstacle(0, ViewHeight / 2, ViewWidth, 50, 0),
                new Obstacle(-ViewWidth * 2/5, 0, ViewWidth / 10, ViewHeight),
                new Obstacle(-ViewWidth * 1/5, 0, ViewWidth / 10, ViewHeight),
                new Obstacle(0, 0, ViewWidth / 10, ViewHeight),
                new Obstacle(ViewWidth * 1/5, 0, ViewWidth / 10, ViewHeight),
                new Obstacle(ViewWidth * 2/5, 0, ViewWidth / 10, ViewHeight)
            }));

            SpawnObstacle(0);
        }

        public static void Update()
        {
            Time += GameController.deltaTime;
            SpawnLoop();
            difficulty = Time / 6;

            for (int i = Entities.Count - 1; i >= 0; i--)
            {
                var ent = Entities[i];
                ent.Update();
                if (ent.Kill)
                {
                    Entities.RemoveAt(i);
                    i++;
                    if (i >= Entities.Count)
                    {
                        break;
                    }
                }
            }
        }

        public static void Reset()
        {
            Time = 0;
            ViewWidth = GameController.Width;
            ViewHeight = GameController.Height;
            curFrequency = -1;
            lastSecond = -1000000;
            obstacleGroups = new List<ObstacleGroup>();
            pGroupX = 0;

            Entities = new List<Entity>();

            difficulty = 0;
            player = null;
            rng = new Random();
    }

        public static void SpawnLoop()
        {
            pGroupX -= 15 * GameController.deltaTime * difficulty;
            int diffMod = (int)(difficulty / 10);
            if (diffMod == 0)
            {
                diffMod = 1;
            }
            int second = (int)Time;
            if (second != lastSecond && second % (3 / diffMod) == 0)
            {
                SpawnCoin();
            }
            if (pGroupX <= 450)
            {
                SpawnObstacle();
            }
            lastSecond = second;
        }

        private static void SpawnObstacle()
        {
            SpawnObstacle(rng.Next(1, obstacleGroups.Count));
        }

        private static void SpawnObstacle(int ind)
        {
            var obstGroup = obstacleGroups[ind];
            obstGroup.Spawn(pGroupX + (obstGroup.maxX - obstGroup.minX) / 2, ViewHeight / 2);
            pGroupX += (obstGroup.maxX - obstGroup.minX);
        }

        private static void SpawnCoin()
        {
            curFrequency = -curFrequency;
            var obst = new Coin(ViewWidth + 50, (float)(rng.Next(0, 20) / 20.0) * ViewHeight,
                ViewWidth / 20, ViewWidth / 20);
            obst.Frequency = curFrequency;
            Entities.Add(obst);
        }
    }
}