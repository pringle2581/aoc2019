using aoc2019.intcode;
namespace aoc2019.day2
{
    public class Day2
    {
        public static string[] Solve(string[] input)
        {
            long[] program = Array.ConvertAll(input[0].Split(","), long.Parse);
            Intcode part1comp = new(program);
            part1comp.NounVerb(12, 2);
            part1comp.Compute();
            int part1 = part1comp.CheckMem(0);
            int part2 = 0;
            foreach (int noun in Enumerable.Range(0, 100))
            {
                foreach (int verb in Enumerable.Range(0, 100))
                {
                    Intcode part2comp = new(program);
                    part2comp.NounVerb(noun, verb);
                    part2comp.Compute();
                    if (part2comp.CheckMem(0) == 19690720)
                    {
                        part2 = 100 * noun + verb;
                    }
                }
            }
            return [part1.ToString(), part2.ToString()];
        }
    }
}