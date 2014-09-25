using System.Windows.Media;

namespace musicvmcompiler.Model
{
    public class OpcodeModel
    {
        private readonly string name;
        private readonly byte value;
        private readonly Brush foreground;

        public OpcodeModel(string name, byte value, Brush foreground)
        {
            this.name = name;
            this.value = value;
            this.foreground = foreground;
        }

        public string Name
        {
            get { return name; }
        }

        public byte Value
        {
            get { return value; }
        }

        public Brush Foreground
        {
            get { return foreground; }
        }
    }
}