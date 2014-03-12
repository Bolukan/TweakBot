using System;
using System.Collections.Generic;
using System.Linq;

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

        #region Player

        public bool IsPlayerUnknown()
        {
            return player.Id == 0;
        }

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

        #endregion

        // Count
        static public int Count(List<Region> regions, Player player)
        {
            return regions.Count(r => r.Player == player);
        }

        static public int Count(Player player)
        {
            return Map.GetInstance().Regions.Count(r => r.Player == player);
        }

        static public int Count(SuperRegion superRegion, Player player)
        {
            return superRegion.Regions.Count(r => r.Player == player);
        }

        // Players
        static public List<Region> Regions(List<Region> regions, Player player)
        {
            return regions.Where(r => r.player == player).ToList();
        }

        static public List<Region> Regions(Player player)
        {
            return Map.GetInstance().Regions.Where(r => r.player == player).ToList();
        }

        static public List<Region> Regions(SuperRegion superRegion, Player player)
        {
            return superRegion.Regions.Where(r => r.player == player).ToList();
        }
    
    }
}
