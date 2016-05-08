using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumAdvance.PageObjects;
using SeleniumAdvance.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumAdvance.TestCases.LoginTestCases
{
    [TestClass]
    public class DA_LOGIN_TC004 : TestBase
    {
        [TestMethod]
        public void TC04()
        {
            Console.WriteLine("TC04 - Verify that user is able to log in different repositories successfully after logging out current repository");

            //1. Navigate to Dashboard login page
            HomePage homePage = new HomePage();
            homePage.Open();

            //2. Enter valid username and password of default repository
            //3. Click on "Login" button
            LoginPage loginPage = new LoginPage();
            GeneralPage generalPage = loginPage.Login(Constant.Username, Constant.Password);

            //4. Click on "Logout" button
            //5. Select a different repository       
            //6. Enter valid username and password of this repository
            loginPage = generalPage.Logout();
            loginPage.SelectRepository(Constant.AdditionalRepo);
            generalPage = loginPage.Login(Constant.Username, Constant.Password);

            //VP: Verify that Dashboard Mainpage appears
            Assert.AreEqual(true, generalPage.LnkAccount.Displayed, "Homepage is not displayed!");
        }
    }
}
