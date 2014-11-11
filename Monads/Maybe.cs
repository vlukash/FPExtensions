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


        public Maybe<TResult> Bind<TResult>(Func<T, TResult> evaluator)
        {
            if (evaluator == null) throw new ArgumentNullException("evaluator");

            return isSome ? Maybe.Apply(evaluator(value)) : Maybe<TResult>.None;
        }

        public Maybe<TResult> Bind<TResult>(Func<T, Maybe<TResult>> evaluator)
        {
            if (evaluator == null) throw new ArgumentNullException("evaluator");

            return isSome ? evaluator(value) : Maybe<TResult>.None;
        }

        public TResult Match<TResult>(Func<TResult> none, Func<T, TResult> some)
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

        public Maybe<T> If(Predicate<T> predicate)            
        {
            if (predicate == null) throw new ArgumentNullException("predicate");

            return isSome && predicate(value) ? this : Maybe<T>.None;
        }

        public Maybe<T> Do(Action<T> action)
        {
            if (action == null) throw new ArgumentNullException("action");

            if (!isSome)
                return Maybe<T>.None;

            action(value);
            return this;            
        }
    }
}
