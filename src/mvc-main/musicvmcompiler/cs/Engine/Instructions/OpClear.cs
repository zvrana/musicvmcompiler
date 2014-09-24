namespace musicvmcompiler.Engine.Instructions
{
    public class OpClear : PureInstruction
    {
        public OpClear() : base(Opcodes.Zerobuf)
        {
        }
    }
}