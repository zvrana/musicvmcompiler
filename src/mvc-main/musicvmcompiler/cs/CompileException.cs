using System;

namespace musicvmcompiler
{
    public class CompileException : Exception
    {
        public CompileException(string message) : base(message)
        {
        }
    }
}