using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumAdvance.PageObjects;
using SeleniumAdvance.Common;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumAdvance.TestCases
{
    [TestClass]
    public class DA_LOGIN_TC001 : TestBase
    {
        [TestMethod]
        public void TC01()
        {
            Console.WriteLine("TC01 - Verify that user can login specific repository successfully via Dashboard login page with correct credentials.");

            //1. Navigate to Dashboard login page
            HomePage homePage = new HomePage();
            homePage.Open();

            //2. Enter valid username and password
            //3. Click on "Login" button
            LoginPage loginPage = new LoginPage();
            GeneralPage generalPage = loginPage.Login(Constant.Username, Constant.Password);

            //VP: Verify that Dashboard Mainpage appears
            Assert.AreEqual(true, generalPage.LnkAccount.Displayed, "Homepage is not displayed!");
        }
    }
}
