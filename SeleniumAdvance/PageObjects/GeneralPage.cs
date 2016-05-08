using OpenQA.Selenium;
using SeleniumAdvance.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAdvance.PageObjects
{
    public class GeneralPage
    {
        #region Locators

        public string _lnkAccount = "//a[@href='#Welcome' and .='{0}']";

        #endregion

        #region Elements
        public IWebElement LnkAccount
        {
            get { return Constant.WebDriver.FindElement(By.XPath(string.Format(_lnkAccount, Constant.Username))); }
        }

        #endregion

    }
}
