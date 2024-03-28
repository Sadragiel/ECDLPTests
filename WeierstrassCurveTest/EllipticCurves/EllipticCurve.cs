using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WeierstrassCurveTest.Types;

namespace WeierstrassCurveTest.EllipticCurves
{
    internal abstract class EllipticCurve
    {
        protected BigInteger order; // TODO: calculate order without third-party

        protected EllipticCurve() { }

        public BigInteger Order() { return order; }

        public abstract bool TestPoint(Point A);

        public abstract Point GetRandomPoint();

        public abstract Point Add(Point A, Point B);

        public abstract Point Double(Point A);

        public abstract Point Invert(Point A);

        public abstract Point Mult(Point A, BigInteger k);

    }
}
