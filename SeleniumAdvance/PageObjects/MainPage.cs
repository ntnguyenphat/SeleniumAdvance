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
using System.Threading;
using System.Text.RegularExpressions;

namespace SeleniumAdvance.PageObjects
{
    public class MainPage : GeneralPage
    {
        private IWebDriver _driverManagePagePage;

        #region Locators

        static readonly By _txtNewPagePageName = By.XPath("//div[@id='div_popup']//input[@class='page_txt_name']");
        static readonly By _btnPageOK = By.XPath("//div[@id='div_popup']//input[contains(@onclick,'doAddPage')]");
        static readonly By _btnPageCancel = By.XPath("//div[@id='div_popup']//input[contains(@onclick,'closeWindow')]");
        static readonly By _cmbPageDisplayAfter = By.XPath("//div[@id='div_popup']//select[@id='afterpage']");
        static readonly By _cmbParentPage = By.XPath("//div[@id='div_popup']//select[@id='parent']");
        static readonly By _cbmNumberOfColumns = By.XPath("//div[@id='div_popup']//select[@id='columnnumber']");
        static readonly By _chbPublic = By.XPath("//input[@id='ispublic']");
        static readonly By _dlgPopupHeader = By.XPath("//div[@id='div_popup']//td[@class='ptc']/h2");
        static string _lnkPage = "//a[.='{0}']";

        #endregion

        #region Elements

        public IWebElement TxtNewPagePageName
        {
            get { return _driverManagePagePage.FindElement(_txtNewPagePageName); }
        }

        public IWebElement BtnPageOK
        {
            get { return _driverManagePagePage.FindElement(_btnPageOK); }
        }

        public IWebElement BtnPageCancel
        {
            get { return _driverManagePagePage.FindElement(_btnPageCancel); }
        }

        public IWebElement CmbNewPageDisplayAfter
        {
            get { return _driverManagePagePage.FindElement(_cmbPageDisplayAfter); }
        }

        public IWebElement CmbParentPage
        {
            get { return _driverManagePagePage.FindElement(_cmbParentPage); }
        }

        public IWebElement CmbNumberOfColumns
        {
            get { return _driverManagePagePage.FindElement(_cbmNumberOfColumns); }
        }

        public IWebElement ChbPublic
        {
            get { return _driverManagePagePage.FindElement(_chbPublic); }
        }

        public IWebElement DlgPopupHeader
        {
            get { return _driverManagePagePage.FindElement(_dlgPopupHeader); }
        }

        #endregion

        #region Methods

        public MainPage(IWebDriver driver)
            : base(driver)
        {
            this._driverManagePagePage = driver;
        }

        //public void AddPage(string pageName)
        //{
        //    GeneralPage generalPage = new GeneralPage(_driverManagePagePage);
        //    generalPage.SelectGeneralSetting("Add Page");

        //    TxtNewPagePageName.SendKeys(pageName);
        //    BtnNewPageOK.Click();

        //    WebDriverWait wait = new WebDriverWait(_driverManagePagePage, TimeSpan.FromSeconds(10));
        //    wait.Until(ExpectedConditions.ElementExists(By.XPath(string.Format(_lnkNewPage, pageName))));
        //}

        //public void AddPage(string pageName, string displayAfter)
        //{
        //    this.SelectGeneralSetting("Add Page");

        //    TxtNewPagePageName.SendKeys(pageName);
        //    CmbNewPageDisplayAfter.SelectItem(displayAfter);
        //    BtnNewPageOK.Click();

        //    WebDriverWait wait = new WebDriverWait(_driverManagePagePage, TimeSpan.FromSeconds(10));
        //    wait.Until(ExpectedConditions.ElementExists(By.XPath(string.Format(_lnkNewPage, pageName))));
        //}

        public void AddPage(string pageName, string parentPage = null, int numberOfColumn = 0, string displayAfer = null, bool publicCheckBox = false)
        {
            this.SelectGeneralSetting("Add Page");
            TxtNewPagePageName.SendKeys(pageName);

            if (parentPage != null)
            {
                CmbParentPage.SelectItem(parentPage);
            }

            if (numberOfColumn != 0)
            {
                CmbNewPageDisplayAfter.SelectItem(numberOfColumn.ToString());
            }

            if (displayAfer != null)
            {
                CmbNewPageDisplayAfter.SelectItem(displayAfer);
            }

            if (publicCheckBox != false)
            {
                ChbPublic.Check();
            }

            Thread.Sleep(1000);
            BtnPageOK.Click();

            WebDriverWait wait = new WebDriverWait(_driverManagePagePage, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(string.Format(_lnkPage, pageName))));
        }

        public void GotoPage(string pageLink)
        {
            string[] pages = Regex.Split(pageLink, "->");
            if (pages.Length == 1)
            {
                By page = By.XPath("//a[.='" + pages[0] + "']");
                IWebElement lnkPage = _driverManagePagePage.FindElement(page);
                lnkPage.Click();
            }
            else
            {
                int pageIndex = 0;
                while (pageIndex + 1 < pages.Length)
                {
                    By page = By.XPath("//a[.='" + pages[pageIndex] + "']");
                    IWebElement lnkParent = _driverManagePagePage.FindElement(page);
                    lnkParent.MouseTo(_driverManagePagePage);
                    pageIndex = pageIndex + 1;
                    page = By.XPath("//a[.='" + pages[pageIndex] + "']");
                    IWebElement lnkPage = _driverManagePagePage.FindElement(page);
                    if(pageIndex + 1 == pages.Length)
                    {
                          lnkPage.Click();
                    }
                }                            
            }
        }

        public void DeletePage(string pageLink)
        {
            WebDriverWait wait = new WebDriverWait(_driverManagePagePage, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(string.Format(_lnkPage, "Overview"))));
            GotoPage(pageLink);
            this.SelectGeneralSetting("Delete");
            IAlert alert = _driverManagePagePage.SwitchTo().Alert();
            alert.Accept();
        }

        public bool IsPageNavigated(string pageName)
        {
            bool isPageNavigated = false;
            WebDriverWait wait = new WebDriverWait(_driverManagePagePage, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(string.Format(_lnkPage, "Overview"))));
            string actualTitle = _driverManagePagePage.Title.Substring(_driverManagePagePage.Title.IndexOf("-") + 1,
                    _driverManagePagePage.Title.Length - _driverManagePagePage.Title.IndexOf("-") - 1).Trim();
            if (actualTitle == pageName)
            {
                isPageNavigated = true;
            }
            return isPageNavigated;
        }
             
        public string GetAlertMessage(bool closeAlert = false)
        {
            bool foundAlert = this.IsAlertDisplayed();

            if (foundAlert == true)
            {
                IAlert alert = _driverManagePagePage.SwitchTo().Alert();
                string alertMessage = alert.Text;
                if (closeAlert == true)
                {
                    alert.Accept();
                    alertMessage = null;
                }
                return alertMessage;
            }
            else
            {
                string alertMessage = null;
                return alertMessage;
            }
        }

        public void EditPageInfomation(string pageName = null, string parentPage = null, int numberOfColumn = 0, string displayAfer = null, bool publicCheckBox = false)
        {
            if (pageName != null)
            {
                TxtNewPagePageName.Clear();
                TxtNewPagePageName.SendKeys(pageName);
            }
            if (parentPage != null)
            {
                CmbParentPage.SelectItem(parentPage);
            }

            if (numberOfColumn != 0)
            {
                CmbNewPageDisplayAfter.SelectItem(numberOfColumn.ToString());
            }

            if (displayAfer != null)
            {
                CmbNewPageDisplayAfter.SelectItem(displayAfer);
            }

            if (publicCheckBox != false)
            {
                ChbPublic.Check();
            }
            else
            {
                ChbPublic.UnCheck();
            }

            BtnPageOK.Click();

            WebDriverWait wait = new WebDriverWait(_driverManagePagePage, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(string.Format(_lnkPage, "Overview"))));
        }

        public void SelectPage(string path)
        {
            By parent = By.XPath("//a[.='" + path + "']");
            IWebElement lnkParent = _driverManagePagePage.FindElement(parent);
            lnkParent.Click();
        }

        public bool IsPageNextToPage(string currentPage, string nextPage)
        {
            bool isPageNextToPage = false;
            By current = By.XPath("//a[.='" + nextPage + "']/parent::*/preceding-sibling::*/a[.='" + currentPage + "']");
            if (_driverManagePagePage.FindElement(current).Text == currentPage)
            {
                isPageNextToPage = true;
            }

                return isPageNextToPage; 
        }

        public bool DoesPageExist(string pageLink)
        {
            WebDriverWait wait = new WebDriverWait(_driverManagePagePage, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(string.Format(_lnkPage, "Overview"))));
            bool doesPageExist = false;
            string[] pages = Regex.Split(pageLink, "->");     
            if (pages.Length == 1)
            {
                By page = By.XPath("//a[.='" + pages[0] + "']");
                doesPageExist = this.isElementExist(page);
            }
            else
            {
                int pageIndex = 0;
                while (pageIndex + 1 < pages.Length)
                {
                    By page = By.XPath("//a[.='" + pages[pageIndex] + "']");
                    IWebElement lnkParent = _driverManagePagePage.FindElement(page);
                    lnkParent.MouseTo(_driverManagePagePage);
                    pageIndex = pageIndex + 1;
                    page = By.XPath("//a[.='" + pages[pageIndex] + "']");
                    doesPageExist = this.isElementExist(page);
                }
            }

            return doesPageExist;
        }

        //public void CheckPageNextToPage(string currentPage, string nextPage)
        //{
        //    By next = By.XPath("//a[.='" + currentPage + "']/following::a[1]");
        //    string nextValue = _driverManagePagePage.FindElement(next).Text;
        //    Assert.AreEqual(nextPage, nextValue, "\nExpected: " + nextPage + "\nActual: " + nextValue);
        //}

        //public void CheckPageNotExist(string parentPage, string childPage = null)
        //{
        //    By page = By.XPath("//a[.='" + parentPage + "']");

        //    if (childPage != null)
        //    {
        //        IWebElement lnkParent = _driverManagePagePage.FindElement(page);
        //        lnkParent.MouseTo(_driverManagePagePage);
        //        page = By.XPath("//a[.='" + childPage + "']");

        //    }

        //    bool isExist = this.isElementExist(page);
        //    Assert.AreEqual(false, isExist, "\nPage is exist");
        //}

        //public void CheckPageVisible(string pageName)
        //{
        //    By page = By.XPath("//a[.='" + pageName + "']");
        //    IWebElement lnkPage = _driverManagePagePage.FindElement(page);

        //    Assert.AreEqual(true, lnkPage.Displayed, "Page: " + pageName + " is invisible and can not be accessed");
        //}

        public bool DoesPopupExist(string headerName)
        {
            bool doesPopupExist = false;
            Thread.Sleep(1000);
            if (headerName == DlgPopupHeader.Text)
            {
                doesPopupExist = true;
            }
            return doesPopupExist;
        }
        
        //public void CheckPopupHeader(string headerName)
        //{
        //    Thread.Sleep(1000);

        //    Assert.AreEqual(headerName, DlgPopupHeader.Text, "\nExpected: " + headerName + "\nActual: " + DlgPopupHeader.Text);
        //}

        #endregion
    }
}
