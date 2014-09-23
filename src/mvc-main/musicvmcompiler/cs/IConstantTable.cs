namespace musicvmcompiler
{
    public interface IConstantTable<T>
    {
        int Index(T value);
    }
}