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
        private const string Output ="output";

        [SetUp]
        public void SetUp()
        {
            mockRunner = new Mock<IRunner>();
            cmd = new Cmd(mockRunner.Object);
        }

        [Test]
        public void InvokingWithoutProvidingCommandShouldBeANoOp()
        {
            string result = cmd();
            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        public void InvokingACommandShouldExecuteIt()
        {
            RunOptions runOptionsPassedToRunner = null;
            mockRunner.Setup(runner => runner.Run(It.IsAny<RunOptions>())).Callback<RunOptions>(runOptions => { runOptionsPassedToRunner = runOptions; }).Returns(Output);

            string result = cmd.Git()();
            
            Assert.That(result, Is.EqualTo(Output));
            Assert.That(runOptionsPassedToRunner.Command, Is.EqualTo("Git"));
        }

        [Test]
        public void ShouldBeAbleToAddCommandAsProperty()
        {
            cmd.Git.Status();

            Assert.That(cmd.Command, Is.EqualTo("Git"));
            Assert.That(cmd.Arguments, Is.EqualTo("Status"));
        }

        [Test]
        public void ShouldAddArgumentWithOnlyValue()
        {
            cmd.Git.Branch("branch1");

            Assert.That(cmd.Arguments, Is.EqualTo("Branch branch1"));
        }

        [Test]
        public void ShouldAddArgumentsWithFlagAndValue()
        {
            cmd.Git.Log(grep: "test");

            Assert.That(cmd.Arguments, Is.EqualTo("Log --grep test"));
        }

        [Test]
        public void ShouldAddArgumentsWithFlagOnly()
        {
            cmd.Git.Branch(a: true);

            Assert.That(cmd.Arguments, Is.EqualTo("Branch -a"));
        }
    }
}