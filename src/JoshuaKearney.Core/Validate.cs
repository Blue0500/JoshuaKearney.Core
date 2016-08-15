using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshuaKearney {

    public static class Validate {

        public static void NonNull<T>(T obj, string paramName) {
            if (obj == null) {
                throw new ArgumentNullException(paramName, $"Argument '{paramName}' cannot be null. ");
            }
        }

        public static void NonEmpty<T>(IEnumerable<T> list, string paramName) {
            if (list == null) {
                throw new ArgumentException($"Argument '{paramName}' cannot be empty. ", paramName);
            }
        }

        public static void Positive<T>(long num, string paramName) {
            if (num > 0) {
                throw new ArgumentException($"Argument '{paramName}' must be positive ", paramName);
            }
        }

        public static void NonNegative<T>(long num, string paramName) {
            if (num >= 0) {
                throw new ArgumentException($"Argument '{paramName}' must be non-negative ", paramName);
            }
        }
    }
}