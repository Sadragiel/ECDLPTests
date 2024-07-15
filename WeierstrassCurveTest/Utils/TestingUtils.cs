using System.Numerics;
using WeierstrassCurveTest.DLP;
using WeierstrassCurveTest.EllipticCurves;
using WeierstrassCurveTest.Performance;
using WeierstrassCurveTest.Types;

namespace WeierstrassCurveTest.Utils
{
    internal static class TestingUtils
    {
        public static void TestCurve(EllipticCurve curve, string curveName)
        {
            Console.WriteLine($"Testing the {curveName} curve");
            Point A = curve.GetRandomPoint();
            Point B = curve.GetRandomPoint();
            Console.WriteLine($"Random point A {A} and point B {B}");

            // Test if the points are on the curve
            if (curve.TestPoint(A))
            {
                Console.WriteLine("A is on the curve");
            }
            else
            {
                Console.WriteLine("A is NOT on the curve, it is an error!");
            }

            if (curve.TestPoint(B))
            {
                Console.WriteLine("B is on the curve");
            }
            else
            {
                Console.WriteLine("B is NOT on the curve, it is an error!");
            }

            // test that A + B == B + A
            Console.WriteLine($"Sum of points A {A} an B {B}:");
            curve.Add(A, B).Print();

            Console.WriteLine("Sum of points (backwards):");
            curve.Add(B, A).Print();
            Console.WriteLine("-----------------------------");


            // Test that A + A == 2A
            Console.WriteLine("Double of points:");
            curve.Double(A).Print();

            Console.WriteLine("Double of points (by multiplying by 2):");
            curve.Mult(A, 2).Print();

            Console.WriteLine("-----------------------------");

            // Test that A + A +...+ A == nA
            Console.WriteLine("Multiplication of point by k:");
            int k = 5;
            curve.Mult(A, k).Print();
            Console.WriteLine("Adding the same point k times:");

            Point C = Point.getPointAtInfinity();

            for (int i = 0; i < k; i++)
            {
                C = curve.Add(A, C);
                C.Print();

                if (curve.TestPoint(C))
                    Console.WriteLine("This point is on the curve!");
                else
                    Console.WriteLine("Oh! This point is NOT on the curve...");
            }
        }

        public static void TestDLPMethods(EllipticCurve curve, string curveName) 
        {
            Console.WriteLine($"Testing the DLP methods on {curveName} curve");

            DLPMethod bsgs = new BSGS(curve);
            DLPMethod grumpyGiants = new GrumpyGiants(curve);
            DLPMethod kangaroo = new Kangaroo(curve);
            DLPMethod rho = new PollardRho(curve);
            DLPMethod lasvegas = new LasVegasc(curve);

            Point point1 = curve.GetRandomPoint();
            BigInteger point1Order = curve.GetPointOrder(point1);
            BigInteger expectedK = BigIntHelper.Random(1, point1Order);
            Point point2 = curve.Mult(point1, expectedK);
            DLPTestData data = new DLPTestData(curve);

            Console.WriteLine($"Input: P {data.P} (of order {data.orderP}) and Q {data.Q}. Expected k={data.expectedK}");
            Console.WriteLine($"Order of P - {data.orderP}. Test P * ordP = {curve.Mult(data.P, data.orderP)}");

            try
            {
                BigInteger factorBsgs = TestMethod(bsgs, data);
                BigInteger factorGrumpy = TestMethod(grumpyGiants, data);
                BigInteger factorKangaroo = TestMethod(kangaroo, data);
                BigInteger factorRho = TestMethod(rho, data);
                BigInteger factorLasVegas = TestMethod(lasvegas, data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine($"For points P {point1} and Q {point2} the solution cannot be found =(");
            }
        }

        private static BigInteger TestMethod(DLPMethod method, DLPTestData data)
        {
            BigInteger factor = ModuloHelper.Abs(method.Solve(data.P, data.Q), data.orderP);


            Console.WriteLine($"Method {method.GetType().Name}: For points P {data.P} and Q {data.Q} the solution of DLP is Q = {factor} * P");
            if (method.Test(data.P, data.Q, factor))
                Console.WriteLine("It is correct!");
            else
                Console.WriteLine("It is incorrect");

            return factor;
        }
    }
}
