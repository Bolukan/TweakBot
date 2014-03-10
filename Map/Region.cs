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
        private int[] armies;
        private int[] player;
                
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
            armies = new int[100];
            player = new int[100];
        }

        public void AddNeighbour(Region neighbour)
        {
            neighbours.Add(neighbour);
        }

        /// <summary>
        /// returns id
        /// </summary>
        /// <returns>id</returns>
        public int getId()
        {
            return id;
        }

        public List<Region> getNeighbours()
        {
            return neighbours;
        }

        public SuperRegion getSuperRegion()
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
            this.armies[Map.GetInstance().GetTurn()] = armies;
            this.player[Map.GetInstance().GetTurn()] = player;
        }

        //public void Update

    }
}
