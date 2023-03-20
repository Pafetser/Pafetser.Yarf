using Pafetser.Yarf.Exceptions;

namespace Pafetser.Yarf.Tests
{
    public class MessageTests
    {
        [Fact]
        public void Test1()
        {
        }

        public class InformationTests
        {
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
                public void ShouldThrowWhenUsingConstructor(Type expectedExceptionType, string information)
                {
                    Assert.Throws(expectedExceptionType, () => new Information(information));
                }

            [Theory]
            [MemberData(nameof(ShouldThrowData))]
            public void ShouldThrowWhenUsingObjectInitializer(Type expectedExceptionType, string information)
            {
                Assert.Throws(expectedExceptionType, () => new Information() { Description = information });
            }

            [Theory]
            [MemberData(nameof(ShouldSucceedData))]
            public void ShouldSucceedUsingConstructorAndHaveToString(string information, string code, string toString)
            {
                var info = new Information(information, code);
                Assert.Equal(information, info.Description);
                Assert.Equal(code, info.Code);
                Assert.Equal(toString, info.ToString());
            }

            [Fact]
            public void ShouldConstructUsingImplicitOperator()
            {
                Information info = "test";
                Assert.Equal("test", info.Description);
                Assert.Null(info.Code);
            }

            [Theory]
            [InlineData("test", null, "test")]
            [InlineData("test", "", "test")]
            [InlineData("test", " ", "test")]
            [InlineData("test", "    ", "test")]
            [InlineData("test", "code", "[code] test")]
            public void ShouldDeconstructUsingImplicitOperator(string description, string code, string result)
            {
                string test = new Information(description, code);
                Assert.Equal(result, test);
            }
        }
    }
}