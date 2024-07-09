using System.Numerics;

namespace WeierstrassCurveTest.Utils
{
    internal class ModuloHelper
    {
        public static BigInteger MultInverse(BigInteger value, BigInteger modulus)
        {
            BigInteger a = Abs(value, modulus);
            BigInteger x = 0, xLast = 1, q, r, mOriginal = modulus;
            while (modulus != 0)
            {
                q = a / modulus;
                r = a % modulus;
                (a, modulus) = (modulus, r);
                (x, xLast) = (xLast - q * x, x);
            }
            if (xLast < 0)
                xLast += mOriginal;

            return xLast;
        }

        public static BigInteger AddInverse(BigInteger value, BigInteger modulus)
        {
            return ((-value % modulus) + modulus) % modulus;
        }

        public static BigInteger Abs(BigInteger value, BigInteger modulus)
        {
            return ((value % modulus) + modulus) % modulus;
        }

        public static BigInteger SquareRootMod(BigInteger quadraticResidue, BigInteger modulus)
        {
            if (BigInteger.ModPow(quadraticResidue, (modulus - 1) / 2, modulus) != 1)
            {
                throw new ArgumentException("n is not a quadratic residue modulo p.");
            }

            if (modulus % 4 == 3)
            {
                return BigInteger.ModPow(quadraticResidue, (modulus + 1) / 4, modulus);
            }

            BigInteger s = 0;
            BigInteger q = modulus - 1;
            while ((q & 1) == 0)
            {
                s += 1;
                q >>= 1;
            }

            BigInteger z = 2;
            while (BigInteger.ModPow(z, (modulus - 1) / 2, modulus) != modulus - 1)
            {
                z++;
            }

            BigInteger m = s;
            BigInteger c = BigInteger.ModPow(z, q, modulus);
            BigInteger t = BigInteger.ModPow(quadraticResidue, q, modulus);
            BigInteger r = BigInteger.ModPow(quadraticResidue, (q + 1) / 2, modulus);

            while (t != 1)
            {
                BigInteger tt = t;
                BigInteger i = 0;
                while (tt != 1 && i < m)
                {
                    tt = BigInteger.ModPow(tt, 2, modulus);
                    i++;
                }
                if (i == m)
                    throw new Exception("Square root not found.");

                BigInteger b = BigInteger.ModPow(c, BigInteger.Pow(2, (int)(m - i - 1)), modulus);
                r = (r * b) % modulus;
                t = (t * b * b) % modulus;
                c = (b * b) % modulus;
                m = i;
            }

            return r;
        }

    }
}
