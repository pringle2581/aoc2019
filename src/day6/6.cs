namespace aoc2019.day6
{
    public class Day6
    {
        static public int[] Solve(string[] input)
        {
            Dictionary<string, string> orbits = [];
            foreach (string line in input)
            {
                string[] split = line.Split(")");
                orbits.Add(split[1], split[0]);
            }

            int part1 = 0, part2 = 0;
            foreach (var orbit in orbits)
            {
                part1 += CountOrbits(orbit.Value, 0);
            }
            List<string> youparents = [], sanparents = [];
            GetParents("YOU", youparents);
            GetParents("SAN", sanparents);
            part2 += youparents.Except(sanparents).Count();
            part2 += sanparents.Except(youparents).Count();
            return [part1, part2];

            int CountOrbits(string node, int sum)
            {
                if (node != "COM")
                {
                    sum = CountOrbits(orbits[node], sum);
                }
                return sum + 1;
            }

            void GetParents(string node, List<string> parents)
            {
                if (node != "COM")
                {
                    parents.Add(orbits[node]);
                    GetParents(orbits[node], parents);
                }
            }
        }
    }
}