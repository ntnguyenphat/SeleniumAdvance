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
    public class PanelConfigurationDialog : GeneralPage
    {
        private IWebDriver _driverPanelConfig;

        #region Locators

        static readonly By _cmbSelectpage = By.XPath("//select[@id='cbbPages']");
        static readonly By _btnCancel = By.XPath("//div[@class='ui-dialog editpanelDlg' and contains(@style,'display: block')]//input[@id='Cancel']");
        static readonly By _txtHeight = By.XPath("//input[@id='txtHeight']");
        static readonly By _txtFolder = By.XPath("//input[@id='txtFolder']");
        static readonly By _btnOk = By.XPath("//div[@class='ui-dialog editpanelDlg' and contains(@style,'display: block')]//input[@id='OK']");
        static readonly By _btnSelectFolder = By.XPath("//a[contains(@href,'treeFolder')]");

        #endregion

        #region Elements

        public IWebElement CmbSelectpage
        {
            get { return MyFindElement(_cmbSelectpage); }
        }
        public IWebElement BtnCancel
        {
            get { return MyFindElement(_btnCancel); }
        }
        public IWebElement TxtHeight
        {
            get { return MyFindElement(_txtHeight); }
        }
        public IWebElement TxtFolder
        {
            get { return MyFindElement(_txtFolder); }
        }
        public IWebElement BtnOk
        {
            get { return MyFindElement(_btnOk); }
        }
        public IWebElement BtnSelectFolder
        {
            get { return MyFindElement(_btnSelectFolder); }
        }

        #endregion

        #region Methods

        public PanelConfigurationDialog(IWebDriver driver)
            : base(driver)
        {
            this._driverPanelConfig = driver;
        }
        /// <summary>
        /// Selects the folder in tree.
        /// </summary>
        /// <param name="folderLink">The folder link. Example format: Car Rental/Actions/Car</param>
        /// <Author>Phat</Author>
        /// <Startdate>04/06/2016</Startdate>
        public PanelConfigurationDialog SelectFolderInTree(string folderLink)
        {
            BtnSelectFolder.Click();
            string[] folder = Regex.Split(folderLink, "/");
            for (int i = 0; i < folder.Count(); i++)
            {
                By xpath = By.XPath("//a[.='  " + folder[i] + "']//preceding-sibling::a[contains(@onclick,'doToggle')]");
                MyFindElement(xpath).Click();
            }
            By lastItem = By.XPath("//a[.='  " + folder[folder.Count() - 1] + "']");
            By btnOk = By.XPath("//input[@id='btnFolderSelectionOK']");

            MyFindElement(lastItem).Click();
            MyFindElement(btnOk).Click();

            return this;
        }

        /// <summary>
        /// Determines whether folder is created.
        /// </summary>
        /// <param name="folderLink">The folder link.</param>
        /// <Author>Phat</Author>
        /// <Startdate>04/06/2016</Startdate>
        /// <returns></returns>
        public bool IsFolderSelected(string folderLink)
        {
            return ("/" + folderLink) == TxtFolder.GetAttribute("value");
        }

        #endregion
    }
}
