using System.Numerics;
using WeierstrassCurveTest.DLP;
using WeierstrassCurveTest.EllipticCurves;
using WeierstrassCurveTest.Types;
using WeierstrassCurveTest.Utils;

// TODO: the factor should be taken by modulo of the order of point1
// TODO: implement method of finding the order of point
// TODO: DLPMethod.Solve method should return the prime taking into account the order of P

EllipticCurve ecc1 = new WeierstrassCurve(
    new BigInteger(273),
    new BigInteger(882),
    new BigInteger(7919),
    new BigInteger(7872) // calculated with SageMath 
);

DLPMethod bsgs = new BSGS(ecc1);
DLPMethod grumpyGiants = new GrumpyGiants(ecc1);

Point point1 = ecc1.GetRandomPoint();
BigInteger expectedK = BigIntHelper.Random(1, ecc1.Order());
Point point2 = ecc1.Mult(point1, expectedK);

Console.WriteLine($"Input: P {point1} and Q {point2}; expected k = {expectedK}");


try
{
    BigInteger factorBsgs = TestMethod(bsgs, point1, point2);
    BigInteger factorGrumpy = TestMethod(grumpyGiants, point1, point2);
}
catch (Exception e)
{
    Console.WriteLine($"For points P {point1} and Q {point2} the solution cannot be found =(");
}

BigInteger TestMethod(DLPMethod method, Point pointA, Point pointQ)
{
    BigInteger factor = method.Solve(pointA, pointQ);

    Console.WriteLine($"For points P {pointA} and Q {pointQ} the solution of DLP is Q = {factor} * P");
    if (bsgs.Test(pointA, pointQ, factor))
        Console.WriteLine("It is correct!");
    else
        Console.WriteLine("It is incorrect");

    return factor;
}