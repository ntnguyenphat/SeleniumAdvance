using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumAdvance.Common;

namespace SeleniumAdvance.PageObjects
{
    public class HomePage:GeneralPage
    {
        #region Locatiors
        #endregion

        #region Elements
        #endregion

        #region Methods
        public HomePage Open()
        {
            Constant.WebDriver.Navigate().GoToUrl(Constant.HomePageURL);
            return this;
        }
        #endregion
    }
}
