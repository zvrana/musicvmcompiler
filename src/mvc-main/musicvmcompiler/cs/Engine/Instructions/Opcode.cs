namespace musicvmcompiler.Engine.Instructions
{
    public class Opcode
    {
        private readonly string name;
        private byte value;
        
        public Opcode(string name, Opcodes initialValue): this(name, (byte)initialValue)
        {
        }

        public Opcode(string name, byte initialValue)
        {
            this.name = name;
            value = initialValue;
        }

        public string Name
        {
            get { return name; }
        }

        public byte Value
        {
            get { return value; }
            set { this.value = value; }
        }
    }
}