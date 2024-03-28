using System;
using System.Numerics;
using WeierstrassCurveTest.DLP;
using WeierstrassCurveTest.EllipticCurves;
using WeierstrassCurveTest.Types;

//EllipticCurve ecc = new WeierstrassCurve(
//    new BigInteger(2),
//    new BigInteger(2),
//    new BigInteger(11),
//    new BigInteger(9)
//);

//Point A3 = new Point(9, 10);
//Point A = new Point(1, 4);
//Point B = new Point(2, 5);

//Console.WriteLine($"Sum of points A {A} an A3 {A3}:");
//ecc.Add(A, A3).Print();

//Console.WriteLine("Sum of points (backwards):");
//ecc.Add(A3, A).Print();
//Console.WriteLine("-----------------------------");

//Console.WriteLine("Sum of points:");
//ecc.Add(A, B).Print();

//Console.WriteLine("Sum of points (backwards):");
//ecc.Add(B, A).Print();

//Console.WriteLine("Double of points:");
//ecc.Double(A).Print();

//Console.WriteLine("Double of points (by multiplying by 2):");
//ecc.Mult(A, 2).Print();

//Console.WriteLine("Multiplying by 4:");
//ecc.Mult(A, 4).Print();

//Console.WriteLine("Multiplication of point by k:");
//int k = 5;
//ecc.Mult(A, k).Print();
//Console.WriteLine("Adding the same point k times:");

//Point C = Point.getPointAtInfinity();

//for (int i = 0; i < k; i++)
//{
//    C = ecc.Add(A, C);
//    C.Print();

//    if (ecc.TestPoint(C))
//        Console.WriteLine("This point is on the curve!");
//    else
//        Console.WriteLine("Oh! This point is NOT on the curve...");
//}

//// Testing of DLP mehtods

//DLPMethod bsgs = new BSGS(ecc);
//Point P = A;
//Point Q = new Point(5, 7);
//// expected: P (1, 4), Q (5, 7), k = 5
//try
//{
//    BigInteger solution = bsgs.Solve(A, Q);

//    Console.WriteLine($"For points A {A} and Q {Q} the solution of DLP is Q = {solution} * A");

//    bool correct = bsgs.Test(A, Q, solution);
//    if (correct)
//        Console.WriteLine("It is correct!");
//    else
//        Console.WriteLine("It is incorrect");
//}
//catch (Exception e)
//{
//    Console.WriteLine($"For points A {A} and Q {Q} the solution cannot be found =(");
//}


//Console.WriteLine("----");
//Console.WriteLine("Testing of random points generation:");

//int numberOfAttepts = 10;
//for(int i = 0; i < numberOfAttepts; i++)
//{
//    Point point = ecc.GetRandomPoint();
//    string testResult = ecc.TestPoint(point) ? "is on the curve" : "is NOT on the curve";

//    Console.WriteLine($"Generated point {point} - {testResult}");
//}

// Different curve

EllipticCurve ecc1 = new WeierstrassCurve(
    new BigInteger(273),
    new BigInteger(882),
    new BigInteger(7919),
    new BigInteger(7872) // calculated with SageMath 
);

DLPMethod bsgs = new BSGS(ecc1);

Point point1 = ecc1.GetRandomPoint();
Point point2 = ecc1.GetRandomPoint();


try
{
    BigInteger factor = bsgs.Solve(point1, point2);

    Console.WriteLine($"For points P {point1} and Q {point2} the solution of DLP is Q = {factor} * P");

    bool correct = bsgs.Test(point1, point2, factor);
    if (correct)
        Console.WriteLine("It is correct!");
    else
        Console.WriteLine("It is incorrect");
}
catch (Exception e)
{
    Console.WriteLine($"For points P {point1} and Q {point2} the solution cannot be found =(");
}