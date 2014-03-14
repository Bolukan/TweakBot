using System;
using System.Text;

namespace TweakBot
{
    public class AttackTransfer
    {
        public int Armies { get; set; }
        public Region SourceRegion { get; set; }
        public Region TargetRegion { get; set; }
    
        public AttackTransfer(int armies, Region sourceRegion, Region targetRegion)
        {
            Armies = armies;
            SourceRegion = sourceRegion;
            TargetRegion = targetRegion;
        }

        public String ToString()
        {
            return String.Format("{0} attack/transfer {1} {2} {3}", PLAYER.NameMe, SourceRegion.Id, TargetRegion.Id, Armies);
        }
    
    }

}
 