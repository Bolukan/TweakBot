using System;
using System.Collections.Generic;
using System.Linq;

namespace TweakBot
{
    class SuperRegion : BaseRegions
    {
        private int id;
        private int armiesReward;

        // Extra statistics
        // private List<Region> neighbours; // neighbours from regions but not own regions

        public int TurnTactics { get; set; }

        #region Initiation

        /// <summary>
        /// setup_map super_regions
        /// </summary>
        /// <param name="id">superregion id</param>
        /// <param name="bonusArmies">rewards</param>
        public SuperRegion(int id, int armiesReward) 
        {
            this.id = id;
            this.armiesReward = armiesReward;
        }

        public int Id
        {
            get { return id; }
        }

        public int ArmiesReward
        {
            get { return armiesReward; }

        }
 
#endregion

        #region Initial Calculations

        /// <summary>
        /// initial calculations
        /// </summary>
        public void CalculateInitial()
        {
        }
        
        #endregion

        #region Turn Calculations

        /// <summary>
        /// turn calculations
        /// </summary>
        public void CalculateTurn()
        {
        }

        #endregion

    }
}
