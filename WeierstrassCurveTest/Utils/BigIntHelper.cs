using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WeierstrassCurveTest.Utils
{
    internal static class BigIntHelper
    {
        private static readonly Random random = new Random();

        public static bool[] BigIntegerToBitsArray(BigInteger bigInteger)
        {
            byte[] byteArray = bigInteger.ToByteArray();
            List<bool> bitsList = new List<bool>();

            foreach (byte b in byteArray)
            {
                for (int i = 0; i < 8; i++)
                {
                    bitsList.Add((b & (1 << i)) != 0);
                }
            }

            return bitsList.ToArray();
        }

        public static BigInteger Pow(BigInteger baseNumber, BigInteger exponent)
        {
            if (exponent == 0) return 1; 
            if (exponent == 1) return baseNumber;

            BigInteger result = 1;
            while (exponent > 0)
            {
                if (exponent % 2 == 1)
                    result *= baseNumber;
                exponent /= 2;
                baseNumber *= baseNumber;
            }

            return result;
        }

        public static BigInteger Random(BigInteger min, BigInteger max)
        {
            if (min >= max)
                throw new ArgumentException("min must be less than max");

            // Create a byte array large enough to hold the maximum value
            byte[] bytes = max.ToByteArray();

            BigInteger result;
            do
            {
                random.NextBytes(bytes);
                // Ensure the BigInteger is unsigned by setting the sign bit to 0
                bytes[bytes.Length - 1] &= 0x7F;
                result = new BigInteger(bytes);
            }
            // Repeat until the generated number is in the desired range
            while (result < min || result >= max);

            return result;
        }

    }
}
