using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Serilog;
using Serilog.Sinks.File;
using Serilog.Sinks.SystemConsole;

namespace TestLibrary
{
    [TestFixture]
    public class TestSuit : SetUpFixture
    {
        [SetUp]
        public void SetUp()
        {
            Log.Information("SetUp");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void TearDown()
        {
            Log.Information("TearDown");
        }

        [Test]
        [Author("Viktor Patsula")]
        [Description("Registering the user according to the given information")]
        [Order(1)]
        [TestCase("Vitya", "vitya.vitya@gmail.com", "0630101010")]
        public void Test1(string name, string email, string telephone)
        {
            TestContext.WriteLine("Navigate to test Url");
            driver.Navigate().GoToUrl($"http://www.seleniumframework.com/Practiceform/");

            TestContext.WriteLine("Finding name element of form by name"); 
            IWebElement nameInput = driver.FindElement(By.Name("name"));

            TestContext.WriteLine("Fiiling name element");
            nameInput.SendKeys(name);

            TestContext.WriteLine("Finding email element of form by Xpath"); 
            IWebElement emailInput = driver.FindElement(By.XPath("//input[@placeholder = 'E-mail *']"));

            TestContext.WriteLine("Fiiling email element");
            emailInput.SendKeys(email);

            TestContext.WriteLine("Finding telephone element of form by name"); 
            IWebElement telephoneInput = driver.FindElement(By.Name("telephone"));

            TestContext.WriteLine("Fiiling telephone element");
            telephoneInput.SendKeys(telephone);

            TestContext.WriteLine("Submiting whole form by using Submit() method on one of forms descendants");
            telephoneInput.Submit();

            TestContext.WriteLine("Finding succed element of form by ClassName");
            By succedForm = By.ClassName("formErrorContent");
            IWebElement elem = driver.FindElement(succedForm);

            Assert.That(elem.Text, Is.EqualTo("Feedback has been sent to the administrator"));
            Log.Information("User registered");
        }

        [Test]
        [Author("Viktor Patsula")]
        [Description("Checking currency rate")]
        [Order(3)]
        public void Test2()
        {
            Log.Information($"Test2");
            TestContext.WriteLine("Navigate to test Url");
            driver.Navigate().GoToUrl($"http://miniaylo.finance.ua/");

            TestContext.WriteLine("Finding bid-rate and ask-rate element of form by css");
            Thread.Sleep(1000);
            IWebElement bidRate = driver.FindElement(By.CssSelector("p.bid-rate > span.value"));
            Assert.That(bidRate.Text, Is.Not.EqualTo("—"));
            Log.Information($"bid-rate is {bidRate.Text}");

            IWebElement askRate = driver.FindElement(By.CssSelector("p.ask-rate > span.value"));
            Assert.That(askRate.Text, Is.Not.EqualTo("—"));
            Log.Information($"ask-rate is {askRate.Text}");
        }

        [Test]
        [Author("Viktor Patsula")]
        [Description("Checking projects")]
        [Order(2)]
        public void Test3()
        {
            Log.Information("Test3");
            TestContext.WriteLine("Navigate to test Url");
            driver.Navigate().GoToUrl($"https://atata-framework.github.io/atata-sample-app/#!/plans");

            TestContext.WriteLine("Find numbers of projects from payment plans using CSS selectors");
            List<IWebElement> numbers = driver.FindElements(By.CssSelector(".projects-num")).ToList();
            Warn.If(numbers.Count > 3);

            foreach(var elem in numbers)
            {
                Warn.If(elem.Text == "1");
            }
        }
    }
}
