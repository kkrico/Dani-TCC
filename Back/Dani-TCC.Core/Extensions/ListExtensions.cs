using System;
using System.Collections.Generic;
using System.Linq;

namespace Dani_TCC.Core.Extensions
{
    public static class ListExtensions
    {
        public static T PopAt<T>(this IList<T> list, int index)
        {
            T r = list[index];
            list.RemoveAt(index);
            return r;
        }
        
        public static IList<T> Clone<T>(this IEnumerable<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T) item.Clone()).ToList();
        }
    }
}