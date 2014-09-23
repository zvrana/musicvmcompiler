using System;

namespace musicvmcompiler.Instructions
{
    public class OpSetFConst : Instruction
    {
        private readonly int target;
        private readonly ConstantReference<float> idx;

        public OpSetFConst(int target, ConstantReference<float> idx): base(Opcodes.SetFConst)
        {
            this.target = target;
            this.idx = idx;
        }

        public override bool IsNop(Context context)
        {
            const double tolerance = 1e-6;
            return Math.Abs(context.FloatConsts[target] - idx.Value) < tolerance;
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
            context.FloatConsts[target] = idx.Value;
        }
    }
}