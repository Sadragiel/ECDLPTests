using System.Numerics;
using WeierstrassCurveTest.EllipticCurves;
using WeierstrassCurveTest.Types;
using WeierstrassCurveTest.Utils;

using ArgumentsList = System.Collections.Generic.List<System.Tuple<System.Numerics.BigInteger, System.Numerics.BigInteger>>;
using Triplet = System.Tuple<WeierstrassCurveTest.Types.Point, System.Numerics.BigInteger, System.Numerics.BigInteger>;


namespace WeierstrassCurveTest.DLP
{
    internal class PollardRho : DLPMethod
    {
        int numberOfSubgroups = 20;

        ArgumentsList argumentsList;

        public PollardRho(EllipticCurve curve) : base(curve)
        {
            argumentsList = new ArgumentsList();
            for (int i = 0; i < numberOfSubgroups; i++)
            {
                argumentsList.Add(
                    new Tuple<BigInteger, BigInteger>(
                        GetRandomArgument(),
                        GetRandomArgument()
                    )
                );
            }
        }

        public override BigInteger Solve(Point P, Point Q)
        {
            // piecewise-defined mapping function F(V) = V + ai * P + bi * Q
            // arguments ai and bi:


            Triplet activeTriplet = null;
            Triplet testedForCollisionTriplet = null;

            for (int i = 1; true; i *= 2)
            {
                activeTriplet = IncrementTriplet(testedForCollisionTriplet, P, Q);
                testedForCollisionTriplet = activeTriplet;

                for (int j = i + 1; j < i * 2; j++)
                {
                    testedForCollisionTriplet = IncrementTriplet(testedForCollisionTriplet, P, Q);

                    // checking for collision
                    if (testedForCollisionTriplet.Item1.Equals(activeTriplet.Item1))
                    {
                        // k = (ui - uj) / (vj - vi) mod(N / gcd(N, vj - vi))
                        BigInteger N = curve.Order();
                        BigInteger ui = activeTriplet.Item2;
                        BigInteger vi = activeTriplet.Item3;
                        BigInteger uj = testedForCollisionTriplet.Item2;
                        BigInteger vj = testedForCollisionTriplet.Item3;

                        BigInteger modulo = N / BigIntHelper.GCD(N, BigInteger.Abs(vj - vi), out _, out _);

                        return ModuloHelper.Abs((ui - uj) * ModuloHelper.MultInverse(vj - vi, modulo), modulo);
                    }
                }
            }
            NotifyNoSolution();
            return 0;
        }

        private Triplet IncrementTriplet(Triplet activeTriplet, Point P, Point Q)
        {
            // starting point P0 = a0 * P + b0 * Q
            if (activeTriplet == null)
            {
                BigInteger a0 = GetRandomArgument();
                BigInteger b0 = GetRandomArgument();
                Point P0 = curve.Add(curve.Mult(P, a0), curve.Mult(Q, b0));
                return new Triplet(P0, a0, b0);
            }

            // Stepping Function F(V) = V + ai * P + bi * Q
            // pair (ai, bi)
            Tuple<BigInteger, BigInteger> argumentPair = argumentsList[GetSubgroupNumber(activeTriplet.Item1)];

            // P(i+1) = Pi + ai * P + bi * Q
            Point incrementedPoint = curve.Add(activeTriplet.Item1, curve.Add(curve.Mult(P, argumentPair.Item1), curve.Mult(Q, argumentPair.Item2)));

            // Pj = uj * P + vj * Q
            BigInteger u = argumentPair.Item1 + activeTriplet.Item2;
            BigInteger v = argumentPair.Item2 + activeTriplet.Item3;

            return new Triplet(incrementedPoint, u, v);
        }

        private int GetSubgroupNumber(Point P)
        {
            return (int)ModuloHelper.Abs(P.GetHashCode(), numberOfSubgroups);
        }

        private BigInteger GetRandomArgument()
        {
            return BigIntHelper.Random(1, curve.Order());
        }
    }
}
