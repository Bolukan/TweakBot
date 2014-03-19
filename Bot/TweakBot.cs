using System;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace TweakBot
{
    class TweakBot
    {
        BotParser parser;
        
        public TweakBot()
        {
            parser = new BotParser();
        }

        private void Test()
        {
            String[] lines = File.ReadAllLines(@"C:\Users\Blok12\Documents\GitHub\test.dump", Encoding.UTF8);
            foreach (String line in lines) parser.Parse(line);
        }

        public void Run()
        {
            while (true)
            {
                // Test();
                
                String line = Console.ReadLine().Trim();
                Regex.Replace(line, "\\s+", " "); // replace multiple spaces for 1 space

                //Let the parser deal with it
                parser.Parse(line);
            }
        }
    }
}
