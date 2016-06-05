using OpenQA.Selenium;
using SeleniumAdvance.Common;
using System;
using SeleniumAdvance.PageObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumAdvance.Ultilities;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Collections.ObjectModel;

namespace SeleniumAdvance.PageObjects
{
    public class GeneralPage : PageBase
    {
        protected IWebDriver _driverGeneralPage;

        #region Locators

        static readonly By _lnkAccount = By.XPath("//a[@href='#Welcome']");
        static readonly By _lnkLogout = By.XPath("//a[@href='logout.do']");
        static readonly By _lblRepositoryName = By.XPath("//a[@href='#Repository']/span");
        static readonly By _tabSetting = By.XPath("//li[@class='mn-setting']");
        static readonly By _btnChoosePanels = By.XPath("//a[@id='btnChoosepanel']");
        static string _lnkMainMenu = "//li[@class='sep']/parent::*/../a[contains(.,'{0}')]";
        static string _lnkSubMenu = "//li[@class='sep']/parent::*/../a[contains(.,'{0}')]/following::a[contains(.,'{1}')]";
        static string _lnkSettingItem = "//li[@class='mn-setting']//a[ .='{0}']";
        //static string _cbbName = "//select[contains(@id,'cbb{0}')]";
        static string _cbbName = "//td[contains(text(), '{0}')]/following-sibling::*/descendant::select";

        #endregion

        #region Elements
        public IWebElement LnkAccount
        {
            get { return MyFindElement(_lnkAccount); }
        }

        public IWebElement LnkLogout
        {
            get { return MyFindElement(_lnkLogout); }
        }

        public IWebElement LblRepositoryName
        {
            get { return MyFindElement(_lblRepositoryName); }
        }

        public IWebElement TabSetting
        {
            get { return MyFindElement(_tabSetting); }
        }

        public IWebElement BtnChoosePanels
        {
            get { return MyFindElement(_btnChoosePanels); }
        }

        #endregion

        #region Methods

        public GeneralPage(IWebDriver driver) : base(driver)
        {
            this._driverGeneralPage = driver;
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
        /// </summary>
        /// <Author>Long and Phat</Author>
        /// <returns></returns>
        public bool IsAlertDisplayed()
        {
            bool foundAlert = false;
            WebDriverWait wait = new WebDriverWait(_driverGeneralPage, TimeSpan.FromSeconds(5));
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
        /// <Author>Long and Phat</Author>
        /// <returns></returns>
        public LoginPage Logout()
        {
            LnkAccount.MouseTo(_driverGeneralPage);
            LnkLogout.Click();
            return new LoginPage(_driverGeneralPage);
        }

        /// <summary>
        /// Selects the menu item.
        /// </summary>
        /// <param name="mainMenu">The main menu.</param>
        /// <param name="subMenu">The sub menu.</param>
        /// <Author>Long and Phat</Author>
        public void SelectMenuItem(string mainMenu, string subMenu)
        {
            IWebElement LnkMainMenu = MyFindElement(By.XPath(string.Format(_lnkMainMenu, mainMenu)));
            LnkMainMenu.MouseTo(_driverGeneralPage);
            IWebElement LnkSubMenu = MyFindElement(By.XPath(string.Format(_lnkSubMenu, mainMenu, subMenu)));
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
            WebDriverWait wait = new WebDriverWait(_driverGeneralPage, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(string.Format(_lnkMainMenu, repositoryName))));
            return new GeneralPage(_driverGeneralPage);
        }

        /// <summary>
        /// Get the name of the repository.
        /// </summary>
        /// <Author>Long and Phat</Author>
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
            TabSetting.MouseTo(_driverGeneralPage);
            IWebElement settingItem = MyFindElement(By.XPath(string.Format(_lnkSettingItem, item)));
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
            WebDriverWait wait = new WebDriverWait(_driverGeneralPage, TimeSpan.FromSeconds(5));
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
                IAlert alert = _driverGeneralPage.SwitchTo().Alert();
                string alertMessage = alert.Text;

                if (closeAlert == true)
                {
                    alert.Accept();
                    WebDriverWait wait = new WebDriverWait(_driverGeneralPage, TimeSpan.FromSeconds(5));
                    wait.Until(ExpectedConditions.ElementExists(_lnkAccount));
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
            WebDriverWait wait = new WebDriverWait(_driverGeneralPage,TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(locator));
        }

        /// <summary>
        /// Unhide Choose Panels page.
        /// </summary>
        /// <Author>Long</Author>
        /// <Startdate>25/05/2016</Startdate>
        public void UnhideChoosePanelsPage()
        {
            string statusOfChoosePanelsButton = BtnChoosePanels.GetAttribute("class");
            if(statusOfChoosePanelsButton != "selected")
            {
                BtnChoosePanels.Click();
            }
            WebDriverWait wait = new WebDriverWait(_driverGeneralPage, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='cpbutton']/span[.='Create new panel']")));
        }

        /// <summary>
        /// Hide Choose Panels page.
        /// </summary>
        /// <Author>Long</Author>
        /// <Startdate>25/05/2016</Startdate>
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
        /// <Startdate>26/05/2016</Startdate>
        /// <returns></returns>
        public bool IsItemPresentInCombobox(string comboboxName, string comboboxItem,string attribute = "value")
        {
            bool isItemPresentInCombobox = false;
            foreach (IWebElement item in MyFindElements(By.XPath(string.Format(_cbbName + "/descendant::*",comboboxName))))
            {
                if (attribute == "value")
                {
                    if (item.GetAttribute("value") == comboboxItem)
                    {
                        isItemPresentInCombobox = true;
                    }
                }
                else if(attribute == "text")
                {
                    if (item.Text.Trim() == comboboxItem)
                    {
                        isItemPresentInCombobox = true;
                    }
                }
            }
            return isItemPresentInCombobox;
        }
        /// <summary>
        /// Get the number of items in ComboBox.
        /// </summary>
        /// <param name="comboboxName">Name of the combobox.</param>
        /// <Author>Long</Author>
        /// <Startdate>26/05/2016</Startdate>
        /// <returns></returns>
        public int GetNumberOfItemsInCombobox(string comboboxName)
        {
            IWebElement Combo = MyFindElement(By.XPath(string.Format(_cbbName, comboboxName)));
            SelectElement ListBox = new SelectElement(Combo);
            int numberOfItems = ListBox.Options.Count();
            return numberOfItems;
        }

        /// <summary>
        /// Get the selected item of combobox.
        /// </summary>
        /// <param name="comboboxName">Name of the combobox.</param>
        /// <Author>Long</Author>
        /// <Startdate>27/05/2016</Startdate>
        /// <returns></returns>
        public string GetSelectedItemOfCombobox(string comboboxName)
        {
            IWebElement Combo = MyFindElement(By.XPath(string.Format(_cbbName, comboboxName)));
            SelectElement ListBox = new SelectElement(Combo);
            string selectedItem = ListBox.SelectedOption.Text.Trim();
            return selectedItem;
        }

        /// <summary>
        /// Clicks the text link.
        /// </summary>
        /// <param name="linkText">The link text.</param>
        /// <param name="exactly">if set to <c>true</c> [exactly].</param>
        /// <Author>Phat</Author>
        /// <Startdate>31/05/2016</Startdate>
        public void ClickTextLink(string linkText, bool exactly = true)
        {
            By xpath;
            if (exactly == true)
                xpath = By.XPath("//a[.='" + linkText + "']");
            else
                xpath = By.XPath("//a[contains(.,'" + linkText + "')]");

           MyFindElement(xpath).Click();
        }

         public int GetItemPositionInCombobox(string comboboxName, string comboboxItem)
        {
             int itemPosition = -1;
             if (IsItemPresentInCombobox(comboboxName, comboboxItem, "value") == false && IsItemPresentInCombobox(comboboxName, comboboxItem, "text") == false)
                 return -1;
             else
             {
                 foreach (IWebElement item in MyFindElements(By.XPath(string.Format(_cbbName + "/descendant::*", comboboxName))))
                 {
                     if (item.GetAttribute("value") == comboboxItem || item.Text.Trim() == comboboxItem)
                         itemPosition = Int32.Parse(item.GetAttribute("index"));
                 }
             }
             return itemPosition;
        }

        #endregion 
    }
}
