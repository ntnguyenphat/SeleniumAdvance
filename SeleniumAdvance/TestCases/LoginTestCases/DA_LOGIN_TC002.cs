using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumAdvance.PageObjects;
using SeleniumAdvance.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumAdvance.TestCases
{
    [TestClass]
    public class DA_LOGIN_TC002 : TestBase
    {
        [TestMethod]
        public void TC02()
        {
            Console.WriteLine("TC02 - Verify that user fails to login specific repository successfully via Dashboard login page with incorrect credentials.");

            //1. Navigate to Dashboard login page
            HomePage homePage = new HomePage();
            homePage.Open();

            //2. Enter invalid username and password
            //3. Click on "Login" button
            LoginPage loginPage = new LoginPage();
            loginPage.Login("aaa", "aaa");

            WebDriverWait wait = new WebDriverWait(Constant.WebDriver, TimeSpan.FromSeconds(120));
            wait.Until(ExpectedConditions.AlertIsPresent());

            IAlert alert = Constant.WebDriver.SwitchTo().Alert();
            string expectedMessage = alert.Text;

            string observedMessage = "Username or password is invalid";

            //VP: Verify that Dashboard Error message "Username or password is invalid" appears
            Assert.AreEqual(expectedMessage,observedMessage,"\nActual: " + observedMessage + "\nExpected: " + expectedMessage);
        }
    }
}
