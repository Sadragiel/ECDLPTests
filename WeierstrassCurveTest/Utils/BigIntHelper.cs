using System.Numerics;

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

        public static int MapHashCodeToInterval(int hashCode, int a, int b)
        {
            // TODO: move this method somewhere else
            long positiveHashCode = hashCode == int.MinValue ? (long)int.MaxValue + 1 : Math.Abs(hashCode);

            int scaledHashCode = (int)(a + (positiveHashCode % (b - a + 1)));

            return scaledHashCode;
        }

    }
}
