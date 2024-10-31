using System.Numerics;
using System.Security.Cryptography;
using WeierstrassCurveTest.EllipticCurves;
using WeierstrassCurveTest.Types;
using WeierstrassCurveTest.Utils;


// WIP: Method is not working for large numbers
namespace WeierstrassCurveTest.DLP
{
    internal class Kangaroo : DLPMethod
    {
        BigInteger a;
        BigInteger b;
        BigInteger epsilon;
        BigInteger hashModulo;

        int r = 50;

        public Kangaroo(EllipticCurve curve) : base(curve) {
            // k is in range [a ,b] = [1, curve.Order()]
            a = 0;
            b = curve.Order();

            // epsilon is needed for adjusting the number of steps done by each kangaroo
            // epsilon - sqrt(b - a)
            epsilon = BigIntHelper.Sqrt((b - a));

            // hashModulo is needed to map all points to the step length
            hashModulo = GetHashModulo();
        }

        public override BigInteger Solve(Point P, Point Q)
        {
            // Tame Kangaroo
            List<Tuple<Point, BigInteger>> tameKangarooTraps = GetTameKangarooTrap(P, a, b, epsilon);

            // Wild kangaroo
            BigInteger maxWildKangarooSteps = 27 * epsilon / 10;
            int incrementFactor = 2;
            Point wildKangarooStartingPoint = Q;
            Point startingPointIncrement = curve.Mult(P, incrementFactor);

            for (int i = 0; i < b; i++)
            {
                Point wildKangaroo = wildKangarooStartingPoint;
                BigInteger wildKangarooDistance = incrementFactor * i;

                for (int j = 0; j < maxWildKangarooSteps; j++)
                {
                    BigInteger stepLength = getStepLength(wildKangaroo, a, 2 * epsilon);

                    // Stepper function
                    wildKangaroo = curve.Add(wildKangaroo, curve.Mult(P, stepLength));
                    wildKangarooDistance += stepLength;

                    Tuple<Point, BigInteger> tameKangaroo = tameKangarooTraps.Find(tuple => tuple.Item1.Equals(wildKangaroo));

                    // Checking for the trap
                    if (tameKangaroo != null)
                    {
                        return ModuloHelper.Abs(tameKangaroo.Item2 - wildKangarooDistance, curve.Order());
                    }
                }

                Console.WriteLine("Wild kangaroo has escaped!");

                wildKangarooStartingPoint = curve.Add(wildKangarooStartingPoint, startingPointIncrement);
            }

            NotifyNoSolution();
            return 0;
        }

        private List<Tuple<Point, BigInteger>> GetTameKangarooTrap(Point P, BigInteger a, BigInteger b, BigInteger epsilon)
        {
            Point tameKangaroo = curve.Mult(P, b);
            Tuple<Point, BigInteger> tameKangarooTrap = new Tuple<Point, BigInteger>(tameKangaroo, b);

            List<Tuple<Point, BigInteger>> traps = new List<Tuple<Point, BigInteger>> { tameKangarooTrap };

            BigInteger tameKangarooSteps = 7 * epsilon / 10;
            Console.WriteLine($"tameKangarooSteps = {tameKangarooSteps}");

            BigInteger distanceBetweenTraps = 1;
            BigInteger stepsFromLastTrap = 0;

            for (BigInteger i = 0; i < tameKangarooSteps; i++)
            {
                stepsFromLastTrap++;

                BigInteger stepLength = getStepLength(tameKangaroo, a, 2 * epsilon);

                // Stepper function
                tameKangaroo = curve.Add(tameKangaroo, curve.Mult(P, stepLength));

                // set up "traps" keeping the distance
                BigInteger distance = stepLength + tameKangarooTrap.Item2;
                tameKangarooTrap = new Tuple<Point, BigInteger>(tameKangaroo, distance);

                if (stepsFromLastTrap == distanceBetweenTraps)
                {
                    stepsFromLastTrap = 0;
                    traps.Add(tameKangarooTrap);
                }
            }

            return traps;
        }

        private BigInteger getStepLength(Point kangarooPosition, BigInteger minStep, BigInteger maxStep)
        {
            //int previousPointHash = kangarooPosition.GetHashCode() % r;
            //if (!stepsMap.ContainsKey(previousPointHash))
            //{
            //    BigInteger step = BigIntHelper.Random(min, max);
            //    stepsMap[previousPointHash] = step;
            //}

            //return stepsMap[previousPointHash];

            if (kangarooPosition.atInfinity)
            {
                return minStep; // Or any other predefined step length you prefer
            }

            return minStep + ((kangarooPosition.x) % hashModulo);

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

        private int GetHashModulo()
        {
            BigInteger sumStepLength = 0;

            for (int i = 1; i < 256; i++)
            {
                sumStepLength += BigInteger.Pow(2,  (i - 1));

                BigInteger currentMeanStepSize = sumStepLength / i;
                BigInteger nextMeanStepSize = (sumStepLength + BigInteger.Pow(2, i)) / i;

                if (epsilon - currentMeanStepSize <= nextMeanStepSize  - epsilon)
                {
                    return i;
                }
            }

            return 256;
        }
    }
}
