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

        // Map, SuperRegions, Regions
        private Map myMap;
        
        public BotState()
        {
            myMap = new Map();
        }
                
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
                            myMap.AddSuperRegion(new SuperRegion(superRegionId, reward));
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
                            SuperRegion superRegion = myMap.getSuperRegion(superRegionId);
                            myMap.AddRegion(new Region(regionId, superRegion));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("ERROR: Unable to parse Regions");
                            Console.WriteLine("Msg: "+e.Message);
                        }
                    }
                    break;

                case "neighbours":
                    for (int i = 2; i < parts.Length; i++)
                    {
                        try
                        {
                            Region region = myMap.getRegion(int.Parse(parts[i]));
                            i++;
                            foreach (String neighbourStr in parts[i].Split(','))
                            {
                                Region neighbour = myMap.getRegion(int.Parse(neighbourStr));
                                region.AddNeighbour(neighbour);
                                neighbour.AddNeighbour(region);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("ERROR: Unable to parse Regions");
                            Console.WriteLine("Msg: "+e.Message);
                        }
                    }
                    break;

                default:
                    // EXCEPTION
                    break;
            }
        }



    } // class
} // nameSpace
