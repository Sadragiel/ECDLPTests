﻿using System.Numerics;
using WeierstrassCurveTest.Types;
using WeierstrassCurveTest.Utils;

namespace WeierstrassCurveTest.EllipticCurves
{
    internal class WeierstrassCurve : EllipticCurve
    {
        private BigInteger a;
        private BigInteger b;

        public WeierstrassCurve(BigInteger a, BigInteger b, BigInteger p, BigInteger order) { 
            this.a = ModuloHelper.Abs(a, p);
            this.b = ModuloHelper.Abs(b, p);
            this.p = p;
            this.order = order;
        }

        public override bool TestPoint(Point A)
        {
            // y^2 = x^3 + ax + b  mod(p)
            BigInteger left = ModuloHelper.Abs(A.y * A.y, p);
            BigInteger right = ModuloHelper.Abs(BigInteger.Pow(A.x, 3) + this.a * A.x + this.b, p);
            return left.Equals(right);
        }

        public override Point GetRandomPoint()
        {
            while(true)
            {
                try
                {
                    BigInteger x = BigIntHelper.Random(0, p);
                    BigInteger quadraticResiduosity = ModuloHelper.Abs(BigInteger.Pow(x, 3) + this.a * x + this.b, p);
                    BigInteger y = ModuloHelper.SquareRootMod(quadraticResiduosity, p);
                    return new Point(x, y);
                }
                catch (Exception e)
                {
                    // for generated x there is no valid value of y
                }
            }
        }

        public override Point Add(Point A, Point B)
        {
            // Handle Doubling
            if (A.Equals(B))
            {
                return Double(A);
            }

            // handle points of Infinity:
            if (A.atInfinity)
            {
                return B;
            }

            if (B.atInfinity)
            {
                return A;
            }

            // A - A = 0
            if (A.x.Equals(B.x)) {
                return Point.getPointAtInfinity();
            }

            // lambda = (y2 - y1) / (x2 - x1)
            BigInteger lambda = ModuloHelper.Abs((B.y - A.y) * ModuloHelper.MultInverse(B.x - A.x, p), p);

            return CalculatePoint(lambda, A, B);
        }

        public override Point Double(Point A)
        {
            if (A.atInfinity || A.y.Equals(0))
            {
                return Point.getPointAtInfinity();
            }

            // lambda = (3x^2 + a) / (2y)
            BigInteger lambda = ModuloHelper.Abs((3 * BigInteger.ModPow(A.x, 2, p) + this.a) * ModuloHelper.MultInverse(2 * A.y, p), p);

            return CalculatePoint(lambda, A, A);
        }

        private Point CalculatePoint(BigInteger lambda, Point A, Point B)
        {
            BigInteger x = ModuloHelper.Abs(BigInteger.ModPow(lambda, 2, p) - A.x - B.x, p);
            BigInteger y = ModuloHelper.Abs(lambda * (A.x - x) - A.y, p);

            return new Point(x, y);
        }
    }
}
