using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logger;
using Moq;

namespace LogTestProject
{
    [TestClass]
    public class JobLoggerUnitTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Logger.Fwk.Locator.Reset();
        }


        [TestMethod]
        public void JobLogger_Test_INFO_Only_Console()
        {
            // arrange  
            var Mensaje = "Hi!";
            var mockConfiguration = new Mock<ILogConfiguration>();
            mockConfiguration.Setup(x => x.AllowLevels).Returns(LogLevel.INFO);
            mockConfiguration.Setup(x => x.AllowSource).Returns(LogSource.CONSOLE);
            
            Logger.Fwk.Locator.Register<ILogConfiguration>(() => mockConfiguration.Object);

            // act 
            JobLogger.Info(Mensaje).Wait();

            // assert  
            mockConfiguration.Verify();
        }


        [TestMethod]
        public void JobLogger_Test_INFO_Only_File()
        {
            // arrange  
            var Mensaje = "Hi!";
            var mockConfiguration = new Mock<ILogConfiguration>();
            mockConfiguration.Setup(x => x.AllowLevels).Returns(LogLevel.INFO);
            mockConfiguration.Setup(x => x.AllowSource).Returns(LogSource.FILE);
            Logger.Fwk.Locator.Reset();
            Logger.Fwk.Locator.Register<ILogConfiguration>(() => mockConfiguration.Object);


            // act 
            JobLogger.Info(Mensaje).Wait();


            // assert  
            mockConfiguration.Verify(); 
        }


        [TestMethod]
        public void JobLogger_Test_thrown_WARNING_Only_DATABASE()
        {
            // arrange  
            var Mensaje = "Hi!";
            var mockConfiguration = new Mock<ILogConfiguration>();
            mockConfiguration.Setup(x => x.AllowLevels).Returns(LogLevel.WARNING);
            mockConfiguration.Setup(x => x.AllowSource).Returns(LogSource.DATABASE);
            Logger.Fwk.Locator.Reset();
            Logger.Fwk.Locator.Register<ILogConfiguration>(() => mockConfiguration.Object);
               
            try
            {
                // act 
                JobLogger.Warning(Mensaje).Wait();
            }
            catch (Exception e)
            {
                // assert  
                StringAssert.Contains(e.ToString(), "Call DB");
                return;
            }
            Assert.Fail("No exception was thrown."); 
        }


         
    }
}
