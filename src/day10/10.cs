namespace aoc2019.day10
{
    public class Day10
    {
        static public string[] Solve(string[] input)
        {
            HashSet<(int, int)> asteroids = [];
            List<(int, int)> vaped = [];
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] == '#')
                    {
                        asteroids.Add((x, y));
                    }
                }
            }
            int part1 = 0;
            (int, int) station = (0, 0);
            foreach ((int, int) ast in asteroids)
            {
                int seen = SeeAsteroids(ast).Count;
                if (seen > part1)
                {
                    part1 = seen;
                    station = ast;
                }
            }
            VapeAsteroids(station);
            int part2 = vaped[199].Item1 * 100 + vaped[199].Item2;
            return [part1.ToString(), part2.ToString()];

            Dictionary<double,List<(int,int)>> SeeAsteroids((int,int) loc)
            {
                Dictionary<double,List<(int,int)>> angles = [];
                foreach((int, int) ast in asteroids)
                {
                    var angle = GetAngle(loc, ast);
                    if (angle < 0)
                    {
                        angle += 360;
                    }
                    if (!angles.ContainsKey(angle))
                    {
                        angles.Add(angle, []);
                    }
                    angles[angle].Add((ast));
                }
                return angles;
            }

            void VapeAsteroids((int, int) station)
            {
                asteroids.Remove(station);
                while (asteroids.Count > 0)
                {
                    var seen = SeeAsteroids(station);
                    List<double> angles = [.. seen.Keys];
                    angles.Sort();
                    foreach (var angle in angles)
                    {
                        (int, int) vape = (0, 0);
                        int distance = int.MaxValue;
                        foreach (var ast in seen[angle])
                        {
                            int astdistance = GetDistance(station, ast);
                            if (astdistance < distance)
                            {
                                distance = astdistance;
                                vape = ast;
                            }
                        }
                        vaped.Add(vape);
                        asteroids.Remove(vape);
                    }
                }
            }

            double GetAngle((int, int) loc, (int, int) ast)
            {
                int x1 = loc.Item1, y1 = loc.Item2, x2 = ast.Item1, y2 = ast.Item2;
                return Math.Atan2(y2 - y1, x2 - x1) * 180 / Math.PI + 90;
            }

            int GetDistance((int, int) station, (int, int) ast)
            {
                int x1 = station.Item1, y1 = station.Item2, x2 = ast.Item1, y2 = ast.Item2;
                return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
            }
        }
    }
}
