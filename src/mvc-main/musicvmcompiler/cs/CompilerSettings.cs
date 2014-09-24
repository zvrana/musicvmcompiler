using System.Collections.Generic;
using System.Linq;
using musicvmcompiler.Utils;

namespace musicvmcompiler
{
    public class CompilerSettings
    {
        private readonly Dictionary<string, int> parameterSlots = new Dictionary<string, int>();

        public Dictionary<string, int> ParameterSlots
        {
            get { return parameterSlots; }
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

                return result;
            }
        }
    }
}