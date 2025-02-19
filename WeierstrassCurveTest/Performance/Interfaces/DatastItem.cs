using System.Numerics;

namespace WeierstrassCurveTest.Performance.Interfaces
{
    internal class DatastItem
    {
        public BigInteger modulo { get; set; }
        public BigInteger curveParam1 { get; set; }
        public BigInteger curveParam2 { get; set; }
        public BigInteger order { get; set; }
        public BigInteger point1_x { get; set; }
        public BigInteger point1_y { get; set; }
        public BigInteger point1_order { get; set; }
        public BigInteger point2_x { get; set; }
        public BigInteger point2_y { get; set; }
        public BigInteger point2_order { get; set; }
        public BigInteger dlpSolution { get; set; }
        public BigInteger x0 { get; set; }
        public BigInteger x1 { get; set; }
        public BigInteger x2 { get; set; }
    }
}
