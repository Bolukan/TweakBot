using System;
using System.Collections.Generic;
using System.Linq;

namespace TweakBot
{
    class Map
    {
        /// <summary>
        /// static self
        /// </summary>
        private static Map instance;

        /// <summary>
        /// give static self
        /// </summary>
        /// <returns>Map</returns>
        public static Map GetInstance()
        {
            if (instance == null)
            {
                instance = new Map();
            }
            return instance;
        }

        private List<Region> regions { get; set; }
        private List<SuperRegion> superRegions { get; set; }
        private List<Path> paths { get; set; }
        
        /// <summary>
        /// Initialise Map
        /// </summary>
        public Map()
        {
            regions = new List<Region>();
            superRegions = new List<SuperRegion>();
            paths = new List<Path>();
        }

        #region SuperRegion

        /// <summary>
        /// Add SuperRegion (setup_map super_regions)
        /// </summary>
        /// <param name="superRegion">SuperRegion</param>
        public void AddSuperRegion(SuperRegion superRegion)
        {
            superRegions.Add(superRegion);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<SuperRegion> SuperRegions
        {
            get { return superRegions; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SuperRegion GetSuperRegion(int id)
        {
            // search
            try
            {
                return superRegions.Find(x => x.Id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: Unable to find SuperRegion");
                Console.WriteLine("Msg: " + e.Message);
                return superRegions[0];
            }
        }

        #endregion

        #region Region

        /// <summary>
        /// add Region (setup_map regions)
        /// </summary>
        /// <param name="region">Region</param>
        public void AddRegion(Region region)
        {
            regions.Add(region);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Region> GetRegions()
        {
            return regions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Region GetRegion(int id)
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
        
        public int CountRegionsMy
        {
            get { return regions.Count(r => r.IsPlayerMy()); }
        }

        #endregion

        #region Path

        /// <summary>
        /// Add SuperRegion (setup_map super_regions)
        /// </summary>
        /// <param name="superRegion">SuperRegion</param>
        public void AddPath(Region regionFrom, Region regionTo)
        {
            paths.Add(new Path(regionFrom, regionTo));
            paths.Add(new Path(regionTo, regionFrom));
            regionFrom.AddNeighbour(regionTo);
            regionTo.AddNeighbour(regionFrom);
        }

        public List<Path> Paths
        {
            get { return paths; }
        }

        public List<Path> PathsFromMe()
        {
            return paths.Where(p => p.RegionFrom.IsPlayerMy()).ToList();
        }

        #endregion
        
        // calculate further map statistics
        public void CalculateMap()
        {
            foreach (SuperRegion superRegion in superRegions)
                superRegion.Calculate();
        }
        
        /// <summary>
        /// static list of favorites regions
        /// </summary>
        /// <returns>42 regions (first best)</returns>
        public static int[] GetFavorites()
        {
            return new int[] {
                39, 40, 41, 42, 
                12, 10, 11, 13, 
                23, 21, 22, 24, 25, 26,
                20, 17, 18, 19, 15, 16, 14,
                5, 9, 3, 2, 4, 7, 8, 1, 6,
                38, 36, 33, 30, 32, 27, 34, 28, 31, 37, 29, 35};
        }

        public void ResetTurn()
        {
            foreach (Region myRegion in Map.GetInstance().GetRegions())
            {
                myRegion.ResetTurn();
            }
        }
        
        public void CalcTurn()
        {
            BotState.GetInstance().CalcTurn();
        }

    }
}
