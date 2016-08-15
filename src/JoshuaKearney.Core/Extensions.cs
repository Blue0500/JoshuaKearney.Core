using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JoshuaKearney {

    public static class Extensions {

        public static IComparable<T> MakeComparable<T>(this T value, Comparison<T> comp) => new ComparableWrapper<T>(value, comp);

        public static Comparison<T> GetComparison<T>(this T value) where T : IComparable<T> {
            return (x, y) => x.CompareTo(y);
        }
    }
}