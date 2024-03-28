using System.Numerics;

namespace WeierstrassCurveTest.Types
{
    internal class Point
    {
        public BigInteger x, y;
        public bool atInfinity;

        public Point(BigInteger x, BigInteger y)
        {
            this.x = x;
            this.y = y;
        }

        private Point() {
            this.atInfinity = true;
        }

        static public Point getPointAtInfinity() {
            return new Point();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Point p = (Point)obj;
            return p.atInfinity && this.atInfinity || p.x == this.x && p.y == this.y;
        }

        public override string ToString()
        {
            if (atInfinity)
            {
                return "(0; Inf)";
            }
            
            return $"({x}; {y})";
        }

        public void Print()
        {
            Console.WriteLine(ToString());
        }
    }
}
