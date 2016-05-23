using OpenQA.Selenium;
using SeleniumAdvance.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumAdvance.Ultilities;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace SeleniumAdvance.PageObjects
{
    public class GeneralPage
    {
        protected IWebDriver _driver;

        #region Locators

        static readonly By _lnkAccount = By.XPath("//a[@href='#Welcome']");
        static readonly By _lnkLogout = By.XPath("//a[@href='logout.do']");
        static readonly By _lblRepositoryName = By.XPath("//a[@href='#Repository']/span");
        static readonly By _tabSetting = By.XPath("//li[@class='mn-setting']");
        static string _lnkMainMenu = "//li[@class='sep']/parent::*/../a[contains(.,'{0}')]";
        static string _lnkSubMenu = "//li[@class='sep']/parent::*/../a[contains(.,'{0}')]/following::a[contains(.,'{1}')]";
        static string _lnkSettingItem = "//li[@class='mn-setting']//a[ .='{0}']";

        #endregion

        #region Elements
        public IWebElement LnkAccount
        {
            get { return _driver.FindElement(_lnkAccount); }
        }

        public IWebElement LnkLogout
        {
            get { return _driver.FindElement(_lnkLogout); }
        }

        public IWebElement LblRepositoryName
        {
            get { return _driver.FindElement(_lblRepositoryName); }
        }

        public IWebElement TabSetting
        {
            get { return _driver.FindElement(_tabSetting); }
        }

        #endregion

        #region Methods

        public GeneralPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        /// <summary>
        /// Determines if dashboard mainpage displayed].
        /// </summary>
        /// <returns></returns>
        public bool IsDashboardMainpageDisplayed()
        {
            bool foundDashboardMainpage = LnkAccount.Displayed;
            return foundDashboardMainpage;
        }

        /// <summary>
        /// Determines if alert dialog displayed].
        /// </summary>
        /// <returns></returns>
        public bool IsAlertDisplayed()
        {
            bool foundAlert = false;
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            try
            {
                wait.Until(ExpectedConditions.AlertIsPresent());
                foundAlert = true;
            }
            catch
            {
                foundAlert = false;
            }
            return foundAlert;
        }

        /// <summary>
        /// Log out from TA Dashboard page.
        /// </summary>
        /// <returns></returns>
        public LoginPage Logout()
        {
            LnkAccount.MouseTo(_driver);
            LnkLogout.Click();
            return new LoginPage(_driver);
        }

        public void SelectMenuItem(string mainMenu, string subMenu)
        {
            IWebElement LnkMainMenu = _driver.FindElement(By.XPath(string.Format(_lnkMainMenu, mainMenu)));
            IWebElement LnkSubMenu = _driver.FindElement(By.XPath(string.Format(_lnkSubMenu, mainMenu, subMenu)));
            LnkMainMenu.MouseTo(_driver);
            LnkSubMenu.Click();
        }

        /// <summary>
        /// Switch the repository which the user wants to work on
        /// </summary>
        /// <param name="repositoryName">Name of the repository.</param>
        /// <returns></returns>
        public GeneralPage ChooseRepository(string repositoryName)
        {
            SelectMenuItem("Repository", repositoryName);

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(string.Format(_lnkMainMenu, repositoryName))));
            return new GeneralPage(_driver);
        }

        /// <summary>
        /// Get the name of the repository.
        /// </summary>
        /// <returns></returns>
        public string GetRepositoryName()
        {
            return LblRepositoryName.Text;
        }

        /// <summary>
        /// Select settings of general setting menu.
        /// </summary>
        /// <param name="item">settings</param>
        public void SelectGeneralSetting(string item)
        {
            TabSetting.MouseTo(_driver);
            IWebElement settingItem = _driver.FindElement(By.XPath(string.Format(_lnkSettingItem, item)));
            settingItem.Click();
        }

        /// <summary>
        /// Determines if dashboard is locked by dialog].
        /// </summary>
        /// <returns></returns>
        public bool IsDashboardLockedByDialog()
        {
            return TabSetting.Enabled;
        }

        /// <summary>
        /// Determines if element exists.
        /// </summary>
        /// <param name="locatorKey">locator key.</param>
        /// <returns></returns>
        public bool IsElementExist(By locatorKey)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            try
            {
                wait.Until(ExpectedConditions.ElementExists(locatorKey));
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the message of the alert dialog.
        /// </summary>
        /// <param name="closeAlert">close alert.</param>
        /// <returns></returns>
        public string GetAlertMessage(bool closeAlert = false)
        {
            bool foundAlert = this.IsAlertDisplayed();

            if (foundAlert == true)
            {
                IAlert alert = _driver.SwitchTo().Alert();
                string alertMessage = alert.Text;

                if (closeAlert == true)
                {
                    alert.Accept();
                    WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                    wait.Until(ExpectedConditions.ElementToBeClickable(_lnkAccount));
                }
                return alertMessage;
            }
            else
            {
                string alertMessage = null;
                return alertMessage;
            }
        }

        /// <summary>
        /// Wait for the element exist.
        /// </summary>
        /// <param name="locator">The locator.</param>
        public void WaitElementExist(By locator)
        {
            WebDriverWait wait = new WebDriverWait(_driver,TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(locator));
        }
        #endregion

    }
}
