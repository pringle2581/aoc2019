﻿namespace aoc2019.intcode
{
    public class Intcode
    {
        bool halt = false;
        bool pause = false;
        bool exit = false;
        int[] memory = [];
        int pointer = 0;
        int opcode = 0;
        int[] pmode = [0, 0, 0];
        int[] p = [0, 0, 0];
        Queue<int> input = [];
        List<int> output = [];


        public Intcode(int[] program)
        {
            this.memory = [.. program];
        }
        public void NounVerb(int noun, int verb)
        {
            memory[1] = noun;
            memory[2] = verb;
        }
        public int CheckMem(int location)
        {
            return memory[location];
        }
        public void Input(int input)
        {
            this.input.Enqueue(input);
        }
        public List<int> Output()
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
            if (!halt)
            {
                if (pause)
                {
                    pause = false;
                    Run();
                }
                else
                {
                    ParseInstructions();
                    Opcode(opcode);
                    Run();
                }
            }
        }
        void ParseInstructions()
        {
            int instruction = memory[pointer];
            opcode = instruction % 100;
            int pmodes = instruction / 100;
            pmode[0] = instruction / 100 % 10;
            pmode[1] = instruction / 1000 % 10;
            pmode[2] = instruction / 10000 % 10;
        }
        void GetParameters(int parametercount)
        {
            pointer++;
            for (int i = 0; i < parametercount; i++)
            {
                p[i] = memory[pointer];
                if (pmode[i] == 0)
                {
                    p[i] = memory[p[i]];
                }
                pointer++;
            }
        }
        void GetParameters(int parametercount, int force)
        {
            pmode[force-1] = 1;
            GetParameters(parametercount);
        }
        void Opcode(int op)
        {
            switch (op)
            {
                case 1:
                    GetParameters(3,3);
                    memory[p[2]] = p[0] + p[1];
                    break;
                case 2:
                    GetParameters(3,3);
                    memory[p[2]] = p[0] * p[1];
                    break;
                case 3:
                    if (input.Count == 0)
                    {
                        halt = true;
                        pause = true;
                        break;
                    }
                    else
                    {
                        GetParameters(1, 1);
                        memory[p[0]] = input.Dequeue();
                        break;
                    }
                case 4:
                    GetParameters(1,1);
                    output.Add(memory[p[0]]);
                    break;
                case 5:
                    GetParameters(2);
                    if (p[0] != 0)
                    {
                        pointer = p[1];
                    }
                    break;
                case 6:
                    GetParameters(2);
                    if (p[0] == 0)
                    {
                        pointer = p[1];
                    }
                    break;
                case 7:
                    GetParameters(3,3);
                    if (p[0] < p[1])
                    {
                        memory[p[2]] = 1;
                    } else
                    {
                        memory[p[2]] = 0;
                    }
                    break;
                case 8:
                    GetParameters(3, 3);
                    if (p[0] == p[1])
                    {
                        memory[p[2]] = 1;
                    }
                    else
                    {
                        memory[p[2]] = 0;
                    }
                    break;
                case 99:
                    halt = true;
                    exit = true;
                    break;
                default:
                    Console.WriteLine($"UNKNOWN OPCODE {op}");
                    break;
            }
        }
    }
}