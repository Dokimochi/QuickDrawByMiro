using System;
using System.Diagnostics;

namespace QuickDrawGame
{
    class Program
    {
        const string Menu = @"Quick Draw
Face your opponent and wait for the signal. Once the
signal is given, shoot your opponent by pressing [space]
before they shoot you. It's all about your reaction time.
Choose Your Opponent:
[1] Easy....1000 milliseconds
[2] Medium...500 milliseconds
[3] Hard.....250 milliseconds
[4] Harder...125 milliseconds";

        const string Wait = @"

        _O                              O_
       |/|_             wait           _|\|
       /\                                /\
      / |                                | \   
------------------------------------------------------";

        const string Fire = @"

                    ********
                    * FIRE *
        _O          ********                   O_
       |/|_                                   _|\|
       /\           spacebar                    /\
      / |                                       | \
------------------------------------------------------";

        const string LoseTooSlow = @"

                                            > ╗__O
      //             Too Slow                    / \
     O/__/\          You Lose                   /\
          \                                    | \
------------------------------------------------------";

        const string LoseTooFast = @"

                    Too Fast                > ╗__O
      //           You Missed                    / \
     O/__/\         You Lose                    /\
          \                                    | \
------------------------------------------------------";

        const string Win = @"

       O__╔ <
      / \                                   \\
        /\          You Win              /\__\O
        / |                              /
------------------------------------------------------";

        static void Main(string[] args)
        {
            Random random = new Random();
            bool playAgain = true;

            while (playAgain)
            {
                Console.Clear();
                Console.WriteLine(Menu);

                Console.Write("Enter the difficulty level (1-4): ");

                int difficulty = int.Parse(Console.ReadLine());

                while (difficulty < 1 || difficulty > 4)
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                    Console.Write("Enter the difficulty level (1-4): ");
                    difficulty = int.Parse(Console.ReadLine());
                }

                TimeSpan requiredReactionTime;
                switch (difficulty)
                {
                    case 1:
                        requiredReactionTime = TimeSpan.FromMilliseconds(1000);
                        break;
                    case 2:
                        requiredReactionTime = TimeSpan.FromMilliseconds(500);
                        break;
                    case 3:
                        requiredReactionTime = TimeSpan.FromMilliseconds(250);
                        break;
                    case 4:
                        requiredReactionTime = TimeSpan.FromMilliseconds(125);
                        break;
                    default:
                        continue;
                }

                Console.Clear();
                Console.WriteLine(Wait);

                TimeSpan signal = TimeSpan.FromMilliseconds(random.Next(5000, 10000));

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Restart();

                bool isTooFast = false;

                while (stopwatch.Elapsed < signal && !isTooFast)
                {
                    if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Spacebar)
                    {
                        isTooFast = true;
                    }
                }

                Console.Clear();

                if (isTooFast)
                {
                    Console.WriteLine(LoseTooFast);
                }
                else
                {
                    Console.WriteLine(Fire);
                    stopwatch.Restart();
                    bool isTooSlow = true;
                    TimeSpan reactionTime = default;

                    while (stopwatch.Elapsed < requiredReactionTime && isTooSlow)
                    {
                        if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Spacebar)
                        {
                            isTooSlow = false;
                            reactionTime = stopwatch.Elapsed;
                        }
                    }

                    Console.Clear();

                    if (isTooSlow)
                    {
                        Console.WriteLine(LoseTooSlow);
                    }
                    else
                    {
                        Console.WriteLine(Win);
                        Console.WriteLine("Reaction Time: " + reactionTime.TotalMilliseconds + " milliseconds");
                    }
                }

                Console.WriteLine("Press [1] to Play Again or [2] to quit: ");
                string playAgainInput = Console.ReadLine();

                while (playAgainInput != "1" && playAgainInput != "2")
                {
                    Console.WriteLine("Invalid input. Please enter '1' or '2'.");
                    playAgainInput = Console.ReadLine();
                }

                if (playAgainInput == "2")
                {
                    Console.WriteLine("Quick Draw was closed.");
                    playAgain = false;
                }
            }
        }
    }
}
