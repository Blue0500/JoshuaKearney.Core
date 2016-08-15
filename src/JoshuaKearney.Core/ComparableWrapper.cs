using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JoshuaKearney {

    internal class ComparableWrapper<T> : IComparable<T> {
        private T Data { get; }

        private Comparison<T> Comp { get; }

        public ComparableWrapper(T data, Comparison<T> comp) {
            this.Data = data;
            this.Comp = comp;
        }

        public int CompareTo(T other) => this.Comp(this.Data, other);
    }
}