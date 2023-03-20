using Pafetser.Yarf.Exceptions;

namespace Pafetser.Yarf.Tests
{
    public class MessageTests
    {
        [Fact]
        public void Test1()
        {
        }

        public abstract class BasicMessageTests<T> where T:  Message
        {
            public abstract Func<string, T> ConstructWithDescriptionOnly { get; }
            public abstract Func<string, T> InitializeWithDescriptionOnly { get; }
            public abstract Func<string, string, T> ConstructWithDescriptionAndCode { get; }
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
        }
        public class WarningTests : BasicMessageTests<Warning>
        {
            public override Func<string, Warning> ConstructWithDescriptionOnly => (description) => new Warning(description);

            public override Func<string, Warning> InitializeWithDescriptionOnly => (description) => new Warning() { Description = description };

            public override Func<string, string, Warning> ConstructWithDescriptionAndCode => (description, code) => new Warning(description, code);

            public override Func<string, Warning> ImplicitOperateToMessage => (description) => description;

            public override Func<string, string, string> ImplicitOperateToString => (description, code) => new Warning(description, code);
        }

        public class ErrorTests : BasicMessageTests<Error>
        {
            public override Func<string, Error> ConstructWithDescriptionOnly => (description) => new Error(description);

            public override Func<string, Error> InitializeWithDescriptionOnly => (description) => new Error() { Description = description };

            public override Func<string, string, Error> ConstructWithDescriptionAndCode => (description, code) => new Error(description, code);

            public override Func<string, Error> ImplicitOperateToMessage => (description) => description;

            public override Func<string, string, string> ImplicitOperateToString => (description, code) => new Error(description, code);
        }
        //public class InformationTests
        //{
        //        public static List<object[]> ShouldThrowData => new List<object[]>()
        //        {
        //            new object[] { typeof(ArgumentNullException), null },
        //            new object[] { typeof(ArgumentEmptyException), "" },
        //            new object[] { typeof(ArgumentEmptyException), " " },
        //            new object[] { typeof(ArgumentEmptyException), "       " },
        //        };

        //        public static List<object[]> ShouldSucceedData => new List<object[]>()
        //        {
        //            new object[] { "test", null, "test" },
        //            new object[] { "test", "", "test" },
        //            new object[] { "test", "  ", "test" },
        //            new object[] { "test", "    ", "test" }, 
        //            new object[] { "test", "key", "[key] test" },
        //        };

        //        [Theory]
        //        [MemberData(nameof(ShouldThrowData))]
        //        public void ShouldThrowWhenUsingConstructor(Type expectedExceptionType, string information)
        //        {
        //            Assert.Throws(expectedExceptionType, () => new Information(information));
        //        }

        //    [Theory]
        //    [MemberData(nameof(ShouldThrowData))]
        //    public void ShouldThrowWhenUsingObjectInitializer(Type expectedExceptionType, string information)
        //    {
        //        Assert.Throws(expectedExceptionType, () => new Information() { Description = information });
        //    }

        //    [Theory]
        //    [MemberData(nameof(ShouldSucceedData))]
        //    public void ShouldSucceedUsingConstructorAndHaveToString(string information, string code, string toString)
        //    {
        //        var info = new Information(information, code);
        //        Assert.Equal(information, info.Description);
        //        Assert.Equal(code, info.Code);
        //        Assert.Equal(toString, info.ToString());
        //    }

        //    [Fact]
        //    public void ShouldConstructUsingImplicitOperator()
        //    {
        //        Information info = "test";
        //        Assert.Equal("test", info.Description);
        //        Assert.Null(info.Code);
        //    }

        //    [Theory]
        //    [InlineData("test", null, "test")]
        //    [InlineData("test", "", "test")]
        //    [InlineData("test", " ", "test")]
        //    [InlineData("test", "    ", "test")]
        //    [InlineData("test", "code", "[code] test")]
        //    public void ShouldDeconstructUsingImplicitOperator(string description, string code, string result)
        //    {
        //        string test = new Information(description, code);
        //        Assert.Equal(result, test);
        //    }
        //}
    }
}