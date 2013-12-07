using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using cmd.Commands;
using cmd.Runner;
using cmd.Runner.Shells;

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
            mockRunner.Setup(runner => runner.Run(It.IsAny<IRunOptions>())).Returns("result");
            mockRunner.Setup(runner => runner.GetCommand()).Returns(new Commando(mockRunner.Object));
            cmd = new Cmd(mockRunner.Object);
        }

        [Test]
        public void ShouldBeAbleToCallArbitraryCommandOnCmd()
        {
            cmd.git();
        }

        [Test]
        public void ShouldBeAbleToCallArbitrarySubCommand()
        {
            var clone = cmd.git.clone;
        }

        [Test]
        public void ShouldBeAbleToExecuteWithSubCommand()
        {
            cmd.git.Clone();
        }

        [Test]
        public void ShouldBeAbleToPassArgumentsToACommand()
        {
            cmd.Git.Clone("http://github.com/manojlds/cmd");
        }

        [Test]
        public void ShouldBeAbleToPassFlags()
        {
            cmd.Git.Pull(r: true);
        }

        [Test]
        public void ShouldBeAbleToPassArgumentstoFlags()
        {
            cmd.Git.Checkout(b: "master");
        }

        [Test]
        public void ShouldBeAbleToPreBuildACommandAndThenExecuteIt()
        {
            var git = cmd.git;
            git.Clone();
        }

        [Test]
        public void ShouldBeAbleToBuildMultipleCommandOnCmd()
        {
            var git = cmd.git;
            var svn = cmd.svn;
        }

        [Test]
        public void ShouldBeAbleToRunMultipleCommandOnCmd()
        {
            cmd.git();
            cmd.svn();
        }

        [Test]
        public void ShouldBeAbleToChooseADifferentShell()
        {
            dynamic cmd = new Cmd(Shell.Cmd);
        }

        [Test]
        public void ShouldBeAbleToSetEnvironmentVariables()
        {
            cmd._Env(new Dictionary<string, string> {{"PATH", @"C:\"}});
        }
    }
}
