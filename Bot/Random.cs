using System;

namespace TweakBot
{
    public static class Rand
    {
        private static readonly Random random = new Random();
        
        public static Random Rnd()
        {
            return random;
        }

    }
}
