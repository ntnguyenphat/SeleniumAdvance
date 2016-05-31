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
        static readonly By _cmbStatisticField = By.XPath("//select[@id='cbbStatField'");
        static readonly By _cmbStatisticFieldValue = By.XPath("//select[@id='cbbStatFieldValue'");
        static readonly By _rbSetAsHeatValue = By.XPath("//input[@id='radHeatValue_default']");
        static string panelTypeInAddNewDialog ="//table[@id='infoSettings']//td[.='Type']/following-sibling::td/descendant::input";
        static string panelTypeInEditDialog = "//table[@id='infoSettings']//td[.='Type']/following-sibling::td/label[@class='panel_setting_paneltype']";
        static string panelTypeToSelect = "//label[contains(.,'{0}')]/input[contains(@id,'radPanelType')]";
       
        //static string _lnkEdit = "a[.='{0}']/following::a[.='Edit']";
        //static string _lnkDelete = "a[.='{0}']/following::a[.='Delete']";

        //static readonly By _chbStatistic = By.XPath("//select[@id='cbbStatField']");

        #endregion

        #region Elements
        public IWebElement TabDisplaySetting
        {
            get { return MyFindElement(_tabDisplaySetting); }
        }

        public IWebElement TabFilter
        {
            get { return MyFindElement(_tabFilter); }
        }

        public IWebElement RbChart
        {
            get { return MyFindElement(_rbChart); }
        }

        public IWebElement RbIndicator
        {
            get { return MyFindElement(_rbIndicator); }
        }

        public IWebElement RbReport
        {
            get { return MyFindElement(_rbReport); }
        }

        public IWebElement RbHeatMap
        {
            get { return MyFindElement(_rbHeatMap); }
        }

        public IWebElement CmbDataProfile
        {
            get { return MyFindElement(_cmbDataProfile); }
        }

        public IWebElement TxtDisplayName
        {
            get { return MyFindElement(_txtDisplayName); }
        }

        public IWebElement BtnOK
        {
            get { return MyFindElement(_btnOK); }
        }

        public IWebElement BtnCancel
        {
            get { return MyFindElement(_btnCancel); }
        }

        public IWebElement LnkAddNew
        {
            get { return MyFindElement(_lnkAddNew); }
        }

        public IWebElement LblSettingHeader
        {
            get { return MyFindElement(_lblSettingHeader); }
        }

        public IWebElement TxtChartTitle
        {
            get { return MyFindElement(_txtChartTitle); }
        }

        public IWebElement BtnCreateNewPanel
        {
            get { return MyFindElement(_btnCreateNewPanel); }
        }

        public IWebElement CmbChartType
        {
            get { return MyFindElement(_cmbChartType); }
        }

        public IWebElement CmbCategory
        {
            get { return MyFindElement(_cmbCategory); }
        }

        public IWebElement TxtCaptionNextToCategory
        {
            get { return MyFindElement(_txtCaptionNextToCategory); }
        }

        public IWebElement CmbSeries
        {
            get { return MyFindElement(_cmbSeries); }
        }

        public IWebElement TxtCaptionNextToSeries
        {
            get { return MyFindElement(_txtCaptionNextToSeries); }
        }

        public IWebElement ChbShowTitle
        {
            get { return MyFindElement(_chbShowTitle); }
        }

        public IWebElement RbLegendsNone
        {
            get { return MyFindElement(_rbLegendsNone); }
        }

        public IWebElement RbLegendsTop
        {
            get { return MyFindElement(_rbLegendsTop); }
        }

        public IWebElement RbLegendsRight
        {
            get { return MyFindElement(_rbLegendsRight); }
        }

        public IWebElement RbLegendsBottom
        {
            get { return MyFindElement(_rbLegendsBottom); }
        }

        public IWebElement RbLegendsLeft
        {
            get { return MyFindElement(_rbLegendsLeft); }
        }

        public IWebElement RbStyle2D
        {
            get { return MyFindElement(_rbStyle2D); }
        }

        public IWebElement RbStyle3D
        {
            get { return MyFindElement(_rbStyle3D); }
        }

        public IWebElement CmbSelectPage
        {
            get { return MyFindElement(_cmbSelectPage); }
        }

        public IWebElement TxtHeight
        {
            get { return MyFindElement(_txtHeight); }
        }

        public IWebElement TxtFolder
        {
            get { return MyFindElement(_txtFolder); }
        }

        public IWebElement ChbDataLabelsSeries
        {
            get { return MyFindElement(_chbDataLabelsSeries); }
        }

        public IWebElement ChbDataLabelsCategories
        {
            get { return MyFindElement(_chbDataLabelsCategories); }
        }

        public IWebElement ChbDataLabelsValue
        {
            get { return MyFindElement(_chbDataLabelsValue); }
        }

        public IWebElement ChbDataLabelsPercentage
        {
            get { return MyFindElement(_chbDataLabelsPercentage); }
        }

        public IWebElement LblPanelDialog
        {
            get { return MyFindElement(_lblPanelDialog); }
        }

        public IWebElement TxtFrom
        {
            get { return MyFindElement(_txtFrom); }
        }

        public IWebElement LbColor
        {
            get { return MyFindElement(_lbColor); }
        }

        public IWebElement CmbStatisticOn
        {
            get { return MyFindElement(_cmbStatisticOn); }
        }

        public IWebElement CmbSeriesValue
        {
            get { return MyFindElement(_cmbSeriesValue); }
        }

        public IWebElement CmbStatisticField
        {
            get { return MyFindElement(_cmbStatisticField); }
        }

        public IWebElement CmbStatisticFieldValue
        {
            get { return MyFindElement(_cmbStatisticFieldValue); }
        }

        public IWebElement RbSetAsHeatValue
        {
            get { return MyFindElement(_rbSetAsHeatValue); }
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
            MyFindElement(xpath).Click();
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
            MyFindElement(xpath).Click();
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
            wait.Until(ExpectedConditions.StalenessOf(LnkAddNew));
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
                ReadOnlyCollection<IWebElement> RadioButtonGroup = _driverPanelPage.FindElements(By.XPath(panelTypeInAddNewDialog));
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
                IWebElement LabelPanelType = MyFindElement(By.XPath(panelTypeInEditDialog));
                typeOfPanel = LabelPanelType.Text;
            }
            return typeOfPanel;      
        }

        /// <summary>
        /// The class is used to make out parameter optional
        /// </summary>
        /// <typeparam name="Type">The type : String, int, bool....</typeparam>
        /// <Author>Long</Author>
        /// <Startdate>Long</Startdate>
        public class OptionalOut<Type>
        {
            public Type Result { get; set; }
        }

        /// <summary>
        /// Get the current settings of panel dialog.
        /// </summary>
        /// <param name="typeOfPanel">The type of panel.</param>
        /// <param name="dataProfileName">Name of the data profile.</param>
        /// <param name="panelDisplayName">Display name of the panel.</param>
        /// <param name="title">The title.</param>
        /// <param name="isShowTitleChecked">if set to <c>true</c> [is show title checked].</param>
        /// <param name="statisticFieldName">Name of the stastic field.</param>
        /// <param name="statisticFieldValue">The stastic field value.</param>
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
        public void GetCurrentSettingsInPanelDialog(out string typeOfPanel, out string dataProfileName, out string panelDisplayName, OptionalOut<string> title = null, OptionalOut<bool> isShowTitleChecked = null,
             OptionalOut<string> statisticFieldName = null, OptionalOut<string> statisticFieldValue = null, OptionalOut<bool> isPercentageChecked = null, OptionalOut<string> from = null, OptionalOut<string> color = null, OptionalOut<string> chartType = null,
             OptionalOut<bool> isCaptionNextToCategoryEnabled = null, OptionalOut<bool> isCaptionNextToSeriesEnabled = null, OptionalOut<bool> isCategoryInChartSettingsEnable = null, OptionalOut<bool> isDataLabelsSeriesEnables = null, OptionalOut<bool> isDataLabelsSeriesChecked = null,
             OptionalOut<bool> isDataLabelsCategoriesEnabled = null, OptionalOut<bool> isDataLabelsCategoriesChecked = null, OptionalOut<bool> isDataLabelsValueEnabled = null, OptionalOut<bool> isDataLabelsValueChecked = null, OptionalOut<bool> isDataLabelsPercentageEnabled = null,
             OptionalOut<bool> isDataLabelsPercentageChecked = null, OptionalOut<string> seriesName = null, OptionalOut<bool> isLegendsNoneChecked = null, OptionalOut<bool> isLegendsTopChecked = null, OptionalOut<bool> isLegendsRightChecked = null, OptionalOut<bool> isLegendsBottomChecked = null, 
             OptionalOut<bool> isLegendsLeftChecked = null, OptionalOut<bool> isStyle2DChecked = null, OptionalOut<bool> isStyle3DChecked = null, OptionalOut<string> captionNexToCategory = null, OptionalOut<string> captionNextToSeries = null, OptionalOut<string> categoryName = null, 
             OptionalOut<bool> isStasticOnEnabled = null, OptionalOut<string> seriesValue = null, OptionalOut<bool> isSetAsHeatValueChecked = null)
        {
            typeOfPanel = GetTypeOfPanel();
            dataProfileName = GetSelectedItemOfCombobox("Profile");
            panelDisplayName = TxtDisplayName.GetAttribute("value");
            if (typeOfPanel != "Report")
            {
                if (title != null)
                    title.Result = TxtChartTitle.GetAttribute("value");
                if (isShowTitleChecked != null)
                    isShowTitleChecked.Result = ChbShowTitle.Selected;
                if (typeOfPanel == "Indicator")
                {
                    if (statisticFieldName != null)
                        statisticFieldName.Result = GetSelectedItemOfCombobox("StatField");
                    if (statisticFieldValue != null)
                        statisticFieldValue.Result = GetSelectedItemOfCombobox("StatFieldValue");
                    if (isPercentageChecked != null)
                        isPercentageChecked.Result = ChbDataLabelsPercentage.Selected;
                    if (from != null)
                        from.Result = TxtFrom.GetAttribute("value");
                    if (color != null)
                        color.Result = LbColor.GetAttribute("style");
                }
                else if (typeOfPanel == "Chart")
                {
                    if (chartType != null)
                        chartType.Result = GetSelectedItemOfCombobox("Chart Type");
                    if (isCaptionNextToCategoryEnabled != null)
                        isCaptionNextToCategoryEnabled.Result = TxtCaptionNextToCategory.Enabled;
                    if (isCaptionNextToSeriesEnabled != null)
                        isCaptionNextToSeriesEnabled.Result = TxtCaptionNextToSeries.Enabled;
                    if (isCategoryInChartSettingsEnable != null)
                        isCategoryInChartSettingsEnable.Result = CmbCategory.Enabled;
                    if (isDataLabelsSeriesEnables != null)
                        isDataLabelsSeriesEnables.Result = ChbDataLabelsSeries.Enabled;
                    if (isDataLabelsSeriesChecked != null)
                        isDataLabelsSeriesChecked.Result = ChbDataLabelsSeries.Selected;
                    if (isDataLabelsCategoriesEnabled != null)
                        isDataLabelsCategoriesEnabled.Result = ChbDataLabelsCategories.Enabled;
                    if (isDataLabelsCategoriesChecked != null)
                        isDataLabelsCategoriesChecked.Result = ChbDataLabelsCategories.Selected;
                    if (isDataLabelsValueEnabled != null)
                        isDataLabelsValueEnabled.Result = ChbDataLabelsValue.Enabled;
                    if (isDataLabelsValueChecked != null)
                        isDataLabelsValueChecked.Result = ChbDataLabelsValue.Selected;
                    if (isDataLabelsPercentageEnabled != null)
                        isDataLabelsPercentageEnabled.Result = ChbDataLabelsPercentage.Enabled;
                    if (isDataLabelsPercentageChecked != null)
                        isDataLabelsPercentageChecked.Result = ChbDataLabelsPercentage.Selected;
                    if (isShowTitleChecked != null)
                        isShowTitleChecked.Result = ChbShowTitle.Selected;
                    if (seriesName != null)
                        seriesName.Result = GetSelectedItemOfCombobox("Series");
                    if (isLegendsNoneChecked != null)
                        isLegendsNoneChecked.Result = RbLegendsNone.Selected;
                    if (isLegendsTopChecked != null)
                        isLegendsTopChecked.Result = RbLegendsTop.Selected;
                    if (isLegendsBottomChecked != null)
                        isLegendsBottomChecked.Result = RbLegendsBottom.Selected;
                    if (isLegendsRightChecked != null)
                        isLegendsRightChecked.Result = RbLegendsRight.Selected;
                    if (isLegendsLeftChecked != null)
                        isLegendsLeftChecked.Result = RbLegendsLeft.Selected;
                    if (isStyle2DChecked != null)
                        isStyle2DChecked.Result = RbStyle2D.Selected;
                    if (isStyle3DChecked != null)
                        isStyle3DChecked.Result = RbStyle3D.Selected;
                    if (chartType.Result == "Single Bar")
                    {
                        if (captionNexToCategory != null)
                            captionNexToCategory.Result = TxtCaptionNextToCategory.GetAttribute("value");
                        if (captionNextToSeries != null)
                            captionNextToSeries.Result = TxtCaptionNextToSeries.GetAttribute("value");
                    }
                    else if (chartType.Result != "Pie" && chartType.Result != "Single Bar")
                    {
                        if (captionNexToCategory != null)
                            captionNexToCategory.Result = TxtCaptionNextToCategory.GetAttribute("value");
                        if (captionNextToSeries != null)
                            captionNextToSeries.Result = TxtCaptionNextToSeries.GetAttribute("value");
                        if (categoryName != null)
                            categoryName.Result = GetSelectedItemOfCombobox("Category");
                        if (chartType.Result == "Line")
                        {
                            if (statisticFieldName != null)
                                statisticFieldName.Result = GetSelectedItemOfCombobox("StatField");
                            if (isStasticOnEnabled != null)
                                isStasticOnEnabled.Result = CmbStatisticOn.Enabled;
                        }
                    }
                }
                else if (typeOfPanel == "Heat Map")
                {

                    if (categoryName != null)
                        categoryName.Result = GetSelectedItemOfCombobox("Category");
                    if (seriesName != null)
                        seriesName.Result = GetSelectedItemOfCombobox("Series");
                    if (seriesValue != null)
                        seriesValue.Result = GetSelectedItemOfCombobox("Series Value");
                    if (isSetAsHeatValueChecked != null)
                        isSetAsHeatValueChecked.Result = RbSetAsHeatValue.Selected;
                    if (color != null)
                        color.Result = LbColor.GetAttribute("style");
                    if (isLegendsNoneChecked != null)
                        isLegendsNoneChecked.Result = RbLegendsNone.Selected;
                    if (isLegendsTopChecked != null)
                        isLegendsTopChecked.Result = RbLegendsTop.Selected;
                    if (isLegendsBottomChecked != null)
                        isLegendsBottomChecked.Result = RbLegendsBottom.Selected;
                    if (isLegendsRightChecked != null)
                        isLegendsRightChecked.Result = RbLegendsRight.Selected;
                    if (isLegendsLeftChecked != null)
                        isLegendsLeftChecked.Result = RbLegendsLeft.Selected;
                }
            }           
        }

        /// <summary>
        /// Create or Edit a panel
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="panelType">Type of the panel.</param>
        /// <param name="dataProfileName">Name of the data profile.</param>
        /// <param name="panelDisplayName">Display name of the panel.</param>
        /// <param name="title">The title.</param>
        /// <param name="showTitle">if set to <c>true</c> [show title].</param>
        /// <param name="chartType">Type of the chart.</param>
        /// <param name="category">The category.</param>
        /// <param name="style2D">if set to <c>true</c> [style2 d].</param>
        /// <param name="style3d">if set to <c>true</c> [style3d].</param>
        /// <param name="series">The series.</param>
        /// <param name="captionNextToCategory">The caption next to category.</param>
        /// <param name="captionNextToSeries">The caption next to series.</param>
        /// <param name="legendsTop">if set to <c>true</c> [legends top].</param>
        /// <param name="legendsNone">if set to <c>true</c> [legends none].</param>
        /// <param name="legendsRight">if set to <c>true</c> [legends right].</param>
        /// <param name="legendsBottom">if set to <c>true</c> [legends bottom].</param>
        /// <param name="legendsLeft">if set to <c>true</c> [legends left].</param>
        /// <param name="dataLabelsSeries">if set to <c>true</c> [data labels series].</param>
        /// <param name="dataLabelsCategories">if set to <c>true</c> [data labels categories].</param>
        /// <param name="dataLabelsValue">if set to <c>true</c> [data labels value].</param>
        /// <param name="dataLabelsPercentage">if set to <c>true</c> [data labels percentage].</param>
        /// <param name="statisticFied">The statistic fied.</param>
        /// <param name="statisticFieldValue">The statistic field value.</param>
        /// <param name="from">From.</param>
        /// <param name="color">The color.</param>
        /// <param name="seriesValue">The series value.</param>
        /// <param name="setAsHeatValue">if set to <c>true</c> [set as heat value].</param>
        /// <param name="statisticFieldOn">The statistic field on.</param>
        /// <param name="statisticOn">The statistic on.</param>
        /// <Author>Long</Author>
        /// <Startdate>29/05/2016</Startdate>
        /// <returns></returns>
        public PanelPage Panel(string action, string panelType, string dataProfileName = null, string panelDisplayName = null, string title = null, bool showTitle = false, string chartType = null,
            string category = null, bool style2D = false, bool style3d = false, string series = null, string captionNextToCategory = null, string captionNextToSeries = null,
            bool legendsTop = false, bool legendsNone = false, bool legendsRight = false, bool legendsBottom = false, bool legendsLeft = false,
            bool dataLabelsSeries = false, bool dataLabelsCategories = false, bool dataLabelsValue = false, bool dataLabelsPercentage = false,
            string statisticFied = null, string statisticFieldValue = null, string from = null, string color = null, string seriesValue = null,
            bool setAsHeatValue = false, string statisticFieldOn = null, string statisticOn = null)
        {
            string currentPanelTypeSelect = GetTypeOfPanel();
            WebDriverWait wait = new WebDriverWait(_driverPanelPage, TimeSpan.FromSeconds(10));
            if (action == "Create")
            {
                IWebElement RbSelectPanelType = MyFindElement(By.XPath(string.Format(panelTypeToSelect, panelType)));
                RbSelectPanelType.Check();
                if (panelType != currentPanelTypeSelect)
                    wait.Until(ExpectedConditions.StalenessOf(ChbShowTitle));
            }
            if (dataProfileName != null)
            {
                CmbDataProfile.SelectItem(dataProfileName);
                wait.Until(ExpectedConditions.StalenessOf(ChbShowTitle));
            }
            if (panelDisplayName != null)
            {
                TxtDisplayName.Clear();
                TxtDisplayName.SendKeys(panelDisplayName);
            }
            if (panelType != "Report")
            {
                if (title != null)
                    TxtChartTitle.SendKeys(title);
                if (showTitle == true)
                    ChbShowTitle.Check();
                else
                    ChbShowTitle.UnCheck();
                if (panelType == "Indicator")
                {
                    if (statisticFied != null)
                        CmbStatisticField.SelectItem(statisticFied);
                    if (statisticFieldValue != null)
                        CmbStatisticFieldValue.SelectItem(statisticFieldValue);
                    if (dataLabelsPercentage == true)
                        ChbDataLabelsPercentage.Check();
                    else
                        ChbDataLabelsPercentage.UnCheck();
                    if (from != null)
                    {
                        TxtFrom.Clear();
                        TxtFrom.SendKeys(from);
                    }
                    if (color != null)
                        LbColor.SelectItem(color);
                }
                else
                {
                    if (category != null)
                        CmbCategory.SelectItem(category,"Value");
                    if (series != null)
                        CmbSeries.SelectItem(series,"Value");
                    if (legendsBottom == true)
                        RbLegendsBottom.Check();
                    if (legendsLeft == true)
                        RbLegendsLeft.Check();
                    if (legendsNone == true)
                        RbLegendsNone.Check();
                    if (legendsRight == true)
                        RbLegendsRight.Check();
                    if (legendsTop == true)
                        RbLegendsTop.Check();
                }
            }
            if (panelType == "Chart")
            {
                if (chartType != null)
                    CmbChartType.SelectItem(chartType, "Value");
                if (style2D == true)
                    RbStyle2D.Check();
                if (style3d == true)
                    RbStyle3D.Check();
                if (captionNextToCategory != null)
                {
                    TxtCaptionNextToCategory.Clear();
                    TxtCaptionNextToCategory.SendKeys(captionNextToCategory);
                }
                if (captionNextToSeries != null)
                {
                    TxtCaptionNextToSeries.Clear();
                    TxtCaptionNextToSeries.SendKeys(captionNextToSeries);
                }
                if (statisticFied != null)
                    CmbStatisticField.SelectItem(statisticFied, "Value");
                if (statisticFieldOn != null)
                    CmbStatisticOn.SelectItem(statisticFieldOn, "Value");
                if (dataLabelsCategories == true)
                    ChbDataLabelsCategories.Check();
                else
                    ChbDataLabelsCategories.UnCheck();
                if (dataLabelsPercentage == true)
                    ChbDataLabelsPercentage.Check();
                else
                    ChbDataLabelsPercentage.UnCheck();
                if (dataLabelsSeries == true)
                    ChbDataLabelsSeries.Check();
                else
                    ChbDataLabelsSeries.UnCheck();
                if (dataLabelsValue == true)
                    ChbDataLabelsValue.Check();
                else
                    ChbDataLabelsValue.UnCheck();
            }
            else if (panelType == "Heat Map")
            {
                if (seriesValue != null)
                    CmbSeriesValue.SelectItem(seriesValue, "Value");
                if (setAsHeatValue == true)
                    RbSetAsHeatValue.Check();
                if (color != null)
                    LbColor.SelectItem(color);
            }
            return this;
        }
        #endregion
    }
}
