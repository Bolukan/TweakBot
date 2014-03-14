using System;
using System.Text;

namespace TweakBot
{
    class AttackTransfer
    {
        public int Armies { get; set; }
        public Region SourceRegion { get; set; }
        public Region TargetRegion { get; set; }
    
        public AttackTransfer(int armies, Region sourceRegion, Region targetRegion)
        {
            this.Armies = armies;
            this.SourceRegion = sourceRegion;
            this.TargetRegion = targetRegion;
        }

        public override String ToString()
        {
            return String.Format("{0} attack/transfer {1} {2} {3}", PLAYER.NameMe, SourceRegion.Id, TargetRegion.Id, Armies);
        }
    
    }

}
 