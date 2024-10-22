namespace aoc2019.day4
{
    public class Day4
    {
        static public string[] Solve(string[] input)
        {
            int[] range = Array.ConvertAll(input[0].Split("-"), int.Parse);
            int part1 = 0, part2 = 0;
            for (int candidate = range[0]; candidate <= range[1]; candidate++)
            {
                var result = Test(candidate.ToString());
                part1 += result.Item1;
                part2 += result.Item2;
            }
            return [part1.ToString(), part2.ToString()];
        }
        static (int,int) Test(string candidate)
        {
            char[] digits = candidate.ToCharArray();
            Array.Sort(digits);
            if (candidate == new string(digits))
            {
                if (digits.Distinct().Count() < 6)
                {
                    foreach (char distinct in digits.Distinct())
                    {
                        if (digits.Count(x => x == distinct) == 2)
                        {
                            return (1, 1);
                        }
                    }
                    return (1, 0);
                }
            }
            return (0, 0);
        }
    }
}