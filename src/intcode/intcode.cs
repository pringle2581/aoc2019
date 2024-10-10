namespace aoc2019.intcode
{
    internal class Intcode
    {
        public static int Compute(int[] input, int noun, int verb)
        {
            int[] memory = [.. input];
            int pointer = 0;
            int opcode = memory[pointer];
            memory[1] = noun;
            memory[2] = verb;
            while (opcode != 99)
            {
                int param1 = memory[pointer + 1];
                int param2 = memory[pointer + 2];
                int param3 = memory[pointer + 3];
                switch (opcode)
                {
                    case 1:
                        memory[param3] = memory[param1] + memory[param2];
                        break;
                    case 2:
                        memory[param3] = memory[param1] * memory[param2];
                        break;
                }
                pointer += 4;
                opcode = memory[pointer];
            }
            return memory[0];
        }
    }
}