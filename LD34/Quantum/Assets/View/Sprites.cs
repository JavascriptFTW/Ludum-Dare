using System.Collections.Generic;

using UnityEngine;

using Model;

namespace View
{
    class Sprites
    {
        public static Object prefab = Resources.Load("Prefabs/Sprite");

        private static Dictionary<string, EntitySprite> sprites = new Dictionary<string, EntitySprite>();
        private static int renderInd = 0;
        private static List<GameObject> gameObjects = new List<GameObject>();

        private static GameObject GetRenderTarget()
        {
            GameObject renderTarget;

            if (renderInd < gameObjects.Count)
            {
                renderTarget = gameObjects[renderInd];
            }
            else
            {
                renderTarget = Object.Instantiate(prefab) as GameObject;
                
                renderTarget.transform.parent = GameView.instance.transform;

                gameObjects.Add(renderTarget);
            }

            ++renderInd;

            return renderTarget;
        }

        public static void Render(Entity ent)
        {
            if (!sprites.ContainsKey(ent.Type))
            {
                sprites.Add(ent.Type,
                    new EntitySprite(ent.spritePath, ent.layer));
            }

            GameObject renderTarget = GetRenderTarget();

            sprites[ent.Type].Apply(renderTarget, ent);

            Vector3 position = GameView.ToWorldVector(ent.X - GameView.unitWidth / 2,
                -ent.Y + GameView.unitHeight / 2);

            position.x *= GameView.ViewWidth;
            position.y *= GameView.ViewHeight;

            renderTarget.transform.position = position;
            renderTarget.transform.localScale = GameView.ToWorldVector(ent.Width, ent.Height);
        }

        public static void HideSpareSprites()
        {
            int first = renderInd;

            for (int i = first; i < gameObjects.Count; i++)
            {
                gameObjects[i].transform.position = new Vector3(1000000, 0, 0);
            }
            renderInd = 0;
        }

        public static void Reset()
        {
            renderInd = 0;
            foreach (GameObject go in gameObjects)
            {
                Object.Destroy(go);
            }
            gameObjects = new List<GameObject>();
        }
    }
}
