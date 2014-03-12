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
            RegionsMy = Region.Regions(Player.Me());

            foreach (SuperRegion sr in Map.GetInstance().SuperRegions)
            {
                List<Region> regionsMe = Region.Regions(sr, Player.Me());
                List<Region> regionsOther = Region.Regions(sr, Player.Other());
                List<Region> regionsNeutral = Region.Regions(sr, Player.Neutral());
                List<Region> regionsUnknown = Region.Regions(sr, Player.Unknown());

                // Part me
                if (regionsMe.Count > 0) 
                {
                    // All me
                    if (regionsMe.Count == sr.Regions.Count)
                    {
                        sr.TurnTactics = 1; // DEFEND
                    } 
                    else 
                    {
                        // Also other
                        if (regionsOther.Count > 0)
                        {
                            sr.TurnTactics = 2; // MIXED HOSTILE
                        } 
                        else 
                        {
                            sr.TurnTactics = 3; // MIXED NEUTRAL
                        }
                    }
                } else {
                    // Other
                    if (regionsOther.Count > 0)
                    {
                        if (regionsNeutral.Count > 0)
                        {
                            sr.TurnTactics = 4; // KEEP WATCH
                        }
                        else
                        {
                            sr.TurnTactics = 5; // ATTACK
                        }
                    } 
                    else 
                    {
                        sr.TurnTactics = 6; // UNKNOWN
                    }
                }

            }
            // SuperRegionsAtLeastOneRegionMy = Map.GetInstance().SuperRegions.Where(sr => Region.Count(sr, Player.Me()) > 0).ToList();
        }

        public static void PlaceArmiesOnMIXED()
        {
            List<SuperRegion> sr_MIXED = Map.GetInstance().SuperRegions.Where(sr => (sr.TurnTactics == 2 || sr.TurnTactics == 3)).ToList();
            if (sr_MIXED.Count == 0) return;

            SuperRegion sr_BEST = sr_MIXED.OrderBy(sr => (sr.Regions.Count - Region.Count(sr, Player.Me()))).First();

            AddPlaceArmies(BestTarget(sr_BEST), BotState.GetInstance().StartingArmies);
        }


        static public String Place_Armies()
        {
            CalculateInfo();
            
            Commands = new List<String>();
            PlaceArmiesOnMIXED();

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
        static public Region BestTarget(SuperRegion SR)
        {
            return Region.Regions(SR, Player.Me()).OrderByDescending(R => 
                   (Region.Count(R.Neighbours, Player.Other()) * 2 + Region.Count(R.Neighbours, Player.Neutral()))).First();
        }

        static public void AddPlaceArmies(Region region, int armies)
        {
            Commands.Add(Player.Me().Name + " place_armies " + region.Id.ToString() + " " + armies.ToString());
            region.AddArmies(armies);
            BotState.GetInstance().StartingArmies -= armies;
        }
        
        static private void AddAttackTransfer(Region SourceRegion, Region TargetRegion, int armies)
        {
            Commands.Add(Player.Me().Name + " attack/transfer " + SourceRegion.Id.ToString() + " " + TargetRegion.Id.ToString() + " " + armies.ToString());
            SourceRegion.AddArmies(-armies);
        }
        

        static public void BestAttack()
        {
            List<SuperRegion> sr_MIXED = Map.GetInstance().SuperRegions.Where(sr => (sr.TurnTactics == 2 || sr.TurnTactics == 3)).ToList();
            if (sr_MIXED.Count == 0) return;

            SuperRegion sr_BEST = sr_MIXED.OrderByDescending(sr => (sr.Regions.Count - Region.Count(sr, Player.Me()))).First();
           
            Region BestRegion = BestTarget(sr_BEST);

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
            else if (Region.Count(BestRegion.Neighbours, Player.Neutral()) > 0)
            {
                Region RegionNeutral = Region.Regions(BestRegion.Neighbours, Player.Neutral()).First();
                AddAttackTransfer(BestRegion, RegionNeutral, BestRegion.Armies - 1);
                BestRegion.Armies = 1;
            }
        }

        static public void MoveAwayFromInland()
        {
            List<Region> RegionsInland = RegionsMy.Where(R => R.Armies > 0 && R.Neighbours.Count(N => ! N.IsPlayerMy()) == 0).ToList();
            if (RegionsInland.Count > 0)
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
