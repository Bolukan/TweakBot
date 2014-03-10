using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TweakBot
{
    /// <summary>
    /// Contains data of the world
    /// </summary>
    
    class BotState
    {
        /// <summary>
        /// static self
        /// </summary>
        private static BotState instance;

        /// <summary>
        /// give static self
        /// </summary>
        /// <returns>Map</returns>
        public static BotState GetInstance()
        {
            if (instance == null)
            {
                instance = new BotState();
            }
            return instance;
        }

        // turn specific
        private int turn;
        private int starting_armies;
        private List<Region> myRegions;
        private List<Region> otherRegions;
        private List<Region> neutralRegions;

        public BotState()
        {
            turn = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="starting_armies"></param>
        public void SetArmies(int starting_armies)
        {
            this.starting_armies = starting_armies;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetArmies()
        {
            return starting_armies;
        }

        /// <summary>
        /// 
        /// </summary>
        private void NextTurn()
        {
            turn++;
            myRegions = new List<Region>();
            otherRegions = new List<Region>();
            neutralRegions = new List<Region>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetTurn()
        {
            return turn;
        }

        public void CalcTurn()
        {
            Map myMap = Map.GetInstance();
            myRegions = myMap.GetRegions().FindAll(R => R.IsPlayerMine());
            otherRegions = myMap.GetRegions().FindAll(R => R.IsPlayerOther());
        }

        public List<Region> MyRegions()
        {
            return myRegions;
        }

        public String Place_armies()
        {
            StringBuilder temp = new StringBuilder();

            for (int i = this.GetArmies(); i > 0; i--)
            {
                temp.Append(Player.Name(2) + " place_armies " + myRegions.First(R => R.IsPlayerMine()).GetId().ToString() + " 1");
                if (i > 1) temp.Append(", ");
            }
            return temp.ToString();
        }

    } // class
} // nameSpace
