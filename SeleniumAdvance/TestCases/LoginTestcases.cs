using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumAdvance.PageObjects;
using SeleniumAdvance.Common;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumAdvance.TestCases
{
    [TestClass]
    public class LoginTestcases : TestBase
    {
        [TestMethod]
        public void TC001()
        {
            Console.WriteLine("DA_LOGIN_TC001 - Verify that user can login specific repository successfully via Dashboard login page with correct credentials.");

            //Navigate to Dashboard login page. Enter valid username and password. Click on "Login" button

            LoginPage loginPage = new LoginPage();
            loginPage.Open().Login(Constant.Username, Constant.Password);

            bool observedResult = loginPage.IsDashboardMainpageDisplayed();

            //VP: Verify that Dashboard Mainpage appears
            Assert.AreEqual(true, observedResult, "Dashboard Mainpage is not displayed!");

        }

        [TestMethod]
        public void TC002()
        {
            Console.WriteLine("DA_LOGIN_TC002 - Verify that user fails to login specific repository successfully via Dashboard login page with incorrect credentials.");

            //Navigate to Dashboard login page. Enter invalid username and password. Click on "Login" button

            LoginPage loginPage = new LoginPage();
            loginPage.Open().Login("aaa", "aaa");

            string expectedMessage = loginPage.GetAlertMessage();
            string observedMessage = "Username or password is invalid";

            //VP: Verify that Dashboard Error message "Username or password is invalid" appears
            Assert.AreEqual(expectedMessage, observedMessage, "\nActual: " + observedMessage + "\nExpected: " + expectedMessage);
        }

        [TestMethod]
        public void TC003()
        {
            Console.WriteLine("DA_LOGIN_TC003 - Verify that user fails to log in specific repository successfully via Dashboard login page with correct username and incorrect password.");

            //1. Navigate to Dashboard login page. Enter valid username and inpassword. Click on "Login" button

            LoginPage loginPage = new LoginPage();
            loginPage.Open().Login(Constant.Username, "aaa");

            string expectedMessage = loginPage.GetAlertMessage();
            string observedMessage = "Username or password is invalid";

            //VP: Verify that Dashboard Error message "Username or password is invalid" appears
            Assert.AreEqual(expectedMessage, observedMessage, "\nActual: " + observedMessage + "\nExpected: " + expectedMessage);
        }

        [TestMethod]
        public void TC004()
        {
            Console.WriteLine("DA_LOGIN_TC004 - Verify that user is able to log in different repositories successfully after logging out current repository");

            //1. Navigate to Dashboard login page. Enter valid username and password of default repository. Click on "Login" button

            LoginPage loginPage = new LoginPage();
            loginPage.Open().Login(Constant.Username, Constant.Password);

            //2. Click on "Logout" button. Select a different repository
            //3. Enter valid username and password of this repository

            loginPage.Logout();
            loginPage.SelectRepository(Constant.AdditionalRepo);
            loginPage.Login(Constant.Username, Constant.Password);

            bool observedResult = loginPage.IsDashboardMainpageDisplayed();

            //VP: Verify that Dashboard Mainpage appears
            Assert.AreEqual(true, observedResult, "Dashboard Mainpage is not displayed!");
        }

        [TestMethod]
        public void TC005()
        {
            Console.WriteLine("DA_LOGIN_TC005 - Verify that there is no Login dialog when switching between 2 repositories with the same account");

            //1. Navigate to Dashboard login page. Login with valid account for the first repository
            //2. Choose another repository in Repository list

            LoginPage loginPage = new LoginPage();
            loginPage.Open().Login(Constant.Username, Constant.Password);
            loginPage.ChooseRepository(Constant.AdditionalRepo);

            string actualRepositoryName = loginPage.GetRepositoryName();

            //VP: There is no Login Repository dialog
            //VP: The Repository menu displays name of switched repository

            Assert.AreEqual(false, loginPage.IsAlertDisplayed(), "Login Repository dialog displayed!");
            Assert.AreEqual(Constant.AdditionalRepo, actualRepositoryName, "\nActual: " + actualRepositoryName + "\nExpected: " + Constant.AdditionalRepo);
        }

        [TestMethod]

        public void TC006()
        {
            Console.WriteLine("DA_LOGIN_TC006 - Verify that \"Password\" input is case sensitive");

            //1. Navigate to Dashboard login page. Login with valid account for the first repository
            //2. Choose another repository in Repository list

            LoginPage loginPage = new LoginPage();
            loginPage.Open().Login(Constant.Username, Constant.Password);
            loginPage.ChooseRepository(Constant.AdditionalRepo);

            string actualRepositoryName = loginPage.GetRepositoryName();

            //VP: There is no Login Repository dialog
            //VP: The Repository menu displays name of switched repository

            Assert.AreEqual(false, loginPage.IsAlertDisplayed(), "Login Repository dialog displayed!");
            Assert.AreEqual(Constant.AdditionalRepo, actualRepositoryName, "\nActual: " + actualRepositoryName + "\nExpected: " + Constant.AdditionalRepo);
        }
    }
}
