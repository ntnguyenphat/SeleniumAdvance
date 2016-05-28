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
using System.Collections.ObjectModel;

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
        static readonly By _chbDataLabelsSeries = By.XPath("//input[@id='chkSeriesName']"); 
        static readonly By _chbDataLabelsCategories = By.XPath("//input[@id='chkCategoriesName']"); 
        static readonly By _chbDataLabelsValue = By.XPath("//input[@id='chkValue']"); 
        static readonly By _chbDataLabelsPercentage = By.XPath("//input[@id='chkPercentage']");
        static readonly By _lblPanelDialog = By.XPath("//span[@id='ui-dialog-title-div_panelPopup']");
        static readonly By _txtFrom = By.XPath("input[@id='criteria'");
        static readonly By _lbColor = By.XPath("input[@id='txtColor'");
        static readonly By _cmbStatisticOn = By.XPath("//select[@id='cbbStatisticOn'");
        static readonly By _cmbSeriesValue = By.XPath("//select[@id='cbbSeriesValue'");
        static readonly By _rbSetAsHeatValue = By.XPath("//input[@id='radHeatValue_default']");
        static string panelType ="//table[@id='infoSettings']//td[.='Type']/following-sibling::td/descendant::input";
       
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

        public IWebElement ChbDataLabelsSeries
        {
            get { return _driverPanelPage.FindElement(_chbDataLabelsSeries); }
        }

        public IWebElement ChbDataLabelsCategories
        {
            get { return _driverPanelPage.FindElement(_chbDataLabelsCategories); }
        }

        public IWebElement ChbDataLabelsValue
        {
            get { return _driverPanelPage.FindElement(_chbDataLabelsValue); }
        }

        public IWebElement ChbDataLabelsPercentage
        {
            get { return _driverPanelPage.FindElement(_chbDataLabelsPercentage); }
        }

        public IWebElement LblPanelDialog
        {
            get { return _driverPanelPage.FindElement(_lblPanelDialog); }
        }

        public IWebElement TxtFrom
        {
            get { return _driverPanelPage.FindElement(_txtFrom); }
        }

        public IWebElement LbColor
        {
            get { return _driverPanelPage.FindElement(_lbColor); }
        }

        public IWebElement CmbStatisticOn
        {
            get { return _driverPanelPage.FindElement(_cmbStatisticOn); }
        }

        public IWebElement CmbSeriesValue
        {
            get { return _driverPanelPage.FindElement(_cmbSeriesValue); }
        }

        public IWebElement RbSetAsHeatValue
        {
            get { return _driverPanelPage.FindElement(_rbSetAsHeatValue); }
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

        /// <summary>
        /// Return the type of Panel.
        /// </summary>
        /// <Author>Long</Author>
        /// <Startdate>28/05/2016</Startdate>
        /// <returns></returns>
        public string GetTypeOfPanel()
        {
            string typeOfPanel = "";
            if (LblPanelDialog.Text == "Add New Panel")
            {
                ReadOnlyCollection<IWebElement> RadioButtonGroup = _driverPanelPage.FindElements(By.XPath(panelType));
                foreach (IWebElement RadioButton in RadioButtonGroup)
                {
                    if (RadioButton.Selected == true)
                    {
                        string index = RadioButton.GetAttribute("value");
                        if (index == "1")
                            typeOfPanel = "Chart";
                        else if (index == "2")
                            typeOfPanel = "Indicator";
                        else if (index == "3")
                            typeOfPanel = "Report";
                        else if (index == "4")
                            typeOfPanel = "Heat Map";
                    }
                }
            }
            else
            {
                IWebElement RadioButton = _driverPanelPage.FindElement(By.XPath(panelType));
                string index = RadioButton.GetAttribute("value");
                    if (index == "1")
                        typeOfPanel = "Chart";
                    else if (index == "2")
                        typeOfPanel = "Indicator";
                    else if (index == "3")
                        typeOfPanel = "Report";
                    else if (index == "4")
                        typeOfPanel = "Heat Map";
            }
            return typeOfPanel;      
        }

        /// <summary>
        /// Get the current settings of panel dialog.
        /// </summary>
        /// <param name="typeOfPanel">The type of panel.</param>
        /// <param name="dataProfileName">Name of the data profile.</param>
        /// <param name="panelDisplayName">Display name of the panel.</param>
        /// <param name="title">The title.</param>
        /// <param name="isShowTitleChecked">if set to <c>true</c> [is show title checked].</param>
        /// <param name="stasticFieldName">Name of the stastic field.</param>
        /// <param name="stasticFieldValue">The stastic field value.</param>
        /// <param name="isPercentageChecked">if set to <c>true</c> [is percentage checked].</param>
        /// <param name="from">From.</param>
        /// <param name="color">The color.</param>
        /// <param name="chartType">Type of the chart.</param>
        /// <param name="isCaptionNextToCategoryEnabled">if set to <c>true</c> [is caption next to category enabled].</param>
        /// <param name="isCaptionNextToSeriesEnabled">if set to <c>true</c> [is caption next to series enabled].</param>
        /// <param name="isCategoryInChartSettingsEnable">if set to <c>true</c> [is category in chart settings enable].</param>
        /// <param name="isDataLabelsSeriesEnables">if set to <c>true</c> [is data labels series enables].</param>
        /// <param name="isDataLabelsCategoriesEnabled">if set to <c>true</c> [is data labels categories enabled].</param>
        /// <param name="isDataLabelsValueEnabled">if set to <c>true</c> [is data labels value enabled].</param>
        /// <param name="isDataLabelsPercentageEnabled">if set to <c>true</c> [is data labels percentage enabled].</param>
        /// <param name="seriesName">Name of the series.</param>
        /// <param name="isLegendsNoneChecked">if set to <c>true</c> [is legends none checked].</param>
        /// <param name="isLegendsTopChecked">if set to <c>true</c> [is legends top checked].</param>
        /// <param name="isLegendsRightChecked">if set to <c>true</c> [is legends right checked].</param>
        /// <param name="isLegendsBottomChecked">if set to <c>true</c> [is legends bottom checked].</param>
        /// <param name="isLegendsLeftChecked">if set to <c>true</c> [is legends left checked].</param>
        /// <param name="isStyle2DChecked">if set to <c>true</c> [is style2 d checked].</param>
        /// <param name="isStyle3DChecked">if set to <c>true</c> [is style3 d checked].</param>
        /// <param name="captionNexToCategory">The caption nex to category.</param>
        /// <param name="captionNextToSeries">The caption next to series.</param>
        /// <param name="categoryName">Name of the category.</param>
        /// <param name="isStasticOnEnabled">if set to <c>true</c> [is stastic on enabled].</param>
        /// <param name="seriesValue">The series value.</param>
        /// <param name="isSetAsHeatValueChecked">if set to <c>true</c> [is set as heat value checked].</param>
        /// <Author>Long</Author>
        /// <Startdate>29/05/2016</Startdate>
        public void GetCurrentSettingsInPanelDialog(out string typeOfPanel, out string dataProfileName, out string panelDisplayName, out string title, out bool isShowTitleChecked,
            out string stasticFieldName, out string stasticFieldValue, out bool isPercentageChecked, out string from, out string color, out string chartType,
            out bool isCaptionNextToCategoryEnabled, out bool isCaptionNextToSeriesEnabled, out bool isCategoryInChartSettingsEnable, out bool isDataLabelsSeriesEnables,
            out bool isDataLabelsCategoriesEnabled, out bool isDataLabelsValueEnabled, out bool isDataLabelsPercentageEnabled,
            out string seriesName, out bool isLegendsNoneChecked, out bool isLegendsTopChecked, out bool isLegendsRightChecked, out bool isLegendsBottomChecked, out bool isLegendsLeftChecked,
            out bool isStyle2DChecked, out bool isStyle3DChecked, out string captionNexToCategory, out string captionNextToSeries, out string categoryName, out bool isStasticOnEnabled,
            out string seriesValue, out bool isSetAsHeatValueChecked)
        {
            typeOfPanel = GetTypeOfPanel();
            dataProfileName = GetSelectedItemOfCombobox("Profile");
            panelDisplayName = TxtDisplayName.GetAttribute("value");
            title = "";
            isShowTitleChecked = false;
            stasticFieldName = "";
            stasticFieldValue = "";
            isPercentageChecked = false;
            from = "";
            color = "";
            chartType = ""; 
            isCaptionNextToCategoryEnabled = false;
            isCaptionNextToSeriesEnabled = false;
            isCategoryInChartSettingsEnable = false;
            isDataLabelsSeriesEnables = false;
            isDataLabelsCategoriesEnabled = false;
            isDataLabelsValueEnabled = false;
            isDataLabelsPercentageEnabled = false;
            seriesName = "";
            isLegendsNoneChecked = false;
            isLegendsTopChecked = false;
            isLegendsRightChecked = false;
            isLegendsBottomChecked = false;
            isLegendsLeftChecked = false;
            isStyle2DChecked = false;
            isStyle3DChecked = false;
            captionNexToCategory = "";
            captionNextToSeries = "";
            categoryName = "";
            isStasticOnEnabled = false;
            seriesValue = "";
            isSetAsHeatValueChecked = false;
            if (typeOfPanel != "Report")
            {
                title = TxtChartTitle.GetAttribute("value");
                isShowTitleChecked = ChbShowTitle.Selected;
                if (typeOfPanel == "Indicator")
                {
                    stasticFieldName = GetSelectedItemOfCombobox("StatField");
                    stasticFieldValue = GetSelectedItemOfCombobox("StatFieldValue");
                    isPercentageChecked = ChbDataLabelsPercentage.Selected;
                    from = TxtFrom.GetAttribute("value");
                    color = LbColor.GetAttribute("style");
                }
                else if (typeOfPanel == "Chart")
                {
                    chartType = GetSelectedItemOfCombobox("Chart Type");
                    isCaptionNextToCategoryEnabled = TxtCaptionNextToCategory.Enabled;
                    isCaptionNextToSeriesEnabled = TxtCaptionNextToSeries.Enabled;
                    isCategoryInChartSettingsEnable = CmbCategory.Enabled;
                    isDataLabelsSeriesEnables = ChbDataLabelsSeries.Enabled;
                    isDataLabelsCategoriesEnabled = ChbDataLabelsCategories.Enabled;
                    isDataLabelsValueEnabled = ChbDataLabelsValue.Enabled;
                    isDataLabelsPercentageEnabled = ChbDataLabelsPercentage.Enabled;
                    isShowTitleChecked = ChbShowTitle.Selected;
                    seriesName = GetSelectedItemOfCombobox("Series");
                    isLegendsNoneChecked = RbLegendsNone.Selected;
                    isLegendsTopChecked = RbLegendsTop.Selected;
                    isLegendsBottomChecked = RbLegendsBottom.Selected;
                    isLegendsRightChecked = RbLegendsRight.Selected;
                    isLegendsLeftChecked = RbLegendsLeft.Selected;
                    isStyle2DChecked = RbStyle2D.Selected;
                    isStyle3DChecked = RbStyle3D.Selected;
                    if (chartType == "Single Bar")
                    {
                        captionNexToCategory = TxtCaptionNextToCategory.GetAttribute("value");
                        captionNextToSeries = TxtCaptionNextToSeries.GetAttribute("value");
                    }
                    else if (chartType != "Pie" && chartType != "Single Bar")
                    {
                        captionNexToCategory = TxtCaptionNextToCategory.GetAttribute("value");
                        captionNextToSeries = TxtCaptionNextToSeries.GetAttribute("value");
                        categoryName = GetSelectedItemOfCombobox("Category");
                        if (chartType == "Line")
                        {
                            stasticFieldName = GetSelectedItemOfCombobox("StatField");
                            isStasticOnEnabled = CmbStatisticOn.Enabled;
                        }
                    }
                }
                else if (typeOfPanel == "Heat Map")
                {
                    categoryName = GetSelectedItemOfCombobox("Category");
                    seriesName = GetSelectedItemOfCombobox("Series");
                    seriesValue = GetSelectedItemOfCombobox("Series Value");
                    isSetAsHeatValueChecked = RbSetAsHeatValue.Selected;
                    color = LbColor.GetAttribute("style");
                    isLegendsNoneChecked = RbLegendsNone.Selected;
                    isLegendsTopChecked = RbLegendsTop.Selected;
                    isLegendsBottomChecked = RbLegendsBottom.Selected;
                    isLegendsRightChecked = RbLegendsRight.Selected;
                    isLegendsLeftChecked = RbLegendsLeft.Selected;
                }
            }           
        }

        #endregion
    }
}
