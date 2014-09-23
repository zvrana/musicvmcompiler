namespace musicvmcompiler.Instructions
{
    public class OpSetBuf : Instruction
    {
        private readonly int target;
        private readonly int idx;

        public OpSetBuf(int target, int idx): base(Opcodes.SetBuf)
        {
            this.target = target;
            this.idx = idx;
        }

        public override bool IsNop(Context context)
        {
            return context.BufIndices[target] == idx;
        }

        public override byte[] ToBytes()
        {
            return new[] {(byte) Opcode, (byte) target, (byte) idx};
        }

        public override string[] Arguments
        {
            get { return new[] {target.ToString(), idx.ToString()}; }
        }

        public override void UpdateContext(Context context)
        {
            context.BufIndices[target] = idx;
        }
    }
}