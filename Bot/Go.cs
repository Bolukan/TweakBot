using System;
using System.Collections.Generic;
using System.Linq;

namespace TweakBot
{
    class Go
    {
        static List<String> Commands;


        // SuperRegions with at least 1 region is mine
        static public List<SuperRegion> SR_AtLeastOneRegionMy()
        {
            return Map.GetInstance().SuperRegions.Where(sr => sr.CountRegionsMy > 0).ToList();
        }
        
        // SuperRegions with least regions not mine
        static public SuperRegion SuperRegionLeastRegionsNotMe(List<SuperRegion> SRs)
        {
            return SRs.OrderBy(sr => sr.CountRegionsNotMy).FirstOrDefault();
        }

        static public Region BestTarget(SuperRegion SR)
        {
            return SR.RegionsMy().OrderByDescending(R => 
                   (R.Neighbours.Count(R2 => R2.IsPlayerOther()) * 2 + 
                    R.Neighbours.Count(R3 => R3.IsPlayerNeutral()))).FirstOrDefault();
        }

        static public String Place_Armies()
        {
            return PlaceArmies(BestTarget(SuperRegionLeastRegionsNotMe(SR_AtLeastOneRegionMy())), BotState.GetInstance().StartingArmies);
        }

        static public String PlaceArmies(Region region, int armies)
        {
            return Player.Me().Name + " place_armies " + region.Id.ToString() + " " + armies.ToString();
            region.AddArmies(armies);
        }
        
        static private void AddAttackTransfer(Region SourceRegion, Region TargetRegion, int armies)
        {
            Commands.Add(Player.Me().Name + " attack/transfer " + SourceRegion.Id.ToString() + " " + TargetRegion.Id.ToString() + " " + armies.ToString());
        }
        
        static public String Attack_Transfer()
        {
            Commands = new List<String>();
            BestAttack();
            return String.Join(", ",Commands);
        }

        static public void BestAttack()
        {
            Region BestRegion = BestTarget(SuperRegionLeastRegionsNotMe(SR_AtLeastOneRegionMy()));
            // Other
            List<Region> RegionsOther = BestRegion.Neighbours.Where(R => R.IsPlayerOther()).OrderByDescending(R2 => R2.Armies).ToList();
            if (RegionsOther.Count > 0)
            {
                if (RegionsOther.First().Armies < (BestRegion.Armies + 1))
                {
                    AddAttackTransfer(BestRegion, RegionsOther.First(), BestRegion.Armies - 1);
                    BestRegion.Armies = 1;
                }
            }
            else
            {
                Region RegionNeutral = BestRegion.Neighbours.Where(R => R.IsPlayerNeutral()).First();
                AddAttackTransfer(BestRegion, RegionNeutral, BestRegion.Armies - 1);
                BestRegion.Armies = 1;
            }
        }

        static public List<Path> AllPathMeToOther()
        {
            return Map.GetInstance().Paths.Where(P => P.RegionFrom.IsPlayerMy() && P.RegionTo.IsPlayerOther()).OrderByDescending(P => P.RegionFrom.Armies).OrderByDescending(P => P.RegionTo.Armies).ToList();
        }


    }
}
