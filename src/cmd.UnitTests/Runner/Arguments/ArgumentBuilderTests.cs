using cmd.Runner.Arguments;
using Xunit;

namespace cmd.UnitTests.Runner.Arguments
{
    public class ArgumentBuilderTests
    {
        private IArgumentBuilder argumentBuilder;
        
        public ArgumentBuilderTests()
        {
            argumentBuilder = new ArgumentBuilder();
        }

        [Fact]
        public void ShouldSetFlagWithSingleHyphenIfOneCharacterFlag()
        {
            var argument = new Argument("f", null);

            var builtArgument = argumentBuilder.Build(argument);

            Assert.Equal("-f", builtArgument);
        }

        [Fact]
        public void ShouldSetFlagWithDoubleHyphenIfMultisCharacterFlag()
        {
            var argument = new Argument("f1", null);

            var builtArgument = argumentBuilder.Build(argument);

            Assert.Equal("--f1", builtArgument);
        }

        [Fact]
        public void ShouldOnlyUseValueIfNoFlagSpecifiedForArgument()
        {
            var argument = new Argument(null, "val");

            var builtArgument = argumentBuilder.Build(argument);

            Assert.Equal("val", builtArgument);
        }

        [Fact]
        public void ShouldOnlyUseTheFlagIfValueIsNotAString()
        {
            var argument = new Argument("flag", true);

            var builtArgument = argumentBuilder.Build(argument);

            Assert.Equal("--flag", builtArgument);
        }

        [Fact]
        public void ShouldUseBothFlagAndValueIfValueIsString()
        {
            var argument = new Argument("flag", "val");

            var builtArgument = argumentBuilder.Build(argument);

            Assert.Equal("--flag val", builtArgument);
        }

        [Fact]
        public void ShouldBeNullIfFlagAndValueAreNull()
        {
            var argument = new Argument(null, null);

            var builtArgument = argumentBuilder.Build(argument);

            Assert.Null(builtArgument);
        }

        [Fact]
        public void ShouldBeNullIfFlagIsNullAndValueIsNotString()
        {
            var argument = new Argument(null, new object());

            var builtArgument = argumentBuilder.Build(argument);

            Assert.Null(builtArgument);
        }
    }
}