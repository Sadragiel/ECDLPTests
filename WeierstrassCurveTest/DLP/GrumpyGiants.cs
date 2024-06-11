using System.Numerics;
using WeierstrassCurveTest.EllipticCurves;
using WeierstrassCurveTest.Types;
using WeierstrassCurveTest.Utils;

namespace WeierstrassCurveTest.DLP
{
    internal class GrumpyGiants : DLPMethod
    {
        BigInteger multInvToTwo;

        public GrumpyGiants(EllipticCurve curve) : base(curve)
        {
            multInvToTwo = ModuloHelper.MultInverse(2, curve.Order());
        }

        public override BigInteger Solve(Point P, Point Q)
        {
            // Params
            BigInteger N = curve.Order();
            int n = 2;
            int m = (int)Math.Ceiling(Math.Sqrt((double)N / 2.0));

            // Baby Steps: { P + inP }
            List <Point> babySteps = new List<Point> { P };
            Point babyStep = curve.Mult(P, n);

            // Giant 1: { Q + imP }
            List<Point> giant1Steps = new List<Point> { Q };
            Point giant1Step = curve.Mult(P, m);

            // Giant 1: { 2Q - iP(m + 1) }
            List<Point> giant2Steps = new List<Point> { curve.Mult(Q, 2) };
            Point giant2Step = curve.Invert(curve.Mult(P, m + 1));

            // Iteration process
            for (int i = 1; i <= m + n; i++)
            {
                Point newBaby = curve.Add(babySteps.Last(), babyStep);
                Point newGiant1 = curve.Add(giant1Steps.Last(), giant1Step);
                Point newGiant2 = curve.Add(giant2Steps.Last(), giant2Step);

                babySteps.Add(newBaby);
                giant1Steps.Add(newGiant1);
                giant2Steps.Add(newGiant2);

                int j = giant1Steps.IndexOf(newBaby);
                // k = 1 + in - jm
                if (j != -1)
                {
                    return 1 + i * n - j * m;
                }

                j = giant2Steps.IndexOf(newBaby);
                // k = (1 + in + jm + j) * MultInv(2, N)
                if (j != -1)
                {
                    return (1 + i * n + j * m + j) * multInvToTwo;
                }

                j = babySteps.IndexOf(newGiant1);
                // k = 1 + jn - im
                if (j != -1)
                {
                    return 1 + j * n - i * m;
                }

                j = giant2Steps.IndexOf(newGiant1);
                // k = im + jm + j
                if (j != -1)
                {
                    return i * m + j * m + j;
                }

                j = babySteps.IndexOf(newGiant2);
                // k = (1 + jn + im + i) * MultInv(2, N)
                if (j != -1)
                {
                    return (1 + j * n + i * m + i) * multInvToTwo;
                }

                j = giant1Steps.IndexOf(newGiant2);
                // k = jm + im + i
                if (j != -1)
                {
                    return j * m + i * m + i;
                }
            }

            NotifyNoSolution();
            return 0;
        }
    }
}
