﻿using System;
using System.Collections.Generic;

namespace TweakBot
{
    class SuperRegion
    {
        private int id;
        private int armiesReward;
        private List<Region> regions;

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

        /// <summary>
        /// setup_map regions
        /// </summary>
        /// <param name="region">Region</param>
        public void addRegion(Region region)
        {
            if (!this.regions.Contains(region))
            {
                this.regions.Add(region);
            }
        }
        
        /// <summary>
        /// return id
        /// </summary>
        /// <returns>id </returns>
        public int getId()
        { 
            return this.id; 
        }

        /// <summary>
        /// return armies reward
        /// </summary>
        /// <returns>armies reward</returns>
        public int getArmiesReward()
        { 
            return this.armiesReward; 
        }

        /// <summary>
        /// return regions of SuperRegion
        /// </summary>
        /// <returns>List of regions</returns>
        public List<Region> getRegions()
        { 
            return this.regions; 
        }

    }
}