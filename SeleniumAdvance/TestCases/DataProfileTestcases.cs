using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumAdvance.PageObjects;
using SeleniumAdvance.Common;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumAdvance.TestCases
{
    [TestClass]
    public class DataProfileTestcases : TestBase
    {
        /// <summary>Verify that all Pre-set Data Profiles are populated correctly
        /// </summary>
        /// <Author>Long</Author>
        /// <Startdate>30/05/2016</Startdate>
        [TestMethod]
        public void TC065()
        {
            Console.WriteLine("DA_LOGIN_TC065 - Verify that all Pre-set Data Profiles are populated correctly");

            //1. Navigate to Dashboard login page
            //2. Select a specific repository 
            //3. Enter valid Username and Password
            //4. Click Login

            LoginPage loginPage = new LoginPage(driver);
            loginPage.Open().Login(Constant.Username, Constant.Password);

            //5. Click Administer->Data Profiles

            DataProfilePage dataProfilePage = new DataProfilePage(driver);
            dataProfilePage.SelectMenuItem("Administer", "Data Profiles");

            //6. VP: Check Pre-set Data Profile are populated correctly in profiles page
            //To do : Create data table to work on
        }
    }
}
