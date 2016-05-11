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

namespace SeleniumAdvance.PageObjects
{
    public class LoginPage : GeneralPage
    {
        private IWebDriver _driver;

        #region Locators

        static readonly By _txtUsername = By.XPath("//input[@id='username']");
        static readonly By _txtPassword = By.XPath("//input[@id='password']");
        static readonly By _btnLogin = By.XPath("//div[@class='btn-login']");
        static readonly By _cmbRepo = By.XPath("//select[@id='repository']");

        #endregion

        #region Elements
        public IWebElement TxtUsername
        {
            get { return _driver.FindElement(_txtUsername); }
        }

        public IWebElement TxtPassword
        {
            get { return _driver.FindElement(_txtPassword); }
        }

        public IWebElement BtnLogin
        {
            get { return _driver.FindElement(_btnLogin); }
        }

        public IWebElement CmbRepo
        {
            get { return _driver.FindElement(_cmbRepo); }
        }

        #endregion

        #region Methods

        public LoginPage(IWebDriver driver)
        {
            this._driver = driver;
        }

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
            _driver.Navigate().GoToUrl(Constant.HomePageURL);
            return this;
        }
        public string GetAlertMessage()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = _driver.SwitchTo().Alert();
            return alert.Text;
        }

        #endregion
    }
}
