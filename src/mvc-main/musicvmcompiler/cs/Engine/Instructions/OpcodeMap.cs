using System.Collections.Generic;
using System.Linq;

namespace musicvmcompiler.Engine.Instructions
{
    public class OpcodeMap
    {
        private readonly Dictionary<Opcodes, Opcode> map;

        public OpcodeMap()
        {
            map = new Dictionary<Opcodes, Opcode>
            {
                {Opcodes.SetBuf, new Opcode("SETBUF", Opcodes.SetBuf)},
                {Opcodes.SetFConst, new Opcode("SETFCONST", Opcodes.SetFConst)},
                {Opcodes.SetTConst, new Opcode("SETTCONST", Opcodes.SetTConst)},
                {Opcodes.SetIConst, new Opcode("SETICONST", Opcodes.SetIConst)},
                {Opcodes.SetNote, new Opcode("SETNOTE", Opcodes.SetNote)},
                {Opcodes.Zerobuf, new Opcode("CLEAR", Opcodes.Zerobuf)},
                {Opcodes.Add, new Opcode("ADD", Opcodes.Add)},
                {Opcodes.Multiply, new Opcode("MULTIPLY", Opcodes.Multiply)},
                {Opcodes.Minbuf, new Opcode("MINBUF", Opcodes.Minbuf)},
                {Opcodes.Interpolate, new Opcode("INTERPOLATE", Opcodes.Interpolate)},
                {Opcodes.RepeatEnvelope, new Opcode("REPEAT_ENVELOPE", Opcodes.RepeatEnvelope)},
                {Opcodes.Makesin, new Opcode("MAKESIN", Opcodes.Makesin)},
                {Opcodes.Pow3, new Opcode("POW3", Opcodes.Pow3)},
                {Opcodes.Lfo, new Opcode("LFO", Opcodes.Lfo)},
                {Opcodes.ResonantFilter, new Opcode("RESONANT_FILTER", Opcodes.ResonantFilter)},
                {Opcodes.Reverb, new Opcode("REVERB", Opcodes.Reverb)},
                {Opcodes.BesselFilter, new Opcode("BESSEL_FILTER", Opcodes.BesselFilter)},
                {Opcodes.Distort, new Opcode("DISTORT", Opcodes.Distort)},
                {Opcodes.Addrange, new Opcode("ADDRANGE", Opcodes.Addrange)},
                {Opcodes.None, new Opcode("", 255)}
            };
        }

        public Dictionary<Opcodes, Opcode> Map
        {
            get { return map; }
        }

        public void AssignOpcodesByFrequency(IEnumerable<Instruction> instructions)
        {
            var optimizedInstructionBytes =
                instructions
                    .Where(i => i.Enabled)
                    .Select(i => i.ToBytes())
                    .ToList();

            var optimizedArgumentsFrequencies = new Frequencies(
                    optimizedInstructionBytes
                        .SelectMany(i => i.Skip(1))
                        .ToArray()
                );

            var optimizedOpcodesFrequencies =
                new Frequencies(
                    optimizedInstructionBytes
                        .Where(b => b.Length > 0)
                        .Select(b => b.First())
                        .ToArray()
                );

            var byteSet = optimizedArgumentsFrequencies.GetMostFrequentBytes().ToList();

            var opcodeSet = optimizedOpcodesFrequencies.GetMostFrequentBytes().ToList();

            for (byte b = 0; b < 255; b++)
            {
                if (byteSet.Count >= opcodeSet.Count) break;

                if (!byteSet.Contains(b))
                {
                    byteSet.Add(b);
                }
            }

            for (var i = 0; i < opcodeSet.Count; i++)
            {
                var b = opcodeSet[i];
                map[(Opcodes)b].Value = byteSet[i];
            }
        }

    }
}