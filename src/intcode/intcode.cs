namespace aoc2019.intcode
{
    public class Intcode
    {
        bool halt = false;
        bool exit = false;
        long[] memory = new long[10000];
        long pointer = 0;
        long rel = 0;
        Queue<int> input = [];
        List<long> output = [];
        public Intcode(long[] program)
        {
            Array.Fill(memory, 0);
            for (int i = 0; i < program.Length; i++)
            {
                memory[i] = program[i];
            }
        }
        public void NounVerb(int noun, int verb)
        {
            memory[1] = noun;
            memory[2] = verb;
        }
        public int CheckMem(int location)
        {
            return Convert.ToInt32(memory[location]);
        }
        public void Input(int input)
        {
            this.input.Enqueue(input);
        }
        public List<long> Output()
        {
            return output;
        }
        public bool CheckExit()
        {
            return exit;
        }
        public void Compute()
        {
            halt = false;
            Run();
        }
        public void Run()
        {
            while (!halt)
            {
                Opcode(ParseInstructions());
            }
        }
        (long, long[]) ParseInstructions()
        {
            long instruction = Step();
            long[] pmode = new long[3];
            long opcode = instruction % 100;
            pmode[0] = instruction / 100 % 10;
            pmode[1] = instruction / 1000 % 10;
            pmode[2] = instruction / 10000 % 10;
            return (opcode, pmode);
        }
        long Step()
        {
            long value = memory[pointer];
            pointer++;
            return value;
        }
        long GetParam(long pmode)
        {
            long param = Step();
            long value = 0;
            if (pmode == 0)
            {
                value = memory[param];
            }
            else if (pmode == 1)
            {
                value = param;
            }
            else if (pmode == 2)
            {
                value = memory[rel + param];
            }
            return value;
        }
        void WriteParam(long value, long pmode)
        {
            long param = Step();
            if (pmode == 0)
            {
                memory[param] = value;
            }
            else if (pmode == 2)
            {
                memory[rel + param] = value;
            }
        }
        void Opcode((long, long[]) instructions)
        {
            long opcode = instructions.Item1;
            long[] pmode = instructions.Item2;
            switch (opcode)
            {
                case 1:
                    long sum = GetParam(pmode[0]) + GetParam(pmode[1]);
                    WriteParam(sum, pmode[2]);
                    break;
                case 2:
                    long product = GetParam(pmode[0]) * GetParam(pmode[1]);
                    WriteParam(product, pmode[2]);
                    break;
                case 3:
                    if (input.Count == 0)
                    {
                        pointer--;
                        halt = true;
                        break;
                    }
                    else
                    {
                        WriteParam(input.Dequeue(), pmode[0]);
                        break;
                    }
                case 4:
                    output.Add(GetParam(pmode[0]));
                    break;
                case 5:
                    if (GetParam(pmode[0]) != 0)
                    {
                        pointer = GetParam(pmode[1]);
                    }
                    else
                    {
                        Step();
                    }
                    break;
                case 6:
                    if (GetParam(pmode[0]) == 0)
                    {
                        pointer = GetParam(pmode[1]);
                    }
                    else
                    {
                        Step();
                    }
                    break;
                case 7:
                    if (GetParam(pmode[0]) < GetParam(pmode[1]))
                    {
                        WriteParam(1, pmode[2]);
                    }
                    else
                    {
                        WriteParam(0, pmode[2]);
                    }
                    break;
                case 8:
                    if (GetParam(pmode[0]) == GetParam(pmode[1]))
                    {
                        WriteParam(1, pmode[2]);
                    }
                    else
                    {
                        WriteParam(0, pmode[2]);
                    }
                    break;
                case 9:
                    rel += GetParam(pmode[0]);
                    break;
                case 99:
                    halt = true;
                    exit = true;
                    break;
                default:
                    Console.WriteLine($"UNKNOWN OPCODE {opcode}");
                    break;
            }
        }
    }
}