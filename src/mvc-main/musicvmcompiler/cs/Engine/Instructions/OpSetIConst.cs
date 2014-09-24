namespace musicvmcompiler.Engine.Instructions
{
    public class OpSetIConst : Instruction
    {
        private readonly int target;
        private readonly ConstantReference<int> idx;

        public OpSetIConst(int target, ConstantReference<int> idx): base(Opcodes.SetIConst)
        {
            this.target = target;
            this.idx = idx;
        }

        public override bool IsNop(Context context)
        {
            return context.IntConsts[target] == idx.Value;
        }

        public override byte[] ToBytes()
        {
            return new[] { (byte)Opcode, (byte)target, (byte)idx.Index };
        }

        public override string[] Arguments
        {
            get { return new[] { target.ToString(), idx.Index.ToString() }; }
        }

        public override void UpdateContext(Context context)
        {
            context.IntConsts[target] = idx.Value;
        }
    }
}