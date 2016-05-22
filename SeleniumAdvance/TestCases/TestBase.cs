using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumAdvance.Common;

namespace SeleniumAdvance.TestCases
{
    [TestClass]
    public class TestBase
    {
        public IWebDriver driver;

        [TestInitialize]
        public void TestInitializeMethod()
        {
            Console.WriteLine("Test Initialize");

            //Start Firefox browser and maximize window
            driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }


        [TestCleanup]
        public void TestCleanupMethod()
        {
            Console.WriteLine("Test Cleanup");

            //Close browser
            driver.Quit();
        }
    }
}
