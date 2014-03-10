using System;

namespace TweakBot
{
    enum Players : int { Unknown, Neutral, Me, Other }

    class Player
    {
        static String[] playerName = new String[4];

        static Player()
        {
            playerName[0] = "Unknown";
            playerName[1] = "neutral";
            playerName[2] = "me";
            playerName[3] = "other";
        }
    
        static public void SetMyName(String myName)
        {
            playerName[2] = myName;
        }
        
        static public void SetOtherName(String otherName)
        {
            playerName[3] = otherName;
        }

        static public int Name(String name)
        {
            if (playerName[2] == name) return 2;
            if (playerName[3] == name) return 3;
            if (playerName[1] == name) return 1;
            return 0;
        }

        static public String Name(int name)
        {
            return playerName[name];
        }
    }
}
