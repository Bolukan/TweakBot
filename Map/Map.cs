using System;
using System.Collections.Generic;

namespace TweakBot
{
    class Map
    {
        private List<Region> regions { get; set; }
        private List<SuperRegion> superRegions { get; set; }

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

        public SuperRegion getSuperRegion(int id)
        {
            // search
            try
            {
                return superRegions.Find(x => x.getId()==id);
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: Unable to find SuperRegion");
                Console.WriteLine("Msg: " + e.Message);
                return superRegions[0];
            }
        }

        public Region getRegion(int id)
        {
            // search
            try
            {
                return regions.Find(x => x.getId() == id);
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: Unable to find Region");
                Console.WriteLine("Msg: " + e.Message);
                return regions[0];
            }
        }

    }
}
