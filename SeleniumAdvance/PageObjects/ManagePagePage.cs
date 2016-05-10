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

namespace SeleniumAdvance.PageObjects
{
    class ManagePagePage : GeneralPage
    {
        #region Locators

        static readonly By _txtNewPagePageName = By.XPath("//div[@id='div_popup']//input[@class='page_txt_name']");
        static readonly By _btnNewPageOK = By.XPath("//div[@id='div_popup']//input[contains(@onclick,'doAddPage')]");
        static readonly By _cmbNewPageDisplayAfter = By.XPath("//div[@id='div_popup']//select[@id='afterpage']");
        static string _lnkNewPage = "//a[.='{0}']";

        #endregion

        #region Elements

        public IWebElement TxtNewPagePageName
        {
            get { return Constant.WebDriver.FindElement(_txtNewPagePageName); }
        }

        public IWebElement BtnNewPageOK
        {
            get { return Constant.WebDriver.FindElement(_btnNewPageOK); }
        }

        public IWebElement CmbNewPageDisplayAfter
        {
            get { return Constant.WebDriver.FindElement(_cmbNewPageDisplayAfter); }
        }

        #endregion

        #region Methods

        public void AddPage(string pageName)
        {
            GeneralPage generalPage = new GeneralPage();
            generalPage.SelectGeneralSetting("Add Page");

            TxtNewPagePageName.SendKeys(pageName);
            BtnNewPageOK.Click();

            WebDriverWait wait = new WebDriverWait(Constant.WebDriver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(string.Format(_lnkNewPage,pageName))));
        }

        public void AddPage(string pageName, string displayAfter)
        {
            this.SelectGeneralSetting("Add Page");

            TxtNewPagePageName.SendKeys(pageName);
            CmbNewPageDisplayAfter.SelectItem(displayAfter);
            BtnNewPageOK.Click();

            WebDriverWait wait = new WebDriverWait(Constant.WebDriver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(string.Format(_lnkNewPage, pageName))));
        }
        #endregion
    }
}
