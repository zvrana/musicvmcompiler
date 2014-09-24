namespace musicvmcompiler.Model
{
    public class StatisticsModel
    {
        private readonly byte b;
        private readonly double optimizedFrequency;
        private readonly double unoptimizedFrequency;

        public StatisticsModel(byte b, double optimizedFrequency, double unoptimizedFrequency)
        {
            this.b = b;
            this.optimizedFrequency = optimizedFrequency;
            this.unoptimizedFrequency = unoptimizedFrequency;
        }

        public byte B
        {
            get { return b; }
        }

        public double OptimizedFrequency
        {
            get { return optimizedFrequency; }
        }

        public double UnoptimizedFrequency
        {
            get { return unoptimizedFrequency; }
        }
    }
}
