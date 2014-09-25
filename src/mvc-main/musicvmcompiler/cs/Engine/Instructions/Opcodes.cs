namespace musicvmcompiler.Engine.Instructions
{
    public enum Opcodes
    {
        SetBuf = 0,
        SetFConst = 1,
        SetTConst = 2,
        SetIConst = 3,
        SetNote = 4,
        Zerobuf = 5,
        Add = 6,
        Multiply = 7,
        Minbuf = 8,
        Interpolate = 9,
        RepeatEnvelope = 10,
        Makesin = 11,
        Pow3 = 12,
        Lfo = 13,
        ResonantFilter = 14,
        Reverb = 15,
        BesselFilter = 16,
        Distort = 17,
        Addrange = 18,
        None = -1
    }
}