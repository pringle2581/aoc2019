using aoc2019.intcode;
namespace aoc2019.day15
{
    public class Day15
    {
        static public string[] Solve(string[] input)
        {
            long[] program = Array.ConvertAll(input[0].Split(","), long.Parse);
            RepairDroid droid = new(program, false);
            droid.Operate();
            return [droid.part1.ToString(), droid.part2.ToString()];
        }

        class RepairDroid(long[] program, bool interactive) : Intcode(program)
        {
            readonly Dictionary<(int, int), long> tiles = [];
            (int, int) pos = (0, 0);
            (int, int) o2 = (99, 99);
            bool done;
            public int part1, part2;

            public void Operate()
            {
                tiles.Add(pos, 1);
                if (interactive)
                {
                    while (!done)
                    {
                        GetSurroundings();
                        PrintScreen([]);
                        UserControl();
                    }
                }
                ExploreMaze();
                var path = Pathfind();
                part1 = path.Count;
                if (interactive)
                {
                    pos = (0, 0);
                    path.Remove(path.First());
                    PrintScreen(path);
                }
                part2 = SearchMaze(o2).Item2;
            }

            List<(int, int)> Pathfind()
            {
                Dictionary<(int, int), (int, int)> history = SearchMaze((0, 0)).Item1;
                List<(int, int)> path = [];
                var current = o2;
                while (current != (0, 0))
                {
                    path.Add(current);
                    current = history[current];
                }
                return path;
            }

            (Dictionary<(int, int), (int, int)>, int) SearchMaze((int, int) start)
            {
                int counter = -1;
                HashSet<(int, int)> frontier = [];
                Dictionary<(int, int), (int, int)> history = [];
                frontier.Add(start);
                while (frontier.Count > 0)
                {
                    foreach (var current in frontier.ToList())
                    {
                        foreach (var neighbor in ListNeighbors(current))
                        {
                            if (!history.ContainsKey(neighbor))
                            {
                                frontier.Add(neighbor);
                                history[neighbor] = current;
                            }
                        }
                        frontier.Remove(current);
                    }
                    counter++;
                }
                return (history, counter);

                List<(int, int)> ListNeighbors((int, int) tile)
                {
                    List<(int, int)> list = [];
                    (int, int)[] neighbors = [
                        (tile.Item1-1, tile.Item2),
                        (tile.Item1+1, tile.Item2),
                        (tile.Item1, tile.Item2+1),
                        (tile.Item1, tile.Item2-1)
                    ];
                    foreach (var neighbor in neighbors)
                    {
                        if (tiles[neighbor] == 1 || tiles[neighbor] == 2)
                        {
                            list.Add(neighbor);
                        }
                    }
                    return list;
                }
            }

            void ExploreMaze()
            {
                int facing = 14;
                bool explored = false;
                while (!explored) {
                    GetSurroundings();
                    if (tiles.Count == 1658)
                    {
                        explored = true;
                    }
                    (int, int) right = (0, 0), front = (0, 0);
                    switch (facing)
                    {
                        case 11:
                            right = (pos.Item1 + 1, pos.Item2);
                            front = (pos.Item1, pos.Item2 - 1);
                            break;
                        case 12:
                            right = (pos.Item1, pos.Item2 + 1);
                            front = (pos.Item1 + 1, pos.Item2);
                            break;
                        case 13:
                            right = (pos.Item1 - 1, pos.Item2);
                            front = (pos.Item1, pos.Item2 + 1);
                            break;
                        case 14:
                            right = (pos.Item1, pos.Item2 - 1);
                            front = (pos.Item1 - 1, pos.Item2);
                            break;
                    }
                    if (tiles[right] == 0)
                    {
                        if (tiles[front] == 0)
                        {
                            Turn(-1);
                        }
                        else
                        {
                            MoveFacing(facing);
                        }
                    }
                    else
                    {
                        Turn(1);
                        MoveFacing(facing);
                    }
                }

                void Turn(int turn)
                {
                    facing += turn;
                    if (facing == 10) { facing = 14; }
                    else if (facing == 15) { facing = 11; }
                }

                void MoveFacing(int facing)
                {
                    int move = facing switch
                    {
                        11 => 1,
                        12 => 4,
                        13 => 2,
                        14 => 3,
                    };
                    Move(move);
                }
            }

            void GetSurroundings()
            {
                var init = pos;
                for (int dir = 1; dir <= 4; dir++)
                {
                    Move(dir);
                    if (pos != init)
                    {
                        int reverse = dir switch
                        {
                            1 => 2,
                            2 => 1,
                            3 => 4,
                            4 => 3,
                        };
                        Move(reverse);
                    }
                }
            }

            void Move(int dir)
            {
                Input(dir);
                Compute();
                ProcessOutput(dir);
            }

            void ProcessOutput(int dir)
            {
                (int, int) newpos = pos;
                switch (dir)
                {
                    case 1:
                        newpos.Item2--; break;
                    case 2:
                        newpos.Item2++; break;
                    case 3:
                        newpos.Item1--; break;
                    case 4:
                        newpos.Item1++; break;
                }
                long output = Output().Last();
                tiles[newpos] = output;
                if (output != 0)
                {
                    pos = newpos;
                    if (output == 2)
                    {
                        o2 = pos;
                    }
                }
                ClearOutput();
            }

            void UserControl()
            {
                int dir = 0;
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.Escape)
                {
                    done = true;
                }
                else
                {
                    dir = key switch
                    {
                        ConsoleKey.W => 1,
                        ConsoleKey.S => 2,
                        ConsoleKey.A => 3,
                        ConsoleKey.D => 4,
                        _ => 0
                    };
                }
                if (dir != 0)
                {
                    Move(dir);
                    if (pos == o2)
                    {
                        done = true;
                    }
                }
            }

            void PrintScreen(List<(int, int)> path)
            {
                Console.Clear();
                string screen = $"Repair Droid Control Interface - Current Position: {pos}\n";
                for (int y = -21; y <= 19; y++)
                {
                    for (int x = -21; x <= 19; x++)
                    {
                        long tile = 99;
                        if ((x, y) == pos)
                        {
                            tile = 3;
                        }
                        else if (path.Contains((x,y)))
                        {
                            tile = 4;
                        }
                        else if (tiles.ContainsKey((x, y)))
                        {
                            tile = tiles[(x, y)];
                        }
                        screen += RenderSymbol(tile);
                    }
                    screen += "\n";
                }
                Console.WriteLine(screen);

                static string RenderSymbol(long digit)
                {
                    // https://stackoverflow.com/a/74807043
                    string NORMAL = Console.IsOutputRedirected ? "" : "\x1b[39m";
                    string RED = Console.IsOutputRedirected ? "" : "\x1b[91m";
                    string YELLOW = Console.IsOutputRedirected ? "" : "\x1b[93m";
                    string BLUE = Console.IsOutputRedirected ? "" : "\x1b[94m";
                    return digit switch
                    {
                        0 => $"{BLUE}▒▒{NORMAL}", // wall
                        1 => $"▒▒", // hallway
                        2 => $"{RED}██{NORMAL}",  // oxygen system
                        3 => $"{YELLOW}RD{NORMAL}", // our humble repair droid
                        4 => $"{RED}▒▒{NORMAL}", // path from start to oxygen system
                        _ => $"  ", // unexplored
                    };
                }
            }
        }
    }
}