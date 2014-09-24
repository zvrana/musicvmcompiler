using System.Collections.Generic;
using musicvmcompiler.Utils;

namespace musicvmcompiler.Engine
{
    public class CompilerSettings
    {
        private readonly Dictionary<string, int> parameterSlots = new Dictionary<string, int>();
        private readonly Dictionary<string, int> opcodes = new Dictionary<string, int>();

        public Dictionary<string, int> ParameterSlots
        {
            get { return parameterSlots; }
        }

        public Dictionary<string, int> Opcodes
        {
            get { return opcodes; }
        }

        public static CompilerSettings Default
        {
            get
            {
                var result = new CompilerSettings();
                result.parameterSlots.AddAll(
                    new Dictionary<string, int>
                    {
                        {"CIDX_ALPHA", 0},
                        {"CIDX_STARTVALUE", 1},
                        {"CIDX_ENDVALUE", 2},
                        {"CIDX_DETUNE", 0},
                        {"CIDX_MAX", 1},
                        {"CIDX_LFO_MAX", 1},
                        {"CIDX_LFO_MIN", 2},
                        {"CIDX_ATTACK_ALPHA", 3},
                        {"CIDX_DECAY_ALPHA", 4},
                        {"CIDX_AMOUNT", 5},
                        {"CIDX_OUTGAIN", 6},

                        {"CIDX_STARTIDX", 0},
                        {"CIDX_ENDIDX", 1},

                        {"CIDX_START", 0},
                        {"CIDX_COUNT", 1},
                        {"CIDX_ATTACK_LENGTH", 2},
                        {"CIDX_DECAY_LENGTH", 3},
                        {"CIDX_LENGTH", 4},

                        {"CIDX_GAIN", 0},
                        {"CIDX_G1", 1},
                        {"CIDX_G2", 2},
                        {"CIDX_G3", 3},
                        
                        {"CIDX_PERIOD", 0}
                    });

                result.opcodes.AddAll(
                    new Dictionary<string, int>
                    {
                        {"OPCODE_SETBUF", 0},
                        {"OPCODE_SETFCONST", 1},
                        {"OPCODE_SETTCONST", 2},
                        {"OPCODE_SETICONST", 3},
                        {"OPCODE_SETNOTE", 4},
                        {"OPCODE_ZEROBUF", 5},
                        {"OPCODE_ADD", 6},
                        {"OPCODE_MULTIPLY", 7},
                        {"OPCODE_MINBUF", 8},
                        {"OPCODE_INTERPOLATE", 9},
                        {"OPCODE_REPEAT_ENVELOPE", 10},
                        {"OPCODE_MAKESIN", 11},
                        {"OPCODE_POW3", 12},
                        {"OPCODE_LFO", 13},
                        {"OPCODE_RESONANT_FILTER", 14},
                        {"OPCODE_REVERB", 15},
                        {"OPCODE_BESSEL_FILTER", 16},
                        {"OPCODE_DISTORT", 17},
                        {"OPCODE_ADDRANGE", 18}
                    });

                return result;
            }
        }
    }
}