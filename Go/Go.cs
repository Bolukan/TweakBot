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
            RegionsMy = Map.GetInstance().RWhere(PLAYER.ME);

            foreach (SuperRegion sr in Map.GetInstance().SuperRegions)
            {
                List<Region> regionsMe = sr.RWhere(PLAYER.ME);
                List<Region> regionsOther = sr.RWhere(PLAYER.OTHER);
                List<Region> regionsNeutral = sr.RWhere(PLAYER.NEUTRAL);
                List<Region> regionsUnknown = sr.RWhere(PLAYER.UNKNOWN);

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
            // SuperRegionsAtLeastOneRegionMy = Map.GetInstance().SuperRegions.Where(sr => Region.Count(sr, PLAYER.ME) > 0).ToList();
        }

        public static void PlaceArmiesOnMIXED()
        {
            List<SuperRegion> sr_MIXED = Map.GetInstance().SuperRegions.Where(sr => (sr.TurnTactics == 2 || sr.TurnTactics == 3)).ToList();
            if (sr_MIXED.Count == 0) return;

            SuperRegion sr_BEST = sr_MIXED.OrderByDescending(sr => (sr.RCount(PLAYER.NOT_ME))).First();

            AddPlaceArmies(BestTarget(sr_BEST), BotState.GetInstance().StartingArmies);
        }


        static public String Place_Armies()
        {
            CalculateInfo();
            
            Commands = new List<String>();
            PlaceArmiesOnMIXED();

            if (BotState.GetInstance().StartingArmies > 0)
            {
                List<Region> NewRegion = Map.GetInstance().RWhere(PLAYER.ME).OrderByDescending(R => R.Neighbours.Count(N => N.Player == PLAYER.OTHER)).ToList();
                if (NewRegion.Count > 0) AddPlaceArmies(NewRegion.First(), BotState.GetInstance().StartingArmies);
            }
            if (BotState.GetInstance().StartingArmies > 0)
            {
                List<Region> NewRegion = Map.GetInstance().RWhere(PLAYER.ME).OrderByDescending(R => R.Neighbours.Count(N => N.Player == PLAYER.NEUTRAL)).ToList();
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
            return SR.RWhere(PLAYER.NOT_ME).SelectMany(Target => Target.Neighbours).Where(N => N.Player == PLAYER.ME).OrderByDescending(R => 
                   R.Neighbours.Count(N => N.Player == PLAYER.OTHER) * 2 + 
                   R.Neighbours.Count(N => N.Player == PLAYER.NEUTRAL)).First();
        }

        static public void AddPlaceArmies(Region region, int armies)
        {
            Commands.Add(PLAYER.NameMe + " place_armies " + region.Id.ToString() + " " + armies.ToString());
            region.AddArmies(armies);
            BotState.GetInstance().StartingArmies -= armies;
        }
        
        static private void AddAttackTransfer(Region SourceRegion, Region TargetRegion, int armies)
        {
            Commands.Add(PLAYER.NameMe + " attack/transfer " + SourceRegion.Id.ToString() + " " + TargetRegion.Id.ToString() + " " + armies.ToString());
            SourceRegion.AddArmies(-armies);
        }
        

        static public void BestAttack()
        {
            foreach (SuperRegion SR in Map.GetInstance().SuperRegions)
            {
                if (SR.TurnTactics == 3) // MIXED NEUTRAL
                {
                    // Kies regio's niet van mij
                    List<Region> R_NotMe = SR.RWhere(PLAYER.NOT_ME);
                    if (R_NotMe.Count > 0) 
                    {
                        foreach (Region R in R_NotMe)
                        {
                            // zoek of de buren van mij zijn
                            List<Region> R_Me = R.Neighbours.Where(N => N.Player == PLAYER.ME && N.Armies > 5).OrderByDescending(N => N.Armies).ToList();
                            if (R_Me.Count > 0) AddAttackTransfer(R_Me.First(), R, 5); // toch met alles
                        }
                    }
                }

                if (SR.TurnTactics == 2) // MIXED HOSTILE
                {
                    List<Region> R_My = SR.RWhere(PLAYER.ME).OrderByDescending(R => R.Armies).ToList();
                    foreach (Region R in R_My)
                    {
                        List<Region> R_Other = R.Neighbours.Intersect(SR.Regions).Where(N => N.Player == PLAYER.OTHER).OrderByDescending(R2 => R2.Armies).ToList();
                        if (R_Other.Count > 0 && R_Other.First().Armies < R.Armies)
                        {
                            AddAttackTransfer(R, R_Other.First(), R.Armies - 1);
                        }
                    }
                }

                if (SR.TurnTactics == 1) // DEFEND
                {
                    // Regions with neighbours not me
                    List<Region> R_My = SR.RWhere(PLAYER.ME).Where(R => R.Neighbours.Count(N => N.Player != PLAYER.ME) > 0).OrderByDescending(R => R.Armies).ToList();
                    if (R_My.Count > 0)
                        foreach(Region R_WithN in R_My)
                        {
                            Region R_Other = R_WithN.Neighbours.Where(N => N.Player != PLAYER.ME).OrderBy(N => N.Armies).First();
                            if (R_WithN.Armies > 5 && ((R_WithN.Armies - 5) > (2 * R_Other.Armies)))
                                AddAttackTransfer(R_WithN, R_Other, R_WithN.Armies - 5);
                    }
                }
            }
        }

        static public void MoveAwayFromInland()
        {
            List<Region> MoveThese = Map.GetInstance().RegionsInland.Regions.Where(R => R.Armies > 1).ToList();
            if (MoveThese.Count > 0)
            MoveThese.ForEach(R =>
                AddAttackTransfer(R, R.Neighbours.Where(N => N.FrontDistance < R.FrontDistance).FirstOrDefault(), R.Armies - 1));
        }

        static public List<Path> AllPathMeToOther()
        {
            return Map.GetInstance().Paths.Where(P => P.RegionFrom.Player == PLAYER.ME && P.RegionTo.Player == PLAYER.OTHER).OrderByDescending(P => P.RegionFrom.Armies).OrderByDescending(P => P.RegionTo.Armies).ToList();
        }


    }
}
