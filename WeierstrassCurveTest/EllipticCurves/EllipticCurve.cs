using System.Numerics;
using WeierstrassCurveTest.Types;
using WeierstrassCurveTest.Utils;

namespace WeierstrassCurveTest.EllipticCurves
{
    internal abstract class EllipticCurve
    {
        protected BigInteger order; // TODO: calculate order without third-party

        protected BigInteger p;

        protected EllipticCurve() { }

        public BigInteger Order() { return order; }

        public BigInteger Modulo() { return p; }

        public abstract bool TestPoint(Point A);

        public abstract Point GetRandomPoint();

        public abstract Point Add(Point A, Point B);

        public abstract Point Double(Point A);

        public virtual Point Invert(Point A)
        {
            return new Point(A.x, ModuloHelper.AddInverse(A.y, p));
        }

        public virtual Point Mult(Point A, BigInteger k)
        {
            int sign = k.Sign;
            k = BigInteger.Abs(k);

            // Double-and-add method of point multiplication
            bool[] factorBitsRepresentation = BigIntHelper.BigIntegerToBitsArray(k);

            Point res = Point.getPointAtInfinity();
            Point temp = A;

            foreach (bool b in factorBitsRepresentation)
            {
                if (b)
                {
                    res = Add(res, temp);
                }

                temp = Double(temp);
            }

            return sign == 1 ? res : Invert(res);
        }

        public BigInteger GetPointOrder(Point A)
        {
            Point multiple = A;
            BigInteger order;
            for (order = BigInteger.One; order < this.order && !multiple.atInfinity; order++)
            {
                multiple = Add(multiple, A);
            }
            // TODO: throw an error if order of the curve is not dividible by the order of point
            return order;
        }
    }
}
