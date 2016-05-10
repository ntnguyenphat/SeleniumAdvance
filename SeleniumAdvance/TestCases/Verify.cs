using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumAdvance.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SeleniumAdvance.TestCases
{
    [TestClass]
    public class Verify
    {
        public static void CheckPageNextToPage(string currentPage, string nextPage)
        {
            By next = By.XPath("//a[.='" + currentPage + "']/following::a[1]");
            string nextValue = Constant.WebDriver.FindElement(next).Text;

            Assert.AreEqual(nextPage, nextValue, "\nExpected: " + nextPage + "\nActual: " + nextValue);
        }
    }
}
