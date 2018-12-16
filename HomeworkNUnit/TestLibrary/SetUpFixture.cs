using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Serilog;
using Serilog.Sinks.File;
using Serilog.Sinks.SystemConsole;

namespace TestLibrary
{
    [SetUpFixture]
    public class SetUpFixture
    {
        public IWebDriver driver;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            driver = new ChromeDriver();

            var TestPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Test", DateTime.Now.ToString("yy-MM-dd HH-mm-ss"));
            Directory.CreateDirectory(TestPath);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(TestPath + @"\Test.txt")
                .WriteTo.Console()
                .CreateLogger();
            Log.Information("OneTimeSetUp");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            var TestPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Test");
            Directory.Move(TestPath, @"C:\Test\");

            Log.Information("OneTimeTearDown");

            driver.Quit();
        }
    }
}
