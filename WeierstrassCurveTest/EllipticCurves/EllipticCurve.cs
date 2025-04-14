using System.Numerics;
using WeierstrassCurveTest.Types;
using WeierstrassCurveTest.Utils;

namespace WeierstrassCurveTest.EllipticCurves
{
    internal abstract class EllipticCurve
    {
        protected BigInteger order; // TODO: calculate order without third-party

        protected BigInteger p;

        public List<Types.Point> pointsOnAbscissaAxis = new List<Types.Point>();

        protected EllipticCurve() { }

        public BigInteger Order() { return order; }

        public BigInteger Modulo() { return p; }

        public abstract bool TestPoint(Types.Point A);

        public abstract Types.Point GetRandomPoint();

        public abstract Types.Point Add(Types.Point A, Types.Point B);

        public abstract Types.Point Double(Types.Point A);

        public void setPointsWithYZero(List<Types.Point> pointsOnAbscissaAxis)
        {
            this.pointsOnAbscissaAxis = pointsOnAbscissaAxis;
        }

        public virtual Types.Point Invert(Types.Point A)
        {
            if (A.atInfinity)
            {
                return Types.Point.getPointAtInfinity();
            }
            return new Types.Point(A.x, ModuloHelper.AddInverse(A.y, p));
        }

        public virtual Types.Point Mult(Types.Point A, BigInteger k)
        {
            int sign = k.Sign;
            k = BigInteger.Abs(k);

            // Double-and-add method of point multiplication
            bool[] factorBitsRepresentation = BigIntHelper.BigIntegerToBitsArray(k);

            Types.Point res = Types.Point.getPointAtInfinity();
            Types.Point temp = A;

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

        public BigInteger GetPointOrder(Types.Point A)
        {
            Types.Point multiple = A;
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
