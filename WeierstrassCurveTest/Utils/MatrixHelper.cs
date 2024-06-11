using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WeierstrassCurveTest.Utils
{
    internal static class MatrixHelper
    {
        public static LargeList<LargeList<BigInteger>> TransposeMatrix(LargeList<LargeList<BigInteger>> matrix)
        {
            long rows = matrix.Count;
            long cols = matrix[0].Count;
            var transpose = new LargeList<LargeList<BigInteger>>();

            for (int i = 0; i < cols; i++)
            {
                var newRow = new LargeList<BigInteger>();
                for (int j = 0; j < rows; j++)
                {
                    newRow.Add(matrix[j][i]);
                }
                transpose.Add(newRow);
            }
            return transpose;
        }

        public static void RowReduce(LargeList<LargeList<BigInteger>> matrix, BigInteger modulo)
        {
            long rowCount = matrix.Count;
            long colCount = matrix[0].Count;

            int lead = 0;
            for (int r = 0; r < rowCount; r++)
            {
                if (colCount <= lead) break;

                int i = r;
                while (matrix[i][lead] == 0)
                {
                    i++;
                    if (i == rowCount)
                    {
                        i = r;
                        lead++;
                        if (colCount == lead)
                        {
                            lead--;
                            break;
                        }
                    }
                }

                LargeList<BigInteger> temp = matrix[i];
                matrix[i] = matrix[r];
                matrix[r] = temp;

                var div = matrix[r][lead];
                if (div != 0)  // Avoid division by zero
                    for (int j = 0; j < colCount; j++)
                        matrix[r][j] = ModuloHelper.Abs(matrix[r][j] * ModuloHelper.MultInverse(div, modulo), modulo);

                for (int j = 0; j < rowCount; j++)
                {
                    if (j != r)
                    {
                        BigInteger mult = matrix[j][lead];
                        for (int k = 0; k < colCount; k++)
                            matrix[j][k] = ModuloHelper.Abs(matrix[j][k] -  matrix[r][k] * mult, modulo);
                    }
                }
                lead++;
            }
        }

        public static string MatrixToString(LargeList<LargeList<BigInteger>> matrix)
        {
            string res = "[\n";

            for (int i = 0; i < matrix.Count; i++)
            {
                LargeList<BigInteger> row = matrix[i];
                res += "[ ";
                for (long j = 0; j < row.Count; j++)
                {
                    res += row[j].ToString() + (j == row.Count - 1 ? "" : ",");
                }
                res += "],\n";
            }
            res += "]";

            return res;
        }

        public static LargeList<LargeList<BigInteger>> FindKernelBasis(LargeList<LargeList<BigInteger>> reducedMatrix, BigInteger modulo)
        {
            long rowCount = reducedMatrix.Count;
            long colCount = reducedMatrix[0].Count;

            List<int> pivotColumns = new List<int>();
            for (int r = 0; r < rowCount; r++)
            {
                for (int c = 0; c < colCount; c++)
                {
                    if (reducedMatrix[r][c] == 1)
                    {
                        pivotColumns.Add(c);
                        break;
                    }
                }
            }

            LargeList<LargeList<BigInteger>> basis = new LargeList<LargeList<BigInteger>>();
            for (int c = 0; c < colCount; c++)
            {
                if (!pivotColumns.Contains(c))
                {
                    BigInteger[] basisVector = new BigInteger[colCount];
                    basisVector[c] = 1;
                    foreach (int p in pivotColumns)
                    {
                        int pivotRow = pivotColumns.IndexOf(p);
                        basisVector[p] = ModuloHelper.Abs(-reducedMatrix[pivotRow][c], modulo);
                    }
                    basis.Add(new LargeList<BigInteger>(basisVector));
                }
            }

            return basis;
        }
    }
}
