using System;
using System.Collections.Generic;
using System.Linq;

namespace TweakBot
{
    class BaseRegions
    {
        protected List<Region> regions;

        public BaseRegions()
        {
            regions = new List<Region>();
        }
        
        public void AddRegion(Region region)
        {
            if (!this.regions.Contains(region))
            {
                this.regions.Add(region);
            }
        }

        public List<Region> Regions
        {
            get { return regions; }
        }

    }
}
