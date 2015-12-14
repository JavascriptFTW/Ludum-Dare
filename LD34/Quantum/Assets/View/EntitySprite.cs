using UnityEngine;

namespace View
{
    public class EntitySprite
    {
        protected static Color negativeColor = new Color(1f, 0f, 0.39f, 1f);
        protected static Color positiveColor = new Color(0.19f, 1f, 0.43f, 1f);
        protected static Color neutralColor = new Color(0f, 0.75f, 1f, 1f);

        public string layer;
        public Sprite sprite;

        public EntitySprite(string path)
        {
            sprite = Resources.Load<Sprite>(path);
            this.layer = "Default";
        }

        public EntitySprite(string path, string layer)
        {
            sprite = Resources.Load<Sprite>(path);
            this.layer = layer;
        }

        public GameObject Apply(GameObject renderTarget, Model.Entity ent)
        {
            var spriteObj = renderTarget.GetComponent<SpriteRenderer>();

            spriteObj.color = neutralColor;
            if (ent.Frequency < 0)
            {
                spriteObj.color = negativeColor;
            }
            else if (ent.Frequency > 0)
            {
                spriteObj.color = positiveColor;
            }

            spriteObj.sprite = sprite;
            spriteObj.sortingOrder = 0;
            spriteObj.sortingLayerName = layer;

            renderTarget.layer = LayerMask.NameToLayer("Game");

            return renderTarget;
        }
    }
}
