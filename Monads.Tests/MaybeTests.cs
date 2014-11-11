using NUnit.Framework;

namespace Monads.Tests.MonadsTests
{
    [TestFixture]
    public class MaybeTests
    {
        [Test]
        public void Apply_should_not_thrown_exception_on_null_reference()
        {
            string nullStr = null;
            Maybe.Apply(nullStr);
        }

        [Test]
        public void Apply_should_not_throw_exception_on_nullable_value_type()
        {
            int? i = null;
            Maybe.Apply(i);
        }

        [Test]
        public void IsNone_should_return_true_if_value_is_null()
        {
            string nullStr = null;
            var strMaybe = Maybe.Apply(nullStr);
            Assert.IsTrue(strMaybe.IsNone);
        }

        [Test]
        public void IsNone_should_return_false_if_value_is_not_null()
        {
            string str = "not null";
            var strMaybe = Maybe.Apply(str);
            Assert.IsFalse(strMaybe.IsNone);
        }

        [Test]
        public void IsSome_should_return_false_if_value_is_null()
        {
            string nullStr = null;
            var strMaybe = Maybe.Apply(nullStr);
            Assert.IsFalse(strMaybe.IsSome);
        }

        [Test]
        public void IsSome_should_return_true_if_value_is_not_null()
        {
            string str = "not null";
            var strMaybe = Maybe.Apply(str);
            Assert.IsTrue(strMaybe.IsSome);
        }

        [Test]
        public void Bind_should_return_Mybe_with_not_null_for_not_nullable_result()
        {
            string str = "not null";
            var result = Maybe.Apply(str).Bind(s => s.IndexOf("l"));
            Assert.IsTrue(result.IsSome);
        }

        [Test]
        public void Bind_should_return_Maybe_with_null_for_nullable_result()
        {
            string str = "not null";
            var result = Maybe.Apply(str)
                              .Bind(s => s.IndexOf("T"))
                              .Bind(i => i > 0 ? "not null" : null);
            Assert.IsTrue(result.IsNone);
        }

        [Test]
        public void Bind_should_return_Maybe_with_null_for_struct_type()
        {
            string str = null;
            var result = Maybe.Apply(str).Bind(s => s.IndexOf("T"));
            Assert.IsTrue(result.IsNone);
        }

        [Test]
        public void If_should_return_not_null_for_true_statement()
        {
            string str = "11 char str";
            var result = Maybe.Apply(str)
                              .If(s => s.Length == 11);
            Assert.IsTrue(result.IsSome);
        }

        [Test]
        public void If_should_return_null_for_false_statement()
        {
            string str = "11 char str";
            var result = Maybe.Apply(str)
                              .If(s => s.Length == 0);
            Assert.IsTrue(result.IsNone);
        }
    }

}
