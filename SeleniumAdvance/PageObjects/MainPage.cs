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

        /// <summary>
        /// Add a new page.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        /// <param name="parentPage">Name of the parent page.</param>
        /// <param name="numberOfColumn">The number of column.</param>
        /// <param name="displayAfer">The page which the added page displays after.</param>
        /// <param name="publicCheckBox">Public/Unpublic page .</param>
        /// <Author>Phat</Author>
        /// <Modidified>Long : Handle White space in page's name. Re-write arguments so that the method can be re-used</Modidified>
        /// <Modified> Phat : Return page object instead of void</Modified>
        /// <returns></returns>
        public MainPage AddPage(string pageName, string parentPage = null, int numberOfColumn = 0, string displayAfer = null, bool publicCheckBox = false)
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
            wait.Until(ExpectedConditions.ElementExists(By.XPath(string.Format(_lnkPage, pageName).Replace(" ", "\u00A0"))));
            return this;
        }

        /// <summary>
        /// Go to a page.
        /// </summary>
        /// <param name="pageLink">The page link.</param>
        /// <Author>Long</Author>
        /// <Modified>Phat: Return page object instead of void</Modified>
        /// <returns></returns>
        public MainPage GotoPage(string pageLink)
        {
            string[] pages = Regex.Split(pageLink, "->");
            if (pages.Length == 1)
            {
                By page = By.XPath("//a[.='" + pages[0].Replace(" ", "\u00A0") + "']");
                IWebElement lnkPage = _driverManagePagePage.FindElement(page);
                lnkPage.Click();
            }
            else
            {
                int pageIndex = 0;
                while (pageIndex + 1 < pages.Length)
                {
                    By page = By.XPath("//a[.='" + pages[pageIndex].Replace(" ", "\u00A0") + "']");
                    IWebElement lnkParent = _driverManagePagePage.FindElement(page);
                    lnkParent.MouseTo(_driverManagePagePage);
                    pageIndex = pageIndex + 1;
                    page = By.XPath("//a[.='" + pages[pageIndex].Replace(" ", "\u00A0") + "']");
                    IWebElement lnkPage = _driverManagePagePage.FindElement(page);
                    if(pageIndex + 1 == pages.Length)
                    {
                          lnkPage.Click();
                    }
                }                            
            }
            return this;
        }

        /// <summary>
        /// Delete a page.
        /// </summary>
        /// <param name="pageLink">The page link.</param>
        /// <Author>Long</Author>
        /// <Modified>Phat: Return page object instead of void</Modified>
        /// <returns></returns>
        public MainPage DeletePage(string pageLink)
        {
            WebDriverWait wait = new WebDriverWait(_driverManagePagePage, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(string.Format(_lnkPage, "Overview"))));
            GotoPage(pageLink);
            this.SelectGeneralSetting("Delete");
            IAlert alert = _driverManagePagePage.SwitchTo().Alert();
            alert.Accept();
            return this;
        }

        /// <summary>
        /// Determines if the page is navigated.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        /// <Author>Long</Author>
        /// <returns></returns>
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

        /// <summary>
        /// Edit the information of a page
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        /// <param name="parentPage">Name of the parent page.</param>
        /// <param name="numberOfColumn">The number of column.</param>
        /// <param name="displayAfer">The page which the edited page displays after.</param>
        /// <param name="publicCheckBox">Public/Unpublic the edited page.</param>
        /// <Author>Phat</Author>
        /// <Modified> Long: Edit some arguments so that the method can be re-used in many TCs</Modified>
        /// <Modified> Phat: Return page object instead of void </Modified>
        /// <returns></returns>
        public MainPage EditPageInfomation(string pageName = null, string parentPage = null, int numberOfColumn = 0, string displayAfer = null, bool publicCheckBox = false)
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

            return this;
        }

        /// <summary>
        /// Determines if a page is next to another page.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <param name="nextPage">The next page.</param>
        /// <Author>Phat</Author>
        /// <Modified>Long: Edit the XPATH </Modified>
        /// <returns></returns>
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

        /// <summary>
        /// Determine if a page exists
        /// </summary>
        /// <param name="pageLink">The page link.</param>
        /// <Author>Long</Author>
        /// <returns></returns>
        public bool DoesPageExist(string pageLink)
        {
            WebDriverWait wait = new WebDriverWait(_driverManagePagePage, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(string.Format(_lnkPage, "Overview"))));
            bool doesPageExist = false;
            string[] pages = Regex.Split(pageLink, "->");     
            if (pages.Length == 1)
            {
                By page = By.XPath("//a[.='" + pages[0].Replace(" ", "\u00A0") + "']");
                doesPageExist = this.IsElementExist(page);
            }
            else
            {
                int pageIndex = 0;
                while (pageIndex + 1 < pages.Length)
                {
                    By page = By.XPath("//a[.='" + pages[pageIndex].Replace(" ", "\u00A0") + "']");
                    IWebElement lnkParent = _driverManagePagePage.FindElement(page);
                    lnkParent.MouseTo(_driverManagePagePage);
                    pageIndex = pageIndex + 1;
                    page = By.XPath("//a[.='" + pages[pageIndex].Replace(" ", "\u00A0") + "']");
                    doesPageExist = this.IsElementExist(page);
                }
            }

            return doesPageExist;
        }

        /// <summary>
        /// Determine if a popup exists
        /// </summary>
        /// <param name="headerName">Name of the header of the popup.</param>
        /// <Author>Phat</Author>
        /// <returns></returns>
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
        
        #endregion
    }
}
