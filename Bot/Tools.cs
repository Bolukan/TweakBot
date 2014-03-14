using System;
using System.Collections.Generic;

namespace TweakBot
{
    class Tools
    {
        /// <summary>
        /// static self
        /// </summary>
        private static Tools instance;

        /// <summary>
        /// give static self
        /// </summary>
        /// <returns>Map</returns>
        public static Tools GetInstance()
        {
            if (instance == null)
            {
                instance = new Tools();
            }
            return instance;
        }

        private double[][] attack; // 0.6
        private double[][] defend; // 0.7
        
        public Tools()
        {
            int armiesmax = 20;
            attack = CalcTable(armiesmax, 0.6);
            defend = CalcTable(armiesmax, 0.7);
        }


        public double[][] CalcTable(int armiesmax, double probability)
        {
            double[][] chances = new double[armiesmax+1][]; // loose armies=0
            for (int armies = 1; armies <= armiesmax; armies++)
            {
                chances[armies] = new double[armies + 1];
                for (int successes = 0; successes <= armies; successes++)
                {
                    chances[armies][successes] = BinomialProbability(armies, successes, probability);
                }
            }
            return chances;
        }

        private static long Factorial(long x)
        {
            if (x <= 1)
                return 1;
            else
                return x * Factorial(x - 1);
        }

        private long Combination(long trials, long successes)
        {
            if (trials <= 1) return 1;
            return Factorial(trials) / (Factorial(successes) * Factorial(trials - successes));
        }

        public double BinomialProbability(int trials, int successes, double probabilityOfSuccess)
        {
            double probOfFailures = 1 - probabilityOfSuccess;

            double c = Combination(trials, successes);
            double px = Math.Pow(probabilityOfSuccess, successes);
            double qnx = Math.Pow(probOfFailures, trials - successes);

            return c * px * qnx;
        }

    }
}
