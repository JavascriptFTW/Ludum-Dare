namespace Model
{
    public class Entity : QuantumObject
    {
        public string spritePath
        {
            get;
            set;
        }

        public string layer
        {
            get;
            set;
        }

        protected string type = "Entity";
        public string Type
        {
            get
            {
                return type;
            }
        }
        protected Entity()
        {

        }

        public virtual void Start()
        {

        }

        override public void Update()
        {
            base.Update();
            foreach (Entity ent in GameModel.Entities)
            {
                if (IsColliding(ent))
                {
                    OnCollision(ent);
                }
            }
        }

        public virtual void OnCollision(Entity o2)
        {
            
        }
    }
}
