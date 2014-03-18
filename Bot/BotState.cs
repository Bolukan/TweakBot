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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Turn
        {
            get { return turn; }
        }

    } // class
} // nameSpace
