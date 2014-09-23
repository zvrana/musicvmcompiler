using musicvmcompiler.Instructions;

namespace musicvmcompiler
{
    public abstract class MutatorInstruction : Instruction
    {
        protected MutatorInstruction(Opcodes opcode) : base(opcode)
        {
        }
    }
}