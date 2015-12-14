using System;

namespace Model
{
    public class QuantumObject
    {
        protected int frequency = 1;

        public int Frequency
        {
            get
            {
                return frequency;
            }
            set
            {
                if (value != 0)
                {
                    frequency = value / Math.Abs(value);
                }
                else
                {
                    frequency = 0;
                }
            }
        }

        protected Vector position;
        protected Vector size;

        protected bool kill = false;
        public bool Kill
        {
            get
            {
                return kill;
            }
        }

        public float X
        {
            get
            {
                return position.X;
            }
        }

        public float Y
        {
            get
            {
                return position.Y;
            }
        }

        public float Width
        {
            get
            {
                return size.X;
            }
        }

        public float Height
        {
            get
            {
                return size.Y;
            }
        }

        protected QuantumObject()
        {
            position = new Vector(0, 0);
            size = new Vector(0, 0);
        }

        public QuantumObject(float x, float y, float w, float h)
        {
            position = new Vector(x, y);
            size = new Vector(w, h);
        }

        public bool PointInside(float x, float y)
        {
            float halfWidth = size.X / 2;
            float halfHeight = size.Y / 2;
            return x >= position.X - halfWidth &&
                y >= position.Y - halfHeight &&
                x <= position.X + halfWidth &&
                y <= position.Y + halfHeight;
        }

        public bool IsInside(QuantumObject o2)
        {
            float halfWidth = size.X / 2;
            float halfHeight = size.Y / 2;
            return o2.PointInside(position.X - halfWidth, position.Y - halfHeight) ||
                o2.PointInside(position.X + halfWidth, position.Y - halfHeight) ||
                o2.PointInside(position.X + halfWidth, position.Y + halfHeight) ||
                o2.PointInside(position.X - halfWidth, position.Y + halfHeight);
        }

        public bool IsColliding(QuantumObject o2)
        {
            return (o2.IsInside(this) || IsInside(o2)) &&
                (o2.frequency == frequency || o2.frequency == 0 || frequency == 0);
        }

        public virtual void OnCollision(QuantumObject o2)
        {
            // Classes which inherit QuantumObject will implement this and do shtuff
        }

        public virtual void Update()
        {
            // Classes which inherit QuantumObject will also implement this and do shtuff
        }

        public void Despawn()
        {
            kill = true;
        }

        public void ToggleFrequency()
        {
            frequency = -frequency;
        }
    }
}