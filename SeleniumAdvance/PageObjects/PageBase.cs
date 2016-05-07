using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumAdvance.Common;
using OpenQA.Selenium.Firefox;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace SeleniumAdvance.PageObjects
{
    public class PageBase:GeneralPage
    {
        [TestInitialize]
        public void TestInitializeMethod()
        {
            Console.WriteLine("Test Initialize");

            //Start Firefox browser and maximize window
            //Constant.WebDriver = new FirefoxDriver();
            Constant.WebDriver = new FirefoxDriver(new FirefoxBinary(), new FirefoxProfile(), TimeSpan.FromSeconds(180));
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
