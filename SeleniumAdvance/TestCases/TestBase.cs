using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumAdvance.Common;
using SeleniumAdvance.PageObjects;

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
            FirefoxBinary ffBinary = new FirefoxBinary(@"C:\Program Files\Mozilla Firefox\firefox.exe");
            driver = new FirefoxDriver(ffBinary,new FirefoxProfile());
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }


        [TestCleanup]
        public void TestCleanupMethod()
        {
            Console.WriteLine("Test Cleanup");
            Console.WriteLine("Cleanup all current page");
            //GeneralPage generalPage = new GeneralPage(driver);
            //generalPage.Logout().Login(Constant.Username, Constant.Password);
            
            //Close browser
            driver.Quit();
        }
    }
}
