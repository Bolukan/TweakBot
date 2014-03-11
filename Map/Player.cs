using System;

namespace TweakBot
{
    enum Players : int { Unknown, Neutral, Me, Other }

    class Player
    {
        #region static

        static Player[] players;

        static Player()
        {
            players = new Player[4];
            players[0] = new Player(0, "unknown");
            players[1] = new Player(1, "neutral");
            players[2] = new Player(2, "me");
            players[3] = new Player(3, "other");
        }

        public static void SetMyName(String myName)
        {
            players[2].Name = myName;
        }

        public static void SetOtherName(String otherName)
        {
            players[3].Name = otherName;
        }

        public static Player GetPlayer(String name)
        {
            if (players[2].Name == name) return players[2];
            if (players[3].Name == name) return players[3];
            if (players[1].Name == name) return players[1];
            return players[0];
        }

        public static Player GetPlayer(int id)
        {
            if ((id > 0) && (id < 4)) return players[id];
            return players[0];
        }

        public static Player Me()
        {
            return players[2];
        }

        public static Player Other()
        {
            return players[3];
        }

        public static Player Neutral()
        {
            return players[1];
        }

        public static Player Unknown()
        {
            return players[0];
        }

        #endregion

        int id;
        String name;

        public Player(int id, String name)
        {
            this.id = id;
            this.name = name;
        }

        public int Id
        {
            get { return id; }
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
