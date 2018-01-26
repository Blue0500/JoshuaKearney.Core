using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshuaKearney {
    public delegate Task BuilderPotential<T>(T builder);

    public static class BuilderPotential {
        public static BuilderPotential<T> Empty<T>() {
            return _ => Task.CompletedTask;
        }

        public static BuilderPotential<T> FromAction<T>(Action<T> action) {
            return wr => {
                action(wr);
                return Task.CompletedTask;
            };
        }

        public static BuilderPotential<T> FromFunc<T>(Func<T, Task> action) {
            return wr => {
                return action(wr);
            };
        }

        public static BuilderPotential<T> Append<T>(this BuilderPotential<T> potential, BuilderPotential<T> other) {
            Validate.NonNull(potential, nameof(potential));

            if (other == null) {
                return potential;
            }

            return async wr => {
                await potential(wr);
                await other(wr);
            };
        }

        public static BuilderPotential<T> AppendAll<T>(this BuilderPotential<T> potential, IEnumerable<BuilderPotential<T>> others) {
            Validate.NonNull(potential, nameof(potential));

            if (others == null) {
                return potential;
            }

            return async wr => {
                await potential(wr);
                
                foreach (var other in others) {
                    if (other != null) {
                        await other(wr);
                    }
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

            return async wr => {
                await other(wr);
                await potential(wr);
            };
        }

        public static BuilderPotential<T> PrependAll<T>(this BuilderPotential<T> potential, IEnumerable<BuilderPotential<T>> others) {
            Validate.NonNull(potential, nameof(potential));

            if (others == null) {
                return potential;
            }

            return async wr => {
                foreach (var other in others) {
                    if (other != null) {
                        await other(wr);
                    }
                }
                
                await potential(wr);
            };
        }

        public static BuilderPotential<T> PrependAll<T>(this BuilderPotential<T> potential, BuilderPotential<T>[] others) {
            return potential.PrependAll(others as IEnumerable<BuilderPotential<T>>);
        }
    }
}