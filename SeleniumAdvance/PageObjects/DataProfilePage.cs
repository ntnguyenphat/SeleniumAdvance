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
        static readonly By _btnNext = By.XPath("//input[@value='Next']");
        static readonly By _btnFinish = By.XPath("//input[@value='Finish']");
        static readonly By _btnCancel = By.XPath("//input[@value='Cancel']");

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

        #endregion

        #region Methods

        public DataProfilePage(IWebDriver driver):base(driver)
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
            By panel = By.XPath("//a[.='" + profileName + "']");
            WebDriverWait wait = new WebDriverWait(_driverDataProfile, TimeSpan.FromSeconds(10));
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
            By xpath = By.XPath("//a[.='" + profileName + "']/ancestor::tr//a[.='Edit']");
            MyFindElement(xpath).Click();
            WebDriverWait wait = new WebDriverWait(_driverDataProfile, TimeSpan.FromSeconds(10));
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
            By xpath = By.XPath("//a[.='" + profileName + "']/ancestor::tr//a[.='Delete']");
            MyFindElement(xpath).Click();
            WebDriverWait wait = new WebDriverWait(_driverDataProfile, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.AlertIsPresent());
        }


        /// <summary>
        /// Deletes a profile.
        /// </summary>
        /// <param name="profileName">Name of the profile.</param>
        /// <Author>Phat</Author>
        /// <Startdate>23/05/2016</Startdate>
        /// <returns></returns>
        public DataProfilePage DeleteProfile(string profileName)
        {
            By xpath = By.XPath("//a[.='" + profileName + "']/ancestor::tr//a[.='Delete']");
            if (this.IsElementExist(xpath))
            {
                this.SelectMenuItem("Administer", "Data Profiles");
            }
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
            bool DoesPresetDataProfileExist = false;
            ReadOnlyCollection<IWebElement> RowCollection = _driverDataProfile.FindElements(By.XPath("//table[@class = 'GridView']/tbody/tr"));
            for (int i_RowNum = 2; i_RowNum <= RowCollection.Count; i_RowNum++)
            {
                ReadOnlyCollection<IWebElement> ColCollection = _driverDataProfile.FindElements(By.XPath(string.Format("//table[@class = 'GridView']/tbody/tr[{0}]/td", i_RowNum)));
                for (int i_ColNum = 1; i_ColNum <= ColCollection.Count; i_ColNum++ )
                {
                    IWebElement ColElement = MyFindElement(By.XPath(string.Format("//table[@class = 'GridView']/tbody/tr[{0}]/td[{1}]", i_RowNum, i_ColNum))); 
                    if (dataProfiles.DataProfileName == ColElement.Text)
                    {
                        ColElement = MyFindElement(By.XPath(string.Format("//table[@class = 'GridView']/tbody/tr[{0}]/td[{1}]", i_RowNum, i_ColNum + 1))); 
                        if (dataProfiles.ItemType == ColElement.Text)
                        {
                            ColElement = MyFindElement(By.XPath(string.Format("//table[@class = 'GridView']/tbody/tr[{0}]/td[{1}]", i_RowNum, i_ColNum + 2))); 
                            if (dataProfiles.RelatedData == ColElement.Text)
                            {
                                DoesPresetDataProfileExist = true;
                                break;
                            }
                        }
                    }
                }
                if (DoesPresetDataProfileExist == true)
                    break;
            }
            return DoesPresetDataProfileExist;
        }
        #endregion
    }
}
