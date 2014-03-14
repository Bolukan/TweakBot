using System;
using System.Text;

namespace TweakBot
{
    public class PlaceArmies
    {
        public int Armies { get; set; }
        public Region Region { get; set; }

        public PlaceArmies(int armies, Region region)
        {
            Armies = armies;
            Region = region;
        }

        public String ToString()
        {
            return String.Format("{0} place_armies {1} {2}", PLAYER.NameMe, Region.Id, Armies);
        }
    }
}
