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
DLPMethod kangaroo = new Kangaroo(ecc1);
DLPMethod rho = new PollardRho(ecc1);

Point point1 = ecc1.GetRandomPoint();
BigInteger expectedK = BigIntHelper.Random(1, ecc1.Order());
Point point2 = ecc1.Mult(point1, expectedK);
BigInteger point1Order = ecc1.GetPointOrder(point1);
expectedK %= point1Order;

Console.WriteLine($"Input: P {point1} (of order {point1Order}) and Q {point2}; expected k = {expectedK}");

try
{
    BigInteger factorBsgs = TestMethod("BSGS", bsgs, point1, point2);
    BigInteger factorGrumpy = TestMethod("GrumpyGiants", grumpyGiants, point1, point2);
    BigInteger factorKangaroo = TestMethod("Kangaroo", kangaroo, point1, point2);
    BigInteger factorRho = TestMethod("PollardRho", rho, point1, point2);
}
catch (Exception e)
{
    Console.WriteLine($"For points P {point1} and Q {point2} the solution cannot be found =(");
}

BigInteger TestMethod(string methodName, DLPMethod method, Point pointA, Point pointQ)
{
    BigInteger factor = ModuloHelper.Abs(method.Solve(pointA, pointQ), point1Order);


    Console.WriteLine($"Method {methodName}: For points P {pointA} and Q {pointQ} the solution of DLP is Q = {factor} * P");
    if (bsgs.Test(pointA, pointQ, factor))
        Console.WriteLine("It is correct!");
    else
        Console.WriteLine("It is incorrect");

    return factor;
}