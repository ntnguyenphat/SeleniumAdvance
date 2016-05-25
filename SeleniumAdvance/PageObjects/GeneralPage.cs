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
        static readonly By _btnChoosePanels = By.XPath("//a[@id='btnChoosepanel']");
        static string _lnkMainMenu = "//li[@class='sep']/parent::*/../a[contains(.,'{0}')]";
        static string _lnkSubMenu = "//li[@class='sep']/parent::*/../a[contains(.,'{0}')]/following::a[contains(.,'{1}')]";
        static string _lnkSettingItem = "//li[@class='mn-setting']//a[ .='{0}']";
        static string _cbbName = "//select[contains(@id,'cbb{0}')]";


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

        public IWebElement BtnChoosePanels
        {
            get { return _driver.FindElement(_btnChoosePanels); }
        }

        #endregion

        #region Methods

        public GeneralPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        /// <summary>
        /// Determines if dashboard mainpage displayed].
        /// <Author>Long and Phat</Author>
        /// </summary>
        /// <returns></returns>
        public bool IsDashboardMainpageDisplayed()
        {
            bool foundDashboardMainpage = LnkAccount.Displayed;
            return foundDashboardMainpage;
        }

        /// <summary>
        /// Determines if alert dialog displayed].
        /// <Author>Long and Phat</Author>
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
        /// <Author>Long and Phat</Author>
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
        /// <Author>Long and Phat</Author>
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
        /// <Author>Long and Phat</Author>
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
        /// <Author>Long and Phat</Author>
        public void SelectGeneralSetting(string item)
        {
            TabSetting.MouseTo(_driver);
            IWebElement settingItem = _driver.FindElement(By.XPath(string.Format(_lnkSettingItem, item)));
            settingItem.Click();
        }

        /// <summary>
        /// Determines if dashboard is locked by dialog].
        /// </summary>
        /// <Author>Long and Phat</Author>
        /// <returns></returns>
        public bool IsDashboardLockedByDialog()
        {
            return TabSetting.Enabled;
        }

        /// <summary>
        /// Determines if element exists.
        /// </summary>
        /// <param name="locatorKey">locator key.</param>
        /// <Author>Long and Phat</Author>
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
        /// <Author>Long and Phat</Author>
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
        /// <Author>Phat</Author>
        public void WaitElementExist(By locator)
        {
            WebDriverWait wait = new WebDriverWait(_driver,TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(locator));
        }

        /// <summary>
        /// Unhide Choose Panels page.
        /// </summary>
        /// <Author>Long</Author>
        /// <Created date>25/05/2016</Created>
        public void UnhideChoosePanelsPage()
        {
            string statusOfChoosePanelsButton = BtnChoosePanels.GetAttribute("class");
            if(statusOfChoosePanelsButton != "selected")
            {
                BtnChoosePanels.Click();
            }
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='cpbutton']/span[.='Create new panel']")));
        }

        /// <summary>
        /// Hide Choose Panels page.
        /// </summary>
        /// <Author>Long</Author>
        /// <Created date>25/05/2016</Created>
        public void HideChoosePanelsPage()
        {
            string statusOfChoosePanelsButton = BtnChoosePanels.GetAttribute("class");
            if (statusOfChoosePanelsButton == "selected")
            {
                BtnChoosePanels.Click();
            }
        }

        /// <summary>
        /// Determines if an item exist in Combobox.
        /// </summary>
        /// <param name="comboboxName">Name of the combobox.</param>
        /// <param name="comboboxItem">The combobox item.</param>
        /// <Author>Long</Author>
        /// <Created date>26/05/2016</Created>
        /// <returns></returns>
        public bool IsItemPresentInCombobox(string comboboxName, string comboboxItem)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(string.Format(_cbbName, comboboxName).Replace(" ", ""))));
            bool isItemPresentInCombobox = false;
            var combo = _driver.FindElement(By.XPath(string.Format(_cbbName,comboboxName).Replace(" ","")));
            foreach (var item in combo.FindElements(By.TagName("option")))
            {
                if (item.GetAttribute("value") == comboboxItem)
                {
                    isItemPresentInCombobox = true;
                }
            }
            return isItemPresentInCombobox;
        }

        #endregion

        /// <summary>
        /// Get the number of items in ComboBox.
        /// </summary>
        /// <param name="comboboxName">Name of the combobox.</param>
        /// <Author>Long</Author>
        /// <Created date>26/05/2016</Created>
        /// <returns></returns>
        public int GetNumberOfItemsInComboBox(string comboboxName)
        {
            IWebElement combo = _driver.FindElement(By.XPath(string.Format(_cbbName, comboboxName).Replace(" ", "")));
            SelectElement listBox = new SelectElement(combo);
            int numberOfItems = listBox.Options.Count();
            return numberOfItems;
        }

    }
}
