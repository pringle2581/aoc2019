using aoc2019.intcode;
namespace aoc2019.day2
{
    public class Day2
    {
        public static int[] Solve(string[] input)
        {
            int[] data = Array.ConvertAll(input[0].Split(","), int.Parse);
            int part1 = Intcode.Compute(data, 12, 2);
            int part2 = 0;
            foreach (int noun in Enumerable.Range(0, 100))
            {
                foreach (int verb in Enumerable.Range(0, 100))
                {
                    if (Intcode.Compute(data, noun, verb) == 19690720)
                    {
                        part2 = 100 * noun + verb;
                    }
                }
            }
            return [part1, part2];
        }
    }
}