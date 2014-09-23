namespace musicvmcompiler.Instructions
{
    public class OpClear : PureInstruction
    {
        public OpClear() : base(Opcodes.Zerobuf)
        {
        }
    }
}