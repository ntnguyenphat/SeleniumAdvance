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
            get { return Constant.WebDriver.FindElement(_lnkAccount); }
        }

        public IWebElement LnkLogout
        {
            get { return Constant.WebDriver.FindElement(_lnkLogout); }
        }

        public IWebElement LblRepositoryName
        {
            get { return Constant.WebDriver.FindElement(_lblRepositoryName); }
        }

        public IWebElement TabSetting
        {
            get { return Constant.WebDriver.FindElement(_tabSetting); }
        }

        #endregion

        #region Methods

        public bool IsDashboardMainpageDisplayed()
        {
            bool foundDashboardMainpage = LnkAccount.Displayed;
            return foundDashboardMainpage;
        }

        public bool IsAlertDisplayed()
        {
            bool foundAlert = false;
            WebDriverWait wait = new WebDriverWait(Constant.WebDriver, TimeSpan.FromSeconds(5));
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

        public LoginPage Logout()
        {
            LnkAccount.MouseTo(Constant.WebDriver);
            LnkLogout.Click();
            return new LoginPage();
        }

        public void SelectMenuItem(string mainMenu, string subMenu)
        {
            IWebElement LnkMainMenu = Constant.WebDriver.FindElement(By.XPath(string.Format(_lnkMainMenu, mainMenu)));
            IWebElement LnkSubMenu = Constant.WebDriver.FindElement(By.XPath(string.Format(_lnkSubMenu, mainMenu, subMenu)));
            LnkMainMenu.MouseTo(Constant.WebDriver);
            LnkSubMenu.Click();
        }

        public GeneralPage ChooseRepository(string repositoryName)
        {
            SelectMenuItem("Repository", repositoryName);

            WebDriverWait wait = new WebDriverWait(Constant.WebDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(string.Format(_lnkMainMenu, repositoryName))));

            //Thread.Sleep(1000);
            return new GeneralPage();
        }

        public string GetRepositoryName()
        {
            return LblRepositoryName.Text;
        }

        public void SelectGeneralSetting(string item)
        {
            TabSetting.MouseTo(Constant.WebDriver);
            IWebElement settingItem = Constant.WebDriver.FindElement(By.XPath(string.Format(_lnkSettingItem, item)));
            settingItem.Click();
        }
        
        public bool IsDashboardLockedByDialog()
        {
            return TabSetting.Enabled;
        }   

        #endregion

        
    }
}
