using System;
using System.Collections.Generic;
using System.Linq;

namespace TweakBot
{
    class BaseRegions
    {
        protected List<Region> regions;

        public BaseRegions()
        {
            regions = new List<Region>();
        }

        public BaseRegions(List<Region> regions)
        {
            this.regions = new List<Region>();
            foreach (Region region in regions)
            {
                AddRegion(region);
            }
        }

        public void AddRegion(Region region)
        {
            if (!this.regions.Contains(region))
            {
                this.regions.Add(region);
            }
        }

        public List<Region> Regions
        {
            get { return regions; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Region Region(int id)
        {
            // search
            try
            {
                return regions.Find(x => x.Id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: Unable to find Region");
                Console.WriteLine("Msg: " + e.Message);
                return regions[0];
            }
        }

        #region Functions

        // Where Player
        public List<Region> RWhere(int player)
        {
            return regions.Where(r => (r.Player & player) != 0).ToList();
        }

        public List<Region> RWhere(int player, List<Region> regions2)
        {
            return regions2.Where(r => (r.Player & player) != 0).ToList();
        }

        // Count Player
        public int RCount(int player)
        {
            return regions.Count(r => (r.Player & player) != 0);
        }

        // Count Player
        public static int RCount(int player, List<Region> regions2)
        {
            return regions2.Count(r => (r.Player & player) != 0);
        }

        #endregion

    }
}
