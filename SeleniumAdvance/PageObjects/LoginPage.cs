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
        private IWebDriver _driverLoginPage;

        #region Locators

        static readonly By _txtUsername = By.XPath("//input[@id='username']");
        static readonly By _txtPassword = By.XPath("//input[@id='password']");
        static readonly By _btnLogin = By.XPath("//div[@class='btn-login']");
        static readonly By _cmbRepo = By.XPath("//select[@id='repository']");

        #endregion

        #region Elements
        public IWebElement TxtUsername
        {
            get { return MyFindElement(_txtUsername); }
        }

        public IWebElement TxtPassword
        {
            get { return MyFindElement(_txtPassword); }
        }

        public IWebElement BtnLogin
        {
            get { return MyFindElement(_btnLogin); }
        }

        public IWebElement CmbRepo
        {
            get { return MyFindElement(_cmbRepo); }
        }

        #endregion

        #region Methods

        public LoginPage(IWebDriver driver) : base(driver)
        {
            this._driverLoginPage = driver;
        }

        /// <summary>
        /// Login to TA Dashboard page
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="repositoryName">Name of the repository.</param>
        /// <Author>Long and Phat</Author>
        /// <returns></returns>
        public MainPage Login(string username, string password, string repositoryName = null)
        {
            if (repositoryName != null)
            {
                CmbRepo.SelectItem(repositoryName);
            }
            TxtUsername.SendKeys(username);
            TxtPassword.SendKeys(password);
            BtnLogin.Click();
            return new MainPage(_driverGeneralPage);
        }

        /// <summary>
        /// Opens Login page of TA Dashboard page
        /// <Author>Long and Phat</Author>
        /// </summary>
        /// <returns></returns>
        public LoginPage Open()
        {
            _driverLoginPage.Navigate().GoToUrl(Constant.HomePageURL);
            return this;
        }

        /// <summary>
        /// Get the message of the alert dialog.
        /// </summary>
        /// <Author>Long and Phat</Author>
        /// <returns></returns>
        public string GetAlertMessage()
        {
            WebDriverWait wait = new WebDriverWait(_driverLoginPage, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = _driverLoginPage.SwitchTo().Alert();
            return alert.Text;
        }

        #endregion
    }
}
