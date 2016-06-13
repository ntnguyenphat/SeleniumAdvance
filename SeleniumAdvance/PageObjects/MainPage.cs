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
        private IWebDriver _driverMainPage;

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
            get { return MyFindElement(_txtNewPagePageName); }
        }

        public IWebElement BtnPageOK
        {
            get { return MyFindElement(_btnPageOK); }
        }

        public IWebElement BtnPageCancel
        {
            get { return MyFindElement(_btnPageCancel); }
        }

        public IWebElement CmbNewPageDisplayAfter
        {
            get { return MyFindElement(_cmbPageDisplayAfter); }
        }

        public IWebElement CmbParentPage
        {
            get { return MyFindElement(_cmbParentPage); }
        }

        public IWebElement CmbNumberOfColumns
        {
            get { return MyFindElement(_cbmNumberOfColumns); }
        }

        public IWebElement ChbPublic
        {
            get { return MyFindElement(_chbPublic); }
        }

        public IWebElement DlgPopupHeader
        {
            get { return MyFindElement(_dlgPopupHeader); }
        }

        #endregion

        #region Methods

        public MainPage(IWebDriver driver)
            : base(driver)
        {
            this._driverMainPage = driver;
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
                CmbNewPageDisplayAfter.SelectItem(numberOfColumn.ToString(),"Index");
            }

            if (displayAfer != null)
            {
                CmbNewPageDisplayAfter.SelectItem(displayAfer);
            }

            if (publicCheckBox != false)
            {
                ChbPublic.Check();
            }

            BtnPageOK.Click();

            WebDriverWait wait = new WebDriverWait(_driverMainPage, TimeSpan.FromSeconds(Constant.TimeOut));
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
                IWebElement lnkPage = MyFindElement(page);
                lnkPage.Click();
            }
            else
            {
                int pageIndex = 0;
                while (pageIndex + 1 < pages.Length)
                {
                    By page = By.XPath("//a[.='" + pages[pageIndex].Replace(" ", "\u00A0") + "']");
                    IWebElement lnkParent = MyFindElement(page);
                    lnkParent.MouseTo(_driverMainPage);
                    pageIndex = pageIndex + 1;
                    page = By.XPath("//a[.='" + pages[pageIndex].Replace(" ", "\u00A0") + "']");
                    IWebElement lnkPage = MyFindElement(page);
                    if (pageIndex + 1 == pages.Length)
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
        /// <Modified>Long: Wait after deleting a page - 27/05/2016</Modified>
        /// <returns></returns>
        public MainPage DeletePage(string pageLink)
        {
            GotoPage(pageLink);
            this.SelectGeneralSetting("Delete");
            IAlert alert = _driverMainPage.SwitchTo().Alert();
            alert.Accept();
            string[] pages = Regex.Split(pageLink, "->");
            WebDriverWait wait = new WebDriverWait(_driverMainPage, TimeSpan.FromSeconds(Constant.TimeOut));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//a[.='" + pages[(pages.Length - 1)].Replace(" ", "\u00A0") + "']")));
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
            WebDriverWait wait = new WebDriverWait(_driverMainPage, TimeSpan.FromSeconds(Constant.TimeOut));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(string.Format(_lnkPage, "Overview"))));
            string actualTitle = _driverMainPage.Title.Substring(_driverMainPage.Title.IndexOf("-") + 1,
                    _driverMainPage.Title.Length - _driverMainPage.Title.IndexOf("-") - 1).Trim();
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
                CmbNumberOfColumns.SelectItem(numberOfColumn.ToString(),"Index");
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

            WebDriverWait wait = new WebDriverWait(_driverMainPage, TimeSpan.FromSeconds(Constant.TimeOut));
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
            By current = By.XPath("//a[.='" + nextPage.Replace(" ", "\u00A0") + "']/parent::*/preceding-sibling::*/a[.='" + currentPage.Replace(" ", "\u00A0") + "']");
            if (MyFindElement(current).Text == currentPage)
            {
                isPageNextToPage = true;
            }

            return isPageNextToPage;
        }

        /// <summary>
        /// Determine if a page exists
        /// </summary>
        /// <param name="pageLink">The page link.</param>
        /// <Author>Phat</Author>
        /// <returns></returns>
        public bool DoesPageExist(string pageLink)
        {
            WebDriverWait wait = new WebDriverWait(_driverMainPage, TimeSpan.FromSeconds(Constant.TimeOut));
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
                    IWebElement lnkParent = MyFindElement(page);
                    lnkParent.MouseTo(_driverMainPage);
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

        /// <summary>
        /// Determines whether Panel is created in page.
        /// </summary>
        /// <param name="panelName">Name of the panel.</param>
        /// <returns></returns>
        /// <Author>Phat</Author>
        /// <Startdate>04/06/2016</Startdate>
        public bool IsPanelCreatedInMainPage(string panelName)
        {
            panelName = panelName.Replace(" ", "\u00A0");
            By xpath = By.XPath("//div[contains(@id,'widget_head_panel')]//div[.='" + panelName + "']");
            return this.IsElementExist(xpath);
        }

        #endregion
    }
}
