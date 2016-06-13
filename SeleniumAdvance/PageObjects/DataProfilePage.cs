using OpenQA.Selenium;
using SeleniumAdvance.Common;
using SeleniumAdvance.DataObjects;
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
    public class DataProfilePage : GeneralPage
    {
        private IWebDriver _driverDataProfile;

        #region Locators

        static readonly By _lnkAddNew = By.XPath("//a[.='Add New']");
        static readonly By _txtName = By.XPath("//input[@id='txtProfileName']");
        static readonly By _cmbItemType = By.XPath("//select[@id ='cbbEntityType']");
        static readonly By _cmbRelatedData = By.XPath("//select[@id ='cbbSubReport']");
        static readonly By _btnNext = By.XPath("//input[@value='Next']");
        static readonly By _btnFinish = By.XPath("//input[@value='Finish']");
        static readonly By _btnCancel = By.XPath("//input[@value='Cancel']");
        static readonly By _lnkDelete = By.XPath("//a[.='Delete']");
        static readonly By _tabGenenalSettings = By.XPath("//li[.='General Settings']");
        static readonly By _tabDisplayFields = By.XPath("//li[.='Display Fields']");
        static readonly By _tabSortFields = By.XPath("//li[.='Sort Fields']");
        static readonly By _tabFilterFields = By.XPath("//li[.='Filter Fields']");
        static readonly By _tabStatisticFields = By.XPath("//li[.='Statistic Fields']");
        static readonly By _tblProfileSettings = By.XPath("//table[@id='profilesettings']/tbody/tr");
        static readonly By _lbFilterList = By.XPath("//select[@id = 'listCondition']");
        static string _chb = "//input[@class = 'box']";

        #endregion

        #region Elements

        public IWebElement LnkAddNew
        {
            get { return MyFindElement(_lnkAddNew); }
        }

        public IWebElement TxtName
        {
            get { return MyFindElement(_txtName); }
        }

        public IWebElement CmbItemType
        {
            get { return MyFindElement(_cmbItemType); }
        }

        public IWebElement CmbRelatedData
        {
            get { return MyFindElement(_cmbRelatedData); }
        }

        public IWebElement BtnNext
        {
            get { return MyFindElement(_btnNext); }
        }

        public IWebElement BtnFinish
        {
            get { return MyFindElement(_btnFinish); }
        }

        public IWebElement BtnCancel
        {
            get { return MyFindElement(_btnCancel); }
        }

        public IWebElement LnkDelete
        {
            get { return MyFindElement(_lnkDelete); }
        }

        public IWebElement TabGenenalSettings
        {
            get { return MyFindElement(_tabGenenalSettings); }
        }
        public IWebElement TabDisplayFields
        {
            get { return MyFindElement(_tabDisplayFields); }
        }
        public IWebElement TabSortFields
        {
            get { return MyFindElement(_tabSortFields); }
        }
        public IWebElement TabFilterFields
        {
            get { return MyFindElement(_tabFilterFields); }
        }

        public IWebElement TabStatisticFields
        {
            get { return MyFindElement(_tabStatisticFields); }
        }

        public ReadOnlyCollection<IWebElement> TblProfileSettings
        {
            get { return MyFindElements(_tblProfileSettings); }
        }

        public IWebElement LbFilterList
        {
            get { return MyFindElement(_lbFilterList); }
        }

        #endregion

        #region Methods

        public DataProfilePage(IWebDriver driver)
            : base(driver)
        {
            this._driverDataProfile = driver;
        }


        /// <summary>
        /// Wait for adding profile.
        /// </summary>
        /// <param name="profileName">Name of the profile.</param>
        /// <Author>Phat</Author>
        /// <Startdate>23/05/2016</Startdate>
        public void WaitForAddingProfile(string profileName)
        {
            By panel = By.XPath("//a[.='" + profileName.Replace(" ", "\u00A0") + "']");
            WebDriverWait wait = new WebDriverWait(_driverDataProfile, TimeSpan.FromSeconds(Constant.TimeOut));
            wait.Until(ExpectedConditions.ElementExists(panel));
            wait.Until(ExpectedConditions.ElementToBeClickable(_lnkAddNew));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='#Administer']")));
        }

        /// <summary>
        /// Click Edit Profile link
        /// </summary>
        /// <param name="profileName">Name of the profile.</param>
        /// <Author>Phat</Author>
        /// <Startdate>23/05/2016</Startdate>
        /// <returns></returns>
        public DataProfilePage ClickEditProfile(string profileName)
        {
            By xpath = By.XPath("//a[.='" + profileName.Replace(" ", "\u00A0") + "']/ancestor::tr//a[.='Edit']");
            MyFindElement(xpath).Click();
            WebDriverWait wait = new WebDriverWait(_driverDataProfile, TimeSpan.FromSeconds(Constant.TimeOut));
            wait.Until(ExpectedConditions.ElementExists(_txtName));
            return this;
        }


        /// <summary>
        /// Click Delete Profile link
        /// </summary>
        /// <param name="profileName">Name of the profile.</param>
        /// <Author>Phat</Author>
        /// <Startdate>23/05/2016</Startdate>
        /// <returns></returns>
        public void ClickDeleteProfile(string profileName)
        {
            By xpath = By.XPath("//a[.='" + profileName.Replace(" ", "\u00A0") + "']/ancestor::tr//a[.='Delete']");
            MyFindElement(xpath).Click();
            WebDriverWait wait = new WebDriverWait(_driverDataProfile, TimeSpan.FromSeconds(Constant.TimeOut));
            wait.Until(ExpectedConditions.AlertIsPresent());
        }


        /// <summary>
        /// Delete a profile.
        /// </summary>
        /// <param name="profileName">Name of the profile.</param>
        /// <Author>Phat</Author>
        /// <Startdate>23/05/2016</Startdate>
        /// <returns></returns>
        public DataProfilePage DeleteProfile(string profileName)
        {
            By xpath = By.XPath("//a[.='" + profileName.Replace(" ", "\u00A0") + "']/ancestor::tr//a[.='Delete']");
            this.SelectMenuItem("Administer", "Data Profiles");
            ClickDeleteProfile(profileName);
            IAlert alert = _driverDataProfile.SwitchTo().Alert();
            alert.Accept();
            return this;
        }

        /// <summary>
        /// Determine if a data profile exist.
        /// </summary>
        /// <param name="dataProfiles"></param>
        /// <Author>Long</Author>
        /// <Startdate>02/06/2016</Startdate>
        /// <returns></returns>
        public bool DoesPresetDataProfileExist(DataProfiles dataProfiles)
        {
            bool doesPresetDataProfileExist = false;
            ReadOnlyCollection<IWebElement> RowCollection = MyFindElements(By.XPath("//table[@class = 'GridView']/tbody/tr"));
            for (int i_RowNum = 2; i_RowNum <= RowCollection.Count; i_RowNum++)
            {
                ReadOnlyCollection<IWebElement> ColCollection = MyFindElements(By.XPath(string.Format("//table[@class = 'GridView']/tbody/tr[{0}]/td", i_RowNum)));
                for (int i_ColNum = 1; i_ColNum <= ColCollection.Count; i_ColNum++)
                {
                    IWebElement ColElement = MyFindElement(By.XPath(string.Format("//table[@class = 'GridView']/tbody/tr[{0}]/td[{1}]", i_RowNum, i_ColNum)));
                    if (dataProfiles.DataProfileName == ColElement.Text)
                    {
                        doesPresetDataProfileExist = true;
                        if (dataProfiles.ItemType == null)
                            break;
                        else
                        {
                            ColElement = MyFindElement(By.XPath(string.Format("//table[@class = 'GridView']/tbody/tr[{0}]/td[{1}]", i_RowNum, i_ColNum + 1)));
                            if (dataProfiles.ItemType == ColElement.Text)
                            {
                                doesPresetDataProfileExist = true;
                                if (dataProfiles.RelatedData == null)
                                    break;
                                else
                                {
                                    ColElement = MyFindElement(By.XPath(string.Format("//table[@class = 'GridView']/tbody/tr[{0}]/td[{1}]", i_RowNum, i_ColNum + 2)));
                                    if (dataProfiles.RelatedData == ColElement.Text)
                                    {
                                        doesPresetDataProfileExist = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                if (doesPresetDataProfileExist == true)
                    break;
            }
            return doesPresetDataProfileExist;
        }

        /// <summary>
        /// Gets the table cell value in Data Profile table
        /// </summary>
        /// <param name="row_number">The row_number.</param>
        /// <param name="column_number">The column_number.</param>
        /// <returns></returns>
        /// <Author>Long</Author>
        /// <Startdate>02/06/2016</Startdate>
        public string GetTableCellValue(int row_number, int column_number)
        {
            string cellValue = "";
            ReadOnlyCollection<IWebElement> RowCollection = MyFindElements(By.XPath("//table[@class = 'GridView']/tbody/tr"));
            ReadOnlyCollection<IWebElement> ColCollection = MyFindElements(By.XPath(string.Format("//table[@class = 'GridView']/tbody/tr[{0}]/td", row_number)));
            IWebElement ColElement = MyFindElement(By.XPath(string.Format("//table[@class = 'GridView']/tbody/tr[{0}]/td[{1}]", row_number, column_number)));
            cellValue = ColElement.Text;
            return cellValue;
        }

        /// <summary>
        /// Gets the index of table cell value in Data Profile table
        /// </summary>
        /// <param name="cellValue">The value of one cell.</param>
        /// <param name="row_number">The row_number.</param>
        /// <param name="column_number">The column_number.</param>
        /// <Author>Long</Author>
        /// <Startdate>02/06/2016</Startdate>
        public void GetIndexOfTableCellValue(string cellValue, out int row_number, out int column_number)
        {
            row_number = column_number = -1;
            ReadOnlyCollection<IWebElement> RowCollection = MyFindElements(By.XPath("//table[@class = 'GridView']/tbody/tr"));
            for (int i_RowNum = 2; i_RowNum <= RowCollection.Count; i_RowNum++)
            {
                ReadOnlyCollection<IWebElement> ColCollection = MyFindElements(By.XPath(string.Format("//table[@class = 'GridView']/tbody/tr[{0}]/td", i_RowNum)));
                for (int i_ColNum = 1; i_ColNum <= ColCollection.Count; i_ColNum++)
                {
                    IWebElement ColElement = MyFindElement(By.XPath(string.Format("//table[@class = 'GridView']/tbody/tr[{0}]/td[{1}]", i_RowNum, i_ColNum)));
                    if (cellValue == ColElement.Text)
                    {
                        row_number = i_RowNum;
                        column_number = i_ColNum;
                        break;
                    }
                }
                if (row_number != -1 && column_number != -1)
                    break;
            }
        }

        /// <summary>
        /// Determines if data profile is a link.
        /// </summary>
        /// <param name="dataProfile">The data profile.</param>
        /// <returns></returns>
        /// <Author>Long</Author>
        /// <Startdate>02/06/2016</Startdate>
        public bool IsDataProfileLink(string dataProfile)
        {
            bool isDataProfileLink;
            int row_number, column_number;
            GetIndexOfTableCellValue(dataProfile, out row_number, out column_number);
            IWebElement ProfileName = MyFindElement(By.XPath(string.Format("//table[@class = 'GridView']/tbody/tr[{0}]/td[{1}]", row_number, column_number)));
            isDataProfileLink = ProfileName.IsLink(_driverDataProfile);
            return isDataProfileLink;
        }

        /// <summary>
        /// Determine if the checkbox appear in the left of data profile.
        /// </summary>
        /// <param name="dataProfile">The data profile.</param>
        /// <returns></returns>
        /// <author>Long</author>
        /// <startdate>04/06/2016</startdate>
        public bool DoesCheckboxAppearInTheLeftOfDataProfile(string dataProfile)
        {
            int row_number, column_number;
            GetIndexOfTableCellValue(dataProfile, out row_number, out column_number);
            try
            {
                IWebElement TheLeftOfProfileName = _driverDataProfile.FindElement(By.XPath(string.Format("//table[@class = 'GridView']/tbody/tr[{0}]/td[{1}]/input", row_number, column_number - 1)));
                if (TheLeftOfProfileName.GetAttribute("type") == "checkbox")
                    return true;
                else
                    return false;
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        /// <summary>
        /// Determine if data profiles are listed alphabetically.
        /// </summary>
        /// <returns></returns>
        /// <author>Long</author>
        /// <startdate>04/06/2016</startdate>
        public bool AreDataProfilesListedAlphabetically()
        {
            bool areDataProfilesListedAlphabetically = true;
            ReadOnlyCollection<IWebElement> RowCollection = MyFindElements(By.XPath("//table[@class = 'GridView']/tbody/tr"));
            for (int i_RowNum = 2; i_RowNum < RowCollection.Count - 2; i_RowNum++)
            {
                IWebElement ColElement1 = MyFindElement(By.XPath(string.Format("//table[@class = 'GridView']/tbody/tr[{0}]/td[2]", i_RowNum)));
                IWebElement ColElement2 = MyFindElement(By.XPath(string.Format("//table[@class = 'GridView']/tbody/tr[{0}]/td[2]", i_RowNum + 1)));
                if (string.Compare(ColElement1.Text, ColElement2.Text) > 0)
                {
                    areDataProfilesListedAlphabetically = false;
                    break;
                }
            }
            return areDataProfilesListedAlphabetically;
        }


        /// <summary>
        /// Create the data profile.
        /// </summary>
        /// <param name="profileName">Name of the profile.</param>
        /// <param name="itemType">Type of the item.</param>
        /// <param name="relatedData">The related data.</param>
        /// <param name="displayFields">The display fields.</param>
        /// <param name="sortFields">The sort fields.</param>
        /// <param name="filterFields">The filter fields.</param>
        /// <param name="statisticFields">The statistic fields.</param>
        /// <returns></returns>
        /// <author>Long</author>
        /// <startdate>04/06/2016</startdate>
        public DataProfilePage CreateDataProfile(string profileName, string itemType, string relatedData, bool displayFields = false, bool sortFields = false, string filterFields = null, bool statisticFields = false)
        {
            TxtName.InputText(profileName);
            CmbItemType.SelectItem(itemType);
            CmbRelatedData.SelectItem(relatedData);
            if (displayFields == false)
                BtnFinish.Click();
            else
                BtnNext.Click();
            return this;
        }

        /// <summary>
        /// Delete the data profile.
        /// </summary>
        /// <param name="profileName">Name of the profile.</param>
        /// <returns></returns>
        /// <author>Long</author>
        /// <startdate>04/06/2016</startdate>
        public DataProfilePage DeleteDataProfile(string profileName)
        {
            int row_number, column_number;
            GetIndexOfTableCellValue(profileName, out row_number, out column_number);
            IWebElement TheLeftOfProfileName = MyFindElement(By.XPath(string.Format("//table[@class = 'GridView']/tbody/tr[{0}]/td[{1}]/input", row_number, column_number - 1)));
            TheLeftOfProfileName.Check();
            LnkDelete.Click();
            IAlert alert = _driverDataProfile.SwitchTo().Alert();
            alert.Accept();
            WebDriverWait wait = new WebDriverWait(_driverGeneralPage, TimeSpan.FromSeconds(Constant.TimeOut));
            wait.Until(ExpectedConditions.StalenessOf(TheLeftOfProfileName));
            return this;
        }

        /// <summary>
        /// Deteminie if all check boxes are unchecked.
        /// </summary>
        /// <returns></returns>
        /// <author>Long</author>
        /// <startdate>05/06/2016</startdate>
        public bool AreAllCheckBoxesUnChecked()
        {
            bool areAllCheckBoxesUnChecked = true;
            ReadOnlyCollection<IWebElement> Checkboxes = MyFindElements(By.XPath(_chb));
            foreach (IWebElement checkbox in Checkboxes)
            {
                if (checkbox.Selected == true)
                {
                    areAllCheckBoxesUnChecked = false;
                }
            }
            return areAllCheckBoxesUnChecked;
        }

        /// <summary>
        /// Gets the number of row in profile settings table.
        /// </summary>
        /// <returns></returns>
        /// <author>Long</author>
        /// <startdate>05/06/2016</startdate>
        public int GetNumberOfRowInProfileSettingsTable()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(Constant.TimeOut));
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//table[@id='profilesettings']/tbody/tr")));
            ReadOnlyCollection<IWebElement> RowCollection = MyFindElements(By.XPath("//table[@id='profilesettings']/tbody/tr"));
            return RowCollection.Count;
        }

        public int GetNumberOfItemInListbox()
        {
            SelectElement ListBox = new SelectElement(LbFilterList);
            int numberOfItems = ListBox.Options.Count();
            return numberOfItems;
        }
        #endregion
    }
}
