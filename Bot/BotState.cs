using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TweakBot
{
    /// <summary>
    /// Contains data of the world at current turn
    /// 
    /// TURN:
    ///  go place_armies
    ///  go attack/transfer
    ///  settings starting_armies
    ///  update_map
    ///  opponent_moves
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
        //private List<Region> myRegions;
        //private List<Region> otherRegions;
        //private List<Region> neutralRegions;

        public BotState()
        {
            turn = -1;
        }

        public int StartingArmies
        {
            get { return starting_armies; }
            set { starting_armies = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private void NextTurn()
        {
            turn++;
            //myRegions = new List<Region>();
            //otherRegions = new List<Region>();
            //neutralRegions = new List<Region>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetTurn()
        {
            return turn;
        }

        //public void CalcTurn()
        //{
        //    Map myMap = Map.GetInstance();
        //    myRegions = myMap.Regions.FindAll(R => R.Player == PLAYER.ME);
        //    otherRegions = myMap.Regions.FindAll(R => R.Player == PLAYER.OTHER);
        //}

        //public List<Region> MyRegions()
        //{
        //    return myRegions;
        //}

        //public String Place_armies()
        //{
        //    StringBuilder temp = new StringBuilder();
        //    Random rnd = Rand.Rnd();
        //    for (int i = this.starting_armies; i > 0; i--)
        //    {
        //        temp.Append(PLAYER.NAMEMY + " place_armies " + myRegions[rnd.Next(myRegions.Count())].Id.ToString() + " 1");
        //        if (i > 1) temp.Append(", ");
        //    }
        //    return temp.ToString();
        //}

        //public String Attack()
        //{
        //    StringBuilder temp = new StringBuilder();
        //    Random rnd = Rand.Rnd();
        //    Region myRegion = myRegions[rnd.Next(myRegions.Count())];
        //    foreach(Region neighbour in myRegion.Neighbours)
        //    {
        //        if (((neighbour.Armies * 2) < myRegion.Armies) && (neighbour.IsPlayerOther()))
        //        {
        //            temp.Append(PLAYER.ME.Name + " attack/transfer " + myRegion.Id.ToString() + " " + neighbour.Id.ToString() + " " + String.Concat(myRegion.Armies - 1));
        //            temp.Append(", ");
        //        }
        //    }

        //    if (temp.Length == 0) return "No moves";
        //        //temp.Append(Player.Name(2) + " place_armies " + myRegions.First(R => R.IsPlayerMine()).GetId().ToString() + " 1");
        //        //if (i > 1) temp.Append(", ");
       
        //    return temp.ToString().TrimEnd(',');
        //}

    } // class
} // nameSpace
