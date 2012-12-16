using NUnit.Framework;
using cmd.Runner.Arguments;

namespace cmd.UnitTests.Runner.Arguments
{
    [TestFixture]
    public class ArgumentBuilderTests
    {
        private IArgumentBuilder argumentBuilder;

        [SetUp]
        public void SetUp()
        {
            argumentBuilder = new ArgumentBuilder();
        }

        [Test]
        public void ShouldSetFlagWithSingleHyphenIfOneCharacterFlag()
        {
            var argument = new Argument("f", null);

            var builtArgument = argumentBuilder.Build(argument);

            Assert.That(builtArgument, Is.EqualTo("-f"));
        }

        [Test]
        public void ShouldSetFlagWithDoubleHyphenIfMultisCharacterFlag()
        {
            var argument = new Argument("f1", null);

            var builtArgument = argumentBuilder.Build(argument);

            Assert.That(builtArgument, Is.EqualTo("--f1"));
        }

        [Test]
        public void ShouldOnlyUseValueIfNoFlagSpecifiedForArgument()
        {
            var argument = new Argument(null, "val");

            var builtArgument = argumentBuilder.Build(argument);

            Assert.That(builtArgument, Is.EqualTo("val"));
        }

        [Test]
        public void ShouldOnlyUseTheFlagIfValueIsNotAString()
        {
            var argument = new Argument("flag", true);

            var builtArgument = argumentBuilder.Build(argument);

            Assert.That(builtArgument, Is.EqualTo("--flag"));
        }

        [Test]
        public void ShouldUseBothFlagAndValueIfValueIsString()
        {
            var argument = new Argument("flag", "val");

            var builtArgument = argumentBuilder.Build(argument);

            Assert.That(builtArgument, Is.EqualTo("--flag val"));
        }

        [Test]
        public void ShouldBeNullIfFlagAndValueAreNull()
        {
            var argument = new Argument(null, null);

            var builtArgument = argumentBuilder.Build(argument);

            Assert.That(builtArgument, Is.Null);
        }

        [Test]
        public void ShouldBeNullIfFlagIsNullAndValueIsNotString()
        {
            var argument = new Argument(null, new object());

            var builtArgument = argumentBuilder.Build(argument);

            Assert.That(builtArgument, Is.Null);
        }
    }
}