using System;
using System.Text;
using System.Text.RegularExpressions;

namespace TweakBot
{
    class TweakBot
    {
        BotParser parser;
        
        public TweakBot()
        {
            parser = new BotParser();
        }

        public void Run()
        {
            while (true)
            {
                String line = Console.ReadLine().Trim();
                Regex.Replace(line, "\\s+", " "); // replace multiple spaces for 1 space

                //Let the parser deal with it
                parser.Parse(line);
            }
        }
    }
}
