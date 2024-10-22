using aoc2019.intcode;
namespace aoc2019.day7
{
    public class Day7
    {
        static public string[] Solve(string[] input)
        {
            long[] program = Array.ConvertAll(input[0].Split(","), long.Parse);
            int part1 = 0, part2 = 0;
            List<int[]> pt1sequences = [], pt2sequences = [];
            Heap([0, 1, 2, 3, 4], 5, pt1sequences);
            Heap([5, 6, 7, 8, 9], 5, pt2sequences);
            foreach (int[] sequence in pt1sequences)
            {
                int signal = 0;
                foreach (int amp in sequence)
                {
                    Intcode computer = new(program);
                    computer.Input(amp);
                    computer.Input(signal);
                    computer.Compute();
                    signal = Convert.ToInt32(computer.Output().Last());
                }
                if (signal > part1)
                {
                    part1 = signal;
                }
            }
            foreach (int[] sequence in pt2sequences)
            {
                int pt2input = 0;
                bool complete = false;
                List<Intcode> amp = [];
                foreach (int i in Enumerable.Range(0,5))
                {
                    amp.Add(new(program));
                    amp[i].Input(sequence[i]);
                }
                while (!complete)
                {
                    foreach (int i in Enumerable.Range(0, 5))
                    {
                        amp[i].Input(pt2input);
                        amp[i].Compute();
                        pt2input = Convert.ToInt32(amp[i].Output().Last());
                    }
                    if (amp[4].CheckExit())
                    {
                        complete = true;
                    }
                }
                if (amp[4].Output().Last() > part2) {
                    part2 = Convert.ToInt32(amp[4].Output().Last());
                }
            }
            return [part1.ToString(), part2.ToString()];

            static void Heap(int[] array, int num, List<int[]> list)
            {
                if (num == 1)
                {
                    list.Add([.. array]);
                }
                else
                {
                    for (int i = 0; i < num; i++)
                    {
                        Heap(array, num - 1, list);
                        if (num % 2 == 0)
                        {
                            (array[i], array[num - 1]) = (array[num - 1], array[i]);
                        }
                        else
                        {
                            (array[0], array[num - 1]) = (array[num - 1], array[0]);
                        }
                    }
                }
            }
        }
    }
}