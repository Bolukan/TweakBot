﻿using System;
using System.Collections.Generic;

namespace TweakBot
{
  
    class FiniteStateMachine
    {
        public struct GameState
        {
            public bool DoPlaceArmies;
            public int[] Regions;
                    
            public GameState(bool placeArmies, int[] regions) 
            {   
                DoPlaceArmies = placeArmies;
                Regions = regions;
            }
        
            public override string ToString()
            {
                return String.Concat(DoPlaceArmies ? "P":"M", Regions);
            }
        } // struct

        public struct BattleOutcome
        {
            int AttackLoss;
            int DefendLoss;
            double Chance;

            public BattleOutcome(int attackLoss, int defendLoss, double chance)
            {
                AttackLoss = attackLoss;
                DefendLoss = defendLoss;
                Chance = chance;
            }

            public override string ToString()
            {
                return String.Format("{0}/{1} {2:N6}", DefendLoss, AttackLoss, Chance);
            }

        } // struct

        public Dictionary<GameState, double> states;

        private void AddChanceToState(GameState state, double addChance)
        {
            if (states.ContainsKey(state))
            {
                states[state] = states[state] + addChance;
            }
            else
            {
                states.Add(state, addChance);
            }
        }

        public FiniteStateMachine()
        { 
            states = new Dictionary<GameState, double>();
            // -2 = neutral, 2 = ME, Chance=100 = 100% 
            states.Add(new GameState(true, new int[] { -2, 2, -2 }),100);

            foreach (KeyValuePair<GameState, double> state in states)
            {
                if (state.Key.DoPlaceArmies)
                {
                    // 1 tactic : place on mid spot
                    AddChanceToState(new GameState(false, new int[] { state.Key.Regions[0], state.Key.Regions[1] + 5, state.Key.Regions[2] }), state.Value);
                }
                else
                {
                    // end state if none is neutral
                    if (state.Key.Regions[0] < 0 || state.Key.Regions[2] < 0)
                    {
                        // no move
                        if (state.Key.Regions[1] < 4) 
                        {
                            AddChanceToState(new GameState(true, new int[] { state.Key.Regions[0], state.Key.Regions[1], state.Key.Regions[2] }), state.Value);
                        } 
                        // attack 1
                        if (state.Key.Regions[1] < 7)
                        {

                        }
                    }
                }
            }


        }

        public BattleOutcome[] CalculateBattleOutcome(int ArmiesAttack, int ArmiesDefend)
        {
            double[] R_Attack = Tools.GetInstance().BattleOutcome(ArmiesAttack, ArmiesDefend, true); // defend loss
            double[] R_Defend = Tools.GetInstance().BattleOutcome(ArmiesDefend, ArmiesAttack, false); // attack loss
            List<BattleOutcome> uitslag = new List<BattleOutcome>();

            for (int defend_loss = 0; defend_loss <= R_Attack.Length - 1; defend_loss++)
            {
                for (int attack_loss = 0; attack_loss <= R_Defend.Length - 1; attack_loss++)
                {
                    uitslag.Add(new BattleOutcome(attack_loss, defend_loss, R_Attack[defend_loss] * R_Defend[attack_loss]));
                }
            }
            return uitslag.ToArray();
        }
    }
    
    
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

        public double[][] attack; // 0.6
        public double[][] defend; // 0.7
        public double[][] attackcum;
        public double[][] defendcum;

        public Tools()
        {
            int armiesmax = 20;
            attack = CalcTable(armiesmax, 0.6);
            defend = CalcTable(armiesmax, 0.7);

            attackcum = new double[armiesmax + 1][];
            defendcum = new double[armiesmax + 1][];
            
            for (int armies = 1; armies <= armiesmax; armies++)
            {
                attackcum[armies] = new double[armies + 1];
                defendcum[armies] = new double[armies + 1];

                attackcum[armies][armies] = attack[armies][armies];
                defendcum[armies][armies] = defend[armies][armies]; // highest cum is same as single

                for (int successes = armies - 1; successes >= 0; successes--)
                {
                    attackcum[armies][successes] = attackcum[armies][successes + 1] + attack[armies][successes];
                    defendcum[armies][successes] = defendcum[armies][successes + 1] + defend[armies][successes];
                }
            }

        }
        
        public double[] BattleOutcome(int Armies, int MaxLoss, bool IsAttack)
        {
            double[] Outcome = new double[MaxLoss+1];
            int maxOutcome = Armies < MaxLoss ? Armies : MaxLoss;
            if (IsAttack)
            {
                for (int c = 0; c < maxOutcome; c++)
                {
                    Outcome[c] = attack[Armies][c];
                }
                Outcome[maxOutcome] = attackcum[Armies][maxOutcome];
            }
            else
            {
                for (int c = 0; c < maxOutcome; c++)
                {
                    Outcome[c] = defend[Armies][c];
                }
                Outcome[maxOutcome] = defendcum[Armies][maxOutcome];
            }
            return Outcome;
        }

        private double[][] CalcTable(int armiesmax, double probability)
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
