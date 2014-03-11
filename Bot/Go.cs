using System;
using System.Collections.Generic;
using System.Linq;

namespace TweakBot
{
    class Go
    {
        static List<String> Commands;
        static List<Region> RegionsMy = new List<Region>();
        static List<SuperRegion> SuperRegionsAtLeastOneRegionMy = new List<SuperRegion>();
        
        static private void CalculateInfo()
        {
            RegionsMy = Map.GetInstance().GetRegions().Where(r => r.IsPlayerMy()).ToList();
            SuperRegionsAtLeastOneRegionMy = Map.GetInstance().SuperRegions.Where(sr => sr.CountRegionsMy > 0).ToList();
        }

        static public String Place_Armies()
        {
            Commands = new List<String>();
            CalculateInfo();
            AddPlaceArmies(BestTarget(SuperRegionLeastRegionsNotMe(SuperRegionsAtLeastOneRegionMy)), BotState.GetInstance().StartingArmies);
            return String.Join(", ", Commands);
        }

        static public String Attack_Transfer()
        {
            Commands = new List<String>();
            BestAttack();
            MoveAwayFromInland();
            return String.Join(", ", Commands);
        }

        // SuperRegions with least regions not mine
        static public SuperRegion SuperRegionLeastRegionsNotMe(List<SuperRegion> SRs)
        {
            return SRs.OrderBy(sr => sr.CountRegionsNotMy).First();
        }

        static public Region BestTarget(SuperRegion SR)
        {
            return SR.RegionsMy().OrderByDescending(R => 
                   (R.Neighbours.Count(R2 => R2.IsPlayerOther()) * 2 + 
                    R.Neighbours.Count(R3 => R3.IsPlayerNeutral()))).First();
        }


        static public void AddPlaceArmies(Region region, int armies)
        {
            Commands.Add(Player.Me().Name + " place_armies " + region.Id.ToString() + " " + armies.ToString());
            region.AddArmies(armies);
        }
        
        static private void AddAttackTransfer(Region SourceRegion, Region TargetRegion, int armies)
        {
            Commands.Add(Player.Me().Name + " attack/transfer " + SourceRegion.Id.ToString() + " " + TargetRegion.Id.ToString() + " " + armies.ToString());
            SourceRegion.AddArmies(-armies);
        }
        

        static public void BestAttack()
        {
            Region BestRegion = BestTarget(SuperRegionLeastRegionsNotMe(SuperRegionsAtLeastOneRegionMy));
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

        static public void MoveAwayFromInland()
        {
            List<Region> RegionsInland = RegionsMy.Where(R => R.Neighbours.Where(N => ! N.IsPlayerMy()).Count() == 0).ToList();
            foreach(Region myRegion in RegionsInland)
            {
                AddAttackTransfer(myRegion, myRegion.Neighbours[Rand.Rnd().Next(myRegion.Neighbours.Count)], myRegion.Armies - 1);
            }
        }

        static public List<Path> AllPathMeToOther()
        {
            return Map.GetInstance().Paths.Where(P => P.RegionFrom.IsPlayerMy() && P.RegionTo.IsPlayerOther()).OrderByDescending(P => P.RegionFrom.Armies).OrderByDescending(P => P.RegionTo.Armies).ToList();
        }


    }
}
