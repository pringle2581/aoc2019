namespace aoc2019.day1 {
    public class Day1
    {
        static public string[] Solve(string[] input)
        {
            int part1 = 0, part2 = 0;
            int[] modules = Array.ConvertAll(input, int.Parse);
            foreach (int module in modules)
            {
                int fuel = Fuel(module);
                part1 += fuel;
                while (fuel > 0)
                {
                    part2 += fuel;
                    fuel = Fuel(fuel);
                }
            }
            return [part1.ToString(), part2.ToString()];
        }
        static int Fuel(int mass)
        {
            return mass / 3 - 2;
        }
    }
}