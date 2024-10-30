using aoc2019.intcode;
namespace aoc2019.day17
{
    public class Day17
    {
        static public string[] Solve(string[] input)
        {
            long[] program = Array.ConvertAll(input[0].Split(","), long.Parse);
            Intcode comp = new(program);
            comp.Compute();
            //PrintScreen(comp.Output());
            (HashSet<(int, int)> scaffolding, (int, int) start, char facing) = GetScaffolding(comp.Output());
            int part1 = GetAlignmentParameters(scaffolding);
            comp.WriteMem(0, 2);
            string path = Pathfind(scaffolding, start, facing);
            string[] lines = ["A,B,B,C,B,C,B,C,A,A\n","L,6,R,8,L,4,R,8,L,12\n","L,12,R,10,L,4\n","L,12,L,6,L,4,L,4\n","n\n"];
            foreach (string line in lines)
            {
                foreach (char character in line)
                {
                    comp.Input(character - 0);
                }
            }
            comp.Compute();
            return [part1.ToString(), comp.Output().Last().ToString()];
        }

        static string Pathfind(HashSet<(int, int)> scaffolding, (int, int) start, char facing)
        {
            bool done = false;
            (int, int) pos = start;
            string path = "";
            int traveled = 0;
            (int, int) next, left, right;
            while (!done) {
                GetSurroundings();
                if ((scaffolding.Contains(next)))
                {
                    pos = next;
                    traveled++;
                }
                else
                {
                    if (traveled > 0)
                    {
                        path += traveled;
                        path += ',';
                    }
                    traveled = 0;
                    Turn();
                }
            }
            return path;

            void Turn() {
                if (scaffolding.Contains(left)) {
                    path += "L";
                    facing = facing switch
                    {
                        '^' => '<',
                        '<' => 'v',
                        'v' => '>',
                        '>' => '^',
                        _ => 'Z'
                    };
                    path += ',';
                }
                else if (scaffolding.Contains(right))
                {
                    path += "R";
                    facing = facing switch
                    {
                        '^' => '>',
                        '>' => 'v',
                        'v' => '<',
                        '<' => '^',
                        _ => 'Z'
                    };
                    path += ',';
                }
                else
                {
                    done = true;
                }
            }

            void GetSurroundings()
            {
                switch (facing)
                {
                    case '^':
                        next = (pos.Item1, pos.Item2 - 1);
                        left = (pos.Item1 - 1, pos.Item2);
                        right = (pos.Item1 + 1, pos.Item2);
                        break;
                    case 'v':
                        next = (pos.Item1, pos.Item2 + 1);
                        left = (pos.Item1 + 1, pos.Item2);
                        right = (pos.Item1 - 1, pos.Item2);
                        break;
                    case '<':
                        next = (pos.Item1 - 1, pos.Item2);
                        left = (pos.Item1, pos.Item2 + 1);
                        right = (pos.Item1, pos.Item2 - 1);
                        break;
                    case '>':
                        next = (pos.Item1 + 1, pos.Item2);
                        left = (pos.Item1, pos.Item2 - 1);
                        right = (pos.Item1, pos.Item2 + 1);
                        break;
                    default:
                        next = (0, 0);
                        left = (0, 0);
                        right = (0, 0);
                        break;
                };
            }
        }

        static int GetAlignmentParameters(HashSet<(int, int)> scaffolding)
        {
            int sum = 0;
            foreach ((int, int) tile in scaffolding)
            {
                if (scaffolding.Contains((tile.Item1 - 1, tile.Item2)) &
                    scaffolding.Contains((tile.Item1 + 1, tile.Item2)) &
                    scaffolding.Contains((tile.Item1, tile.Item2 + 1)) &
                    scaffolding.Contains((tile.Item1, tile.Item2 - 1)))
                {
                    sum += (tile.Item1 * tile.Item2);
                }
            }
            return sum;
        }

        static (HashSet<(int,int)>, (int, int), char) GetScaffolding(List<long> ascii)
        {
            HashSet<(int, int)> list = [];
            (int, int) start = (0, 0);
            char facing = 'x';
            int x = 0, y = 0;
            foreach (long character in ascii)
            {
                if (character == 10)
                {
                    y++;
                    x = 0;
                }
                else
                {
                    if (character != 46)
                    {
                        list.Add((x, y));
                        if (character != 35)
                        {
                            start = (x, y);
                            facing = (char)character;
                        }
                    }
                    x++;
                }
            }
            return (list, start, facing);
        }

        static void PrintScreen(List<long> ascii)
        {
            string output = "";
            foreach (long character in ascii)
            {
                output += (char)character;
            }
            Console.WriteLine(output);
        }
    }
}