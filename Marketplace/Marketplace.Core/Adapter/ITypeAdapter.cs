namespace Marketplace.Core.Adapter
{
    public interface ITypeAdapter
    {
        TTarget Adapt<TSource, TTarget>(TSource source)
            where TTarget : class,new()
            where TSource : class;

        TTarget Adapt<TTarget>(object source)
            where TTarget : class,new();

        TTarget Adapt<TSource, TTarget>(TSource source, TTarget target)
            where TTarget : class
            where TSource : class;
    }
}
