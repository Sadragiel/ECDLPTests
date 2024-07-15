using System.Numerics;
using WeierstrassCurveTest.EllipticCurves;
using WeierstrassCurveTest.Utils;

// TODO: implement method of finding the order of curve

EllipticCurve edwardsCurve = new EdwardsCurve(
    new BigInteger(2207),
    new BigInteger(3533),
    new BigInteger(3568) // calculated with SageMath
);

EllipticCurve weierstrassCurve = new WeierstrassCurve(
    new BigInteger(1091),
    new BigInteger(761),
    new BigInteger(2081),
    new BigInteger(2111) // calculated with SageMath 
);

//TestingUtils.TestCurve(edwardsCurve, "Edwards");
//TestingUtils.TestCurve(weierstrassCurve, "Weierstrass");

TestingUtils.TestDLPMethods(edwardsCurve, "Edwards");
TestingUtils.TestDLPMethods(weierstrassCurve, "Weierstrass");

