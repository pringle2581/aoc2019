using System.Text.RegularExpressions;
namespace aoc2019.day3
{
    public class Day3
    {
        public static string[] Solve(string[] input)
        {
            List<int> part1 = [], part2 = [];
            var wire1 = FindPath(input[0].Split(","));
            var wire2 = FindPath(input[1].Split(","));
            var intersections = wire1.Intersect(wire2);
            foreach (var result in intersections)
            {
                part1.Add(result.Item1 + result.Item2);
                part2.Add(wire1.IndexOf(result) + wire2.IndexOf(result) + 2);
            }
            return [part1.Min().ToString(), part2.Min().ToString()];
        }
        static List<(int, int)> FindPath(string[] instructions)
        {
            var path = new List<(int, int)> ();
            int x = 0, y = 0;
            foreach (string instr in instructions)
            {
                Regex regex = new(@"([A-Z])(\d+)");
                Match result = regex.Match(instr);
                string dir = result.Groups[1].Value;
                int steps = int.Parse(result.Groups[2].Value);
                foreach (int step in Enumerable.Range(1, steps))
                {
                    switch (dir)
                    {
                        case "U":
                            y += 1;
                            break;
                        case "D":
                            y -= 1;
                            break;
                        case "L":
                            x -= 1;
                            break;
                        case "R":
                            x += 1;
                            break;
                    }
                    path.Add((x, y));
                }
            }
            return path;
        }
    }
}