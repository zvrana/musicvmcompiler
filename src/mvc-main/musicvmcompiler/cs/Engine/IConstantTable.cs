namespace musicvmcompiler.Engine
{
    public interface IConstantTable<T>
    {
        int Index(T value);
    }
}