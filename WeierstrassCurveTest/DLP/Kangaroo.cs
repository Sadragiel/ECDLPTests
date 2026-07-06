using System.Numerics;
using System.Security.Cryptography;
using WeierstrassCurveTest.EllipticCurves;
using WeierstrassCurveTest.Types;
using WeierstrassCurveTest.Utils;

using Point = WeierstrassCurveTest.Types.Point;

// WIP: Method is not working for large numbers
namespace WeierstrassCurveTest.DLP
{
    internal class Kangaroo : DLPMethod
    {
        BigInteger a;
        BigInteger b;
        double epsilon;
        int hashModulo;
        List<BigInteger> stepMap;

        // tunning koefficients
        double tameStepsCoefficient = .7;
        double wildStepsCoefficient = 3;
        double tameTrapsDistanceCoefficient = .1;

        public Kangaroo(EllipticCurve curve) : base(curve)
        {
            RecalculateInput(curve.Order());
        }

        public override void SetModulo(BigInteger modulo)
        {
            this.modulo = modulo;
            RecalculateInput(modulo);
        }

        public override BigInteger Solve(Point P, Point Q)
        {
            //Console.WriteLine($"Solving by Kangaroo method: P {P}, Q {Q}");
            //Console.WriteLine($"b: {b}");
            //Console.WriteLine($"hashModulo: {hashModulo}");
            //Console.WriteLine($"epsilon: {epsilon}");

            iterationsCount = 0;

            // Tame Kangaroo
            List<Tuple<Point, BigInteger>> tameKangarooTraps = GetTameKangarooTrap(P);
            //Console.WriteLine($"Tame kangaroo set the {tameKangarooTraps.Count} traps");

            // Wild kangaroo
            int maxWildKangarooSteps = (int)(wildStepsCoefficient * epsilon);
            int incrementFactor = (int)(epsilon / (double)hashModulo);
            Point wildKangarooStartingPoint = Q;
            Point startingPointIncrement = curve.Mult(P, incrementFactor);

            for (int i = 0; i < b; i++)
            {
                Point wildKangaroo = wildKangarooStartingPoint;
                BigInteger wildKangarooDistance = incrementFactor * i;

                for (int j = 0; j < maxWildKangarooSteps; j++)
                {
                    BigInteger stepLength = getStepLength(wildKangaroo/*, a, 2 * epsilon*/);

                    // Stepper function
                    wildKangaroo = curve.Add(wildKangaroo, curve.Mult(P, stepLength));
                    wildKangarooDistance += stepLength;

                    Tuple<Point, BigInteger> tameKangaroo = tameKangarooTraps.Find(tuple => tuple.Item1.Equals(wildKangaroo));

                    // Checking for the trap
                    if (tameKangaroo != null)
                    {
                        iterationsCount += j;
                        return ModuloHelper.Abs(tameKangaroo.Item2 - wildKangarooDistance, b);
                    }
                }

                //Console.WriteLine("Wild kangaroo has escaped!");
                iterationsCount += maxWildKangarooSteps;
                wildKangarooStartingPoint = curve.Add(wildKangarooStartingPoint, startingPointIncrement);
            }

            NotifyNoSolution();
            return 0;
        }

        private List<Tuple<Point, BigInteger>> GetTameKangarooTrap(Point P)
        {
            // Middle of the interval
            BigInteger tameKangarooStartingPosition = (b - a) / 2;

            Point tameKangaroo = curve.Mult(P, tameKangarooStartingPosition);
            Tuple<Point, BigInteger> tameKangarooTrap = new Tuple<Point, BigInteger>(tameKangaroo, tameKangarooStartingPosition);

            List<Tuple<Point, BigInteger>> traps = new List<Tuple<Point, BigInteger>> { tameKangarooTrap };

            int tameKangarooSteps = (int)(tameStepsCoefficient * epsilon);
            //Console.WriteLine($"tameKangarooSteps = {tameKangarooSteps}");

            int distanceBetweenTraps = (int)(tameTrapsDistanceCoefficient * tameKangarooSteps);
            BigInteger stepsFromLastTrap = 0;

            for (BigInteger i = 0; i < tameKangarooSteps; i++)
            {
                stepsFromLastTrap++;

                BigInteger stepLength = getStepLength(tameKangaroo/*, a, 2 * epsilon*/);

                // Stepper function
                tameKangaroo = curve.Add(tameKangaroo, curve.Mult(P, stepLength));

                // set up "traps" keeping the distance
                BigInteger distance = stepLength + tameKangarooTrap.Item2;
                tameKangarooTrap = new Tuple<Point, BigInteger>(tameKangaroo, distance);

                if (stepsFromLastTrap == distanceBetweenTraps || i == tameKangarooSteps - 1)
                {
                    stepsFromLastTrap = 0;
                    traps.Add(tameKangarooTrap);
                }
            }

            return traps;
        }

        private BigInteger getStepLength(Point kangarooPosition/*, BigInteger minStep, BigInteger maxStep*/)
        {
            //int previousPointHash = kangarooPosition.GetHashCode() % r;
            //if (!stepsMap.ContainsKey(previousPointHash))
            //{
            //    BigInteger step = BigIntHelper.Random(min, max);
            //    stepsMap[previousPointHash] = step;
            //}

            //return stepsMap[previousPointHash];

            int numberOfSubgroup = kangarooPosition.atInfinity ? 1 : (int)((kangarooPosition.x) % hashModulo);

            return stepMap[numberOfSubgroup];

            //if (kangarooPosition.atInfinity)
            //{
            //    return 1 /*minStep*/;
            //}

            //return /*minStep +*/ ((kangarooPosition.x) % hashModulo);

            //byte[] combinedBytes = (kangarooPosition.x + kangarooPosition.y).ToByteArray();
            ////byte[] yBytes = kangarooPosition.y.ToByteArray();

            ////byte[] combinedBytes = new byte[xBytes.Length + yBytes.Length];
            ////Buffer.BlockCopy(xBytes, 0, combinedBytes, 0, xBytes.Length);
            ////Buffer.BlockCopy(yBytes, 0, combinedBytes, xBytes.Length, yBytes.Length);

            //using (SHA256 sha256 = SHA256.Create())
            //{
            //    byte[] hashBytes = sha256.ComputeHash(combinedBytes);
            //    BigInteger hashValue = BigInteger.Abs(new BigInteger(hashBytes));

            //    BigInteger range = maxStep - minStep + 1;

            //    BigInteger stepLength = minStep + (hashValue % range);

            //    return stepLength;
            //}
        }

        private void RecalculateInput(BigInteger b)
        {
            // k is in range [a ,b] = [1, curve.Order()]
            this.a = 1;
            this.b = b;

            // epsilon is needed for adjusting the number of steps done by each kangaroo
            // epsilon - sqrt(b - a)
            epsilon = (double)BigIntHelper.Sqrt((b - a));

            // hashModulo is needed to map all points to the step length
            hashModulo = GetHashModulo();

            stepMap = GenerateModularListWithMean((BigInteger)epsilon, hashModulo, b);
        }

        private int GetHashModulo()
        {
            BigInteger epsilon = (BigInteger)this.epsilon;
            BigInteger sumStepLength = 0;

            for (int i = 1; i < 256; i++)
            {
                sumStepLength += BigInteger.Pow(2, (i - 1));

                BigInteger currentMeanStepSize = sumStepLength / i;
                BigInteger nextMeanStepSize = (sumStepLength + BigInteger.Pow(2, i)) / i;

                if (epsilon - currentMeanStepSize <= nextMeanStepSize - epsilon)
                {
                    return i;
                }
            }

            return 256;
        }

        private static List<BigInteger> GenerateModularListWithMean(BigInteger m, int n, BigInteger N)
        {
            if (n <= 0) throw new ArgumentException("n must be positive");
            if (N <= 1) throw new ArgumentException("N must be > 1");

            var rnd = new Random();
            List<BigInteger> list = new List<BigInteger>();

            // Generate n - 1 random values mod N
            BigInteger sum = 0;
            for (int i = 0; i < n - 1; i++)
            {
                byte[] buffer = new byte[N.GetByteCount()];
                BigInteger xi;

                do
                {
                    rnd.NextBytes(buffer);
                    xi = new BigInteger(buffer) % N;
                    if (xi < 0) xi += N;
                } while (xi >= N);

                list.Add(xi);
                sum = (sum + xi) % N;
            }

            // Compute the final value to satisfy the mean condition
            BigInteger requiredSum = (n * m) % N;
            BigInteger last = (requiredSum - sum + N) % N;
            list.Add(last);

            return list;
        }

    }
}
