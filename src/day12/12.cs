using System.Text.RegularExpressions;
namespace aoc2019.day12
{
    public class Day12
    {
        static public string[] Solve(string[] input)
        {
            List<Moon> moons = [];
            List<int[]> pairs = [[0, 1], [0, 2], [0, 3], [1, 2], [1, 3], [2, 3]];
            ParseMoons(input);
            StepTime(1000);
            int part1 = GetTotalEnergy(moons);
            return [part1.ToString(), ""];

            void ParseMoons(string[] input)
            {
                foreach (string line in input)
                {
                    MatchCollection matches = Regex.Matches(line, "-?\\d+");
                    int[] pos = new int[matches.Count];
                    for (int i = 0; i < matches.Count; i++)
                    {
                        pos[i] = int.Parse(matches[i].Value);
                    }
                    moons.Add(Moon.New(pos));
                }
            }

            void StepTime(int steps)
            {
                for (int step = 0; step < steps; step++)
                {
                    foreach (int[] pair in pairs)
                    {
                        Moon moon1 = moons[pair[0]], moon2 = moons[pair[1]];
                        moon1.ApplyGravity(moon2);
                        moon2.ApplyGravity(moon1);
                    }
                    foreach (Moon moon in moons)
                    {
                        moon.ApplyVelocity();
                    }
                }
            }

            int GetTotalEnergy(List<Moon> moons)
            {
                int energy = 0;
                foreach (Moon moon in moons)
                {
                    energy += moon.GetEnergy();
                }
                return energy;
            }
        }

        class Moon
        {
            public required int[] pos;
            public int[] vel = [0, 0, 0];

            static public Moon New(int[] pos)
            {
                Moon moon = new()
                {
                    pos = [pos[0], pos[1], pos[2]]
                };
                return moon;
            }

            public void ApplyGravity(Moon moon2)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (pos[i] < moon2.pos[i])
                    {
                        vel[i]++;
                    }
                    else if (pos[i] > moon2.pos[i])
                    {
                        vel[i]--;
                    }
                }
            }

            public void ApplyVelocity()
            {
                for (int i = 0; i < 3; i++)
                {
                    pos[i] += vel[i];
                }
            }

            public int GetEnergy()
            {
                int pot = 0, kin = 0;
                for (int i = 0; i < 3; i++)
                {
                    pot += Math.Abs(pos[i]);
                    kin += Math.Abs(vel[i]);
                }
                return pot * kin;
            }
        }
    }
}