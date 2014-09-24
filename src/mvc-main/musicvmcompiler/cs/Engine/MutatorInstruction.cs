using musicvmcompiler.Engine.Instructions;

namespace musicvmcompiler.Engine
{
    public abstract class MutatorInstruction : Instruction
    {
        protected MutatorInstruction(Opcodes opcode) : base(opcode)
        {
        }
    }
}