using Pafetser.Yarf.Exceptions;

namespace Pafetser.Yarf.Tests
{
    public class MessageTests
    {

        public abstract class BasicMessageTests<T> where T:  Message
        {
            public abstract Func<string, T> ConstructWithDescriptionOnly { get; }
            public abstract Func<string, T> InitializeWithDescriptionOnly { get; }
            public abstract Func<string, string, T> ConstructWithDescriptionAndCode { get; }
            public abstract Func<string, string, T> InitializeWithDescriptionAndCode { get; }
            public abstract Func<string, T> ImplicitOperateToMessage { get; }
            public abstract Func<string, string, string> ImplicitOperateToString { get; }
            public static List<object[]> ShouldThrowData => new List<object[]>()
                {
                    new object[] { typeof(ArgumentNullException), null },
                    new object[] { typeof(ArgumentEmptyException), "" },
                    new object[] { typeof(ArgumentEmptyException), " " },
                    new object[] { typeof(ArgumentEmptyException), "       " },
                };

            public static List<object[]> ShouldSucceedData => new List<object[]>()
                {
                    new object[] { "test", null, "test" },
                    new object[] { "test", "", "test" },
                    new object[] { "test", "  ", "test" },
                    new object[] { "test", "    ", "test" },
                    new object[] { "test", "key", "[key] test" },
                };

            [Theory]
            [MemberData(nameof(ShouldThrowData))]
            public void ShouldThrowWhenUsingConstructor(Type expectedExceptionType, string description)
            {
                Assert.Throws(expectedExceptionType, () => ConstructWithDescriptionOnly(description));
            }

            [Theory]
            [MemberData(nameof(ShouldThrowData))]
            public void ShouldThrowWhenUsingObjectInitializer(Type expectedExceptionType, string description)
            {
                Assert.Throws(expectedExceptionType, () => InitializeWithDescriptionOnly(description));
            }

            [Theory]
            [MemberData(nameof(ShouldSucceedData))]
            public void ShouldSucceedUsingConstructorAndHaveToString(string description, string code, string toString)
            {
                var message = ConstructWithDescriptionAndCode(description, code);
                Assert.Equal(description, message.Description);
                Assert.Equal(code, message.Code);
                Assert.Equal(toString, message.ToString());
            }

            [Theory]
            [MemberData(nameof(ShouldSucceedData))]
            public void ShouldSucceedUsingInitializerAndHaveToString(string description, string code, string toString)
            {
                var message = InitializeWithDescriptionAndCode(description, code);
                Assert.Equal(description, message.Description);
                Assert.Equal(code, message.Code);
                Assert.Equal(toString, message.ToString());
            }

            [Fact]
            public void ShouldConstructUsingImplicitOperator()
            {
                T message = ImplicitOperateToMessage("test");
                Assert.Equal("test", message.Description);
                Assert.Null(message.Code);
            }

            [Theory]
            [InlineData("test", null, "test")]
            [InlineData("test", "", "test")]
            [InlineData("test", " ", "test")]
            [InlineData("test", "    ", "test")]
            [InlineData("test", "code", "[code] test")]
            public void ShouldDeconstructUsingImplicitOperator(string description, string code, string result)
            {
                string test = ImplicitOperateToString(description, code);
                Assert.Equal(result, test);
            }
        }
        public class InformationTests : BasicMessageTests<Information>
        {
            public override Func<string, Information> ConstructWithDescriptionOnly => (description) => new Information(description);

            public override Func<string, Information> InitializeWithDescriptionOnly => (description) => new Information() { Description= description };

            public override Func<string, string, Information> ConstructWithDescriptionAndCode => (description, code) => new Information(description, code);

            public override Func<string, Information> ImplicitOperateToMessage => (description) => description;

            public override Func<string, string, string> ImplicitOperateToString => (description, code) => new Information(description, code);

            public override Func<string, string, Information> InitializeWithDescriptionAndCode => (description, code) => new Information() { Description= description, Code = code };
        }
        public class WarningTests : BasicMessageTests<Warning>
        {
            public override Func<string, Warning> ConstructWithDescriptionOnly => (description) => new Warning(description);

            public override Func<string, Warning> InitializeWithDescriptionOnly => (description) => new Warning() { Description = description };

            public override Func<string, string, Warning> ConstructWithDescriptionAndCode => (description, code) => new Warning(description, code);

            public override Func<string, Warning> ImplicitOperateToMessage => (description) => description;

            public override Func<string, string, string> ImplicitOperateToString => (description, code) => new Warning(description, code);
            public override Func<string, string, Warning> InitializeWithDescriptionAndCode => (description, code) => new Warning() { Description = description, Code = code };
        }

        public class ErrorTests : BasicMessageTests<Error>
        {
            public override Func<string, Error> ConstructWithDescriptionOnly => (description) => new Error(description);

            public override Func<string, Error> InitializeWithDescriptionOnly => (description) => new Error() { Description = description };

            public override Func<string, string, Error> ConstructWithDescriptionAndCode => (description, code) => new Error(description, code);

            public override Func<string, Error> ImplicitOperateToMessage => (description) => description;

            public override Func<string, string, string> ImplicitOperateToString => (description, code) => new Error(description, code);
            public override Func<string, string, Error> InitializeWithDescriptionAndCode => (description, code) => new Error() { Description = description, Code = code };
        }
        public class ValidationErrorTests
        {
            public static List<object[]> ShouldThrowData => new List<object[]>()
                {
                    new object[] { typeof(ArgumentNullException), null, null },
                    new object[] { typeof(ArgumentNullException), "", null },
                    new object[] { typeof(ArgumentEmptyException), null, "" },
                    new object[] { typeof(ArgumentNullException), " ", null },
                    new object[] { typeof(ArgumentEmptyException), null, " " },
                    new object[] { typeof(ArgumentNullException), "       ", null },
                    new object[] { typeof(ArgumentEmptyException), null, "     " },
                    new object[] { typeof(ArgumentEmptyException), "", "" },
                    new object[] { typeof(ArgumentEmptyException), "", " " },
                    new object[] { typeof(ArgumentEmptyException), " ", "" },
                    new object[] { typeof(ArgumentEmptyException), "", "     " },
                    new object[] { typeof(ArgumentEmptyException), "     ", "" },
                    new object[] { typeof(ArgumentEmptyException), "       ", "      " },
                };

            public static List<object[]> ShouldSucceedData => new List<object[]>()
                {
                    new object[] { "prop1", "test", null, "test (prop1)" },
                    new object[] { "prop1", "test", "", "test (prop1)" },
                    new object[] { "prop1", "test", " ", "test (prop1)" },
                    new object[] { "prop1", "test", "   ", "test (prop1)" },
                    new object[] { "prop1", "test", "key", "[key] test (prop1)" },
                };

            [Theory]
            [MemberData(nameof(ShouldThrowData))]
            public void ShouldThrowWhenUsingConstructor(Type expectedExceptionType, string source, string description)
            {
                Assert.Throws(expectedExceptionType, () => new ValidationError(source, description));
            }

            [Theory]
            [MemberData(nameof(ShouldThrowData))]
            public void ShouldThrowWhenUsingObjectInitializer(Type expectedExceptionType, string source, string description)
            {
                Assert.Throws(expectedExceptionType, () => new ValidationError() { Description = description, Source = source });
            }

            [Theory]
            [MemberData(nameof(ShouldSucceedData))]
            public void ShouldSucceedUsingConstructorAndHaveToString(string source, string description, string code, string toString)
            {
                var message = new ValidationError(source, description, code);
                Assert.Equal(description, message.Description);
                Assert.Equal(code, message.Code);
                Assert.Equal(source, message.Source);
                Assert.Equal(toString, message.ToString());
            }

            [Theory]
            [MemberData(nameof(ShouldSucceedData))]
            public void ShouldSucceedUsingInitializerAndHaveToString(string source, string description, string code, string toString)
            {
                var message = new ValidationError() { Code= code, Source = source, Description = description };
                Assert.Equal(description, message.Description);
                Assert.Equal(code, message.Code);
                Assert.Equal(source, message.Source);
                Assert.Equal(toString, message.ToString());
            }


            [Theory]
            [InlineData("prop1", "test", null, "test (prop1)")]
            [InlineData("prop1", "test", "", "test (prop1)")]
            [InlineData("prop1", "test", " ", "test (prop1)")]
            [InlineData("prop1", "test", "    ", "test (prop1)")]
            [InlineData("prop1", "test", "code", "[code] test (prop1)")]
            public void ShouldDeconstructUsingImplicitOperator(string source, string description, string code, string result)
            {
                string test = new ValidationError(source, description, code);
                Assert.Equal(result, test);
            }
        }

        public class ExceptionalErrorTests
        {
            public static List<object[]> ShouldThrowData => new List<object[]>()
                {
                    new object[] { typeof(ArgumentNullException), null, null },
                    new object[] { typeof(ArgumentNullException), "", null },
                    new object[] { typeof(ArgumentEmptyException), null, "" },
                    new object[] { typeof(ArgumentNullException), " ", null },
                    new object[] { typeof(ArgumentEmptyException), null, " " },
                    new object[] { typeof(ArgumentNullException), "       ", null },
                    new object[] { typeof(ArgumentEmptyException), null, "     " },
                    new object[] { typeof(ArgumentEmptyException), "", "" },
                    new object[] { typeof(ArgumentEmptyException), "", " " },
                    new object[] { typeof(ArgumentEmptyException), " ", "" },
                    new object[] { typeof(ArgumentEmptyException), "", "     " },
                    new object[] { typeof(ArgumentEmptyException), "     ", "" },
                    new object[] { typeof(ArgumentEmptyException), "       ", "      " },
                };

            public static List<object[]> ShouldSucceedData => new List<object[]>()
                {
                    new object[] { "prop1", "test", null, "test (prop1)" },
                    new object[] { "prop1", "test", "", "test (prop1)" },
                    new object[] { "prop1", "test", " ", "test (prop1)" },
                    new object[] { "prop1", "test", "   ", "test (prop1)" },
                    new object[] { "prop1", "test", "key", "[key] test (prop1)" },
                };

            [Fact]
            public void ShouldThrowWhenUsingConstructor()
            {
                Assert.Throws<ArgumentNullException>(() => new ExceptionalError(null));
            }
        }
    }
}