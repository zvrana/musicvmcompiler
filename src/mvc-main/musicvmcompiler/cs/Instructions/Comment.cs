namespace musicvmcompiler.Instructions
{
    public class Comment : Instruction
    {
        private readonly string line;

        public Comment(string line): base(Opcodes.None)
        {
            this.line = line;
        }

        public override bool IsNop(Context context)
        {
            return false;
        }

        public override byte[] ToBytes()
        {
            return new byte[0];
        }

        public override string[] Arguments
        {
            get { return new string[0]; }
        }

        public override string ToListing()
        {
            return string.Format("// {0}", line);
        }

        public override void UpdateContext(Context context)
        {
        }
    }
}