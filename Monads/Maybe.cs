using System;
using System.Diagnostics;

namespace Monads
{
    [DebuggerStepThrough]
    public struct Maybe
    {
        public static Maybe<TInput> Apply<TInput>(TInput value)
        {
            return new Maybe<TInput>(value);
        }
    }

    [DebuggerStepThrough]
    public struct Maybe<TInput>
    {
        readonly TInput value;
        readonly bool isSome;

        public Maybe(TInput value)
        {
            isSome = value != null;
            this.value = value;
        }

        public bool IsSome { get { return isSome; } }
        public bool IsNone { get { return !isSome; } }
        public static readonly Maybe<TInput> None = new Maybe<TInput>();


        public Maybe<TResult> Bind<TResult>(Func<TInput, TResult> evaluator)
        {
            if (evaluator == null) throw new ArgumentNullException("evaluator");

            return isSome ? Maybe.Apply(evaluator(value)) : Maybe<TResult>.None;
        }

        public Maybe<TResult> Bind<TResult>(Func<TInput, Maybe<TResult>> evaluator)
        {
            if (evaluator == null) throw new ArgumentNullException("evaluator");

            return isSome ? evaluator(value) : Maybe<TResult>.None;
        }

        public TResult Match<TResult>(Func<TResult> none, Func<TInput, TResult> some)
        {
            if (none == null) throw new ArgumentNullException("none");
            if (some == null) throw new ArgumentNullException("some");

            return isSome ? some(value) : none();
        }

        public void Match(Action none, Action<TInput> some)
        {
            if (none == null) throw new ArgumentNullException("none");
            if (some == null) throw new ArgumentNullException("some");

            if (isSome) some(value);

            else none();
        }
    }
}
