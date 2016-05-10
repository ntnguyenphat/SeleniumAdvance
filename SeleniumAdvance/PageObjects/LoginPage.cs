using OpenQA.Selenium;
using SeleniumAdvance.Common;
using SeleniumAdvance.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumAdvance.Ultilities;

namespace SeleniumAdvance.PageObjects
{
    public class LoginPage : GeneralPage
    {
        #region Locators

        static readonly By _txtUsername = By.XPath("//input[@id='username']");
        static readonly By _txtPassword = By.XPath("//input[@id='password']");
        static readonly By _btnLogin = By.XPath("//div[@class='btn-login']");
        static readonly By _cmbRepo = By.XPath("//select[@id='repository']");

        #endregion

        #region Elements
        public IWebElement TxtUsername
        {
            get { return Constant.WebDriver.FindElement(_txtUsername); }
        }

        public IWebElement TxtPassword
        {
            get { return Constant.WebDriver.FindElement(_txtPassword); }
        }

        public IWebElement BtnLogin
        {
            get { return Constant.WebDriver.FindElement(_btnLogin); }
        }

        public IWebElement CmbRepo
        {
            get { return Constant.WebDriver.FindElement(_cmbRepo); }
        }

        #endregion

        #region Methods
        public GeneralPage Login(string username, string password)
        {
            TxtUsername.SendKeys(username);
            TxtPassword.SendKeys(password);
            BtnLogin.Click();
            return new GeneralPage();
        }

        public void SelectRepository(string repositoryName)
        {
            CmbRepo.SelectItem(repositoryName);
        }

        public LoginPage Open()
        {
            Constant.WebDriver.Navigate().GoToUrl(Constant.HomePageURL);
            return this;
        }

        #endregion
    }
}
