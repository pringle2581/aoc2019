using aoc2019.intcode;
using System.Reflection.Metadata.Ecma335;
namespace aoc2019.day7
{
    public class Day7
    {
        static public int[] Solve(string[] input)
        {
            int[] program = Array.ConvertAll(input[0].Split(","), int.Parse);
            List<int[]> sequences = [];
            Heap([0, 1, 2, 3, 4], 5);
            int part1 = 0;
            foreach (int[] sequence in sequences)
            {
                int signal = 0;
                foreach (int amp in sequence)
                {
                    Intcode computer = new(program);
                    computer.Input(amp);
                    computer.Input(signal);
                    computer.Compute();
                    signal = computer.Output().Last();
                }
                if (signal > part1)
                {
                    part1 = signal;
                }
            }

            return [part1, 0];

            void Heap(int[] array, int num)
            {
                if (num == 1)
                {
                    sequences.Add([.. array]);
                }
                else
                {
                    for (int i = 0; i < num; i++)
                    {
                        Heap(array, num - 1);
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