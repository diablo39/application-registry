﻿//using ApplicationRegistry.Web;
//using Microsoft.AspNetCore;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Hosting;
//using PactNet;
//using PactNet.Extensions;
//using PactNet.Infrastructure.Outputters;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;
//using Xunit.Abstractions;



//namespace ApplicationRegistry.UnitTests.ContractTests
//{
//    public class ProviderApiTests : IDisposable
//    {
//        private string _providerUri { get; }
//        private string _pactServiceUri { get; }
//        private IHost _webHost { get; }
//        private ITestOutputHelper _outputHelper { get; }

//        public ProviderApiTests(ITestOutputHelper output)
//        {
//            _outputHelper = output;
//            _providerUri = "http://localhost:5000";
//            _pactServiceUri = "http://localhost:9001";

//            _webHost = Program.CreateHostBuilder(new string[] { "--urls", _providerUri }).Build();
//            _webHost.Start();
//        }

//        [Fact]
//        public void EnsureProviderApiHonoursPactWithConsumer()
//        {
//            // Arrange
//            var config = new PactVerifierConfig
//            {

//                // NOTE: We default to using a ConsoleOutput,
//                // however xUnit 2 does not capture the console output,
//                // so a custom outputter is required.
//                Outputters = new List<IOutput>
//                        {
//                            new XUnitOutput(_outputHelper)
//                        },

//                // Output verbose verification logs to the test output
//                Verbose = true,

//            };

//            //Act / Assert

//            IPactVerifier pactVerifier = new PactVerifier(config);
//            pactVerifier
//                //.ProviderState($"{_pactServiceUri}/provider-states")
//                .ServiceProvider("Provider", _providerUri)
//                .HonoursPactWith("Consumer")
//                .PactUri(@"..\..\..\Pacts\consumer-provider.json")
//                .Verify();
//        }

//        #region IDisposable Support
//        private bool disposedValue = false; // To detect redundant calls

//        protected virtual void Dispose(bool disposing)
//        {
//            if (!disposedValue)
//            {
//                if (disposing)
//                {
//                    _webHost.StopAsync().GetAwaiter().GetResult();
//                    _webHost.Dispose();
//                }

//                disposedValue = true;
//            }
//        }

//        // This code added to correctly implement the disposable pattern.
//        public void Dispose()
//        {
//            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
//            Dispose(true);
//        }
//        #endregion
//    }
//}
