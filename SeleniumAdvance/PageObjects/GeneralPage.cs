using OpenQA.Selenium;
using SeleniumAdvance.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumAdvance.Ultilities;

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
        public LoginPage Logout()
        {
            LnkAccount.MouseTo(Constant.WebDriver);
            LnkLogout.Click();
            return new LoginPage();
        }

        public void SelectMenuItem(string mainMenu, string subMenu)
        {
            //Todo: Create an method to select main menu > sub menu in Dashboard (Need to customize arguments)
            IWebElement LnkMainMenu = Constant.WebDriver.FindElement(By.XPath(string.Format(_lnkMainMenu, mainMenu)));
            IWebElement LnkSubMenu = Constant.WebDriver.FindElement(By.XPath(string.Format(_lnkSubMenu, mainMenu, subMenu)));
            LnkMainMenu.MouseTo(Constant.WebDriver);
            LnkSubMenu.Click();
        }

        public string GetRepositoryName()
        {
            return LblRepositoryName.Text;
        }
        #endregion
    }
}
