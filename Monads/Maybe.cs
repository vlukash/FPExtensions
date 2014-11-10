using System;
using System.Diagnostics;

namespace Monads
{
    [DebuggerStepThrough]
    public struct Maybe
    {
        public static Maybe<T> Apply<T>(T value)
        {
            return new Maybe<T>(value);
        }
    }

    [DebuggerStepThrough]
    public struct Maybe<T>
    {
        readonly T value;
        readonly bool isSome;

        public Maybe(T value)
        {
            isSome = value != null;
            this.value = value;
        }

        public bool IsSome { get { return isSome; } }

        public bool IsNone { get { return !isSome; } }

        public static readonly Maybe<T> None = new Maybe<T>();

        public Maybe<A> Bind<A>(Func<T, A> func)
        {
            if (func == null) throw new ArgumentNullException("func");

            return isSome ? Maybe.Apply(func(value)) : Maybe<A>.None;
        }

        public Maybe<A> Bind<A>(Func<T, Maybe<A>> func)
        {
            if (func == null) throw new ArgumentNullException("func");

            return isSome ? func(value) : Maybe<A>.None;
        }

        public A Match<A>(Func<A> none, Func<T, A> some)
        {
            if (none == null) throw new ArgumentNullException("none");
            if (some == null) throw new ArgumentNullException("some");

            return isSome ? some(value) : none();
        }

        public void Match(Action none, Action<T> some)
        {
            if (none == null) throw new ArgumentNullException("none");
            if (some == null) throw new ArgumentNullException("some");

            if (isSome) some(value);

            else none();
        }
    }
}
