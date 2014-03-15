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
                // BEGIN TEST
                FiniteStateMachine FSM = new FiniteStateMachine();
                FiniteStateMachine.BattleOutcome[] test = FSM.CalculateBattleOutcome(2, 2);

                for (int i = 0; i <= 2; i++)
                {
                    for (int j = 0; j <= 2; j++)
                    {
                        Console.Write(test[i*3+j] + "  ");
                    }
                    Console.WriteLine();
                }
                Console.ReadLine();
               


                // EINDE TEST
                String line = Console.ReadLine().Trim();
                Regex.Replace(line, "\\s+", " "); // replace multiple spaces for 1 space

                //Let the parser deal with it
                parser.Parse(line);
            }
        }
    }
}
