using System.Text;
using musicvmcompiler.Instructions;

namespace musicvmcompiler
{
    public abstract class Instruction
    {
        private readonly Opcodes opcode;

        protected Instruction(Opcodes opcode)
        {
            this.opcode = opcode;
            Enabled = true;
        }

        public abstract bool IsNop(Context context);

        public abstract byte[] ToBytes();

        public virtual string Mnemonic {
            get { return opcode.ToString().ToUpperInvariant(); }
        }

        public abstract string[] Arguments { get; }

        public Opcodes Opcode { get { return opcode; } }
        public bool Enabled { get; set; }

        public virtual string ToListing()
        {
            var builder = new StringBuilder();
            var bytes = ToBytes();

            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
                builder.Append(" ");
            }

            while (builder.Length < 16)
            {
                builder.Append(' ');
            }

            builder.Append(Mnemonic);
            builder.Append(' ');
            builder.Append(string.Join(", ", Arguments));

            return builder.ToString();
        }

        public abstract void UpdateContext(Context context);
    }

    public class Context
    {
        private readonly int[] bufIndices = new int[16];
        private readonly int[] intConsts = new int[16];
        private readonly float[] floatConsts = new float[16];
        private int note;

        public int[] BufIndices
        {
            get { return bufIndices; }
        }

        public int[] IntConsts
        {
            get { return intConsts; }
        }

        public float[] FloatConsts
        {
            get { return floatConsts; }
        }

        public int Note
        {
            get { return note; }
            set { note = value; }
        }
    }
}