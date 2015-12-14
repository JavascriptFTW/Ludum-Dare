using System;

namespace Model
{
    public class Vector
    {
        private static double degToRadRatio = 2 * Math.PI / 360;

        private float x;
        private float y;
        private float z;

        public Vector()
        {
            this.x = 0;
            this.y = 0;
            this.z = 0;
        }

        public Vector(float x)
        {
            this.x = x;
            this.y = 0;
            this.z = 0;
        }

        public Vector(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.z = 0;
        }

        public Vector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public float X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        public float Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        public float Z
        {
            get
            {
                return z;
            }
            set
            {
                z = value;
            }
        }

        public void Add(Vector v2)
        {
            this.x += v2.x;
            this.y += v2.y;
            this.z += v2.z;
        }

        public void Subtract(Vector v2)
        {
            this.x -= v2.x;
            this.y += v2.y;
            this.z += v2.z;
        }

        public void Multiply(Vector v2)
        {
            this.x *= v2.x;
            this.y *= v2.y;
            this.z *= v2.z;
        }

        public void Divide(Vector v2)
        {
            if (v2.x != 0 && v2.y != 0 && v2.z != 0)
            {
                this.x /= v2.x;
                this.y /= v2.y;
                this.z /= v2.z;
            }
        }

        public void Scale(float amt)
        {
            this.x *= amt;
            this.y *= amt;
            this.z *= amt;
        }

        public void Rotate(Vector deg)
        {
            RotateX(deg.x);
            RotateY(deg.y);
            RotateZ(deg.z);
        }

        public void RotateX(float theta)
        {
            float sinTheta = (float)Math.Sin(theta * degToRadRatio);
            float cosTheta = (float)Math.Cos(theta * degToRadRatio);
            float z1 = z;
            float y1 = y;
            y = cosTheta * y1 - sinTheta * z1;
            z = sinTheta * y1 + cosTheta * z1;
        }

        public void RotateY(float theta)
        {
            float sinTheta = (float)Math.Sin(theta * degToRadRatio);
            float cosTheta = (float)Math.Cos(theta * degToRadRatio);
            float x1 = x;
            float z1 = z;
            x = cosTheta * x1 - sinTheta * z1;
            z = sinTheta * x1 + cosTheta * z1;
        }

        public void RotateZ(float theta)
        {
            float sinTheta = (float)Math.Sin(theta * degToRadRatio);
            float cosTheta = (float)Math.Cos(theta * degToRadRatio);
            float x1 = x;
            float y1 = y;
            x = cosTheta * x1 - sinTheta * y1;
            z = sinTheta * x1 + cosTheta * y1;
        }

        public float Angle2D(Vector v2)
        {
            float xDiff = v2.x - x;
            float yDiff = v2.y - y;
            return (float)(Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI) + 90;
        }

        public float Dist2D(Vector v2)
        {
            return (float)Math.Sqrt(Math.Pow(v2.x - x, 2) + Math.Pow(v2.y - y, 2));
        }

        public static Vector Add(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.y + v2.y, v1.z + v2.z);
        }

        public static Vector Subtract(Vector v1, Vector v2)
        {
            return new Vector(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        public static Vector Multiply(Vector v1, Vector v2)
        {
            return new Vector(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
        }

        public static Vector Divide(Vector v1, Vector v2)
        {
            return new Vector(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z);
        }

        public static Vector Scale(Vector v, int amt)
        {
            return new Vector(v.x * amt, v.y * amt, v.z * amt);
        }

        public static Vector Rotate(Vector v1, Vector deg)
        {
            Vector v2 = new Vector(v1.x, v1.y, v1.z);
            v2.Rotate(deg);
            return v2;
        }

        public static Vector RotateX(Vector v1, float theta)
        {
            Vector v2 = new Vector(v1.x, v1.y, v1.z);
            v2.RotateX(theta);
            return v2;
        }

        public static Vector RotateY(Vector v1, float theta)
        {
            Vector v2 = new Vector(v1.x, v1.y, v1.z);
            v2.RotateY(theta);
            return v2;
        }

        public static Vector RotateZ(Vector v1, float theta)
        {
            Vector v2 = new Vector(v1.x, v1.y, v1.z);
            v2.RotateZ(theta);
            return v2;
        }

        public static float Angle2D(Vector v1, Vector v2)
        {
            return v1.Angle2D(v2);
        }

        public static float Dist2D(Vector v1, Vector v2)
        {
            return v1.Dist2D(v2);
        }
    }
}