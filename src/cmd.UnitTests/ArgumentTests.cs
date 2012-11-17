using NUnit.Framework;

namespace cmd.UnitTests
{
    [TestFixture]
    public class ArgumentTests
    {
        [Test]
        public void ShouldSetFlagWithSingleHyphenIfOneCharacterFlag()
        {
            var argument = new Argument("f", null);
            Assert.That(argument.ToString(), Is.EqualTo("-f"));
        }

        [Test]
        public void ShouldSetFlagWithDoubleHyphenIfMultisCharacterFlag()
        {
            var argument = new Argument("f1", null);
            Assert.That(argument.ToString(), Is.EqualTo("--f1"));
        }

        [Test]
        public void ShouldOnlyUseValueIfNoFlagSpecifiedForArgument()
        {
            var argument = new Argument(null, "val");
            Assert.That(argument.ToString(), Is.EqualTo("val"));
        }

        [Test]
        public void ShouldOnlyUseTheFlagIfValueIsNotAString()
        {
            var argument = new Argument("flag", true);
            Assert.That(argument.ToString(), Is.EqualTo("--flag"));
        }

        [Test]
        public void ShouldUseBothFlagAndValueIfValueIsString()
        {
            var argument = new Argument("flag", "val");
            Assert.That(argument.ToString(), Is.EqualTo("--flag val"));
        }
    }
}