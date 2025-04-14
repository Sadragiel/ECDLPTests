using System.Numerics;
using WeierstrassCurveTest.Types;
using WeierstrassCurveTest.Utils;

namespace WeierstrassCurveTest.EllipticCurves
{
    internal class WeierstrassCurve : EllipticCurve
    {
        private BigInteger a;
        private BigInteger b;


        public WeierstrassCurve(BigInteger a, BigInteger b, BigInteger p, BigInteger order) { 
            this.a = ModuloHelper.Abs(a, p);
            this.b = ModuloHelper.Abs(b, p);
            this.p = p;
            this.order = order;
        }

        public override bool TestPoint(Types.Point A)
        {
            // y^2 = x^3 + ax + b  mod(p)
            BigInteger left = ModuloHelper.Abs(A.y * A.y, p);
            BigInteger right = ModuloHelper.Abs(BigInteger.Pow(A.x, 3) + this.a * A.x + this.b, p);
            return left.Equals(right);
        }

        public override Types.Point GetRandomPoint()
        {
            while(true)
            {
                try
                {
                    BigInteger x = BigIntHelper.Random(0, p);
                    BigInteger quadraticResiduosity = ModuloHelper.Abs(BigInteger.Pow(x, 3) + this.a * x + this.b, p);
                    BigInteger y = ModuloHelper.SquareRootMod(quadraticResiduosity, p);
                    return new Types.Point(x, y);
                }
                catch (Exception e)
                {
                    // for generated x there is no valid value of y
                }
            }
        }

        public override Types.Point Add(Types.Point A, Types.Point B)
        {
            // Handle Doubling
            if (A.Equals(B))
            {
                return Double(A);
            }

            // handle points of Infinity:
            if (A.atInfinity)
            {
                return B;
            }

            if (B.atInfinity)
            {
                return A;
            }

            // A - A = 0
            if (A.x.Equals(B.x)) {
                return Types.Point.getPointAtInfinity();
            }

            // lambda = (y2 - y1) / (x2 - x1)
            BigInteger lambda = ModuloHelper.Abs((B.y - A.y) * ModuloHelper.MultInverse(B.x - A.x, p), p);

            return CalculatePoint(lambda, A, B);
        }

        public override Types.Point Double(Types.Point A)
        {
            if (A.atInfinity || A.y.Equals(0))
            {
                return Types.Point.getPointAtInfinity();
            }

            // lambda = (3x^2 + a) / (2y)
            BigInteger lambda = ModuloHelper.Abs((3 * BigInteger.ModPow(A.x, 2, p) + this.a) * ModuloHelper.MultInverse(2 * A.y, p), p);

            return CalculatePoint(lambda, A, A);
        }

        private Types.Point CalculatePoint(BigInteger lambda, Types.Point A, Types.Point B)
        {
            BigInteger x = ModuloHelper.Abs(BigInteger.ModPow(lambda, 2, p) - A.x - B.x, p);
            BigInteger y = ModuloHelper.Abs(lambda * (A.x - x) - A.y, p);

            return new Types.Point(x, y);
        }
    }
}
