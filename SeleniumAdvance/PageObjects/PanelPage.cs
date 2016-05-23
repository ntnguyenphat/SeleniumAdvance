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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Text.RegularExpressions;

namespace SeleniumAdvance.PageObjects
{
    public class PanelPage : GeneralPage
    {
        private IWebDriver _driverPanelPage;

        #region Locators

        static readonly By _tabDisplaySetting = By.XPath("//a[@href='#tabs-displaySettings']");
        static readonly By _tabFilter = By.XPath("//a[@href='#tabs-data']");
        static readonly By _rdChart = By.XPath("//label[contains(.,'Chart')]/input[contains(@id,'radPanelType')]");
        static readonly By _rdIndicator = By.XPath("//label[contains(.,'Indicator')]/input[contains(@id,'radPanelType')]");
        static readonly By _rdReport = By.XPath("//label[contains(.,'Report')]/input[contains(@id,'radPanelType')]");
        static readonly By _rdHeatMap = By.XPath("//label[contains(.,'Heat Map')]/input[contains(@id,'radPanelType')]");
        static readonly By _chbDataProfile = By.XPath("//select[@id='cbbProfile']");
        static readonly By _txtDisplayName = By.XPath("//input[@id='txtDisplayName']");
        static readonly By _btnOK = By.XPath("//input[@id='OK']");
        static readonly By _btnCancel = By.XPath("//input[@id='Cancel']");
        static readonly By _lnkAddNew = By.XPath("//a[contains(@href,'openAddPanel')]");
        static readonly By _chbSeries = By.XPath("//select[@id='cbbSeriesField']");
        static readonly By _lblSettingHeader = By.XPath("//fieldset[@id='fdSettings']/legend");

        //static readonly By _chbStatistic = By.XPath("//select[@id='cbbStatField']");

        #endregion

        #region Elements
        public IWebElement TabDisplaySetting
        {
            get { return _driver.FindElement(_tabDisplaySetting); }
        }
        public IWebElement TabFilter
        {
            get { return _driver.FindElement(_tabFilter); }
        }
        public IWebElement RdChart
        {
            get { return _driver.FindElement(_rdChart); }
        }
        public IWebElement RdIndicator
        {
            get { return _driver.FindElement(_rdIndicator); }
        }
        public IWebElement RdReport
        {
            get { return _driver.FindElement(_rdReport); }
        }
        public IWebElement RdHeatMap
        {
            get { return _driver.FindElement(_rdHeatMap); }
        }
        public IWebElement ChbDataProfile
        {
            get { return _driver.FindElement(_chbDataProfile); }
        }
        public IWebElement TxtDisplayName
        {
            get { return _driver.FindElement(_txtDisplayName); }
        }
        public IWebElement BtnOK
        {
            get { return _driver.FindElement(_btnOK); }
        }
        public IWebElement BtnCancel
        {
            get { return _driver.FindElement(_btnCancel); }
        }
        public IWebElement LnkAddNew
        {
            get { return _driver.FindElement(_lnkAddNew); }
        }
        public IWebElement ChbSeries
        {
            get { return _driver.FindElement(_chbSeries); }
        }
        public IWebElement LblSettingHeader
        {
            get { return _driver.FindElement(_lblSettingHeader); }
        }
        #endregion

        #region Methods

        public PanelPage(IWebDriver driver) : base(driver)
        {
            this._driverPanelPage = driver;
        }

        public bool IsProfileExist(string profileName)
        {
            return ChbDataProfile.IsItemExist(profileName);
        }

        public bool IsPanelCreated(string panelName)
        {
            By panel = By.XPath("//a[.='" + panelName + "']");
            return this.IsElementExist(panel);
        }

        public string GetSettingHeader()
        {
            return LblSettingHeader.Text;
        }
        #endregion
    }
}
