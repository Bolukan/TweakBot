using System;

namespace TweakBot
{
    class Program
    {
        static void test()
        {
            BattleOutcomes[] bo = new BattleOutcomes[20];
            for (int d = 1; d <= 7; d++)
            {
                for (int a = 2; a <= 19; a++)
                {
                    bo[a] = new BattleOutcomes(a, d);
                    Console.WriteLine(bo[a].ToString());
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            new TweakBot().Run();
        }
    }
}
