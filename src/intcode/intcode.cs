namespace aoc2019.intcode
{
    public class Intcode
    {
        public int[] memory = [];
        int pointer = 0;
        public Intcode(int[] program)
        {
            this.memory = [.. program];
        }
        public void NounVerb(int noun, int verb)
        {
            memory[1] = noun;
            memory[2] = verb;
        }
        public void Compute()
        {
            int opcode = memory[pointer];
            while (opcode != 99)
            {
                switch (opcode)
                {
                    case 1:
                        Opcode1(); break;
                    case 2:
                        Opcode2(); break;
                }
                opcode = memory[pointer];
            }
        }
        void Opcode1()
        {
            var parameters = GetParameters(3);
            memory[parameters[2]] = memory[parameters[0]] + memory[parameters[1]];
            pointer += 4;
        }
        void Opcode2()
        {
            var parameters = GetParameters(3);
            memory[parameters[2]] = memory[parameters[0]] * memory[parameters[1]];
            pointer += 4;
        }
        List<int> GetParameters(int parametercount)
        {
            List<int> parameters = [];
            for (int i = 1; i <= parametercount; i++)
            {
                parameters.Add(memory[pointer + i]);
            }
            return parameters;
        }
    }
}