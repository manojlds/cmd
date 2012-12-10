using Moq;
using NUnit.Framework;
using cmd.Runner;

namespace cmd.UnitTests
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
            IRunOptions expectedRunOptions = null;
            mockRunner.Setup(runner => runner.Run(It.IsAny<IRunOptions>())).Callback<IRunOptions>(options =>
            {
                expectedRunOptions
                    = options;
            });

            cmd.git(help: true);

            Assert.That(expectedRunOptions, Is.Not.Null);
            Assert.That(expectedRunOptions.Command, Is.EqualTo("git"));
            Assert.That(expectedRunOptions.Arguments, Is.EqualTo("--help"));
        }

        [Test]
        public void ShouldBeAbleToRunWithArgumentsOnSubCommand()
        {
            IRunOptions expectedRunOptions = null;
            mockRunner.Setup(runner => runner.Run(It.IsAny<IRunOptions>())).Callback<IRunOptions>(options =>
            {
                expectedRunOptions
                    = options;
            });

            cmd.git.clone("https://github.com/manojlds/cmd");

            Assert.That(expectedRunOptions, Is.Not.Null);
            Assert.That(expectedRunOptions.Command, Is.EqualTo("git"));
            Assert.That(expectedRunOptions.Arguments, Is.EqualTo("clone https://github.com/manojlds/cmd"));
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
