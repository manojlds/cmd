using NUnit.Framework;

namespace cmd.UnitTests
{
    [TestFixture]
    public class CmdDslTests
    {
        private dynamic cmd;

        [SetUp]
        public void SetUp()
        {
            cmd = new Cmd();
        }

        [Test]
        public void ShouldBeAbleToCallArbitraryCommandOnCmd()
        {
            cmd.Git();
        }

        [Test]
        public void ShouldBeAbleToCallArbitrarySubCommandOnCommandRunningOnCmd()
        {
            cmd.Git().Clone();
        }

        [Test]
        public void ShouldBeAbleToPassArgumentsToACommand()
        {
            cmd.Git().Clone("http://github.com/manojlds/cmd");
        }

        [Test]
        public void ShouldBeAbleToTriggerComandExecutionWithParanthses()
        {
            cmd.Git().Clone("http://github.com/manojlds/cmd")();
        }
    }
}
