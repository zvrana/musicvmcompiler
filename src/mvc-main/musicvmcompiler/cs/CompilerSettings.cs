using System.Collections.Generic;

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

                result.ParameterSlots.Add("CIDX_ALPHA", 0);
                result.ParameterSlots.Add("CIDX_STARTVALUE", 1);
                result.ParameterSlots.Add("CIDX_ENDVALUE", 2);
                result.ParameterSlots.Add("CIDX_DETUNE", 0);
                result.ParameterSlots.Add("CIDX_MAX", 1);
                result.ParameterSlots.Add("CIDX_LFO_MAX", 1);
                result.ParameterSlots.Add("CIDX_LFO_MIN", 2);
                result.ParameterSlots.Add("CIDX_ATTACK_ALPHA", 3);
                result.ParameterSlots.Add("CIDX_DECAY_ALPHA", 4);
                result.ParameterSlots.Add("CIDX_AMOUNT", 5);
                result.ParameterSlots.Add("CIDX_OUTGAIN", 6);

                result.ParameterSlots.Add("CIDX_STARTIDX", 0);
                result.ParameterSlots.Add("CIDX_ENDIDX", 1);

                result.ParameterSlots.Add("CIDX_START", 0);
                result.ParameterSlots.Add("CIDX_COUNT", 1);
                result.ParameterSlots.Add("CIDX_ATTACK_LENGTH", 2);
                result.ParameterSlots.Add("CIDX_DECAY_LENGTH", 3);
                result.ParameterSlots.Add("CIDX_LENGTH", 4);

                result.ParameterSlots.Add("CIDX_GAIN", 0);
                result.ParameterSlots.Add("CIDX_G1", 1);
                result.ParameterSlots.Add("CIDX_G2", 2);
                result.ParameterSlots.Add("CIDX_G3", 3);

                result.ParameterSlots.Add("CIDX_PERIOD", 0);

                return result;
            }
        }
    }
}