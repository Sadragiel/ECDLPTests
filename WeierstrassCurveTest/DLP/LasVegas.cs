using System.Numerics;
using WeierstrassCurveTest.EllipticCurves;
using WeierstrassCurveTest.Types;
using WeierstrassCurveTest.Utils;

namespace WeierstrassCurveTest.DLP
{
    internal class LasVegasc : DLPMethod
    {
        long n_;
        long l;
        LargeList<LargeList<BigInteger>> matrix;
        BigInteger mod;

        int maxNumberOfAttepts = 10;

        public LasVegasc(EllipticCurve curve) : base(curve)
        {
            mod = curve.Order();

            // Select n' - sum of max degrees of monomials of curve C
            n_ = BigIntHelper.LogBase2(curve.Order());

            // l = 3n'
            l = 3 * n_;
        }

        public override BigInteger Solve(Point P, Point Q)
        {
            LargeList<BigInteger> v = null;
            LargeList<BigInteger> pCoefs = null;
            LargeList<BigInteger> qCoefs = null;
            int attempt = 0;
            while (attempt++ < maxNumberOfAttepts && v == null)
            {
                // matrix should have rows made out of x^i*y^j*z^k such that i+j+k = n'
                // and also, the order of i, j and k should be fixed
                matrix = new LargeList<LargeList<BigInteger>>(); // resetting matrix on every attempt

                pCoefs = FillTheMatrix(l - 1, (BigInteger num) => curve.Mult(P, num));
                qCoefs = FillTheMatrix(l + 1, (BigInteger num) => curve.Invert(curve.Mult(Q, num)));
                
                // Extracting left kernel
                LargeList<LargeList<BigInteger>> transposed = MatrixHelper.TransposeMatrix(matrix);
                MatrixHelper.RowReduce(transposed, mod);
                LargeList<LargeList<BigInteger>> leftKernel = MatrixHelper.FindKernelBasis(transposed, mod);

                // Problem L - in left kernel find a vector with l zeros
                v = GetRowWithLZeros(leftKernel);
            }


            if (v != null)
            {
                Console.WriteLine($"V (vector with l({l}) zeros): {string.Join(", ", v)}, after {attempt - 1}-th attempt");
            }
            else
            {
                Console.WriteLine($"V is not found in {attempt} attempt");
                NotifyNoSolution();
                return 0;
            }


            BigInteger a = 0;
            for (long i = 0; i < l - 1; i++)
            {
                if (v[i] != 0)
                {
                    a = ModuloHelper.Abs(a + pCoefs[i], mod);
                }
            }

            BigInteger b = 0;
            for (long i = l - 1; i < 2 * l; i++)
            {
                if (v[i] != 0)
                {
                    b = ModuloHelper.Abs(a + qCoefs[i - l + 1], mod);
                }
            }

            return ModuloHelper.Abs(a * ModuloHelper.MultInverse(b, mod), mod);
        }

        LargeList<BigInteger> FillTheMatrix(long numOfRowsFilled, Func<BigInteger, Point> getNewPoint)
        {
            LargeList<BigInteger> generatedRandomNumbers = new LargeList<BigInteger>(); 
            for (int i = 0; i < numOfRowsFilled; i++)
            {
                // generate new unique random number
                do
                {
                    BigInteger newNumber = BigIntHelper.Random(1, curve.Order());
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

                LargeList<BigInteger> row = new LargeList<BigInteger>();

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

                            BigInteger monomialValue = ModuloHelper.Abs(BigIntHelper.Pow(x, xIndex) * BigIntHelper.Pow(y, yIndex) * BigIntHelper.Pow(z, zIndex), mod);

                            row.Add(monomialValue);
                        }
                    }
                }
                matrix.Add(row);
            }

            return generatedRandomNumbers;
        }

        LargeList<BigInteger>? GetRowWithLZeros(LargeList<LargeList<BigInteger>> matrix)
        {
            return matrix.Find((LargeList<BigInteger> row) =>
            {
                return row.Count(value => value == 0) == l;
            });
        }

    }
}
