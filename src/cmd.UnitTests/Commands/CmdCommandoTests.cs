using Moq;
using NUnit.Framework;
using cmd.Commands;
using cmd.Runner;

namespace cmd.UnitTests.Commands
{
    [TestFixture]
    public class CmdCommandoTests
    {
        private Mock<IRunner> mockRunner;
        private dynamic cmd;

        [SetUp]
        public void SetUp()
        {
            mockRunner = new Mock<IRunner>();
            mockRunner.Setup(runner => runner.GetCommand()).Returns(new CmdCommando(mockRunner.Object));
            cmd = new Cmd(mockRunner.Object);
        }

        [Test]
        public void ShouldRunTheCommandAgainstCmd()
        {
            IRunOptions expectedRunOptions = null;
            mockRunner.Setup(runner => runner.Run(It.IsAny<IRunOptions>())).Callback<IRunOptions>(options =>
            {
                expectedRunOptions
                    = options;
            });

            cmd.dir();

            Assert.That(expectedRunOptions.Command, Is.EqualTo("cmd"));
            Assert.That(expectedRunOptions.Arguments, Is.EqualTo("/c dir"));
        }
    }
}