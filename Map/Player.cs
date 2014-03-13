using System;

namespace TweakBot
{
    class PLAYER
    {
        public const int UNKNOWN = 1;
        public const int NEUTRAL = 2;
        public const int ME = 4;
        public const int OTHER = 8;
        public const int NOT_ME = 11;
        public const int ALL = 15;

        private const String NAME_NEUTRAL = "neutral";
        private const String NAME_UNKNOWN = "unknown";

        static String nameMe;
        static String nameOther;
        
        static PLAYER()
        {
            // Engine will assign player1|player2 or player2|player1
            nameMe = "me";
            nameOther = "other";
        }

        public static String NameMe
        {
            get { return nameMe; }
            set { nameMe = value; }
        }

        public static String NameOther
        {
            get { return nameOther; }
            set { nameOther = value; }
        }

        public static int GetPlayer(String name)
        {
            if (name == nameMe) return PLAYER.ME;
            if (name == nameOther) return PLAYER.OTHER;
            if (name == NAME_NEUTRAL) return PLAYER.NEUTRAL;
            return PLAYER.UNKNOWN;
        }

        public static String GetPlayer(int id)
        {
            if (id == PLAYER.ME) return nameMe;
            if (id == PLAYER.OTHER) return nameOther;
            if (id == PLAYER.NEUTRAL) return NAME_NEUTRAL;
            return NAME_UNKNOWN;
        }

    }
}
