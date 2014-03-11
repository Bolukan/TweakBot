using System;
using System.Collections.Generic;

namespace TweakBot
{
    class Region
    {
        // final
        private int id;
        private List<Region> neighbours;
        private SuperRegion superRegion;
        
        // round specific
        private int armies;
        private Player player;

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
            this.neighbours = new List<Region>();
            //
        }

        public void AddNeighbour(Region neighbour)
        {
            neighbours.Add(neighbour);
        }

        /// <summary>
        /// returns id
        /// </summary>
        /// <returns>id</returns>
        public int Id
        {
            get { return id; }
        }

        public List<Region> Neighbours
        {
            get { return neighbours; }
        }

        public SuperRegion GetSuperRegion
        {
            get { return superRegion; }
        }

        #endregion

        #region round specific

        public void AddArmies(int extraArmies)
        {
            armies += extraArmies;
        }

        public int Armies
        {
            get { return armies; }
            set { armies = value; }
        }

        public Player Player
        {
            get { return player; }
        }

        public void ResetTurn()
        {
            Armies = 0;
            player = Player.Unknown();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="armies"></param>
        public void UpdateMap(Player player, int armies)
        {
            Armies = armies;
            this.player = player;
        }

        #endregion

        public bool IsPlayerNeutral()
        {
            return player.Id == 1;
        }

        public bool IsPlayerMy()
        {
            return player.Id == 2;
        }

        public bool IsPlayerOther()
        {
            return player.Id == 3;
        }

    }
}
