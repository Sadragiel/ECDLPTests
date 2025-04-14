using System.Numerics;
using WeierstrassCurveTest.EllipticCurves;
using WeierstrassCurveTest.Types;

using Point = WeierstrassCurveTest.Types.Point;

namespace WeierstrassCurveTest.DLP
{
    internal abstract class DLPMethod
    {
        protected EllipticCurve curve;

        protected BigInteger modulo;

        protected bool negationMapEnabled;
        protected bool extendedNegationMapEnabled;

        public DLPMethod(EllipticCurve curve)
        {
            this.curve = curve;
            SetModulo(curve.Order());
        }

        abstract public BigInteger Solve(Point P, Point Q);
        
        public virtual void SetModulo(BigInteger modulo)
        {
            this.modulo = modulo;
        }

        public bool Test(Point P, Point Q, BigInteger k) {
            Point expected = curve.Mult(P, k);

            return Q.Equals(expected);
        }

        protected void NotifyNoSolution()
        {
            throw new Exception("No solution found.");
        }

        public virtual void EnableNegationMaps(bool extended)
        {
            negationMapEnabled = true;
            extendedNegationMapEnabled = extended;
        }
    }
}
