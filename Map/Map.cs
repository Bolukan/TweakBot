﻿using System;
using System.Collections.Generic;

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

        private String[] names;
        
        private List<Region> regions { get; set; }
        private List<SuperRegion> superRegions { get; set; }

        /// <summary>
        /// Initialise Map
        /// </summary>
        public Map()
        {
            regions = new List<Region>();
            superRegions = new List<SuperRegion>();
            names = new String[3];
            names[0] = "neutral";
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<SuperRegion> getSuperRegions()
        {
            return superRegions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Region> getRegions()
        {
            return regions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        
        public void SetMyName(String name)
        {
            names[1] = name;
        }

        public void SetOpponentName(String name)
        {
            names[2] = name;
        }


    }
}
