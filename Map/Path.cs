using System;

namespace TweakBot
    {
    class Path
    {
        private Region regionFrom;
        private Region regionTo;
    
        /// <summary>
        /// Connection between 2 regions (for Risk 2 x 82 = 164)
        /// </summary>
        /// <param name="regionFrom">Region</param>
        /// <param name="regionTo">Region</param>
        public Path(Region regionFrom, Region regionTo)
        {
            this.regionFrom = regionFrom;
            this.regionTo = regionTo;
        }

        public Region RegionFrom
        {
            get { return regionFrom; }
        }

        public Region RegionTo
        {
            get { return regionTo; }
        }
    }
}
