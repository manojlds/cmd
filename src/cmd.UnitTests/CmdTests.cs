using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using cmd.Commands;
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
            mockRunner.Setup(runner => runner.GetCommand()).Returns(new Commando(mockRunner.Object));
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
            mockRunner.Setup(runner => runner.GetCommand()).Returns(new Commando(mockRunner.Object));
            var git = cmd.git;
            mockRunner.Setup(runner => runner.GetCommand()).Returns(new Commando(mockRunner.Object));
            var svn = cmd.svn;

            Assert.That(git, Is.Not.EqualTo(svn));
        }

        [Test]
        public void ShouldBeAbleToRunMultipleCommandsOnCmd()
        {
            mockRunner.Setup(runner => runner.GetCommand()).Returns(new Commando(mockRunner.Object));
            cmd.git();
            mockRunner.Setup(runner => runner.GetCommand()).Returns(new Commando(mockRunner.Object));
            cmd.svn();

            mockRunner.Verify(runner => runner.Run(It.Is<IRunOptions>(options => options.Command == "git")), Times.Once());
            mockRunner.Verify(runner => runner.Run(It.Is<IRunOptions>(options => options.Command == "svn")), Times.Once());
        }

        [Test]
        public void ShouldBeAbleToSetEnvironmentVariablesOnCmd()
        {
            var environmentDictionary = new Dictionary<string, string> { { "PATH", @"C:\" } };
            cmd._Env(environmentDictionary);

            mockRunner.VerifySet(runner => runner.EnvironmentVariables = environmentDictionary);
        }
    }
}