using Moq;
using NUnit.Framework;
using cmd.Runner;

namespace cmd.UnitTests
{
    [TestFixture]
    public class CmdTests
    {
        private dynamic cmd;
        private Mock<IRunner> mockRunner;

        [SetUp]
        public void SetUp()
        {
            mockRunner = new Mock<IRunner>();
            cmd = new Cmd(mockRunner.Object);
        }

        [Test]
        public void ShouldBeAbleToBuildACommandAsProperty()
        {
            var commando = cmd.git;

            Assert.That(commando, Is.Not.Null);
        }

        [Test]
        public void ShouldCreateCommandWithRunner()
        {
            cmd.git();

            mockRunner.Verify(runner => runner.Run(It.IsAny<IRunOptions>()), Times.Once());
        }

        [Test]
        public void ShouldBeAbleToBuildMultipleCommandsOnCmd()
        {
            var git = cmd.git;
            var svn = cmd.svn;

            Assert.That(git, Is.Not.EqualTo(svn));
        }

        [Test]
        public void ShouldBeAbleToRunMultipleCommandsOnCmd()
        {
            cmd.git();
            cmd.svn();

            mockRunner.Verify(runner => runner.Run(It.Is<IRunOptions>(options => options.Command == "git")), Times.Once());
            mockRunner.Verify(runner => runner.Run(It.Is<IRunOptions>(options => options.Command == "svn")), Times.Once());
        }
    }
}