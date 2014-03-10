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
        private int player;
                
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
        public int GetId()
        {
            return id;
        }

        public List<Region> GetNeighbours()
        {
            return neighbours;
        }

        public SuperRegion GetSuperRegion()
        {
            return superRegion;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="armies"></param>
        public void UpdateMap(int player, int armies)
        {
            this.armies = armies;
            this.player = player;
        }

        public void ResetTurn()
        {
            this.armies = 0;
            this.player = 0;
        }

        public int GetArmies()
        {
            return armies;
        }

        public bool IsPlayerMine()
        {
            return player == 2;
        }

        public bool IsPlayerOther()
        {
            return player == 3;
        }

        public bool IsPlayerNeutral()
        {
            return player == 1;
        }

    }
}
