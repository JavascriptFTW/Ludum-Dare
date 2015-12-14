using System;
using System.Collections.Generic;

namespace Model
{
    public class ObstacleGroup
    {
        private static Random rng = new Random();
        protected List<Obstacle> obstacles = new List<Obstacle>();
        protected int[] frequencies = { -1, 1 };
        public float minX = 0;
        public float minY = 0;
        public float maxX = 0;
        public float maxY = 0;

        protected ObstacleGroup()
        {

        }

        public ObstacleGroup(Obstacle[] obst)
        {
            foreach (var obstacle in obst)
            {
                if (obstacle.X - obstacle.Width / 2 < minX)
                {
                    minX = obstacle.X - obstacle.Width / 2;
                }
                if (obstacle.Y - obstacle.Height / 2 > minY)
                {
                    minY = obstacle.Y - obstacle.Height / 2;
                }
                if (obstacle.X + obstacle.Width / 2 > maxX)
                {
                    maxX = obstacle.X + obstacle.Width / 2;
                }
                if (obstacle.Y + obstacle.Height / 2 > maxY)
                {
                    maxY = obstacle.Y + obstacle.Height / 2;
                }
                obstacles.Add(obstacle);
            }
        }

        public void Spawn(float x, float y)
        {
            foreach (var obstacle in obstacles)
            {
                var obstInst = new Obstacle(obstacle.X + x, obstacle.Y + y,
                    obstacle.Width, obstacle.Height,
                    obstacle.Frequency);
                if (obstacle.Frequency != 0)
                {
                    obstInst.Frequency = frequencies[rng.Next(0, frequencies.Length)];
                } 
                GameModel.Entities.Add(obstInst);
            }
        }
    }
}
