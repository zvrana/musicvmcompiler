using System;

namespace musicvmcompiler.Engine
{
    public class CompileException : Exception
    {
        public CompileException(string message) : base(message)
        {
        }
    }
}