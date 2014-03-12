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
            if (BotState.GetInstance().StartingArmies > 0)
            {
                List<Region> NewRegion = Region.Regions(Player.Me()).OrderByDescending(R => Region.Count(R.Neighbours, Player.Other())).ToList();
                if (NewRegion.Count > 0) AddPlaceArmies(NewRegion.First(), BotState.GetInstance().StartingArmies);
            }
            if (BotState.GetInstance().StartingArmies > 0)
            {
                List<Region> NewRegion = Region.Regions(Player.Me()).OrderByDescending(R => Region.Count(R.Neighbours, Player.Neutral())).ToList();
                if (NewRegion.Count > 0) AddPlaceArmies(NewRegion.First(), BotState.GetInstance().StartingArmies);
            }

            return String.Join(", ", Commands);
        }

        static public String Attack_Transfer()
        {
            Commands = new List<String>();
            
            BestAttack();
            MoveAwayFromInland();

            if (Commands.Count == 0) return "No moves";
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
            foreach (SuperRegion SR in Map.GetInstance().SuperRegions)
            {
                if (SR.TurnTactics == 3) // MIXED NEUTRAL
                {
                    List<Region> R_My = Region.Regions(SR.Regions, Player.Me()).OrderByDescending(R => R.Armies).ToList();
                    foreach (Region R in R_My)
                    {
                        while (R.Armies > 4 && Region.Count(R.Neighbours.Intersect(SR.Regions).ToList(), Player.Neutral()) > 0)
                        {
                            AddAttackTransfer(R, Region.Regions(R.Neighbours.Intersect(SR.Regions).ToList(), Player.Neutral()).First(), R.Armies - 1); // toch met alles
                        }
                    }
                }

                if (SR.TurnTactics == 2) // MIXED HOSTILE
                {
                    List<Region> R_My = Region.Regions(SR.Regions, Player.Me()).OrderByDescending(R => R.Armies).ToList();
                    foreach (Region R in R_My)
                    {
                        List<Region> R_Other = Region.Regions(R.Neighbours.Intersect(SR.Regions).ToList(), Player.Other()).OrderByDescending(R2 => R2.Armies).ToList();
                        if (R_Other.Count > 0 && R_Other.First().Armies < R.Armies)
                        {
                            AddAttackTransfer(R, R_Other.First(), R.Armies - 1);
                        }
                    }
                }

                if (SR.TurnTactics == 1) // DEFEND
                {
                    // Regions with neighbours not me
                    List<Region> R_My = SR.Regions.Where(R => R.Neighbours.Count > Region.Count(R.Neighbours, Player.Me())).OrderByDescending(R => R.Armies).ToList();
                    if (R_My.Count > 0)
                        foreach(Region R_WithN in R_My)
                        {
                            Region R_Other = R_WithN.Neighbours.OrderByDescending(N => N.Armies).First();
                            if (R_WithN.Armies > 5 && (R_WithN.Armies - 5) > (2 * R_Other.Armies) && (R_Other.Player == Player.Other() || R_Other.Player == Player.Neutral()))
                                AddAttackTransfer(R_WithN, R_Other, R_WithN.Armies - 5);
                    }
                }
            }
        }

        static public void MoveAwayFromInland()
        {
            List<Region> RegionsInland = Region.Regions(Player.Me()).Where(r => r.Armies > 1 && Region.Count(r.Neighbours, Player.Me()) == r.Neighbours.Count()).ToList();
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
