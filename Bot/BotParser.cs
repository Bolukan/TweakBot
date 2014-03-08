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
                case "settings":
                    switch (parts[1].ToLowerInvariant())
                    {
                        case "starting_armies":
                            // TODO
                            break;
                        case "your_bot":
                            Map.GetInstance().SetMyName(parts[2]);
                            break;
                        case "opponent_bot":
                            Map.GetInstance().SetOpponentName(parts[2]);
                            break;
                        default:
                            // EXCEPTION
                            break;
                    }
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
                                    SuperRegion superRegion = Map.GetInstance().getSuperRegion(superRegionId);
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
                                    Region region = Map.GetInstance().getRegion(int.Parse(parts[i]));
                                    i++;
                                    foreach (String neighbourStr in parts[i].Split(','))
                                    {
                                        Region neighbour = Map.GetInstance().getRegion(int.Parse(neighbourStr));
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
