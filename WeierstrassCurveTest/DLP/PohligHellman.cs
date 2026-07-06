using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WeierstrassCurveTest.EllipticCurves;
using WeierstrassCurveTest.Types;
using WeierstrassCurveTest.Utils;

using Point = WeierstrassCurveTest.Types.Point; 

namespace WeierstrassCurveTest.DLP
{
    internal class PohligHellman : DLPMethod
    {
        public DLPMethod supportMethod;

        public PohligHellman(EllipticCurve curve, DLPMethod supportMethod) : base(curve)
        {
            this.supportMethod = supportMethod;
        }

        public override BigInteger Solve(Point P, Point Q)
        {
            //Console.WriteLine($"Starting Solving PH Input: P {P}  and Q {Q} ");


            BigInteger N = curve.Order();
            var primeFactors = Factorize(N);
            //Console.WriteLine($"Factorized {N} ===> {primeFactors}");

            List<BigInteger> residues = new List<BigInteger>();
            List<BigInteger> moduli = new List<BigInteger>();

            foreach (var (p, e) in primeFactors)
            {
                //Console.WriteLine($"p={p} e={e}");

                BigInteger x = SolvePrimePower(P, Q, p, e);
                residues.Add(x);
                moduli.Add(BigInteger.Pow(p, e));
            }

            return ChineseRemainderTheorem(residues, moduli);
        }

        private List<(BigInteger, int)> Factorize(BigInteger n)
        {
            List<(BigInteger, int)> factors = new List<(BigInteger, int)>();
            for (BigInteger i = 2; i * i <= n; i++)
            {
                if (n % i == 0)
                {
                    int count = 0;
                    while (n % i == 0)
                    {
                        n /= i;
                        count++;
                    }
                    factors.Add((i, count));
                }
            }
            if (n > 1)
            {
                factors.Add((n, 1));
            }
            return factors;
        }

        private BigInteger ChineseRemainderTheorem(List<BigInteger> residues, List<BigInteger> moduli)
        {
            BigInteger M = moduli.Aggregate(BigInteger.One, (a, b) => a * b);
            BigInteger result = 0;

            for (int i = 0; i < moduli.Count; i++)
            {
                BigInteger Mi = M / moduli[i];
                BigInteger MiInverse = ModuloHelper.MultInverse(Mi, moduli[i]);
                result += residues[i] * Mi * MiInverse;
            }

            return result % M;
        }

        private BigInteger SolvePrimePower(Point P, Point Q, BigInteger p, int e)
        {
            BigInteger pe = BigInteger.Pow(p, e);
            BigInteger N = curve.Order();


            // Work in subgroup of order p
            BigInteger exponent = N / pe;
            Point P0 = curve.Mult(P, exponent);
            Point Q0 = curve.Mult(Q, exponent);

            if (P0.atInfinity)
            {
                Console.WriteLine($"Ecxeptional case. P0 is at infinity. Input: P {P}, Q {Q}, p {p}, e {e}");
                return 0;
            }

            BigInteger result = 0;
            BigInteger factor = 1;

            Point Q_current = Q0;

            for (int k = 0; k < e; k++)
            {
                // Subtract known part from Q
                if (k > 0)
                {
                    Point subtract = curve.Invert(curve.Mult(P0, result));
                    Q_current = curve.Add(Q0, subtract);
                }

                Point Qk = curve.Mult(Q_current, BigInteger.Pow(p, e - k - 1));
                Point Pk = curve.Mult(P0, BigInteger.Pow(p, e - 1));

                //Console.WriteLine($"Known points: P_reduced = {P0}; Qk = {Qk}");
                //Console.WriteLine($"Result before next calculations: result = {result}; factor = {factor}");
                BigInteger dk;
                if (Qk.atInfinity)
                {
                    dk = 0;
                } 
                else if (Qk.Equals(Pk))
                {
                    dk = 1;
                }
                else if (curve.Invert(Qk).Equals(Pk))
                {
                    dk = pe - 1;
                }
                else
                {
                    supportMethod.SetModulo(p);
                    dk = supportMethod.Solve(Pk, Qk);
                }

                result = (result + dk * factor) % pe;
                factor = (factor * p) % pe;
            }

            return result;
        }

        public override void EnableNegationMaps(bool extended)
        {
            base.EnableNegationMaps(extended);
            supportMethod.EnableNegationMaps(extended);
        }
    }
}
