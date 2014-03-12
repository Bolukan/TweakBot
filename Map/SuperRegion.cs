using System;
using System.Collections.Generic;
using System.Linq;

namespace TweakBot
{
    class SuperRegion
    {
        private int id;
        private int armiesReward;
        private List<Region> regions;

        // Extra statistics
        private List<Region> neighbours; // neighbours from regions but not own regions

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
            this.regions = new List<Region>();
        }

        public int Id
        {
            get { return id; }
        }

        public int ArmiesReward
        {
            get { return armiesReward; }

        }

        public List<Region> Regions
        {
            get { return regions; }
        }

        /// <summary>
        /// setup_map regions
        /// </summary>
        /// <param name="region">Region</param>
        public void AddRegion(Region region)
        {
            if (!this.regions.Contains(region))
            {
                this.regions.Add(region);
            }
        }
 
#endregion

        #region Initial Calculations

        /// <summary>
        /// initialise calculations!
        /// </summary>
        public void Calculate()
        {
            CalcNeighbours();
        }
        
        private void CalcNeighbours()
        {
            // get all neighbours of regions of superregion
            List<Region> AllNeighbours = regions.SelectMany(r => r.Neighbours).ToList();
            // get all neighbours of superregion
            neighbours = AllNeighbours.Distinct().Except(regions).ToList();
        }

        #endregion

        public List<Region> Neighbours()
        {
            return neighbours;
        }

//        public int CountRegions
//        {
//            get { return regions.Count; }
//        }

        #region Turn dependent info



        #endregion

    }
}
