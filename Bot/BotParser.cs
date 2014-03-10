using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace TweakBot
{
    class BotParser
    {
     
        public void Parse(String line)
        {
            String[] parts = line.Split(' ');

            switch (parts[0].ToLowerInvariant())
            {
                // settings
                case "go":
                   switch (parts[1].ToLowerInvariant())
                    {
                        case "place_armies":
                            Console.WriteLine(BotState.GetInstance().Place_armies());
                            break;
                        case "attack/transfer":
                            Console.WriteLine("No moves");
                            break;
                        default:
                            // EXCEPTION
                            break;
                    }
                    break;

                
                // settings
                case "settings":
                    switch (parts[1].ToLowerInvariant())
                    {
                        case "starting_armies":
                            BotState.GetInstance().SetArmies(int.Parse(parts[2]));
                            break;
                        case "your_bot":
                            Player.SetMyName(parts[2]);
                            break;
                        case "opponent_bot":
                            Player.SetOtherName(parts[2]);
                            break;
                        default:
                            // EXCEPTION
                            break;
                    }
                    break;

                case "update_map":
                    // Reset values
                    Map.GetInstance().ResetTurn();
                    // Read no situation
                    for (int i = 1; i < parts.Length; i++)
                    {
                        try
                        {
                            Map.GetInstance().GetRegion(int.Parse(parts[i++])).UpdateMap(Player.Name(parts[i++]), int.Parse(parts[i]));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("ERROR: Unable to parse update_map");
                            Console.WriteLine("Msg: " + e.Message);
                        }
                    }
                    // Calculate map
                    Map.GetInstance().CalcTurn();

                    break;

                case "pick_starting_regions":
                    Map.GetInstance().CalculateMap();
                    int[] regionsoffered = parts.Skip(2).Select(p => int.Parse(p)).ToArray();
                    int[] fav = Map.GetFavorites().Intersect(regionsoffered).ToArray();
                    Console.WriteLine(string.Join(" ", fav.Take(6).Select(x => x.ToString()).ToArray()));
                    break;
            
              
                // setup_map
                case "setup_map":
                    switch (parts[1].ToLowerInvariant())
                    {
                        case "super_regions":
                            for (int i = 2; i < parts.Length; i++)
                            {
                                try
                                {
                                    int superRegionId = int.Parse(parts[i]);
                                    i++;
                                    int reward = int.Parse(parts[i]);
                                    Map.GetInstance().AddSuperRegion(new SuperRegion(superRegionId, reward));
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("ERROR: Unable to parse SuperRegions");
                                    Console.WriteLine("Msg: " + e.Message);
                                }
                            }
                            break;

                        case "regions":
                            for (int i = 2; i < parts.Length; i++)
                            {
                                try
                                {
                                    int regionId = int.Parse(parts[i]);
                                    i++;
                                    int superRegionId = int.Parse(parts[i]);
                                    SuperRegion superRegion = Map.GetInstance().GetSuperRegion(superRegionId);
                                    Map.GetInstance().AddRegion(new Region(regionId, superRegion));
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("ERROR: Unable to parse Regions");
                                    Console.WriteLine("Msg: " + e.Message);
                                }
                            }
                            break;

                        case "neighbours":
                            for (int i = 2; i < parts.Length; i++)
                            {
                                try
                                {
                                    Region region = Map.GetInstance().GetRegion(int.Parse(parts[i]));
                                    i++;
                                    foreach (String neighbourStr in parts[i].Split(','))
                                    {
                                        Region neighbour = Map.GetInstance().GetRegion(int.Parse(neighbourStr));
                                        region.AddNeighbour(neighbour);
                                        neighbour.AddNeighbour(region);
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("ERROR: Unable to parse Regions");
                                    Console.WriteLine("Msg: " + e.Message);
                                }
                            }
                            break;

                        default:
                            // EXCEPTION
                            break;
                    }
                    break;
                
                default:
                    // EXCEPTION
                    break;

            }
        } // Parse
        
    } // class
} // namespace
