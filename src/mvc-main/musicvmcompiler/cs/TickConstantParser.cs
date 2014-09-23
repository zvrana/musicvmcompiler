using System;
using NCalc;

namespace musicvmcompiler
{
    public class TickConstantParser
    {
        public int ParseIntValue(string value)
        {
            var expression = new Expression(value);
            expression.Parameters.Add("T_16TH", 1000);
            expression.Parameters.Add("T_8TH", 2000);
            expression.Parameters.Add("T_4TH", 4000);

            return Convert.ToInt32(expression.Evaluate());
        }
    }
}