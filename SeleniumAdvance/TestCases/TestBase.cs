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
        [TestInitialize]
        public void TestInitializeMethod()
        {
            Console.WriteLine("Test Initialize");

            //Start Firefox browser and maximize window
            Constant.WebDriver = new FirefoxDriver();
            Constant.WebDriver.Manage().Window.Maximize();
            Constant.WebDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
        }

        [TestCleanup]
        public void TestCleanupMethod()
        {
            Console.WriteLine("Test Cleanup");

            //Close browser
            Constant.WebDriver.Quit();
        }
    }
}
