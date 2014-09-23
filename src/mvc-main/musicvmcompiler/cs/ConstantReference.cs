using System;

namespace musicvmcompiler
{
    public class ConstantReference<T> where T : IComparable
    {
        private readonly IConstantTable<T> constantTable;

        private readonly T value;

        public ConstantReference(IConstantTable<T> constantTable, T value)
        {
            this.constantTable = constantTable;
            this.value = value;
        }

        public int Index
        {
            get
            {
                return constantTable.Index(value);
            }
        }

        public T Value
        {
            get { return value; }
        }
    }
}