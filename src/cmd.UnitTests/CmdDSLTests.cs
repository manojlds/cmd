using Moq;
using NUnit.Framework;
using cmd.Runner;

namespace cmd.UnitTests
{
    [TestFixture]
    public class CmdDslTests
    {
        private dynamic cmd;
        private Mock<IRunner> mockRunner;

        [SetUp]
        public void SetUp()
        {
            mockRunner = new Mock<IRunner>();
            mockRunner.Setup(runner => runner.Run(It.IsAny<RunOptions>())).Returns("result");
            cmd = new Cmd(mockRunner.Object);
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
        public void ShouldBeAbleToUseThePropertyNotationToIndicateCommand()
        {
            cmd.Git.Clone();
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

        [Test]
        public void ShouldBeAbleToPassFlags()
        {
            cmd.Git().Pull(r: true);
        }

        [Test]
        public void ShouldBeAbleToPassArgumentstoFlags()
        {
            cmd.Git().Checkout(b: "master");
        }
    }
}
