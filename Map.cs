using System;
using System.Collections.Generic;

namespace TweakBot
{
    class Map
    {
        public List<Region> regions;
	    public List<SuperRegion> superRegions;

        /// <summary>
        /// Initialise Map
        /// </summary>
        public Map()
        {
            regions = new List<Region>();
            superRegions = new List<SuperRegion>();
        }
        
        /// <summary>
        /// Add SuperRegion (setup_map super_regions)
        /// </summary>
        /// <param name="superRegion">SuperRegion</param>
        public void AddSuperRegion(SuperRegion superRegion)
        {
            superRegions.Add(superRegion);
        }

        /// <summary>
        /// add Region (setup_map regions)
        /// </summary>
        /// <param name="region">Region</param>
        public void AddRegion(Region region)
        {
            regions.Add(region);
        }

        public List<SuperRegion> getSuperRegions()
        {
            return superRegions;
        }

        public List<Region> getRegions()
        {
            return regions;
        }

        public SuperRegion SuperRegion(int id)
        {
            // quick
            if (superRegions[id-1].getId() == id) return superRegions[id-1];
            // search
            return superRegions.Find(x => x.getId()==id);
        }

        public Region Region(int id)
        {
            // quick
            if (regions[id - 1].getId() == id) return regions[id - 1];
            // search
            return regions.Find(x => x.getId() == id);
        }



    }
}
