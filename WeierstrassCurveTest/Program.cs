using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography;
using WeierstrassCurveTest.DLP;
using WeierstrassCurveTest.EllipticCurves;
using WeierstrassCurveTest.Performance;
using WeierstrassCurveTest.Types;
using WeierstrassCurveTest.Utils;

PerformanceEvaluator evaluator = new PerformanceEvaluator();

//evaluator.CalculateAndLogStatistics("v2_Weierstrass_PollardRho.com-data25.csv");
//evaluator.CalculateAndLogStatistics("v2_Weierstrass_PollardRho.neg.com-data25.csv");
//evaluator.CalculateAndLogStatistics("v2_Weierstrass_PollardRho.neg-ext.com-data25.csv");

//evaluator.CalculateAndLogStatistics("v2_Weierstrass_PollardRho.com-data26.csv");
//evaluator.CalculateAndLogStatistics("v2_Weierstrass_PollardRho.neg.com-data26.csv");
//evaluator.CalculateAndLogStatistics("v2_Weierstrass_PollardRho.neg-ext.com-data26.csv");
//evaluator.CalculateAndLogStatistics("v3_Weierstrass_PollardRho.neg-ext.v2-com-data26.csv");

//evaluator.CalculateAndLogStatistics("v2_Weierstrass_PollardRho.com-data25.csv");
//evaluator.CalculateAndLogStatistics("v2_Weierstrass_PollardRho.neg.com-data25.csv");
//evaluator.CalculateAndLogStatistics("v2_Weierstrass_PollardRho.neg-ext.com-data25.csv");


//evaluator.CalculateAndLogStatistics("v2_Weierstrass_PollardRho.com-data26.csv");
//evaluator.CalculateAndLogStatistics("v2_Weierstrass_PollardRho.neg.com-data26.csv");
//evaluator.CalculateAndLogStatistics("v2_Weierstrass_PollardRho.neg-ext.com-data26.csv");


//evaluator.CalculateAndLogStatistics("v2_Weierstrass_PollardRho.com-data27.csv");
//evaluator.CalculateAndLogStatistics("v2_Weierstrass_PollardRho.neg.com-data27.csv");
//evaluator.CalculateAndLogStatistics("v2_Weierstrass_PollardRho.neg-ext.com-data27.csv");


//evaluator.CalculateAndLogStatistics("v2_Weierstrass_PollardRho.com-data28.csv");
//evaluator.CalculateAndLogStatistics("v2_Weierstrass_PollardRho.neg.com-data28.csv");
//evaluator.CalculateAndLogStatistics("v2_Weierstrass_PollardRho.neg-ext.com-data28.csv");


//evaluator.CalculateAndLogStatistics("v2_Weierstrass_PollardRho.com-data29.csv");
//evaluator.CalculateAndLogStatistics("v2_Weierstrass_PollardRho.neg.com-data29.csv");
//evaluator.CalculateAndLogStatistics("v2_Weierstrass_PollardRho.neg-ext.com-data29.csv");


//evaluator.CalculateAndLogStatistics("v2_Weierstrass_PollardRho.com-data30.csv");
//evaluator.CalculateAndLogStatistics("v2_Weierstrass_PollardRho.neg.com-data30.csv");
//evaluator.CalculateAndLogStatistics("v2_Weierstrass_PollardRho.neg-ext.com-data30.csv");


//evaluator.CalculateAndLogStatistics("Weierstrass_BSGS.data25.csv");
//evaluator.CalculateAndLogStatistics("Weierstrass_BSGS.data26.csv");
//evaluator.CalculateAndLogStatistics("Weierstrass_BSGS.data27.csv");
//evaluator.CalculateAndLogStatistics("Weierstrass_BSGS.data28.csv");
//evaluator.CalculateAndLogStatistics("Weierstrass_BSGS.data29.csv");
//evaluator.CalculateAndLogStatistics("Weierstrass_BSGS.data30.csv");
//evaluator.CalculateAndLogStatistics("Weierstrass_GrympyGiants.data25.csv");
//evaluator.CalculateAndLogStatistics("Weierstrass_GrympyGiants.data26.csv");
//evaluator.CalculateAndLogStatistics("Weierstrass_GrympyGiants.data27.csv");
//evaluator.CalculateAndLogStatistics("Weierstrass_GrympyGiants.data28.csv");
//evaluator.CalculateAndLogStatistics("Weierstrass_GrympyGiants.data29.csv");
//evaluator.CalculateAndLogStatistics("Weierstrass_GrympyGiants.data30.csv");
//evaluator.CalculateAndLogStatistics("Weierstrass_PollardRho.data25.csv");
//evaluator.CalculateAndLogStatistics("Weierstrass_PollardRho.data26.csv");
//evaluator.CalculateAndLogStatistics("Weierstrass_PollardRho.data27.csv");
//evaluator.CalculateAndLogStatistics("Weierstrass_PollardRho.data28.csv");
//evaluator.CalculateAndLogStatistics("Weierstrass_PollardRho.data29.csv");
//evaluator.CalculateAndLogStatistics("Weierstrass_PollardRho.data30.csv");

//evaluator.Evaluate("data26.csv", CurveName.Weierstrass, MethodName.BSGS);
//evaluator.Evaluate("data26.csv", CurveName.Weierstrass, MethodName.GrympyGiants);
//evaluator.Evaluate("com-data25.csv", CurveName.Weierstrass, MethodName.PollardRho, false, false);
//evaluator.Evaluate("com-data28.csv", CurveName.Weierstrass, MethodName.PollardRho, true, false);
//evaluator.Evaluate("v2-com-data26.csv", CurveName.Weierstrass, MethodName.PollardRho, true, true);

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


//WeierstrassCurve weierstrassCurve = new WeierstrassCurve(
//    new BigInteger(1091),
//    new BigInteger(761),
//    new BigInteger(2081),
//    new BigInteger(2111) // calculated with SageMath 
//);

// modulo,curveParam1,curveParam2,order,point1_x,point1_y,point1_order,point2_x,point2_y,point2_order,dlpSolution

// 33554393,3475383,5317434,33565937,3192429,369664,33565937,23860407,10200230,33565937,3420243

//List<BigInteger> values = new List<BigInteger> {
//    //33554393,28925137,14906480,33559380,3491853,11671408,16779690,31896471,22533984,578610,12003883,28936301,28707962,9464523
//    //67108859,43708393,64480280,67094460,41829127,32447182,3727470,21749139,4809439,372747,3414560,52174184,49312462,32731072,
//    //67108859,50139131,63801233,67110648,47884225,54415565,11185108,12454604,51884890,508414,3724798,36494441,16999076,13615342,

//    //67108859,41877641,10743733,67105128,1968892,64195425,70786,22317866,50511368,70786,14971,28967527,24164593,13976739,
//    67108859,44270115,3656060,67123084,57914359,15014676,33561542,9678760,67039352,2397253,9682232,52131958,13280726,1696175,
//    //67108859,34617211,5633131,67106124,39220873,33344974,11184354,33631940,67093921,3728118,645555,30519720,25556298,11032841,
//};




//int row = 0;
//BigInteger p, a, b, ord, px, py, qx, qy, ek, x1, x2, x3;


//WeierstrassCurve weierstrassCurve;
//do
//{
//    Console.WriteLine("Trying out new curve");
//    p = values[row * 14];
//    a = values[row * 14 + 1];
//    b = values[row * 14 + 2];
//    ord = values[row * 14 + 3];
//    px = values[row * 14 + 4];
//    py = values[row * 14 + 5];
//    qx = values[row * 14 + 7];
//    qy = values[row * 14 + 8];
//    ek = values[row * 14 + 10];
//    x1 = values[row * 14 + 11];
//    x2 = values[row * 14 + 12];
//    x3 = values[row * 14 + 13];

//    List<Point> pointsWithYZero = new List<Point> {
//        new Point(x1, 0),
//        new Point(x2, 0),
//        new Point(x3, 0),
//    };

//    weierstrassCurve = new WeierstrassCurve(
//        a,
//        b,
//        p,
//        ord // calculated with SageMath 
//    );
//    weierstrassCurve.setPointsWithYZero(pointsWithYZero);
//    row++;
//} while (weierstrassCurve.pointsOnAbscissaAxis.Count != 0 && row < values.Count / 11);

//Point point1 = new Point(px, py);
////BigInteger point1Order = weierstrassCurve.GetPointOrder(point1);
//Point point2 = new Point(qx, qy);
//BigInteger expectedK = ek;

////Console.WriteLine($"|P| = {point1Order}; k mod |P| = {ModuloHelper.Abs(ek, point1Order)}");
////Console.WriteLine($"kP = {ek} * {point1} = Q({point2}) = {weierstrassCurve.Mult(point1, ek)}");


////// hareTriplet: 118 * P + 67 * Q
////// turtleTriplet: 74 * P + 15 * Q
////Console.WriteLine($"Triplet1: {weierstrassCurve.Add(weierstrassCurve.Mult(point1, 118), weierstrassCurve.Mult(point2, 67))}");
////Console.WriteLine($"Triplet2: {weierstrassCurve.Add(weierstrassCurve.Mult(point1, 74), weierstrassCurve.Mult(point2, 15))}");

////BigInteger N = ord;
////BigInteger ui = 118;
////BigInteger vi = 67;
////BigInteger uj = 74;
////BigInteger vj = 15;
////BigInteger gcd = BigIntHelper.GCD(N, ModuloHelper.Abs(vj - vi, N), out _, out _);
////Console.WriteLine($"gcd(vj - vi , N) = gcd( {vj - vi},  {N}) = {gcd}");
////BigInteger modulo = N / gcd;
////BigInteger v = ModuloHelper.Abs(vj - vi, N) / gcd;


//////BigInteger diff = vj - vi;
//////BigInteger diffAbs = BigInteger.Abs(diff);
//////BigInteger diffAbsMod1 = ModuloHelper.Abs(diff, N); // curve order
//////BigInteger diffAbsMod2 = ModuloHelper.Abs(diff, BigIntHelper.GCD(N, diff, out _, out _)); // gcd(N, diff)
//////BigInteger diffAbsMod3 = ModuloHelper.Abs(diff, BigIntHelper.GCD(N, diffAbs, out _, out _)); // gcd(N, diffAbs)




////Console.WriteLine($"modulo: {modulo}");
////Console.WriteLine($"v: {v} -> inv: {ModuloHelper.MultInverse(v, modulo)}");
////Console.WriteLine($"Calculations (v): {ModuloHelper.Abs((ui - uj) * ModuloHelper.MultInverse(v, modulo), modulo)}");

////Console.WriteLine($"Mult inv: vj - vi = {vj - vi} = {ModuloHelper.Abs(vj - vi, modulo)} -> {ModuloHelper.MultInverse(vj - vi, modulo)}");

////Console.WriteLine($"Check: vj - vi = {ModuloHelper.Abs(ModuloHelper.MultInverse(vj - vi, modulo) * ModuloHelper.Abs(vj - vi, modulo), modulo)}");
////Console.WriteLine($"Calculations: {ModuloHelper.Abs((ui - uj) * ModuloHelper.MultInverse(vj - vi, modulo), modulo)}");
////Console.WriteLine($"Calculations with point ord: {ModuloHelper.Abs((ui - uj) * ModuloHelper.MultInverse(vj - vi, point1Order), point1Order)}");
////Console.WriteLine($"Mod inv: {vj - vi} === {ModuloHelper.Abs(vj - vi, modulo)} -> {ModuloHelper.MultInverse(vj - vi, modulo)}");



////EllipticCurve weierstrassCurve = new WeierstrassCurve(
////    new BigInteger(100219020),
////    new BigInteger(301367758),
////    new BigInteger(1073741789),
////    new BigInteger(1073725157) // calculated with SageMzath 
////);

//////DLPMethod method = new LasVegasc(weierstrassCurve);
//PollardRho method = new PollardRho(weierstrassCurve);

////Point point1 = weierstrassCurve.GetRandomPoint();
//////BigInteger point1Order = weierstrassCurve.GetPointOrder(point1);
////BigInteger expectedK = BigIntHelper.Random(1, weierstrassCurve.Order());
////Point point2 = weierstrassCurve.Mult(point1, expectedK);

//Console.WriteLine((int)ModuloHelper.Abs(6, 3));
//Console.WriteLine("");
//var stopwatch = Stopwatch.StartNew();
//BigInteger k = method.Solve(point1, point2);
//stopwatch.Stop();
//double timeUsed = stopwatch.ElapsedMilliseconds;

//method.EnableNegationMaps(false);
//stopwatch = Stopwatch.StartNew();
//BigInteger k2 = method.Solve(point1, point2);
//stopwatch.Stop();
//double timeUsed2 = stopwatch.ElapsedMilliseconds;

//method.EnableNegationMaps(true);
//stopwatch = Stopwatch.StartNew();
//BigInteger k3 = method.Solve(point1, point2);
//stopwatch.Stop();
//double timeUsed3 = stopwatch.ElapsedMilliseconds;

//Console.WriteLine($"Input: P {point1} and Q {point2}. Expected k={expectedK}");
//Console.WriteLine($"Result ({k == ek}) ({weierstrassCurve.Mult(point1, k).Equals(point2)}): k={k}, kP = Q = {weierstrassCurve.Mult(point1, k)}");
//Console.WriteLine($"Result with negation maps ({k2 == ek}): k={k2}, kP = Q = {weierstrassCurve.Mult(point1, k2)}");
//Console.WriteLine($"Result with extended negation maps ({k3 == ek}): k={k3}, kP = Q = {weierstrassCurve.Mult(point1, k3)}");
//Console.WriteLine($"Time used (ms): {timeUsed}");
//Console.WriteLine($"Time used with negation maps (ms): {timeUsed2}");
//Console.WriteLine($"Time used with extended negation maps (ms): {timeUsed3}");
//Console.WriteLine($"Check: kP={weierstrassCurve.Mult(point1, k)}");


