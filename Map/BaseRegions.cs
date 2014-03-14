using System;
using System.Collections.Generic;
using System.Linq;

namespace TweakBot
{
    class BaseRegions
    {
        #region Basic Region
        protected List<Region> regions;

        /// <summary>
        /// Initialise
        /// </summary>
        public BaseRegions()
        {
            regions = new List<Region>();
        }

        /// <summary>
        /// Initialise and fill, filter on double entries.
        /// </summary>
        /// <param name="regions">Double entries are eliminated</param>
        public BaseRegions(List<Region> regions)
        {
            // initialise
            this.regions = new List<Region>();
            // eliminate double entries
            foreach (Region region in regions)
            {
                AddRegion(region);
            }
        }

        /// <summary>
        /// Add single region, no double entries
        /// </summary>
        /// <param name="region">region</param>
        public void AddRegion(Region region)
        {
            if (!this.regions.Contains(region))
            {
                this.regions.Add(region);
            }
        }

        /// <summary>
        /// Return all regions
        /// </summary>
        public List<Region> Regions
        {
            get { return regions; }
        }

        /// <summary>
        /// Find region on id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>1 region</returns>
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

        #endregion

        #region Functions RWhere RCount

        /// <summary>
        /// return regions using player-filter
        /// </summary>
        /// <param name="player">player filter (1+2+4+8)</param>
        /// <returns>List of regions</returns>
        public List<Region> RWhere(int player)
        {
            return regions.Where(r => (r.Player & player) != 0).ToList();
        }

        /// <summary>
        /// return regions using player-filter (static)
        /// </summary>
        /// <param name="player">player filter (1+2+4+8)</param>
        /// <param name="regions2">List of regions</param>
        /// <returns>List of regions</returns>
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
