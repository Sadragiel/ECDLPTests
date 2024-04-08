using System.Numerics;
using WeierstrassCurveTest.EllipticCurves;
using WeierstrassCurveTest.Types;

namespace WeierstrassCurveTest.DLP
{
    internal class Kangaroo : DLPMethod
    {
        Random random = new Random();

        public Kangaroo(EllipticCurve curve) : base(curve) { }

        public override BigInteger Solve(Point P, Point Q)
        {
            // k is in range [a ,b] = [1, curve.Order()]
            int a = 1;
            int b = (int)curve.Order();

            // epsilon is needed for adjusting the number of steps done by each kangaroo
            // epsilon - sqrt(b - a)
            int epsilon = (int)Math.Ceiling(Math.Sqrt((double)(b - a)));
            
            // Steps map
            Dictionary<int, int> stepsMap = new Dictionary<int, int>();

            // Tame Kangaroo
            Point tameKangaroo = curve.Mult(P, b);
            List <Tuple <Point, int>> tameKangarooPath = GetTameKangarooPath(P, stepsMap, a, b, epsilon);

            // Wild kangaroo
            int maxWildKangarooSteps = (int)Math.Ceiling(2.7 * epsilon);
            int incrementFactor = 2;
            Point wildKangarooStartingPoint = Q;
            Point startingPointIncrement = curve.Mult(P, incrementFactor);

            for (int i = 0; i < b; i++)
            {
                Point wildKangaroo = wildKangarooStartingPoint;
                int wildKangarooDistance = incrementFactor * i;

                for (int j = 0; j < maxWildKangarooSteps; j++)
                {
                    int stepLength = getStepLength(wildKangaroo, stepsMap, a, epsilon);

                    // Stepper function
                    wildKangaroo = curve.Add(wildKangaroo, curve.Mult(P, stepLength));
                    wildKangarooDistance += stepLength;

                    // Checking for the trap
                    var collision = tameKangarooPath.Find((Tuple<Point, int> item) => item.Item1.Equals(wildKangaroo));
                    
                    if (collision != null)
                    {
                        return collision.Item2 - wildKangarooDistance;
                    }
                }

                wildKangarooStartingPoint = curve.Add(wildKangarooStartingPoint, startingPointIncrement);
            }

            NotifyNoSolution();
            return 0;
        }

        private List<Tuple<Point, int>> GetTameKangarooPath(Point P, Dictionary<int, int> stepsMap, int a, int b, int epsilon)
        {
            Point tameKangaroo = curve.Mult(P, b);
            List<Tuple<Point, int>> tameKangarooPath = new List<Tuple<Point, int>>
            {
                new Tuple<Point, int>(tameKangaroo, b),
            };

            int tameKangarooSteps = (int)Math.Ceiling(0.7 * epsilon);
            for (int i = 0; i < tameKangarooSteps; i++)
            {
                int stepLength = getStepLength(tameKangaroo, stepsMap, a, epsilon);

                // Stepper function
                tameKangaroo = curve.Add(tameKangaroo, curve.Mult(P, stepLength));

                // set up "traps" keeping the distance
                int distance = stepLength + tameKangarooPath.Last().Item2;
                tameKangarooPath.Add(new Tuple<Point, int>(tameKangaroo, distance));
            }

            return tameKangarooPath;
        }

        private int getStepLength(Point kangarooPosition, Dictionary<int, int> stepsMap, int min, int max)
        {
            int previousPointHash = kangarooPosition.GetHashCode();
            if (!stepsMap.ContainsKey(previousPointHash))
            {
                int step = random.Next(min, max);
                stepsMap[previousPointHash] = step;
            }

            return stepsMap[previousPointHash];
        }
    }
}
