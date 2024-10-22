using aoc2019.intcode;
namespace aoc2019.day11
{
    public class Day11
    {
        static public string[] Solve(string[] input)
        {
            Dictionary<(int, int), int> pt1panels = [], pt2panels = [];
            long[] program = Array.ConvertAll(input[0].Split(","), long.Parse);
            int part1 = DeployRobot(pt1panels);
            pt2panels.Add((0, 0), 1);
            DeployRobot(pt2panels);
            string part2 = PrintPanels(pt2panels);
            return [part1.ToString(), part2];

            int DeployRobot(Dictionary<(int,int),int> panels)
            {
                (int, int) pos = (0, 0);
                int dir = 0;
                HashSet<(int, int)> painted = [];
                Intcode comp = new(program);
                while (!comp.CheckExit())
                {
                    if (!panels.TryGetValue(pos, out int color))
                    {
                        color = 0;
                    }
                    comp.Input(color);
                    comp.Compute();
                    int newcolor = (int)comp.Output()[^2];
                    int turn = (int)comp.Output()[^1];
                    if (newcolor != color)
                    {
                        painted.Add(pos);
                        panels[pos] = newcolor;
                    }
                    dir = Turn(dir, turn);
                    pos = Move(pos, dir);
                }
                return painted.Count;
            }

            int Turn(int dir, int turn)
            {
                dir += (turn == 0) ? -1 : 1;
                if (dir < 0)
                {
                    dir = 3;
                }
                else if (dir > 3)
                {
                    dir = 0;
                }
                return dir;
            }

            (int, int) Move((int, int) pos, int dir)
            {
                switch (dir)
                {
                    case 0:
                        pos.Item2 -= 1;
                        break;
                    case 1:
                        pos.Item1 += 1;
                        break;
                    case 2:
                        pos.Item2 += 1;
                        break;
                    case 3:
                        pos.Item1 -= 1;
                        break;
                }
                return pos;
            }

            string PrintPanels(Dictionary<(int, int), int> panels)
            {
                string print = "\n\n";
                int left = 0, right = 0, up = 0, down = 0;
                foreach (var panel in panels.Keys)
                {
                    if (panel.Item1 < left)
                    {
                        left = panel.Item1;
                    }
                    if (panel.Item1 > right)
                    {
                        right = panel.Item1;
                    }
                    if (panel.Item2 < up)
                    {
                        up = panel.Item2;
                    }
                    if (panel.Item2 > down)
                    {
                        down = panel.Item2;
                    }
                }
                for (int y = up; y <= down; y++)
                {
                    for (int x = left; x <= right; x++)
                    {
                        panels.TryGetValue((x, y), out int color);
                        print += (color == 1) ? "##" : "  ";
                    }
                    print += "\n";
                }
                return print;
            }
        }
    }
}