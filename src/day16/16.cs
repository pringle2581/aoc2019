namespace aoc2019.day16
{
    public class Day16
    {
        static public string[] Solve(string[] input)
        {
            int[] insignal = ParseSignal(input[0]);
            int[] part1 = FFT(insignal, 100)[0..8];
            int offset = int.Parse(input[0][0..7]);
            int[] pt2signal = new int[insignal.Length * 10000];
            for (int i = 0; i < insignal.Length * 10000; i++)
            {
                pt2signal[i] = insignal[i % insignal.Length];
            }
            int[] part2 = FFT2(pt2signal, 100, offset)[offset..(offset + 8)];
            return [string.Join("", part1), string.Join("", part2)];
        }

        static int[] FFT2(int[] insignal, int phases, int offset)
        {
            int[] outsignal = [.. insignal];
            for (int phase = 0; phase < phases; phase++)
            {
                int sum = 0;
                for (int i = outsignal.Length - 1; i >= offset; i--)
                {
                    sum += outsignal[i];
                    outsignal[i] = sum % 10;
                }
            }
            return outsignal;
        }

        static int[] FFT(int[] insignal, int phases)
        {
            int[] outsignal = [.. insignal];
            int[] pattern = [0, 1, 0, -1];
            for (int phase = 0; phase < phases; phase++)
            {
                for (int i = 0; i < outsignal.Length; i++)
                {
                    int sum = 0;
                    int pdigit = 0;
                    for (int j = 0; j < outsignal.Length; j++)
                    {
                        if ((j + 1) % (i + 1) == 0)
                        {
                            pdigit++;
                        }
                        sum += outsignal[j] * pattern[pdigit % 4];
                    }
                    outsignal[i] = Math.Abs(sum % 10);
                }
            }
            return outsignal;
        }

        static int[] ParseSignal(string input)
        {
            int[] array = new int[input.Length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = int.Parse(input[i].ToString());
            }
            return array;
        }
    }
}