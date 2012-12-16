using NUnit.Framework;
using cmd.Runner.Shells;

namespace cmd.UnitTests.Shells
{
    [TestFixture]
    class ShellTests
    {
        [Test]
        public void ShouldReturnACmdShellRunner()
        {
            Assert.That(Shell.Cmd as CmdShell, Is.Not.Null);
        }
        
        [Test]
        public void ShouldReturnAPowerShellRunner()
        {
            Assert.That(Shell.Powershell as Posh, Is.Not.Null);
        }

    }
}
