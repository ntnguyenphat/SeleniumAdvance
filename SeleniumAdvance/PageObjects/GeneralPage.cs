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

        static string _lnkAccount = "//a[@href='#Welcome' and .='{0}']";
        static string _lnkMainMenu = "//li[@class='sep']/parent::*/../a[contains(.,'{0}')]";
        static string _lnkSubMenu =  "//li[@class='sep']/parent::*/../a[contains(.,'{0}')]/following::a[contains(.,'{1}')]";
        static readonly By _lnkLogout = By.XPath("//a[@href='logout.do']");
        static readonly By _lblRepositoryName = By.XPath("//a[@href='#Repository']/span");
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

        public IWebElement LnkMainMenu
        {
            get { return Constant.WebDriver.FindElement(By.XPath(string.Format(_lnkAccount, Constant.Username))); }
        }

        public IWebElement LblRepositoryName
        {
            get { return Constant.WebDriver.FindElement(_lblRepositoryName); }
        }
        #endregion

        #region Methods

        public bool IsDashboardMainpageDisplayed()
        {
            bool foundDashboardMainpage = LnkAccount.Displayed;
            return foundDashboardMainpage;
        }

        public bool isDialogDisplayed()
        {
            bool foundDialog = false;
            WebDriverWait wait = new WebDriverWait(Constant.WebDriver, TimeSpan.FromSeconds(5));
            if (foundDialog)
            {
                wait.Until(ExpectedConditions.AlertIsPresent());
                foundDialog = true;
            }
            else
            {
                foundDialog = false;
            }
            return foundDialog;
        }

        public LoginPage Logout()
        {
            LnkAccount.MouseTo(Constant.WebDriver);
            LnkLogout.Click();
            return new LoginPage();
        }

        public string GetDialogText()
        {
            WebDriverWait wait = new WebDriverWait(Constant.WebDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = Constant.WebDriver.SwitchTo().Alert();
            return alert.Text;
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
            Thread.Sleep(1000);
            return new GeneralPage();
        }

        public string GetRepositoryName()
        {
            return LblRepositoryName.Text;
        }

        #endregion

        
    }
}
