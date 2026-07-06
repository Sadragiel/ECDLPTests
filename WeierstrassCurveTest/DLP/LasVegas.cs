using System.Numerics;
using WeierstrassCurveTest.EllipticCurves;
using WeierstrassCurveTest.Types;
using WeierstrassCurveTest.Utils;

using Point = WeierstrassCurveTest.Types.Point;

namespace WeierstrassCurveTest.DLP
{
    // WIP: Method is not working for large numbers
    internal class LasVegasc : DLPMethod
    {
        int n_;
        int l;
        List<List<BigInteger>> matrix;
        BigInteger mod;
        BigInteger ord;

        int maxNumberOfAttepts = 10;

        public LasVegasc(EllipticCurve curve) : base(curve)
        {
            SetTunningParams(curve.Order());
        }

        public override void SetModulo(BigInteger modulo)
        {
            this.modulo = modulo;
            SetTunningParams(modulo);
        }

        public override BigInteger Solve(Point P, Point Q)
        {
            List<BigInteger> v = null;
            List<BigInteger> pCoefs = null;
            List<BigInteger> qCoefs = null;
            int attempt = 0;

            while (++attempt < maxNumberOfAttepts && v == null)
            {
                // matrix should have rows made out of x^i*y^j*z^k such that i+j+k = n'
                // and also, the order of i, j and k should be fixed
                matrix = new List<List<BigInteger>>(); // resetting matrix on every attempt

                Console.WriteLine("Starting attempt");
                pCoefs = FillTheMatrix(Math.Max(1, 3 * n_ - 1), (BigInteger num) => curve.Mult(P, num));
                Console.WriteLine("pCoefs are done");
                Console.WriteLine($"Calculating q coefs with l={l}; ord={ord}");
                qCoefs = FillTheMatrix(l + 1, (BigInteger num) => curve.Invert(curve.Mult(Q, num)));
                Console.WriteLine("qCoefs are done");

                // Extracting left kernel
                List<List<BigInteger>> transposed = MatrixHelper.TransposeMatrix(matrix);
                Console.WriteLine("transposed are done");
                MatrixHelper.RowReduce(transposed, mod);
                Console.WriteLine("RowReduce are done");
                List<List<BigInteger>> leftKernel = MatrixHelper.FindKernelBasis(transposed, mod);
                Console.WriteLine("leftKernel are done");

                // Problem L - in left kernel find a vector with l zeros
                v = GetRowWithLZeros(leftKernel);
                Console.WriteLine($"GetRowWithLZeros are done - {v}");
            }


            if (v == null)
            {
                Console.WriteLine($"Failure: Problem L is not solved in {attempt} attempt");
                NotifyNoSolution();
                return 0;
            } else
            {
                Console.WriteLine($"Success: Problem L is solved in {attempt} attempt");
            }


            BigInteger a = 0;
            for (int i = 0; i < l - 1; i++)
            {
                if (v[i] != 0)
                {
                    a = ModuloHelper.Abs(a + pCoefs[i], ord);
                }
            }

            BigInteger b = 0;
            for (int i = l - 1; i < 2 * l; i++)
            {
                if (v[i] != 0)
                {
                    b = ModuloHelper.Abs(b + qCoefs[i - l + 1], ord);
                }
            }

            return ModuloHelper.Abs(a * ModuloHelper.MultInverse(b, ord), ord);
        }

        List<BigInteger> FillTheMatrix(long numOfRowsFilled, Func<BigInteger, Point> getNewPoint)
        {
            List<BigInteger> generatedRandomNumbers = new List<BigInteger>(); 
            for (int i = 0; i < numOfRowsFilled; i++)
            {
                // generate new unique random number
                do
                {
                    BigInteger newNumber = BigIntHelper.Random(0, ord);
                    long index = generatedRandomNumbers.FindIndex(x => x == newNumber);

                    if (index < 0)
                    {
                        generatedRandomNumbers.Add(newNumber);
                        break;
                    }
                } while (true);

                BigInteger num = generatedRandomNumbers.Last();
                Point point = getNewPoint(num);
                BigInteger x = point.x;
                BigInteger y = point.y;
                BigInteger z = 1; // In projective space we set z to 1

                List<BigInteger> row = new List<BigInteger>();

                // calculating monomials
                for (long zIndex = 0; zIndex <= n_; zIndex++)
                {
                    for (long yIndex = 0; yIndex <= n_; yIndex++)
                    {
                        for (long xIndex = 0; xIndex <= n_; xIndex++)
                        {
                            // monomials of degree n' only
                            if (xIndex + yIndex + zIndex != n_)
                            {
                                continue;
                            }

                            BigInteger monomialValue = ModuloHelper.Abs(BigInteger.ModPow(x, xIndex, mod) * BigInteger.ModPow(y, yIndex, mod) * BigInteger.ModPow(z, zIndex, mod), mod);

                            row.Add(monomialValue);
                        }
                    }
                }
                matrix.Add(row);
            }

            return generatedRandomNumbers;
        }

        List<BigInteger>? GetRowWithLZeros(List<List<BigInteger>> matrix)
        {
            return matrix.Find((List<BigInteger> row) =>
            {
                return row.Count(value => value == 0) >= l;
            });
        }

        void SetTunningParams(BigInteger modulo)
        {

            // Modulo is used for operations within field, such as managin matrix
            mod = curve.Modulo();
            // Order is used for operations on the curve, such as calculating random points
            ord = modulo;

            // Select n' - sum of max degrees of monomials of curve C
            n_ = BigIntHelper.LogBase2(ord);
            

            // l = 3n'
            l = 3 * n_;
            if (l + 1 >= ord)
            {
                l = (int)ord - 1;
            }
        }
    }
}
