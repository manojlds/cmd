using Moq;
using cmd.Commands;
using cmd.Runner;
using Xunit;

namespace cmd.UnitTests.Commands
{
    public class CmdCommandoTests
    {
        private Mock<IRunner> mockRunner;
        private dynamic cmd;

        public CmdCommandoTests()
        {
            mockRunner = new Mock<IRunner>();
            mockRunner.Setup(runner => runner.GetCommand()).Returns(new CmdCommando(mockRunner.Object));
            cmd = new Cmd(mockRunner.Object);
        }

        [Fact]
        public void ShouldRunTheCommandAgainstCmd()
        {
            IRunOptions expectedRunOptions = null;
            mockRunner.Setup(runner => runner.Run(It.IsAny<IRunOptions>())).Callback<IRunOptions>(options =>
            {
                expectedRunOptions
                    = options;
            });

            cmd.dir();

            Assert.Equal("cmd", expectedRunOptions.Command);
            Assert.Equal("/c dir", expectedRunOptions.Arguments);
        }
    }
}