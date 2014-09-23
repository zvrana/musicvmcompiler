namespace musicvmcompiler.Instructions
{
    public class OpSetNote : Instruction
    {
        private readonly int note;

        public OpSetNote(int note): base(Opcodes.SetNote)
        {
            this.note = note;
        }

        public override bool IsNop(Context context)
        {
            return false;
        }

        public override byte[] ToBytes()
        {
            return new[] { (byte)Opcode, (byte)note };
        }

        public override string[] Arguments
        {
            get { return new[] { note.ToString() }; }
        }

        public override void UpdateContext(Context context)
        {
            context.Note = note;
        }
    }
}