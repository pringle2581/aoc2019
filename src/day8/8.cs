namespace aoc2019.day8
{
    public class Day8
    {
        static public int[] Solve(string[] input)
        {
            int width = 25;
            int height = 6;
            int zeroes = int.MaxValue;
            int part1 = 0;
            int[] part2 = new int[width * height];
            Array.Fill(part2, 2);
            string[] layers = input[0].Chunk(width * height).Select(x => new string(x)).ToArray();
            {
                foreach (var layer in layers)
                {
                    if (layer.Count(x => x == '0') < zeroes)
                    {
                        zeroes = layer.Count(x => x == '0');
                        part1 = layer.Count(x => x == '1') * layer.Count(x => x == '2');
                    }
                    foreach (int i in Enumerable.Range(0, layer.Length))
                    {
                        if (part2[i] == 2)
                        {
                            part2[i] = int.Parse(layer[i].ToString());
                        }
                    }
                }
            }
            int ptr = 0;
            foreach (int row in Enumerable.Range(0, height))
            {
                foreach (int col in Enumerable.Range(0, width))
                {
                    string output = (part2[ptr] == 1) ? "##" : "  ";
                    Console.Write(output);
                    ptr++;
                }
                Console.WriteLine();
            }
            return [part1, 0];
        }
    }
}