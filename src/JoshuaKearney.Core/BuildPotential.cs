using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshuaKearney.Core {
    public delegate void BuilderPotential<T>(T builder);

    public static class BuilderPotential {
        public static BuilderPotential<T> Empty<T>() {
            return _ => { };
        }

        public static BuilderPotential<T> Append<T>(this BuilderPotential<T> potential, BuilderPotential<T> other) {
            Validate.NonNull(potential, nameof(potential));

            if (other == null) {
                return potential;
            }

            return wr => {
                potential(wr);
                other(wr);
            };
        }

        public static BuilderPotential<T> AppendAll<T>(this BuilderPotential<T> potential, IEnumerable<BuilderPotential<T>> others) {
            Validate.NonNull(potential, nameof(potential));

            if (others == null) {
                return potential;
            }

            return wr => {
                potential(wr);
                
                foreach (var other in others) {
                    other?.Invoke(wr);
                }
            };
        }

        public static BuilderPotential<T> AppendAll<T>(this BuilderPotential<T> potential, BuilderPotential<T>[] others) {
            return potential.AppendAll(others as IEnumerable<BuilderPotential<T>>);
        }

        public static BuilderPotential<T> Prepend<T>(this BuilderPotential<T> potential, BuilderPotential<T> other) {
            Validate.NonNull(potential, nameof(potential));

            if (other == null) {
                return potential;
            }

            return wr => {
                other(wr);
                potential(wr);
            };
        }

        public static BuilderPotential<T> PrependAll<T>(this BuilderPotential<T> potential, IEnumerable<BuilderPotential<T>> others) {
            Validate.NonNull(potential, nameof(potential));

            if (others == null) {
                return potential;
            }

            return wr => {
                foreach (var other in others) {
                    other?.Invoke(wr);
                }
                
                potential(wr);
            };
        }

        public static BuilderPotential<T> PrependAll<T>(this BuilderPotential<T> potential, BuilderPotential<T>[] others) {
            return potential.PrependAll(others as IEnumerable<BuilderPotential<T>>);
        }
    }
}