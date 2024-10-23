using System.Text.RegularExpressions;
namespace aoc2019.day12
{
    public class Day12
    {
        static public string[] Solve(string[] input)
        {
            MoonList pt1moons = new(input),pt2moons = new(input);
            pt1moons.StepTime(1000);
            int part1 = pt1moons.TotalEnergy();
            long part2 = pt2moons.FindCycle();
            return [part1.ToString(), part2.ToString()];
        }

        class MoonList : List<Moon>
        {
            readonly List<int[]> pairs = [[0, 1], [0, 2], [0, 3], [1, 2], [1, 3], [2, 3]];
            public MoonList(string[] input)
            {
                this.ParseMoons(input);
            }

            public void ParseMoons(string[] input)
            {
                foreach (string line in input)
                {
                    MatchCollection matches = Regex.Matches(line, "-?\\d+");
                    int[] pos = new int[matches.Count];
                    for (int i = 0; i < matches.Count; i++)
                    {
                        pos[i] = int.Parse(matches[i].Value);
                    }
                    this.Add(Moon.New(pos));
                }
            }

            public void StepTime(int steps)
            {
                for (int step = 0; step < steps; step++)
                {
                    foreach (int[] pair in pairs)
                    {
                        Moon moon1 = this[pair[0]], moon2 = this[pair[1]];
                        moon1.ApplyGravity(moon2);
                        moon2.ApplyGravity(moon1);
                    }
                    foreach (Moon moon in this)
                    {
                        moon.ApplyVelocity();
                    }
                }
            }

            public int TotalEnergy()
            {
                int energy = 0;
                foreach (Moon moon in this)
                {
                    energy += moon.GetEnergy();
                }
                return energy;
            }

            public int[] GetDimension(int dim)
            {
                int[] dimension = new int[this.Count * 2];
                for (int i = 0; i < this.Count; i++)
                {
                    dimension[i * 2] = this[i].pos[dim];
                    dimension[i * 2 + 1] = this[i].vel[dim];
                }
                return dimension;
            }

            public long FindCycle()
            {
                int[] cycles = new int[3];
                for (int i = 0; i < 3; i++)
                {
                    int step = 1;
                    int[] init = this.GetDimension(i);
                    this.StepTime(1);
                    while (!init.SequenceEqual(this.GetDimension(i)))
                    {
                        this.StepTime(1);
                        step++;
                    }
                    cycles[i] = step;
                }
                return LCM(LCM(cycles[0], cycles[1]), cycles[2]);

                static long GCD(long a, long b)
                {
                    if (a == 0)
                    {
                        return b;
                    }
                    return GCD(b % a, a);
                }

                static long LCM(long a, long b)
                {
                    return (a / GCD(a, b)) * b;
                }
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