using aoc2019.intcode;
namespace aoc2019.day9
{
    public class Day9
    {
        static public string[] Solve(string[] input)
        {
            long[] program = Array.ConvertAll(input[0].Split(","), long.Parse);
            Intcode pt1comp = new(program);
            pt1comp.Input(1);
            pt1comp.Compute();
            long part1 = pt1comp.Output().Last();
            Intcode pt2comp = new(program);
            pt2comp.Input(2);
            pt2comp.Compute();
            long part2 = pt2comp.Output().Last();
            return [part1.ToString(), part2.ToString()];
        }
    }
}