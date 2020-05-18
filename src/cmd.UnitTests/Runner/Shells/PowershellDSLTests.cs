using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using cmd.Commands;
using cmd.Runner;
using Xunit;

namespace cmd.UnitTests.Runner.Shells
{
    public class PowershellDSLTests
    {
        private dynamic cmd;
        private Mock<IRunner> mockRunner;
        
        public PowershellDSLTests()
        {
            mockRunner = new Mock<IRunner>();
            mockRunner.Setup(runner => runner.Run(It.IsAny<IRunOptions>())).Returns("result");
            mockRunner.Setup(runner => runner.GetCommand()).Returns(new Commando(mockRunner.Object));
            cmd = new Cmd(mockRunner.Object);
        }

        [Fact]
        public void ShouldInvokeACmdlet()
        {
            cmd.Write.Host();
        }

        [Fact]
        public void ShouldInvokeACmdletWithArgument()
        {
            cmd.Write.Host("test");
        }

        [Fact]
        public void ShouldInvokeACmdletWithParameter()
        {
            cmd.Write.Host(Object: "test");
        }

        [Fact]
        public void ShouldInvokeACmdletWithSwitchFlag()
        {
            cmd.Write.Host(NoNewLine: true);
        }
    }
}