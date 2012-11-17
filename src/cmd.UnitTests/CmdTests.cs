using System;
using Moq;
using NUnit.Framework;
using cmd.Runner;

namespace cmd.UnitTests
{
    [TestFixture]
    public class CmdTests
    {
        [SetUp]
        public void SetUp()
        {
            var mockRunner = new Mock<IRunner>();
            dynamic cmd = new Cmd(mockRunner.Object);
        }
    }
}