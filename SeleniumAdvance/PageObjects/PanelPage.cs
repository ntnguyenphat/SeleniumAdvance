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
        static readonly By _rbChart = By.XPath("//label[contains(.,'Chart')]/input[contains(@id,'radPanelType')]");
        static readonly By _rbIndicator = By.XPath("//label[contains(.,'Indicator')]/input[contains(@id,'radPanelType')]");
        static readonly By _rbReport = By.XPath("//label[contains(.,'Report')]/input[contains(@id,'radPanelType')]");
        static readonly By _rbHeatMap = By.XPath("//label[contains(.,'Heat Map')]/input[contains(@id,'radPanelType')]");
        static readonly By _cmbDataProfile = By.XPath("//select[@id='cbbProfile']");
        static readonly By _txtDisplayName = By.XPath("//input[@id='txtDisplayName']");
        static readonly By _btnOK = By.XPath("//div[@class='ui-dialog editpanelDlg' and contains(@style,'display: block')]//input[@id='OK']");
        static readonly By _btnCancel = By.XPath("//input[@id='Cancel']");
        static readonly By _lnkAddNew = By.XPath("//a[contains(@href,'openAddPanel')]");
        static readonly By _lblSettingHeader = By.XPath("//fieldset[@id='fdSettings']/legend");
        static readonly By _txtChartTitle = By.XPath("//input[@id='txtChartTitle']");
        static readonly By _btnCreateNewPanel = By.XPath("//div[@class='cpbutton']/span[.='Create new panel']");
        static readonly By _cmbChartType = By.XPath("//select[@id='cbbChartType']");
        static readonly By _cmbCategory = By.XPath("//select[@id='cbbCategoryField']");
        static readonly By _txtCaptionNextToCategory = By.XPath("//input[@id='txtCategoryXAxis']");
        static readonly By _cmbSeries = By.XPath("//select[@id='cbbSeriesField']");
        static readonly By _txtCaptionNextToSeries = By.XPath("//input[@id='txtValueYAxis']");
        static readonly By _chbShowTitle = By.XPath("//input[@id='chkShowTitle']");
        static readonly By _rbLegendsNone = By.XPath("//input[@id='radPlacementNone']");
        static readonly By _rbLegendsTop = By.XPath("//input[@id='radPlacementTop']");
        static readonly By _rbLegendsRight = By.XPath("//input[@id='radPlacementRight']");
        static readonly By _rbLegendsBottom = By.XPath("//input[@id='radPlacementBottom']");
        static readonly By _rbLegendsLeft = By.XPath("//input[@id='radPlacementLeft']");
        static readonly By _rbStyle2D = By.XPath("//input[@id='rdoChartStyle2D']");
        static readonly By _rbStyle3D = By.XPath("//input[@id='rdoChartStyle3D']");
        static readonly By _cmbSelectPage = By.XPath("//select[@id='cbbPages']");
        static readonly By _txtHeight = By.XPath("//input[@id='txtHeight']");
        static readonly By _txtFolder = By.XPath("//input[@id='txtFolder']");
        //static string _lnkEdit = "a[.='{0}']/following::a[.='Edit']";
        //static string _lnkDelete = "a[.='{0}']/following::a[.='Delete']";

        //static readonly By _chbStatistic = By.XPath("//select[@id='cbbStatField']");

        #endregion

        #region Elements
        public IWebElement TabDisplaySetting
        {
            get { return _driverPanelPage.FindElement(_tabDisplaySetting); }
        }

        public IWebElement TabFilter
        {
            get { return _driverPanelPage.FindElement(_tabFilter); }
        }

        public IWebElement RbChart
        {
            get { return _driverPanelPage.FindElement(_rbChart); }
        }

        public IWebElement RbIndicator
        {
            get { return _driverPanelPage.FindElement(_rbIndicator); }
        }

        public IWebElement RbReport
        {
            get { return _driverPanelPage.FindElement(_rbReport); }
        }

        public IWebElement RbHeatMap
        {
            get { return _driverPanelPage.FindElement(_rbHeatMap); }
        }

        public IWebElement CmbDataProfile
        {
            get { return _driverPanelPage.FindElement(_cmbDataProfile); }
        }

        public IWebElement TxtDisplayName
        {
            get { return _driverPanelPage.FindElement(_txtDisplayName); }
        }

        public IWebElement BtnOK
        {
            get { return _driverPanelPage.FindElement(_btnOK); }
        }

        public IWebElement BtnCancel
        {
            get { return _driverPanelPage.FindElement(_btnCancel); }
        }

        public IWebElement LnkAddNew
        {
            get { return _driverPanelPage.FindElement(_lnkAddNew); }
        }

        public IWebElement LblSettingHeader
        {
            get { return _driverPanelPage.FindElement(_lblSettingHeader); }
        }

        public IWebElement TxtChartTitle
        {
            get { return _driverPanelPage.FindElement(_txtChartTitle); }
        }

        public IWebElement BtnCreateNewPanel
        {
            get { return _driverPanelPage.FindElement(_btnCreateNewPanel); }
        }

        public IWebElement CmbChartType
        {
            get { return _driverPanelPage.FindElement(_cmbChartType); }
        }

        public IWebElement CmbCategory
        {
            get { return _driverPanelPage.FindElement(_cmbCategory); }
        }

        public IWebElement TxtCaptionNextToCategory
        {
            get { return _driverPanelPage.FindElement(_txtCaptionNextToCategory); }
        }

        public IWebElement CmbSeries
        {
            get { return _driverPanelPage.FindElement(_cmbSeries); }
        }

        public IWebElement TxtCaptionNextToSeries
        {
            get { return _driverPanelPage.FindElement(_txtCaptionNextToSeries); }
        }

        public IWebElement ChbShowTitle
        {
            get { return _driverPanelPage.FindElement(_chbShowTitle); }
        }

        public IWebElement RbLegendsNone
        {
            get { return _driverPanelPage.FindElement(_rbLegendsNone); }
        }

        public IWebElement RbLegendsTop
        {
            get { return _driverPanelPage.FindElement(_rbLegendsTop); }
        }

        public IWebElement RbLegendsRight
        {
            get { return _driverPanelPage.FindElement(_rbLegendsRight); }
        }

        public IWebElement RbLegendsBottom
        {
            get { return _driverPanelPage.FindElement(_rbLegendsBottom); }
        }

        public IWebElement RbLegendsLeft
        {
            get { return _driverPanelPage.FindElement(_rbLegendsLeft); }
        }

        public IWebElement RbStyle2D
        {
            get { return _driverPanelPage.FindElement(_rbStyle2D); }
        }

        public IWebElement RbStyle3D
        {
            get { return _driverPanelPage.FindElement(_rbStyle3D); }
        }

        public IWebElement CmbSelectPage
        {
            get { return _driverPanelPage.FindElement(_cmbSelectPage); }
        }

        public IWebElement TxtHeight
        {
            get { return _driverPanelPage.FindElement(_txtHeight); }
        }

        public IWebElement TxtFolder
        {
            get { return _driverPanelPage.FindElement(_txtFolder); }
        }

        #endregion

        #region Methods

        public PanelPage(IWebDriver driver) : base(driver)
        {
            this._driverPanelPage = driver;
        }

        /// <summary>
        /// Determines if a profile exists
        /// </summary>
        /// <param name="profileName">Name of the profile.</param>
        /// <Author>Phat</Author>
        /// <<Startdate>>23/05/2016</<Startdate>>
        /// <returns></returns>
        public bool IsProfileExist(string profileName)
        {
            return CmbDataProfile.IsItemExist(profileName);
        }

        /// <summary>
        /// Determines if a panel is created.
        /// </summary>
        /// <param name="panelName">Name of the panel.</param>
        /// <Author>Phat</Author>
        /// <returns></returns>
        public bool IsPanelCreated(string panelName)
        {
            By panel = By.XPath("//a[.='" + panelName + "']");
            return this.IsElementExist(panel);
        }

        /// <summary>
        /// Get header of the setting
        /// </summary>
        /// <Author>Phat</Author>
        /// <<Startdate>>23/05/2016</<Startdate>>
        /// <returns></returns>
        public string GetSettingHeader()
        {
            return LblSettingHeader.Text;
        }

        /// <summary>
        /// Wait for adding panel.
        /// </summary>
        /// <param name="panelName">Name of the panel.</param>
        /// <Author>Phat</Author>
        /// <<Startdate>>23/05/2016</<Startdate>>
        public void WaitForAddingPanel(string panelName)
        {
            By panel = By.XPath("//a[.='" + panelName + "']");
            WebDriverWait wait = new WebDriverWait(_driverPanelPage, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(panel));
            wait.Until(ExpectedConditions.ElementToBeClickable(_lnkAddNew));
        }

        public void WaitForDialog()
        {

        }

        /// <summary>
        /// Click Edit Panel link
        /// </summary>
        /// <param name="panelName">Name of the panel.</param>
        /// <Author>Phat</Author>
        /// <Startdate>23/05/2016</<Startdate>>
        /// <returns></returns>
        public PanelPage ClickEditPanel(string panelName)
        {
            By xpath = By.XPath("//a[.='" + panelName + "']/ancestor::tr//a[.='Edit']");
            _driverPanelPage.FindElement(xpath).Click();
            WebDriverWait wait = new WebDriverWait(_driverPanelPage, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(_txtDisplayName));
            return this;
        }

        /// <summary>
        /// Click Delete Panel link
        /// </summary>
        /// <param name="panelName">Name of the panel.</param>
        /// <Author>Phat</Author>
        /// <Startdate>23/05/2016</Startdate>
        /// <returns></returns>
        public void ClickDeletePanel(string panelName)
        {
            By xpath = By.XPath("//a[.='" + panelName + "']/ancestor::tr//a[.='Delete']");
            _driverPanelPage.FindElement(xpath).Click();
            WebDriverWait wait = new WebDriverWait(_driverPanelPage, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.AlertIsPresent());
        }

        /// <summary>
        /// Delete a panel
        /// </summary>
        /// <param name="panelName">Name of the panel.</param>
        /// <Author>Phat</Author>
        /// <Startdate>23/05/2016</Startdate>
        /// <Modified>Long: Wait until Panel is deleted - 28/05/2016</Modified>
        /// <returns></returns>
        public PanelPage DeletePanel(string panelName)
        {
            By xpath = By.XPath("//a[.='" + panelName + "']/ancestor::tr//a[.='Delete']");
            if(this.IsElementExist(xpath))
            {
                this.SelectMenuItem("Administer", "Panels");
            }
            ClickDeletePanel(panelName);
            IAlert alert = _driverPanelPage.SwitchTo().Alert();
            alert.Accept();
            WebDriverWait wait = new WebDriverWait(_driverPanelPage, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.StalenessOf(_driverPanelPage.FindElement(xpath)));
            return this;
        }

        #endregion
    }
}
