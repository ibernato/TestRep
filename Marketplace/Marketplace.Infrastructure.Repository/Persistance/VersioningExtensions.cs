using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Infrastructure
{
    public static class VersioningExtensions
    {
        public static T ApplyVersion<T>(this T entity) where T : class
        {
            if (entity is IVersioning)
            {
                ((IVersioning)entity).Version = new Versioning();

                ((IVersioning)entity).Version.Id = "1";
            }

            return entity;
        }
    }
}
