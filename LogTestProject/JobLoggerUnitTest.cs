using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logger;
using Moq;
using System.Threading.Tasks;

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
            JobLogger.SetLogConfiguration(mockConfiguration.Object);

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
            JobLogger.SetLogConfiguration(mockConfiguration.Object);



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
            JobLogger.SetLogConfiguration(mockConfiguration.Object);

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


        [TestMethod]
        public void JobLogger_Test_thrown_MockFactory_WARNING_Only_DATABASE()
        {
            // arrange  
            var Mensaje = "Hi!";
            var mockConfiguration = new Mock<ILogConfiguration>();
            mockConfiguration.Setup(x => x.AllowLevels).Returns(LogLevel.WARNING);
            mockConfiguration.Setup(x => x.AllowSource).Returns(LogSource.DATABASE);
            JobLogger.SetLogConfiguration(mockConfiguration.Object);
             
            var mocksource = new Mock<LogSourceBase>();
            mocksource.Setup(x => x.Log(Mensaje, LogLevel.WARNING)).Returns(Task.CompletedTask);

            var mockFactory = new Mock<ILogSourceFactory>();
            mockFactory.Setup(x => x.Create(LogSource.DATABASE)).Returns(mocksource.Object); 
            JobLogger.SetLogSourceFactory(mockFactory.Object);
            
            // act 
            JobLogger.Warning(Mensaje).Wait();


            // assert  
            mocksource.Verify(foo => foo.Log(Mensaje, LogLevel.WARNING), Times.Once());
            mocksource.Verify(foo => foo.Log(Mensaje, LogLevel.ERROR), Times.Never());
            mocksource.Verify(foo => foo.Log(Mensaje, LogLevel.INFO), Times.Never());

            mockFactory.Verify(x => x.Create(LogSource.DATABASE));
            mockFactory.Verify(x => x.Create(LogSource.CONSOLE), Times.Never());
            mockFactory.Verify(x => x.Create(LogSource.FILE), Times.Never());

        }



    }
}
