using System.Diagnostics;
using System.Numerics;
using WeierstrassCurveTest.DLP;
using WeierstrassCurveTest.EllipticCurves;

namespace WeierstrassCurveTest.Performance
{
    internal class PerformanceEvaluator
    {
        public Dictionary<string, double> Evaluate(EllipticCurve curve)
        {
            List<DLPMethod> methods = new List<DLPMethod>
            {
                new BSGS(curve),
                new GrumpyGiants(curve),
                new Kangaroo(curve),
                new PollardRho(curve),
            };
            DLPTestData data = new DLPTestData(curve);
            var results = new Dictionary<string, double>();

            foreach (var method in methods)
            {
                (BigInteger solution, double time) = EvaluateTimeComplexity(method, data, results);
                bool isCorrect = method.Test(data.P, data.Q, solution);
                // TODO: store data: time and correctness
            }
            return results;
        }

        private (BigInteger, double) EvaluateTimeComplexity(DLPMethod method, DLPTestData data, Dictionary<string, double> storedResults)
        {
            var stopwatch = Stopwatch.StartNew();
            var solution = method.Solve(data.P, data.Q);
            stopwatch.Stop();

            return (solution, stopwatch.ElapsedMilliseconds);
        }
    }
}
