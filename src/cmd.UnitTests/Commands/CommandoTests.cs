using Moq;
using NUnit.Framework;
using cmd.Commands;
using cmd.Runner;
using cmd.Runner.Arguments;

namespace cmd.UnitTests.Commands
{
    [TestFixture]
    class CommandoTests
    {
        private Mock<IRunner> mockRunner;
        private dynamic cmd;

        [SetUp]
        public void SetUp()
        {
            mockRunner = new Mock<IRunner>();
            mockRunner.Setup(runner => runner.GetCommand()).Returns(new Commando(mockRunner.Object));
            cmd = new Cmd(mockRunner.Object);
        }

        [Test]
        public void ShouldBeAbleToBuildACommand()
        {
            var command = cmd.git;
        }

        [Test]
        public void ShouldBeAbleToRunACommand()
        {
            cmd.git();

            mockRunner.Verify(runner =>
                runner.Run(It.Is<IRunOptions>(options => options.Command == "git" && options.Arguments == string.Empty)), Times.Once());
        }

        [Test]
        public void ShouldBeAbleToGetOutputFromCommand()
        {
            mockRunner.Setup(runner => runner.Run(It.IsAny<IRunOptions>())).Returns("out");

            var output = cmd.git();

            Assert.That(output, Is.EqualTo("out"));

        }

        [Test]
        public void ShouldBeAbleToBuildSubCommands()
        {
            var command = cmd.git.clone;
        }

        [Test]
        public void ShouldBeAbleToRunWithSubCommand()
        {
            IRunOptions expectedRunOptions = null;
            mockRunner.Setup(runner => runner.Run(It.IsAny<IRunOptions>())).Callback<IRunOptions>(options =>
                                                                                                      {
                                                                                                          expectedRunOptions
                                                                                                              = options;
                                                                                                      });

            cmd.git.clone();

            Assert.That(expectedRunOptions, Is.Not.Null);
            Assert.That(expectedRunOptions.Command, Is.EqualTo("git"));
            Assert.That(expectedRunOptions.Arguments, Is.EqualTo("clone"));
        }

        [Test]
        public void ShouldBeAbleToRunWithArgumentsOnCommand()
        {
            const string Argument = "--help";
            IRunOptions expectedRunOptions = null;
            mockRunner.Setup(runner => runner.BuildArgument(It.IsAny<Argument>())).Returns(Argument);
            mockRunner.Setup(runner => runner.Run(It.IsAny<IRunOptions>())).Callback<IRunOptions>(options =>
            {
                expectedRunOptions
                    = options;
            });

            cmd.git(help: true);

            Assert.That(expectedRunOptions, Is.Not.Null);
            Assert.That(expectedRunOptions.Command, Is.EqualTo("git"));
            Assert.That(expectedRunOptions.Arguments, Is.EqualTo(Argument));
        }

        [Test]
        public void ShouldBeAbleToRunWithArgumentsOnSubCommand()
        {
            const string Argument = "https://github.com/manojlds/cmd";
            IRunOptions expectedRunOptions = null;
            mockRunner.Setup(runner => runner.BuildArgument(It.IsAny<Argument>()))
                      .Returns(Argument);
            mockRunner.Setup(runner => runner.Run(It.IsAny<IRunOptions>())).Callback<IRunOptions>(options =>
            {
                expectedRunOptions
                    = options;
            });

            cmd.git.clone(Argument);

            Assert.That(expectedRunOptions, Is.Not.Null);
            Assert.That(expectedRunOptions.Command, Is.EqualTo("git"));
            Assert.That(expectedRunOptions.Arguments, Is.EqualTo(string.Concat("clone ",Argument)));
        }

        [Test, Ignore]
        public void ShouldBeAbleToCallMultipleCommandsWithPreBuiltCommando()
        {
            IRunOptions branchRunOptions = null;
            mockRunner.Setup(runner => runner.Run(It.Is<IRunOptions>(options => options.Arguments.StartsWith("branch")))).Callback<IRunOptions>(options =>
            {
                branchRunOptions
                    = options;
            });

            IRunOptions cloneRunOptions = null;
            mockRunner.Setup(runner => runner.Run(It.Is<IRunOptions>(options => options.Arguments.StartsWith("clone")))).Callback<IRunOptions>(options =>
            {
                cloneRunOptions
                    = options;
            });

            var git = cmd.git;
            git.Clone();
            git.branch(async: true);

            Assert.That(branchRunOptions, Is.Not.Null);
            Assert.That(cloneRunOptions, Is.Not.Null);
        }
    }
}
