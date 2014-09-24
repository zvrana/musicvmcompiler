using System.Collections.Generic;

namespace musicvmcompiler.Engine.Instructions
{
    public class TickConstantComparer : IComparer<string>
    {
        private readonly TickConstantParser parser = new TickConstantParser();

        public int Compare(string x, string y)
        {
            var ix = parser.ParseIntValue(x);
            var iy = parser.ParseIntValue(y);

            return Comparer<int>.Default.Compare(ix, iy);
        }
    }
}