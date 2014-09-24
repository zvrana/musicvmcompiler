using musicvmcompiler.Engine.Instructions;

namespace musicvmcompiler.Engine
{
    public abstract class PureInstruction: Instruction
    {
        protected PureInstruction(Opcodes opcode) : base(opcode)
        {
        }

        public override bool IsNop(Context context)
        {
            return false;
        }

        public override byte[] ToBytes()
        {
            return new[] { (byte)Opcode };
        }

        public override string[] Arguments
        {
            get { return new string[0]; }
        }

        public override void UpdateContext(Context context)
        {
        }
    }
}