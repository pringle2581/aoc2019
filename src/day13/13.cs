using aoc2019.intcode;
namespace aoc2019.day13
{
    public class Day13
    {
        static public string[] Solve(string[] input)
        {
            long[] program = Array.ConvertAll(input[0].Split(","), long.Parse);
            program[0] = 2;
            BlockGame comp = new(program)
            {
                interactive = false
            };
            comp.Step();
            int part1 = comp.CountBlocks();
            comp.Play();
            long part2 = comp.score;
            return [part1.ToString(), part2.ToString()];
        }

        class BlockGame(long[] program) : Intcode(program)
        {
            public long score = 0;
            public bool interactive;
            bool finish = false;
            Dictionary<(long, long), long> pixels = [];

            public void Play()
            {
                while (!finish)
                {
                    while (!CheckExit())
                    {
                        Step();
                    }
                    if (interactive)
                    {
                        GameOver();
                    }
                    else
                    {
                        finish = true;
                    }
                }
            }

            void GameOver()
            {
                Console.WriteLine("Press UP ARROW to play again, press ESCAPE to exit");
                ConsoleKey response = Console.ReadKey().Key;
                if (response == ConsoleKey.UpArrow)
                {
                    Reset();
                }
                else if (response == ConsoleKey.Escape)
                {
                    finish = true;
                }
            }

            int Cheat()
            {
                long paddle = 0;
                long ball = 0;
                foreach (var pixel in pixels)
                {
                    if (pixel.Value == 3)
                    {
                        paddle = pixel.Key.Item1;
                    }
                    else if (pixel.Value == 4)
                    {
                        ball = pixel.Key.Item1;
                    }
                }
                return paddle > ball ? -1 : paddle < ball ? 1 : 0;
            }

            int GetInput()
            {
                var key = Console.ReadKey().Key;
                int dir = key switch
                {
                    ConsoleKey.LeftArrow => -1,
                    ConsoleKey.RightArrow => 1,
                    ConsoleKey.Z => Cheat(),
                    _ => 0
                };
                return dir;
            }

            public int CountBlocks()
            {
                int blocks = 0;
                foreach ((long, long) pixel in pixels.Keys)
                {
                    if (pixels[pixel] == 2)
                    {
                        blocks++;
                    }
                }
                return blocks;
            }

            public void Step()
            {
                Compute();
                GetPixels();
                if (interactive)
                {
                    PrintScreen();
                    Input(GetInput());
                }
                else
                {
                    Input(Cheat());
                }
            }

            void GetPixels()
            {
                for (int i = 0; i < Output().Count; i += 3)
                {
                    if (Output()[i] == -1)
                    {
                        score = Output()[i + 2];
                    }
                    else
                    {
                        pixels[(Output()[i], Output()[i + 1])] = Output()[i + 2];
                    }
                }
                ClearOutput();
            }

            void PrintScreen()
            {
                Console.Clear();
                string screen = "\n\n";
                for (int y = 0; y <= 20; y++)
                {
                    for (int x = 0; x <= 37; x++)
                    {
                        screen += RenderSymbol(pixels[(x, y)]);
                    }
                    screen += "\n";
                }
                screen += $"Score: {score}";
                Console.WriteLine(screen);
            }

            static string RenderSymbol(long digit)
            {
                return digit switch
                {
                    1 => "▒",
                    2 => "▄",
                    3 => "─",
                    4 => "O",
                    _ => " ",
                };
            }
        }
    }
}