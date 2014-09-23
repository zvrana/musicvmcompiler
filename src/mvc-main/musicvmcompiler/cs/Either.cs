namespace musicvmcompiler
{
    public class Either<T1, T2>
    {
        private readonly T1 first;
        private readonly T2 second;

        private readonly bool isFirst;
        private readonly bool isSecond;

        public Either(T1 first)
        {
            this.first = first;
            isFirst = true;
        }

        public Either(T2 second)
        {
            this.second = second;
            isSecond = true;
        }

        public T1 First
        {
            get { return first; }
        }

        public T2 Second
        {
            get { return second; }
        }

        public bool IsFirst
        {
            get { return isFirst; }
        }

        public bool IsSecond
        {
            get { return isSecond; }
        }
    }
}