using OpenQA.Selenium;
using SeleniumAdvance.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumAdvance.Ultilities;

namespace SeleniumAdvance.PageObjects
{
    public class GeneralPage
    {
        #region Locators

        public string _lnkAccount = "//a[@href='#Welcome' and .='{0}']";
        static readonly By _lnkLogout = By.XPath("//a[@href='logout.do']");

        #endregion

        #region Elements
        public IWebElement LnkAccount
        {
            get { return Constant.WebDriver.FindElement(By.XPath(string.Format(_lnkAccount, Constant.Username))); }
        }

        public IWebElement LnkLogout
        {
            get { return Constant.WebDriver.FindElement(_lnkLogout); }
        }

        #endregion

        #region Methods
        public LoginPage Logout()
        {
            LnkAccount.MouseTo(Constant.WebDriver);
            LnkLogout.Click();
            return new LoginPage();
        }

        #endregion

    }
}
