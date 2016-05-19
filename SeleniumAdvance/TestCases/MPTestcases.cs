﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumAdvance.PageObjects;
using SeleniumAdvance.Common;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumAdvance.Ultilities;

namespace SeleniumAdvance.TestCases
{
    [TestClass]
    public class MPTestcases : TestBase
    {
        [TestMethod]
        public void TC011()
        {
            Console.WriteLine("DA_MP_TC011 - Verify that user is unable open more than 1 \"New Page\" dialog");

            //1. Navigate to Dashboard login page. Login with valid account
            //2. Go to Global Setting -> Add page. Try to go to Global Setting -> Add page again

            LoginPage loginPage = new LoginPage(driver);
            GeneralPage generalPage = loginPage.Open().Login(Constant.Username, Constant.Password);

            generalPage.SelectGeneralSetting("Add Page");

            bool actualResult = generalPage.IsDashboardLockedByDialog();

            //VP: User cannot go to Global Setting -> Add page while "New Page" dialog appears.

            Assert.AreEqual(true, actualResult, "Dashboard is not locked by dialog!");
        }

        [TestMethod]
        public void TC012()
        {
            Console.WriteLine("DA_MP_TC012 - Verify that user is able to add additional pages besides \"Overview\" page successfully");

            string pageName = CommonMethods.GetUniqueString();

            //1. Navigate to Dashboard login page. Login with valid account
            //2. Go to Global Setting -> Add page. Enter Page Name field

            LoginPage loginPage = new LoginPage(driver);
            GeneralPage generalPage = loginPage.Open().Login(Constant.Username, Constant.Password);

            MainPage mainPage = new MainPage(driver);

            mainPage.AddPage(pageName);

            //VP: New page is displayed besides "Overview" page
            bool isPageNextToPage = mainPage.IsPageNextToPage("Overview", pageName);
            Assert.AreEqual(true, isPageNextToPage, "The new page isn't displayed besides \"Overview\" page");
        }

        [TestMethod]
        public void TC013()
        {
            Console.WriteLine("DA_MP_TC013 - Verify that the newly added main parent page is positioned at the location specified as set with \"Displayed After\" field of \"New Page\" form on the main page bar/\"Parent Page\" dropped down menu");

            string pageName1 = string.Concat("Page1", CommonMethods.GetUniqueString());
            string pageName2 = string.Concat("Page2", CommonMethods.GetUniqueString());

            //1. Navigate to Dashboard login page. Login with valid account
            //2. Go to Global Setting -> Add page. Enter Page Name field

            LoginPage loginPage = new LoginPage(driver);
            GeneralPage generalPage = loginPage.Open().Login(Constant.Username, Constant.Password);

            MainPage mainPage = new MainPage(driver);

            mainPage.AddPage(pageName1);
            mainPage.AddPage(pageName: pageName2, displayAfer: pageName1);

            //VP: Page 1 is positioned besides the Page 2
            bool isPageNextToPage = mainPage.IsPageNextToPage(pageName1, pageName2);
            Assert.AreEqual(true, isPageNextToPage, "" + pageName2 + "isn't positioned besides" + pageName2);     
        }

        [TestMethod]
        public void TC014()
        {
            Console.WriteLine("DA_MP_TC014 - Verify that \"Public\" pages can be visible and accessed by all users of working repository");

            string pageName = string.Concat("Page1", CommonMethods.GetUniqueString());

            //1.Navigate to Dashboard login page
            //2.Log in specific repository with valid account

            LoginPage loginPage = new LoginPage(driver);
            GeneralPage generalPage = loginPage.Open().Login(Constant.Username, Constant.Password);

            //3.Go to Global Setting -> Add page
            //4.Enter Page Name field
            //5.Check Public checkbox
            //6.Click OK button

            MainPage mainPage = new MainPage(driver);
            mainPage.AddPage(pageName: pageName, publicCheckBox: true);

            //7.Click on Log out link
            //8.Log in with another valid account

            loginPage = generalPage.Logout();
            loginPage.Login(Constant.OtherUsername, Constant.OtherPassword);

            //VP: Check newly added page is visibled
            bool doesPageExist = mainPage.DoesPageExist(pageName);
            Assert.AreEqual(true, doesPageExist, "" + pageName + "isn't visibled");   
        }

        [TestMethod]
        public void TC015()
        {
            Console.WriteLine("DA_MP_TC015 - Verify that non \"Public\" pages can only be accessed and visible to their creators with condition that all parent pages above it are \"Public\"");

            string parentPageName = string.Concat("Parent", CommonMethods.GetUniqueString());
            string childPageName = string.Concat("Child", CommonMethods.GetUniqueString());

            //1. Navigate to Dashboard login page. Log in specific repository with valid account
            //2. Go to Global Setting -> Add page. Enter Page Name field. Check Public checkbox. Click OK button

            LoginPage loginPage = new LoginPage(driver);
            loginPage.Open().Login(Constant.Username, Constant.Password);

            MainPage mainPage = new MainPage(driver);
            mainPage.AddPage(pageName: parentPageName, publicCheckBox: true);

            //3. Go to Global Setting -> Add page. Enter Page Name field. Click on  Select Parent dropdown list
            //4. Select specific page. Click OK button. Click on Log out link. Log in with another valid account

            mainPage.AddPage(pageName: childPageName, parentPage: parentPageName);
            mainPage.Logout();

            loginPage.Login(Constant.OtherUsername, Constant.OtherPassword);

            //VP: Children is invisibled
            bool doesPageExist = mainPage.DoesPageExist(parentPageName + "->" + childPageName);
            Assert.AreEqual(true, doesPageExist, "" + childPageName + "isn't visibled"); 
        }

        [TestMethod]
        public void TC016()
        {
            Console.WriteLine("DA_MP_TC016 - Verify that user is able to edit the \"Public\" setting of any page successfully");

            string pageName1 = string.Concat("Page1", CommonMethods.GetUniqueString());
            string pageName2 = string.Concat("Page2", CommonMethods.GetUniqueString());

            //1. Navigate to Dashboard login page. Log in specific repository with valid account
            //2. Go to Global Setting -> Add page. Enter Page Name. Click OK button

            LoginPage loginPage = new LoginPage(driver);
            loginPage.Open().Login(Constant.Username, Constant.Password);

            MainPage mainPage = new MainPage(driver);
            mainPage.AddPage(pageName: pageName1);

            //3. Go to Global Setting -> Add page.  Enter Page Name. Check Public checkbox. Click OK button
            //4. Click on "Test" page. Click on "Edit" link.

            mainPage.AddPage(pageName: pageName2, publicCheckBox: true);
            mainPage.SelectPage(pageName1);
            mainPage.SelectGeneralSetting("Edit");

            //VP: "Edit Page" pop up window is displayed

            mainPage.CheckPopupHeader("Edit Page");


            //5. Check Public checkbox. Click OK button
            //6. Click on "Another Test" page. Click on "Edit" link.

            mainPage.EditPageInfomation(publicCheckBox: true);
            mainPage.SelectPage(pageName2);
            mainPage.SelectGeneralSetting("Edit");

            //VP: "Edit Page" pop up window is displayed

            mainPage.CheckPopupHeader("Edit Page");

            //7. Uncheck Public checkbox. Click OK button
            //8. Click Log out link. Log in with another valid account

            mainPage.EditPageInfomation(publicCheckBox: false);
            mainPage.Logout();

            loginPage.Login(Constant.OtherUsername, Constant.OtherPassword);

            //VP: Check "Test" Page is visible and can be accessed. Check "Another Test" page is invisible.
            bool doesPageName1Exist = mainPage.DoesPageExist(pageName1);
            Assert.AreEqual(true, doesPageName1Exist, "" + pageName1 + "isn't visibled");

            bool doesPageName2Exist = mainPage.DoesPageExist(pageName2);
            Assert.AreEqual(false, doesPageName2Exist, "" + pageName2 + "is visibled"); 
        }
    }
}
