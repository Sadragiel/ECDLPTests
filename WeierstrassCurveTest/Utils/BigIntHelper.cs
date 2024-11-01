﻿using System.Numerics;

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

        public static BigInteger Sqrt(BigInteger number)
        {
            if (number < 0) throw new ArgumentException("Square root of a negative number is undefined.");

            if (number == 0) return 0;

            BigInteger n = number / 2 + 1; // Initial guess
            BigInteger n1 = (n + number / n) / 2;

            while (n1 < n)
            {
                n = n1;
                n1 = (n + number / n) / 2;
            }

            return n;
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

        public static BigInteger GCD(BigInteger a, BigInteger b, out BigInteger x, out BigInteger y)
        {
            if (b == 0)
            {
                x = 1;
                y = 0;
                return a;
            }

            BigInteger gcd = GCD(b, a % b, out BigInteger x1, out BigInteger y1);

            x = y1;
            y = x1 - (a / b) * y1;

            return gcd;
        }

        public static List<BigInteger> Factorize(BigInteger n)
        {
            List<BigInteger> factors = new List<BigInteger>();
            // Handle 2 separately to allow incrementing i by 2 later (for only odd numbers)
            while (n % 2 == 0)
            {
                factors.Add(2);
                n /= 2;
            }

            // Check odd numbers from 3 to sqrt(n)
            BigInteger sqrt = Sqrt(n);
            for (BigInteger i = 3; i <= sqrt; i += 2)
            {
                while (n % i == 0)
                {
                    factors.Add(i);
                    n /= i;
                }
            }

            // If n is a prime number greater than 2
            if (n > 2)
            {
                factors.Add(n);
            }

            return factors;
        }

        public static int LogBase2(BigInteger value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException("value", "Value must be positive.");

            // BitLength returns the number of bits required to represent the number in binary.
            // The logarithm base 2 is the highest position of a set bit, as the bit length minus one.
            // We assume that value is small enough to use int
            return (int)value.GetBitLength() - 1;
        }

        public static BigInteger Combination(BigInteger n, BigInteger k)
        {
            if (k > n) return 0;
            if (k > n - k) k = n - k; // Take advantage of symmetry, C(n, k) == C(n, n-k)

            BigInteger result = 1;
            for (int i = 1; i <= k; i++)
            {
                result = result * (n - i + 1) / i;
            }
            return result;
        }

        public static int MapHashCodeToInterval(int hashCode, int a, int b)
        {
            // TODO: move this method somewhere else
            long positiveHashCode = hashCode == int.MinValue ? (long)int.MaxValue + 1 : Math.Abs(hashCode);

            int scaledHashCode = (int)(a + (positiveHashCode % (b - a + 1)));

            return scaledHashCode;
        }

    }
}
