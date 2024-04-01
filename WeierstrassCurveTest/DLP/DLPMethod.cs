using System.Numerics;
using WeierstrassCurveTest.EllipticCurves;
using WeierstrassCurveTest.Types;

namespace WeierstrassCurveTest.DLP
{
    internal abstract class DLPMethod
    {
        protected EllipticCurve curve;

        public DLPMethod(EllipticCurve curve)
        {
            this.curve = curve;
        }

        abstract public BigInteger Solve(Point P, Point Q);

        public bool Test(Point P, Point Q, BigInteger k) {
            Point expected = curve.Mult(P, k);

            return Q.Equals(expected);
        }

        protected void NotifyNoSolution()
        {
            throw new Exception("No solution found.");
        }
    }
}
