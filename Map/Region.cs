using System;
using System.Collections.Generic;
using System.Linq;

namespace TweakBot
{
    class Region
    {
        // final
        private int id;
        private SuperRegion superRegion;
        private BaseRegions neighbours;
        
        // round specific
        private int armies;
        private int player;
        // analyse
        public bool IsFront { get; set; }
        public int FrontDistance { get; set; }

        #region initial values

        /// <summary>
        /// Initialise Region
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="superRegion">SuperRegion</param>
        public Region(int id, SuperRegion superRegion)
        {
            this.id = id;
            this.superRegion = superRegion;
            this.neighbours = new BaseRegions();
            player = PLAYER.UNKNOWN;
            armies = 2;
        }

        /// <summary>
        /// Add neighbour
        /// </summary>
        /// <param name="neighbour">Region</param>
        public void AddNeighbour(Region neighbour)
        {
            neighbours.AddRegion(neighbour);
        }

        /// <summary>
        /// returns id
        /// </summary>
        /// <returns>id</returns>
        public int Id
        {
            get { return id; }
        }

        /// <summary>
        /// Return SuperRegion
        /// </summary>
        public SuperRegion SuperRegion
        {
            get { return superRegion; }
        }

        public List<Region> Neighbours
        {
            get { return neighbours.Regions; }
        }

        #endregion

        #region Initial Calculations

        /// <summary>
        /// initial calculations
        /// </summary>
        public void CalculateInitial()
        {
        }

        #endregion

        #region Turn input

        public void ResetTurn()
        {
            Armies = 0;
            // keep Other
            if ((player == PLAYER.ME) || (player == PLAYER.NEUTRAL))
            {
                player = PLAYER.UNKNOWN;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="armies"></param>
        public void UpdateMap(int player, int armies)
        {
            Armies = armies;
            this.player = player;
        }

        public int Armies
        {
            get { return armies; }
            set { armies = value; }
        }

        public int Player
        {
            get { return player; }
        }
        
        public void AddArmies(int extraArmies)
        {
            armies += extraArmies;
        }

        #endregion

        #region Turn Calculations

        /// <summary>
        /// turn calculations
        /// </summary>
        public void CalculateTurn()
        {
        }

        #endregion
 
    }
}
