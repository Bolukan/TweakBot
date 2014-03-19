using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TweakBot
{
    class BattleOutcome
    {
        int attackArmies;
        int defendArmies;
        int defendLoss;
        int attackLoss;
        double chance;

        public BattleOutcome(int attackArmies, int defendArmies, int defendLoss, int attackLoss, double chance)
        {
            this.attackArmies = attackArmies;
            this.defendArmies = defendArmies;
            this.defendLoss = defendLoss;
            this.attackLoss = attackLoss;
            this.chance = chance;
        }

        public int DefendLoss
        {
            get { return defendLoss; }
        }

        public int AttackLoss
        {
            get { return attackLoss; }
        }

        public double WeightedDefendLoss
        {
            get { return defendLoss * chance; }
        }

        public double WeightedAttackLoss
        {
            get { return attackLoss * chance; }
        }

        public double Chance
        {
            get { return this.chance; }
        }

        public int DefendArmiesLeft
        {
            get { return defendArmies - defendLoss; }
        }

        public int AttackArmiesLeft
        {
            get { return attackArmies - attackLoss; }
        }

        public bool AttackSuccess
        {
            get { return (DefendArmiesLeft == 0) && (AttackArmiesLeft != 0); }
        }

        public override string ToString()
        {
            return String.Format("{0}-{1} -> {2}/{3} {4:N6}", attackArmies, defendArmies, attackLoss, defendLoss, chance);
        }

    }
}
