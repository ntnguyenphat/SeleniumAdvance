using OpenQA.Selenium;
using SeleniumAdvance.Common;
using SeleniumAdvance.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumAdvance.Ultilities;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SeleniumAdvance.PageObjects
{
    public class ManagePagePage : GeneralPage
    {
        private IWebDriver _driverManagePagePage;

        #region Locators

        static readonly By _txtNewPagePageName = By.XPath("//div[@id='div_popup']//input[@class='page_txt_name']");
        static readonly By _btnNewPageOK = By.XPath("//div[@id='div_popup']//input[contains(@onclick,'doAddPage')]");
        static readonly By _cmbNewPageDisplayAfter = By.XPath("//div[@id='div_popup']//select[@id='afterpage']");
        static string _lnkNewPage = "//a[.='{0}']";

        #endregion

        #region Elements

        public IWebElement TxtNewPagePageName
        {
            get { return _driverManagePagePage.FindElement(_txtNewPagePageName); }
        }

        public IWebElement BtnNewPageOK
        {
            get { return _driverManagePagePage.FindElement(_btnNewPageOK); }
        }

        public IWebElement CmbNewPageDisplayAfter
        {
            get { return _driverManagePagePage.FindElement(_cmbNewPageDisplayAfter); }
        }

        #endregion

        #region Methods

        public ManagePagePage(IWebDriver driver) : base(driver)
        {
            this._driverManagePagePage = driver;
        }

        public void AddPage(string pageName)
        {
            GeneralPage generalPage = new GeneralPage(_driverManagePagePage);
            generalPage.SelectGeneralSetting("Add Page");

            TxtNewPagePageName.SendKeys(pageName);
            BtnNewPageOK.Click();

            WebDriverWait wait = new WebDriverWait(_driverManagePagePage, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(string.Format(_lnkNewPage, pageName))));
        }

        public void AddPage(string pageName, string displayAfter)
        {
            this.SelectGeneralSetting("Add Page");

            TxtNewPagePageName.SendKeys(pageName);
            CmbNewPageDisplayAfter.SelectItem(displayAfter);
            BtnNewPageOK.Click();

            WebDriverWait wait = new WebDriverWait(_driverManagePagePage, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(string.Format(_lnkNewPage, pageName))));
        }

        public void CheckPageNextToPage(string currentPage, string nextPage)
        { 
            By next = By.XPath("//a[.='" + currentPage + "']/following::a[1]");
            string nextValue = _driverManagePagePage.FindElement(next).Text;
            Assert.AreEqual(nextPage, nextValue, "\nExpected: " + nextPage + "\nActual: " + nextValue);
        }
        #endregion
    }
}
