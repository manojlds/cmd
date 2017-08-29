using cmd.Runner.Shells;
using Xunit;

namespace cmd.UnitTests.Runner.Shells
{
    public class ShellTests
    {
        [Fact]
        public void ShouldReturnAProcessRunnerAsDEfaul()
        {
            Assert.IsAssignableFrom<ProcessRunner>(Shell.Default);
        }

        [Fact]
        public void ShouldReturnACmdShellRunner()
        {
            Assert.IsAssignableFrom<CmdShell>(Shell.Cmd);
        }

        [Fact]
        public void ShouldReturnAPowerShellRunner()
        {
            Assert.IsAssignableFrom<Posh>(Shell.Powershell);
        }
    }
}
