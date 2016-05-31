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
using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace SeleniumAdvance.PageObjects
{
    public class PanelConfigurationPage : GeneralPage
    {
        private IWebDriver _driverPanelConfig;

        #region Locators

        static readonly By _cmbSelectpage = By.XPath("//select[@id='cbbPages']");
        static readonly By _btnCancel = By.XPath("//input[@id='Cancel']");
        static readonly By _txtHeight = By.XPath("//input[@id='txtHeight']");
        static readonly By _txtFolder = By.XPath("//input[@id='txtFolder']");
        static readonly By _btnOk = By.XPath("//input[@id='OK']");

        #endregion

        #region Elements

        public IWebElement CmbSelectpage
        {
            get { return _driverPanelConfig.FindElement(_cmbSelectpage); }
        }
        public IWebElement BtnCancel
        {
            get { return _driverPanelConfig.FindElement(_btnCancel); }
        }
        public IWebElement TxtHeight
        {
            get { return _driverPanelConfig.FindElement(_txtHeight); }
        }
        public IWebElement TxtFolder
        {
            get { return _driverPanelConfig.FindElement(_txtFolder); }
        }
        public IWebElement BtnOk
        {
            get { return _driverPanelConfig.FindElement(_btnOk); }
        }
        #endregion

        #region Methods

        public PanelConfigurationPage(IWebDriver driver)
            : base(driver)
        {
            this._driverPanelConfig = driver;
        }

        public PanelConfigurationPage ChoosePanel(string panelName)
        {
            this.ClickTextLink(panelName.Replace(" ", "\u00A0"));

            WebDriverWait wait = new WebDriverWait(_driverPanelConfig, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(_txtHeight));

            //Thread.Sleep(2000);
            return this;
        }

        #endregion
    }
}
