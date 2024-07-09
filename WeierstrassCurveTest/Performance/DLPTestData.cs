using System.Numerics;
using WeierstrassCurveTest.EllipticCurves;
using WeierstrassCurveTest.Types;
using WeierstrassCurveTest.Utils;

namespace WeierstrassCurveTest.Performance
{
    internal class DLPTestData
    {
        public EllipticCurve curve;
        // generated points Q = kP
        public Point P;
        public Point Q;
        public BigInteger orderP;
        public BigInteger expectedK;


        public DLPTestData(EllipticCurve curve)
        {
            SetCurve(curve);
        }

        public void SetCurve(EllipticCurve curve)
        {
            this.curve = curve;
            UpdateSelectedPoints();
        }

        public void UpdateSelectedPoints()
        {
            P = curve.GetRandomPoint();
            orderP = curve.GetPointOrder(P);
            expectedK = BigIntHelper.Random(1, orderP);
            Q = curve.Mult(P, expectedK);
        }
    }
}
