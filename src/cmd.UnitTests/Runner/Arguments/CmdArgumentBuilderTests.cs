using NUnit.Framework;
using cmd.Runner.Arguments;

namespace cmd.UnitTests.Runner.Arguments
{
    [TestFixture]
    public class CmdArgumentBuilderTests
    {
        private IArgumentBuilder argumentBuilder;

        [SetUp]
        public void SetUp()
        {
            argumentBuilder = new CmdArgumentBuilder();
        }

        [Test]
        public void ShouldSetFlagWithForwardSlash()
        {
            var argument = new Argument("f", null);

            var builtArgument = argumentBuilder.Build(argument);

            Assert.That(builtArgument, Is.EqualTo("/f"));
        }

    }
}