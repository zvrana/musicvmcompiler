using System;
using NCalc;

namespace musicvmcompiler
{
    public class TickConstantReference : ConstantReference<string>
    {
        public TickConstantReference(IConstantTable<string> constantTable, string value)
            : base(constantTable, value)
        {
        }
    }
}