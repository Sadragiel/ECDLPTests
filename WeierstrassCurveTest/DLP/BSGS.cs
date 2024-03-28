using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WeierstrassCurveTest.EllipticCurves;
using WeierstrassCurveTest.Types;

namespace WeierstrassCurveTest.DLP
{
    internal class BSGS : DLPMethod
    {
        public BSGS(EllipticCurve curve) : base(curve) { }

        public override BigInteger Solve(Point P, Point Q)
        {
            BigInteger N = curve.Order();
            // Solution: k = mq - r
            int m = (int)Math.Ceiling(Math.Sqrt((double)N));

            // Baby Steps: { iP }, 0 <= i <= m
            List<Point> babySteps = new List<Point> { Point.getPointAtInfinity() };
            for (int i = 1; i <= m; i++)
            {
                babySteps.Add(curve.Add(P, babySteps.Last()));
            }

            // Giant Steps: { Q - jmP }, 0 <= j <= (m - 1)
            Point giantStep = curve.Mult(P, m);
            Point giantPointer = Q;
            int j = 0;

            do
            {
                // Check for collision
                int i = babySteps.IndexOf(giantPointer);

                if (i != -1)
                {
                    // k = i + jm (mod N)

                    return (i + j * m) % N;
                }

                // Giant takes a step
                giantPointer = curve.Add(giantPointer, curve.Invert(giantStep));
                j++;

            } while (j <= m - 1);

            throw new Exception("No solution found.");
        }
    }
}
