using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshuaKearney.Core {
    public delegate void BuildPotential<T>(T builder);

    public static class BuildPotential {
        public static BuildPotential<T> Empty<T>() {
            return _ => { };
        }

        public static BuildPotential<T> Append<T>(this BuildPotential<T> potential, BuildPotential<T> other) {
            Validate.NonNull(potential, nameof(potential));

            if (other == null) {
                return potential;
            }

            return wr => {
                potential(wr);
                other(wr);
            };
        }

        public static BuildPotential<T> AppendAll<T>(this BuildPotential<T> potential, IEnumerable<BuildPotential<T>> others) {
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

        public static BuildPotential<T> AppendAll<T>(this BuildPotential<T> potential, BuildPotential<T>[] others) {
            return potential.AppendAll(others as IEnumerable<BuildPotential<T>>);
        }

        public static BuildPotential<T> Prepend<T>(this BuildPotential<T> potential, BuildPotential<T> other) {
            Validate.NonNull(potential, nameof(potential));

            if (other == null) {
                return potential;
            }

            return wr => {
                other(wr);
                potential(wr);
            };
        }

        public static BuildPotential<T> PrependAll<T>(this BuildPotential<T> potential, IEnumerable<BuildPotential<T>> others) {
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

        public static BuildPotential<T> PrependAll<T>(this BuildPotential<T> potential, BuildPotential<T>[] others) {
            return potential.PrependAll(others as IEnumerable<BuildPotential<T>>);
        }
    }
}