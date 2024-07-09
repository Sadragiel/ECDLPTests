using System.Numerics;
using WeierstrassCurveTest.Types;
using WeierstrassCurveTest.Utils;

namespace WeierstrassCurveTest.EllipticCurves
{
    internal class EdwardsCurve : EllipticCurve
    {
        private BigInteger d;

        public EdwardsCurve( BigInteger d, BigInteger p, BigInteger order)
        {
            this.d = ModuloHelper.Abs(d, p);
            this.p = p;


            this.order = order;
        }

        public override bool TestPoint(Point A)
        {
            // y^2 + x^2 = 1 + d * x^2 + y^2  mod(p)
            BigInteger x2 = ModuloHelper.Abs(A.x * A.x, p);
            BigInteger y2 = ModuloHelper.Abs(A.y * A.y, p);
            BigInteger left = ModuloHelper.Abs(y2 + x2, p);
            BigInteger right = ModuloHelper.Abs(1 + d * x2 * y2, p);
            return left.Equals(right);
        }

        public override Point GetRandomPoint()
        {
            while (true)
            {
                try
                {
                    BigInteger x = BigIntHelper.Random(0, p);

                    // y^2 = (1 - x^2) / (1 - d * x^2)
                    BigInteger x2 = ModuloHelper.Abs(x * x, p);
                    BigInteger multInvPart = ModuloHelper.MultInverse(ModuloHelper.Abs(1 - d * x2, p), p);
                    BigInteger quadraticResiduosity = ModuloHelper.Abs((1 - x2) * multInvPart, p);

                    BigInteger y = ModuloHelper.SquareRootMod(quadraticResiduosity, p);
                    return new Point(x, y);
                }
                catch (Exception e)
                {
                    // for generated x there is no valid value of y
                }
            }
        }

        public override Point Add(Point A, Point B)
        {
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
            if (A.x.Equals(B.x) && A.y.Equals(ModuloHelper.AddInverse(B.y, p)))
            {
                return Point.getPointAtInfinity();
            }

            // (x1, y1) + (x2, y2) = ( (x1y2 + x2y1) / (1 + dx1x2y1y2), (y1y2 - x1x2) / (1 - dx1x2y1y2) )
            BigInteger x1y2 = ModuloHelper.Abs(A.x * B.y, p);
            BigInteger x2y1 = ModuloHelper.Abs(B.x * A.y, p);
            BigInteger y1y2 = ModuloHelper.Abs(A.y * B.y, p);
            BigInteger x1x2 = ModuloHelper.Abs(A.x * B.x, p);
            BigInteger dx1x2y1y2 = ModuloHelper.Abs(d * x1x2 * y1y2, p);



            BigInteger x3 = ModuloHelper.Abs((x1y2 + x2y1) * ModuloHelper.MultInverse(1 + dx1x2y1y2, p), p);
            BigInteger y3 = ModuloHelper.Abs((y1y2 - x1x2) * ModuloHelper.MultInverse(1 - dx1x2y1y2, p), p);

            return new Point(x3, y3);
        }

        public override Point Double(Point A)
        {
            return Add(A, A);
        }
    }
}
