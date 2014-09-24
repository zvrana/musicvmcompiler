using System.Collections.Generic;

namespace musicvmcompiler.Engine
{
    public class Optimizer
    {
        private readonly Context context = new Context();

        public void Optimize(IEnumerable<Instruction> instructions)
        {
            foreach (var instruction in instructions)
            {
                if (instruction.IsNop(context))
                {
                    instruction.Enabled = false;
                }
                instruction.UpdateContext(context);
            }
        }
    }
}
