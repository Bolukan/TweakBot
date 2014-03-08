using System;
using System.Collections.Generic;

namespace TweakBot
{
    /// <summary>
    /// Contains data of the world
    /// </summary>
    
    class BotState
    {
        // settings your_bot -b : The name of your bot is given.
        private String myName;

        // settings opponent_bot -b : The name of your opponent bot is given.
        private String opponentName;

        private List<Region> regions;
        private List<SuperRegion> superRegions;

        public void setup_map(String[] parts)
        {
            switch (parts[1])
            {
                
                case "super_regions":
                    for (int i = 2; i < parts.Length; i++)
                    {
                        try
                        {
                            int superRegionId = int.Parse(parts[i]);
                            i++;
                            int reward = int.Parse(parts[i]);
                            superRegions.Add(new SuperRegion(superRegionId, reward));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("ERROR: Unable to parse SuperRegions");
                            Console.WriteLine("Msg: "+e.Message);
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
                            SuperRegion sR = superRegions[superRegionId-1];
                            regions.Add(new Region(regionId, sR));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("ERROR: Unable to parse Regions");
                            Console.WriteLine("Msg: "+e.Message);
                        }
                    }
                    break;
                case "neighbours":
                    // TODO: read and process neighbours
                    break;
                default:
                    // EXCEPTION
                    break;
            }
        }

        public void settings(String[] parts)
        {
            switch (parts[1])
            {
                case "your_bot":
                    // TODO: read and process super_regions
                    break;
                case "opponent_bot":
                    // TODO: read and process regions
                    break;
                case "starting_armies":
                    // TODO: read and process neighbours
                    break;
                default:
                    // EXCEPTION
                    break;
            }
        }

    } // class
} // nameSpace
