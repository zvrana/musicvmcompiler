using System;
using System.Collections.Generic;

namespace musicvmcompiler
{
    public class ConstantTable<T, K>: IConstantTable<T> where T : IComparable where K: ConstantReference<T>
    {
        private readonly Func<ConstantTable<T, K>, T, K> factory;
        private readonly IComparer<T> comparator;

        public ConstantTable(Func<ConstantTable<T,K>, T, K> factory): this(factory, Comparer<T>.Default)
        {
        }

        public ConstantTable(Func<ConstantTable<T,K>, T, K> factory, IComparer<T> comparator)
        {
            this.factory = factory;
            this.comparator = comparator;
        }

        private readonly List<T> values = new List<T>();
        private bool isDirty;

        public K Introduce(T f)
        {
            if (values.Contains(f))
            {
                return factory(this, f);
            }

            values.Add(f);
            isDirty = true;

            return factory(this, f);
        }

        public void EnsureSorted()
        {
            if (!isDirty) return;

            values.Sort(comparator);
            isDirty = false;
        }

        public int Index(T value)
        {
            EnsureSorted();

            return values.IndexOf(value);
        }

        public List<T> Values
        {
            get
            {
                EnsureSorted();

                return values;
            }
        }
    }
}