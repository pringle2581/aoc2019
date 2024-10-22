using aoc2019.intcode;
namespace aoc2019.day5
{
    public class Day5
    {
        static public string[] Solve(string[] input)
        {
            long[] program = Array.ConvertAll(input[0].Split(","), long.Parse);
            Intcode part1computer = new(program);
            part1computer.Input(1);
            part1computer.Compute();
            long part1 = part1computer.Output().Last();
            Intcode part2computer = new(program);
            part2computer.Input(5);
            part2computer.Compute();
            long part2 = part2computer.Output().Last();
            return [part1.ToString(), part2.ToString()];
        }
    }
}