using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JoshuaKearney {

    public class EventArgs<T> : EventArgs, IEquatable<EventArgs<T>> {
        public T Data { get; }

        public EventArgs(T data) {
            this.Data = data;
        }

        public override string ToString() => $"EventArgs[{ (this.Data == null ? "null" : this.Data.ToString()) }]";

        public override int GetHashCode() => this.Data == null ? 0 : this.Data.GetHashCode();

        public override bool Equals(object obj) => this.Equals(obj as EventArgs<T>);

        public bool Equals(EventArgs<T> other) {
            if (other == null) {
                return false;
            }
            else {
                if (this.Data == null && other.Data == null) {
                    return true;
                }
                else if (this.Data == null || other.Data == null) {
                    return false;
                }
                else {
                    return this.Data.Equals(other.Data);
                }
            }
        }
    }
}