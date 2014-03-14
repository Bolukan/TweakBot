using System;
using System.Text;

namespace TweakBot
{
    class PlaceArmies
    {
        public int Armies { get; set; }
        public Region Region { get; set; }

        public PlaceArmies(int armies, Region region)
        {
            Armies = armies;
            Region = region;
        }

        public override String ToString()
        {
            return String.Format("{0} place_armies {1} {2}", PLAYER.NameMe, Region.Id, Armies);
        }
    }
}
