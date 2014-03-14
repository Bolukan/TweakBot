using System;
using System.Collections.Generic;
using System.Linq;

namespace TweakBot
{
    class Map : BaseRegions
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

        private List<SuperRegion> superRegions { get; set; }
        private List<Path> paths { get; set; }
        
        /// <summary>
        /// Initialise Map
        /// </summary>
        public Map()
        {
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
            return paths.Where(p => p.RegionFrom.Player == PLAYER.ME).ToList();
        }

        #endregion

        #region Initial Calculations

        // calculate further map statistics
        public void CalculateInitial()
        {
            foreach (SuperRegion superRegion in superRegions)
                superRegion.CalculateInitial();

            foreach (Region region in regions)
                region.CalculateInitial();
        }

        #endregion

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
            foreach (Region myRegion in Map.GetInstance().Regions)
            {
                myRegion.ResetTurn();
            }
        }
        
        public void CalcTurn()
        {
        //    BotState.GetInstance().CalcTurn();
        }

        private void CalcFront()
        {
            BaseRegions RegionsMe = new BaseRegions(Map.GetInstance().RWhere(PLAYER.ME));
            BaseRegions RegionsFront = new BaseRegions(Map.GetInstance().Regions.Where(R => R.Player == PLAYER.ME && R.Neighbours.Any(N => N.Player != PLAYER.ME)).ToList());
            BaseRegions RegionsInland = new BaseRegions(RegionsMe.Regions.Except(RegionsFront.Regions).ToList());

            // Reset all Regions
            Map.GetInstance().Regions.ForEach(R => { R.IsFront = false; R.FrontDistance = 0; });
            // Set Front on True and 1
            RegionsFront.Regions.ForEach(R => { R.IsFront = true; R.FrontDistance = 1; } );

            List<Region> RegionsOpen = RegionsInland.Regions;
            while (RegionsOpen.Count > 0)
            {
                RegionsOpen.ForEach(R => R.FrontDistance = R.Neighbours.Max(N => N.FrontDistance) + 1);
                RegionsOpen = RegionsOpen.Where(R => R.FrontDistance == 0).ToList();
            }

        }
    }
}
