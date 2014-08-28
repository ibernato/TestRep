namespace Marketplace.Core.Adapter
{
    using AutoMapper;

    public class AutomapperTypeAdapter
        :ITypeAdapter
    {
        public AutomapperTypeAdapter()
        {
            Mapper.AllowNullDestinationValues = true;
        }

        #region ITypeAdapter Members

        public TTarget Adapt<TSource, TTarget>(TSource source)
            where TSource : class
            where TTarget : class, new()
        {
            if (source == (TSource)null)
                return null;

            return Mapper.Map<TSource, TTarget>(source);
        }

        public TTarget Adapt<TTarget>(object source) where TTarget : class, new()
        {
            if (source == null)
                return null;

            return Mapper.Map<TTarget>(source);
        }

        public TTarget Adapt<TSource, TTarget>(TSource source, TTarget target)
            where TSource : class
            where TTarget : class
        {
            if (source == (TSource)null)
                return null;

            return Mapper.Map<TSource, TTarget>(source, target);
        }

        #endregion
    }
}
