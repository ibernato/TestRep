using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Infrastructure
{
    public static class ObjectStateExtensions
    {
        public static T ChangeObjectState<T>(this T entity, State state = State.Active) where T : class
        {
            if (entity is IObjectState)
            {
                ((IObjectState)entity).ObjectState = new ObjectState();

                ((IObjectState)entity).ObjectState.State = state;
            }

            return entity;
        }
    }
}
