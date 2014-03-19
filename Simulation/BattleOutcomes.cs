using System;
using System.Collections.Generic;
using System.Linq;

namespace TweakBot
{
    class BattleOutcomes
    {
        public const int MAX_ARMIES_IN_BATTLE = 20;
        public const double SUCCES_IN_ATTACK = 0.6;
        public const double SUCCES_IN_DEFEND = 0.7;
        // [0] will stay empty
        // [armies] has [0..armies] outcomes -> Size of table => armies * (armies + 1) + overhead
        // in this table the number of defenders - which maximises losses - is not included, uses the 'cum' for the chance of defeating maximum number of defenders
        private static double[][] chanceAttackArmiesSuccesses; // chance [armies][successes] 0.6
        private static double[][] chanceDefendArmiesSuccesses; // chance [armies][successes] 0.7
        private static double[][] chanceAttackArmiesSuccessesAtLeast; // chance [armies][minimum successes] 0.6
        private static double[][] chanceDefendArmiesSuccessesAtLeast; // chance [armies][minimum successes] 0.7

        private int armiesAttack;
        private int armiesDefend;
        private BattleOutcome[] battleOutcome;
        
        public BattleOutcomes(int armiesAttack, int armiesDefend)
        {
            if ((armiesAttack < 1) || (armiesDefend < 1) || (armiesAttack > MAX_ARMIES_IN_BATTLE) || (armiesDefend > MAX_ARMIES_IN_BATTLE))
            {
                // throw new InvalidOperationException();
                armiesAttack = 1;
                armiesDefend = 1;
            }
                   
            this.armiesAttack = armiesAttack;
            this.armiesDefend = armiesDefend;
            CalcBattleOutcome();
        }

        private void CalcBattleOutcome()
        {
            int max_Losses = armiesAttack < armiesDefend ? armiesAttack : armiesDefend;
            battleOutcome = new BattleOutcome[(max_Losses + 1) * (max_Losses + 1) - (armiesAttack == armiesDefend ? 1 : 0)];
            // create single outcome
            double[] R_Attack = new double[max_Losses+1];
            double[] R_Defend = new double[max_Losses+1];
            // copy chances for < max_Losses
            Array.Copy(chanceAttackArmiesSuccesses[armiesAttack], R_Attack, max_Losses);
            Array.Copy(chanceDefendArmiesSuccesses[armiesDefend], R_Defend, max_Losses);
            // highest cumulative
            R_Attack[max_Losses] = chanceAttackArmiesSuccessesAtLeast[armiesAttack][max_Losses];
            R_Defend[max_Losses] = chanceDefendArmiesSuccessesAtLeast[armiesDefend][max_Losses];

            // multiply chances for attack and defend losses
            for (int defend_loss = 0; defend_loss <= max_Losses; defend_loss++)
            {
                for (int attack_loss = 0; attack_loss <= max_Losses; attack_loss++)
                {
                    // special case
                    if ((attack_loss == armiesAttack) && (defend_loss == armiesDefend))
                    {
                        // do nothing
                    }
                    else if ((attack_loss == armiesAttack) && (defend_loss == (max_Losses - 1)) && (defend_loss == (armiesDefend - 1)))
                    {
                        // if attack_loss is max, dan defend_loss can not be max.
                        battleOutcome[defend_loss * (max_Losses + 1) + attack_loss] =
                            new BattleOutcome(armiesAttack, armiesDefend, defend_loss, attack_loss, R_Attack[defend_loss] * R_Defend[attack_loss] + R_Attack[defend_loss + 1] * R_Defend[attack_loss]);
                    }
                    else
                    {
                        battleOutcome[defend_loss * (max_Losses + 1) + attack_loss] =
                            new BattleOutcome(armiesAttack, armiesDefend, defend_loss, attack_loss, R_Attack[defend_loss] * R_Defend[attack_loss]);
                    }
                }
            }
        }

        public int ArmiesAttack
        {
            get { return armiesAttack; }
        }

        public int ArmiesDefend
        {
            get { return armiesDefend; }
        }

        public BattleOutcome[] BattleOutcome
        {
            get { return battleOutcome; }
        }

        public double AttackSuccess()
        {
            double success = 0;
            foreach (BattleOutcome bo in battleOutcome)
            {
                if (bo.AttackSuccess) success += bo.Chance;
            }
            return success;
        }

        public double AttackLosses()
        {
            return battleOutcome.Sum(bo => bo.WeightedAttackLoss);
        }

        public double DefendLosses()
        {
            return battleOutcome.Sum(bo => bo.WeightedDefendLoss);
        }

        static BattleOutcomes()
        {
            CalcChanceArmiesSuccesses();
        }
        
        #region Calculate chances
        
        private static void CalcChanceArmiesSuccesses()
        {
            chanceAttackArmiesSuccesses = ChanceArmiesSuccesses(MAX_ARMIES_IN_BATTLE, SUCCES_IN_ATTACK, false);
            chanceDefendArmiesSuccesses = ChanceArmiesSuccesses(MAX_ARMIES_IN_BATTLE, SUCCES_IN_DEFEND, false);
            chanceAttackArmiesSuccessesAtLeast = ChanceArmiesSuccesses(MAX_ARMIES_IN_BATTLE, SUCCES_IN_ATTACK, true);
            chanceDefendArmiesSuccessesAtLeast = ChanceArmiesSuccesses(MAX_ARMIES_IN_BATTLE, SUCCES_IN_DEFEND, true);
        }

        private static double[][] ChanceArmiesSuccesses(int armiesMax, double probability, bool cumulative)
        {
            double[][] chances = new double[armiesMax+1][]; 
            // do not fill [0] armies=0
            for (int armies = 1; armies <= armiesMax; armies++)
            {
                // outcome can be 0-armies, so armies+1 size
                chances[armies] = new double[armies + 1];
                // max is always single
                chances[armies][armies] = BinomialProbability(armies, armies, probability);
                for (int successes = armies - 1; successes >= 0; successes--)
                {
                    chances[armies][successes] = BinomialProbability(armies, successes, probability) + (cumulative ? chances[armies][successes + 1] : 0);
                }
            }
            return chances;
        }
      
        private static double BinomialProbability(int trials, int successes, double probabilityOfSuccess)
        {
            double probOfFailures = 1 - probabilityOfSuccess;

            double c = Combination(trials, successes);
            double px = Math.Pow(probabilityOfSuccess, successes);
            double qnx = Math.Pow(probOfFailures, trials - successes);

            return c * px * qnx;
        }

        private static long Combination(long trials, long successes)
        {
            if (trials <= 1) return 1;
            return Factorial(trials) / (Factorial(successes) * Factorial(trials - successes));
        }

        private static long Factorial(long x)
        {
            if (x <= 1)
                return 1;
            else
                return x * Factorial(x - 1);
        }

        #endregion

        public override string ToString()
        {
            return String.Format("A{0,2}-D{1,2}: {2:N3} LR:{3:N3}", this.ArmiesAttack, this.ArmiesDefend, this.AttackSuccess(), AttackLosses() / DefendLosses());
        }

    } // Class
} // nameSpace
        
     
