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
        private String playerName;
        
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
            this.armies = 0;
            this.playerName = "Unknown";
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

    }
}
