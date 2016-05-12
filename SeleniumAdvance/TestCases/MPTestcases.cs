using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumAdvance.PageObjects;
using SeleniumAdvance.Common;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumAdvance.TestCases
{
    [TestClass]
    public class MPTestcases : TestBase
    {
        [TestMethod]
        public void TC011()
        {
            Console.WriteLine("DA_LOGIN_TC011 - Verify that user is unable open more than 1 \"New Page\" dialog");

            //1. Navigate to Dashboard login page. Login with valid account
            //2. Go to Global Setting -> Add page. Try to go to Global Setting -> Add page again

            LoginPage loginPage = new LoginPage(driver);
            GeneralPage generalPage = loginPage.Open().Login(Constant.Username, Constant.Password);

            generalPage.SelectGeneralSetting("Add Page");

            bool actualResult = generalPage.IsDashboardLockedByDialog();

            //VP: There is a message "Please enter username"

            Assert.AreEqual(true, actualResult, "Dashboard is not locked by dialog!");
        }

        [TestMethod]
        public void TC012()
        {
            Console.WriteLine("DA_LOGIN_TC012 - Verify that user is able to add additional pages besides \"Overview\" page successfully");

            string pageName = CommonMethods.GetUniqueString();

            //1. Navigate to Dashboard login page. Login with valid account
            //2. Go to Global Setting -> Add page. Enter Page Name field

            LoginPage loginPage = new LoginPage(driver);
            GeneralPage generalPage = loginPage.Open().Login(Constant.Username, Constant.Password);

            ManagePagePage managePage = new ManagePagePage(driver);

            managePage.AddPage(pageName);

            //VP: New page is displayed besides "Overview" page

            managePage.CheckPageNextToPage("Overview", pageName);
        }

        [TestMethod]
        public void TC013()
        {
            Console.WriteLine("DA_LOGIN_TC013 - Verify that the newly added main parent page is positioned at the location specified as set with \"Displayed After\" field of \"New Page\" form on the main page bar/\"Parent Page\" dropped down menu");

            string pageName1 = CommonMethods.GetUniqueString();
            string pageName2 = CommonMethods.GetUniqueString();

            //1. Navigate to Dashboard login page. Login with valid account
            //2. Go to Global Setting -> Add page. Enter Page Name field

            LoginPage loginPage = new LoginPage(driver);
            GeneralPage generalPage = loginPage.Open().Login(Constant.Username, Constant.Password);

            ManagePagePage managePage = new ManagePagePage(driver);

            managePage.AddPage(pageName1);
            managePage.AddPage(pageName2,pageName1);

            //VP: Page 1 is positioned besides the Page 2

            managePage.CheckPageNextToPage(pageName1, pageName2);
        }
    }
}
