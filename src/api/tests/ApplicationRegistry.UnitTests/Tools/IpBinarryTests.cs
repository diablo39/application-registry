using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace ApplicationRegistry.UnitTests.Tools
{
    public class IpBinarryTests
    {
        [Fact]
        public void Test()
        {
            var ip = "10.96.42.42";

            var result = ip.ConvertIpToHexString();

            result.Should().Be("0x0A602A2A");
        }
    }
}
