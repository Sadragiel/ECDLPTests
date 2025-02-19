using System.Numerics;
using WeierstrassCurveTest.EllipticCurves;
using WeierstrassCurveTest.Types;
using WeierstrassCurveTest.Utils;

using ArgumentsList = System.Collections.Generic.List<System.Tuple<System.Numerics.BigInteger, System.Numerics.BigInteger, System.Numerics.BigInteger>>;
using Triplet = System.Tuple<WeierstrassCurveTest.Types.Point, System.Numerics.BigInteger, System.Numerics.BigInteger>;


namespace WeierstrassCurveTest.DLP
{
    internal class PollardRho : DLPMethod
    {
        // Metadata:
        public int iterationsCount = 0;
        public bool foundWithNegationMap = false;
        public bool foundWithExtendedNegationMap = false;

        int numberOfSubgroups = 20;

        ArgumentsList argumentsList;
        List<Point> pointsList;

        bool negationMapEnabled;
        bool extendedNegationMapEnabled;

        public PollardRho(EllipticCurve curve) : base(curve)
        {
            //argumentsList =
            //[
            //    new Tuple<BigInteger, BigInteger, BigInteger>(1, 1, 0),
            //    new Tuple<BigInteger, BigInteger, BigInteger>(2, 0, 0),
            //    new Tuple<BigInteger, BigInteger, BigInteger>(1, 0, 1),
            //];
            argumentsList = new ArgumentsList();
            pointsList = new List<Point>();

            // argumentsList[numberOfSubgroups] is the pair of arguments of a startign point P0
            for (int i = 0; i <= numberOfSubgroups; i++)
            {
                BigInteger a, b;
                do
                {
                    a = GetRandomArgument();
                    b = GetRandomArgument();
                } while (argumentsList.FindIndex((tuple) => tuple.Item2 == a && tuple.Item3 == b) != -1);

                argumentsList.Add(new Tuple<BigInteger, BigInteger, BigInteger>(1, a, b));
            }

        }

        public void EnableNegationMaps(bool extended)
        {
            negationMapEnabled = true;
            extendedNegationMapEnabled = extended;
        }

        public override BigInteger Solve(Point P, Point Q)
        {
            // filling up points list in order to avoid recalculations
            foreach (Tuple<BigInteger, BigInteger, BigInteger> arguments in argumentsList)
            {
                pointsList.Add(curve.Add(curve.Mult(P, arguments.Item2), curve.Mult(Q, arguments.Item3)));
            }

            // piecewise-defined mapping function F(V) = V + ai * P + bi * Q
            // arguments ai and bi:

            // starting point

            var pair0 = argumentsList[numberOfSubgroups];
            var P0 = pointsList[numberOfSubgroups];

            Triplet hareTriplet = new Triplet(P0, pair0.Item2, pair0.Item3);

            for (int i = 1; true; i *= 2)
            {
                Triplet turtleTriplet = hareTriplet;

                for (int j = i + 1; j <= i * 2 + 1; j++)
                {
                    turtleTriplet = IncrementTriplet(turtleTriplet, P, Q);

                    // checking for collision
                    Tuple<int, int> colisionSearchResult = CollisionCheck(hareTriplet, turtleTriplet);
                    if (colisionSearchResult != null)
                    {
                        //Console.WriteLine($"Collision solved on itteration ({j}): P{i} == P{j}");
                        var k = CollisionSolve(hareTriplet, turtleTriplet, colisionSearchResult);

                        if (k != 0)
                        {
                            iterationsCount = j;
                            return k;
                        }
                        foundWithExtendedNegationMap = false;
                        foundWithNegationMap = false;
                    }
                }
                hareTriplet = turtleTriplet;
            }
            NotifyNoSolution();
            return 0;
        }

        private Triplet IncrementTriplet(Triplet triplet, Point P, Point Q)
        {
            // Stepping Function 
            // set (a_i, b_i, c_i)
            int subgroupNumber = GetSubgroupNumber(triplet.Item1);
            Tuple<BigInteger, BigInteger, BigInteger> argumentPair = argumentsList[subgroupNumber];
            Point Pi = pointsList[subgroupNumber];

            // P_ (i+1) = a_i * P_i + b_i * P + c_i * Q
            Point incrementedPoint = curve.Add(
                triplet.Item1,
                Pi
            );

            // Pj = uj * P + vj * Q
            BigInteger u = argumentPair.Item2 + triplet.Item2 ;
            BigInteger v = argumentPair.Item3 + triplet.Item3 ;

            return new Triplet(incrementedPoint, ModuloHelper.Abs(u, curve.Order()), ModuloHelper.Abs(v, curve.Order()));
        }

        // This function returns tuple (sign, multiplier) which will be used in dinding discrete logarithm,
        // or null if collision is not found
        private Tuple<int, int> CollisionCheck(Triplet hareTriplet, Triplet turtleTriplet)
        {
            // anti-collision: u1 == u2 <=> v1 == v2
            if (turtleTriplet.Item2.Equals(hareTriplet.Item2) || turtleTriplet.Item3.Equals(hareTriplet.Item3))
            {
                return null;
            }

            // same point: P1 == P2
            if (turtleTriplet.Item1.Equals(hareTriplet.Item1))
            {
                //Console.WriteLine($"same point: P1{turtleTriplet.Item1} == P2{hareTriplet.Item1}");
                return new Tuple<int, int>(1, 1);
            }

            // Negastion map: P1 == -P2
            if (negationMapEnabled && turtleTriplet.Item1.Equals(curve.Invert(hareTriplet.Item1)))
            {
                //Console.WriteLine($"Negastion map: P1 == -P2");
                foundWithNegationMap = true;
                return new Tuple<int, int>(-1, 1);
            }

            // Extended negation map
            if (extendedNegationMapEnabled)
            {
                // TODO: move to the 
                var pointsOfOrder2 = (curve as WeierstrassCurve).pointsOnAbscissaAxis;
                Point P_inv = curve.Invert(turtleTriplet.Item1);

                foreach (Point Y in pointsOfOrder2)
                {
                    Point PY = curve.Add(hareTriplet.Item1, Y);
                    if (turtleTriplet.Item1.Equals(PY))
                    {
                        //Console.WriteLine($"P1 === P2 + Y0");
                        foundWithExtendedNegationMap = true;
                        return new Tuple<int, int>(1, 2);
                    }

                    // -P1 === P2 + Y0
                    if (P_inv.Equals(PY))
                    {
                        //Console.WriteLine($"-P1 === P2 + Y0");
                        foundWithExtendedNegationMap = true;
                        return new Tuple<int, int>(-1, 2);
                    }
                }
            }

            return null;
        }

        private BigInteger CollisionSolve(Triplet hareTriplet, Triplet turtleTriplet, Tuple<int, int> colisionSearchResult)
        {
            // k = (ui -+ uj) / (vj -+ vi) mod(N / gcd(N, vj -+ vi))
            int sign = colisionSearchResult.Item1;
            int multiplier = colisionSearchResult.Item2;
            BigInteger N = curve.Order();
            BigInteger ui = hareTriplet.Item2 * multiplier;
            BigInteger vi = hareTriplet.Item3 * multiplier;
            BigInteger uj = turtleTriplet.Item2 * sign * multiplier;
            BigInteger vj = turtleTriplet.Item3 * sign * multiplier;
            BigInteger u = ModuloHelper.Abs(ui - uj, N);
            BigInteger v = ModuloHelper.Abs(vj - vi, N);

            BigInteger gcd = BigIntHelper.GCD(N, v, out _, out _);
            BigInteger modulo = N / gcd;
            //Console.WriteLine($"N: {N}; gcd: {gcd}; modulo: {modulo}");
            //Console.WriteLine($"vi: {vi}; vj: {vj}; v: {v}; gcd(v, modulo): {BigIntHelper.GCD(v, modulo, out _, out _)}");

            if (BigIntHelper.GCD(v, modulo, out _, out _) > 1)
            {
                return 0; // anti-collision, The collision information is not sufficient to solve k uniquely 
            }

            //Console.WriteLine($"modulo: {modulo}");
            //Console.WriteLine($"u: {u}");
            //Console.WriteLine($"ModuloHelper.MultInverse(v, modulo): {ModuloHelper.MultInverse(v, modulo)}");
            //Console.WriteLine($"hareTriplet: {ui} * P + {vi} * Q = {hareTriplet.Item1}");
            //Console.WriteLine($"turtleTriplet: {uj} * P + {vj} * Q = {turtleTriplet.Item1}");

            return ModuloHelper.Abs(u * ModuloHelper.MultInverse(v, modulo), N);
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
