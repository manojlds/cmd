using NUnit.Framework;
using cmd.Runner.Arguments;

namespace cmd.UnitTests.Runner.Arguments
{
    public class PowershellArgumentBuilderTests
    {
        private IArgumentBuilder argumentBuilder;

        [SetUp]
        public void SetUp()
        {
            argumentBuilder = new PowershellArgumentBuilder();
        }

        [Test]
        public void ShouldSetSwitchFlag()
        {
            var argument = new Argument("Force", null);

            var builtArgument = argumentBuilder.Build(argument);

            Assert.That(builtArgument, Is.EqualTo("-Force"));
        }
 
    }
}