namespace musicvmcompiler.Instructions
{
    public class OpSetTConst : Instruction
    {
        private readonly int target;
        private readonly TickConstantReference idx;
        private readonly TickConstantParser tickConstantParser = new TickConstantParser();

        public OpSetTConst(int target, TickConstantReference idx)
            : base(Opcodes.SetTConst)
        {
            this.target = target;
            this.idx = idx;
        }

        public override bool IsNop(Context context)
        {
            var value = tickConstantParser.ParseIntValue(idx.Value);

            return context.IntConsts[target] == value;
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
            var value = tickConstantParser.ParseIntValue(idx.Value);

            context.IntConsts[target] = value;
        }
    }
}