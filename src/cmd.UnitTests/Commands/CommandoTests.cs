using Moq;
using cmd.Commands;
using cmd.Runner;
using cmd.Runner.Arguments;
using Xunit;

namespace cmd.UnitTests.Commands
{
    public class CommandoTests
    {
        private Mock<IRunner> mockRunner;
        private dynamic cmd;

        public CommandoTests()
        {
            mockRunner = new Mock<IRunner>();
            mockRunner.Setup(runner => runner.GetCommand()).Returns(new Commando(mockRunner.Object));
            cmd = new Cmd(mockRunner.Object);
        }

        [Fact]
        public void ShouldBeAbleToBuildACommand()
        {
            var command = cmd.git;
        }

        [Fact]
        public void ShouldBeAbleToRunACommand()
        {
            cmd.git();

            mockRunner.Verify(runner =>
                runner.Run(It.Is<IRunOptions>(options => options.Command == "git" && options.Arguments == string.Empty)), Times.Once());
        }

        [Fact]
        public void ShouldBeAbleToGetOutputFromCommand()
        {
            mockRunner.Setup(runner => runner.Run(It.IsAny<IRunOptions>())).Returns("out");

            var output = cmd.git();

            Assert.Equal("out", output);

        }

        [Fact]
        public void ShouldBeAbleToBuildSubCommands()
        {
            var command = cmd.git.clone;
        }

        [Fact]
        public void ShouldBeAbleToRunWithSubCommand()
        {
            IRunOptions expectedRunOptions = null;
            mockRunner.Setup(runner => runner.Run(It.IsAny<IRunOptions>())).Callback<IRunOptions>(options =>
                                                                                                      {
                                                                                                          expectedRunOptions
                                                                                                              = options;
                                                                                                      });

            cmd.git.clone();

            Assert.NotNull(expectedRunOptions);
            Assert.Equal("git", expectedRunOptions.Command);
            Assert.Equal("clone", expectedRunOptions.Arguments);
        }

        [Fact]
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

            Assert.NotNull(expectedRunOptions);
            Assert.Equal("git", expectedRunOptions.Command);
            Assert.Equal(Argument, expectedRunOptions.Arguments);
        }

        [Fact]
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

            Assert.NotNull(expectedRunOptions);
            Assert.Equal("git", expectedRunOptions.Command);
            Assert.Equal(string.Concat("clone ", Argument), expectedRunOptions.Arguments);
        }

        [Fact(Skip = "Not implemented")]
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

            Assert.NotNull(branchRunOptions);
            Assert.NotNull(cloneRunOptions);
        }
    }
}
