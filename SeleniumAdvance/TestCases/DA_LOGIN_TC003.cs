using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumAdvance.PageObjects;
using SeleniumAdvance.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumAdvance.TestCases
{
    [TestClass]
    public class DA_LOGIN_TC003 : PageBase
    {
        [TestMethod]
        public void TC03()
        {
            Console.WriteLine("TC03 - Verify that user fails to log in specific repository successfully via Dashboard login page with correct username and incorrect password.");
            //1. Navigate to Dashboard login page
            HomePage homePage = new HomePage();
            homePage.Open();

            //2. Enter valid username and inpassword
            //3. Click on "Login" button
            //VP: Verify that Dashboard Error message "Username or password is invalid" appears
            LoginPage loginPage = new LoginPage();
            loginPage.Login(Constant.Username, "aaa");
            //Constant.WebDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
            WebDriverWait wait = new WebDriverWait(Constant.WebDriver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = Constant.WebDriver.SwitchTo().Alert();
            string expectedMessage = alert.Text;
            Assert.AreEqual(expectedMessage, "Username or password is invalid");
        }
    }
}
