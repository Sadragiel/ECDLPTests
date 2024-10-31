using System.Numerics;
using System.Security.Cryptography;
using WeierstrassCurveTest.DLP;
using WeierstrassCurveTest.EllipticCurves;
using WeierstrassCurveTest.Performance;
using WeierstrassCurveTest.Types;
using WeierstrassCurveTest.Utils;
using static System.Runtime.InteropServices.JavaScript.JSType;

PerformanceEvaluator evaluator = new PerformanceEvaluator();

evaluator.CalculateAndLogStatistics("Weierstrass_BSGS.data25.csv");
evaluator.CalculateAndLogStatistics("Weierstrass_BSGS.data26.csv");
evaluator.CalculateAndLogStatistics("Weierstrass_BSGS.data27.csv");
evaluator.CalculateAndLogStatistics("Weierstrass_BSGS.data28.csv");
evaluator.CalculateAndLogStatistics("Weierstrass_BSGS.data29.csv");
evaluator.CalculateAndLogStatistics("Weierstrass_BSGS.data30.csv");
evaluator.CalculateAndLogStatistics("Weierstrass_GrympyGiants.data25.csv");
evaluator.CalculateAndLogStatistics("Weierstrass_GrympyGiants.data26.csv");
evaluator.CalculateAndLogStatistics("Weierstrass_GrympyGiants.data27.csv");
evaluator.CalculateAndLogStatistics("Weierstrass_GrympyGiants.data28.csv");
evaluator.CalculateAndLogStatistics("Weierstrass_GrympyGiants.data29.csv");
evaluator.CalculateAndLogStatistics("Weierstrass_GrympyGiants.data30.csv");
evaluator.CalculateAndLogStatistics("Weierstrass_PollardRho.data25.csv");
evaluator.CalculateAndLogStatistics("Weierstrass_PollardRho.data26.csv");
evaluator.CalculateAndLogStatistics("Weierstrass_PollardRho.data27.csv");
evaluator.CalculateAndLogStatistics("Weierstrass_PollardRho.data28.csv");
evaluator.CalculateAndLogStatistics("Weierstrass_PollardRho.data29.csv");
evaluator.CalculateAndLogStatistics("Weierstrass_PollardRho.data30.csv");

//evaluator.Evaluate("data26.csv", CurveName.Weierstrass, MethodName.BSGS);
//evaluator.Evaluate("data26.csv", CurveName.Weierstrass, MethodName.GrympyGiants);
//evaluator.Evaluate("data26.csv", CurveName.Weierstrass, MethodName.PollardRho);

//evaluator.Evaluate("data27.csv", CurveName.Weierstrass, MethodName.BSGS);
//evaluator.Evaluate("data27.csv", CurveName.Weierstrass, MethodName.GrympyGiants);
//evaluator.Evaluate("data27.csv", CurveName.Weierstrass, MethodName.PollardRho);

//evaluator.Evaluate("data28.csv", CurveName.Weierstrass, MethodName.BSGS);
//evaluator.Evaluate("data28.csv", CurveName.Weierstrass, MethodName.GrympyGiants);
//evaluator.Evaluate("data28.csv", CurveName.Weierstrass, MethodName.PollardRho);

//evaluator.Evaluate("data25.csv", CurveName.Weierstrass, MethodName.LasVegas);
//evaluator.Evaluate("data25.csv", CurveName.Weierstrass, MethodName.Kangaroo);


// evaluator.Evaluate("data30.csv", CurveName.Weierstrass, MethodName.BSGS); // already procecced (69+5)k data items
//evaluator.Evaluate("data30.csv", CurveName.Weierstrass, MethodName.GrympyGiants);
//evaluator.Evaluate("data30.csv", CurveName.Weierstrass, MethodName.PollardRho);
//evaluator.Evaluate("data30.csv", CurveName.Weierstrass, MethodName.LasVegas);
//evaluator.Evaluate("data30.csv", CurveName.Weierstrass, MethodName.Kangaroo);

//evaluator.Evaluate("data35.csv", CurveName.Weierstrass, MethodName.BSGS);
//evaluator.Evaluate("data35.csv", CurveName.Weierstrass, MethodName.GrympyGiants);
////evaluator.Evaluate("data35.csv", CurveName.Weierstrass, MethodName.Kangaroo);
//evaluator.Evaluate("data35.csv", CurveName.Weierstrass, MethodName.PollardRho);
//evaluator.Evaluate("data35.csv", CurveName.Weierstrass, MethodName.LasVegas);

//evaluator.Evaluate("data40.csv", CurveName.Weierstrass, MethodName.BSGS);
//evaluator.Evaluate("data40.csv", CurveName.Weierstrass, MethodName.GrympyGiants);
//evaluator.Evaluate("data40.csv", CurveName.Weierstrass, MethodName.Kangaroo);
//evaluator.Evaluate("data40.csv", CurveName.Weierstrass, MethodName.PollardRho);
//evaluator.Evaluate("data40.csv", CurveName.Weierstrass, MethodName.LasVegas);

//evaluator.Evaluate("data45.csv", CurveName.Weierstrass, MethodName.BSGS);
//evaluator.Evaluate("data45.csv", CurveName.Weierstrass, MethodName.GrympyGiants);
//evaluator.Evaluate("data45.csv", CurveName.Weierstrass, MethodName.Kangaroo);
//evaluator.Evaluate("data45.csv", CurveName.Weierstrass, MethodName.PollardRho);
//evaluator.Evaluate("data45.csv", CurveName.Weierstrass, MethodName.LasVegas);


//EllipticCurve weierstrassCurve = new WeierstrassCurve(
//    new BigInteger(1091),
//    new BigInteger(761),
//    new BigInteger(2081),
//    new BigInteger(2111) // calculated with SageMath 
//);

//EllipticCurve weierstrassCurve = new WeierstrassCurve(
//    new BigInteger(100219020),
//    new BigInteger(301367758),
//    new BigInteger(1073741789),
//    new BigInteger(1073725157) // calculated with SageMath 
//);

////DLPMethod method = new LasVegasc(weierstrassCurve);
//DLPMethod method = new Kangaroo(weierstrassCurve);

//Point point1 = weierstrassCurve.GetRandomPoint();
////BigInteger point1Order = weierstrassCurve.GetPointOrder(point1);
//BigInteger expectedK = BigIntHelper.Random(1, weierstrassCurve.Order());
//Point point2 = weierstrassCurve.Mult(point1, expectedK);

//Console.WriteLine("");
//BigInteger k = method.Solve(point1, point2);
//Console.WriteLine($"Input: P {point1} and Q {point2}. Expected k={expectedK}");
//Console.WriteLine($"Result: k={k}");