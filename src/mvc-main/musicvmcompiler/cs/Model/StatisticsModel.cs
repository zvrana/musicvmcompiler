namespace musicvmcompiler.Model
{
    public class StatisticsModel<T>
    {
        private readonly T b;
        private readonly double optimizedFrequency;
        private readonly double unoptimizedFrequency;

        public StatisticsModel(T b, double optimizedFrequency, double unoptimizedFrequency)
        {
            this.b = b;
            this.optimizedFrequency = optimizedFrequency;
            this.unoptimizedFrequency = unoptimizedFrequency;
        }

        public T B
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
