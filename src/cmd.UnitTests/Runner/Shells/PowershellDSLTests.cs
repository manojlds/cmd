using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using cmd.Commands;
using cmd.Runner;

namespace cmd.UnitTests.Runner.Shells
{
    internal class PowershellDSLTests
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
        public void ShouldInvokeACmdlet()
        {
            cmd.Write.Host();
        }

        [Test]
        public void ShouldInvokeACmdletWithArgument()
        {
            cmd.Write.Host("test");
        }

        [Test]
        public void ShouldInvokeACmdletWithParameter()
        {
            cmd.Write.Host(Object: "test");
        }

        [Test]
        public void ShouldInvokeACmdletWithSwitchFlag()
        {
            cmd.Write.Host(NoNewLine: true);
        }
    }
}