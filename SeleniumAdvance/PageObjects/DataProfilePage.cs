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
    public class DataProfilePage:GeneralPage
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
            get { return _driverDataProfile.FindElement(_lnkAddNew); }
        }
        public IWebElement TxtName
        {
            get { return _driverDataProfile.FindElement(_txtName); }
        }
        public IWebElement BtnNext
        {
            get { return _driverDataProfile.FindElement(_btnNext); }
        }
        public IWebElement BtnFinish
        {
            get { return _driverDataProfile.FindElement(_btnFinish); }
        }
        public IWebElement BtnCancel
        {
            get { return _driverDataProfile.FindElement(_btnCancel); }
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
            _driverDataProfile.FindElement(xpath).Click();
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
            _driverDataProfile.FindElement(xpath).Click();
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

        #endregion
    }
}
