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
        /// initialise calculations!
        /// </summary>
        public void CalculateInitial()
        {
            // CalcNeighbours();
        }
        
//        private void CalcNeighbours()
//        {
            // get all neighbours of regions of superregion
            // List<Region> AllNeighbours = regions.SelectMany(r => r.Neighbours).ToList();
            // get all neighbours of superregion
            // neighbours = AllNeighbours.Distinct().Except(regions).ToList();
//        }

        #endregion

        //public List<Region> Neighbours()
        //{
        //    return neighbours;
        //}

//        public int CountRegions
//        {
//            get { return regions.Count; }
//        }

        #region Turn dependent info



        #endregion

    }
}
