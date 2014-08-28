using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marketplace.Core.Adapter;

namespace Marketplace.Core
{
    public static class ProjectionsExtensionMethods
    {
        public static TProjection As<TProjection>(this object item)
            where TProjection : class, new()
        {
            var adapter = TypeAdapterFactory.CreateAdapter();
            return adapter.Adapt<TProjection>(item);
        }

        public static TProjection As<TSource, TProjection>(this TSource item, TProjection projection)
            where TSource : class
            where TProjection : class
        {
            var adapter = TypeAdapterFactory.CreateAdapter();
            return adapter.Adapt<TSource, TProjection>(item, projection);
        }

        public static List<TProjection> AsCollection<TProjection>(this IEnumerable<object> items)
            where TProjection : class, new()
        {
            var adapter = TypeAdapterFactory.CreateAdapter();
            return adapter.Adapt<List<TProjection>>(items);
        }
    }
}
