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

        /// <summary>
        /// Verify that user can login specific repository successfully via Dashboard login page with correct credentials
        /// </summary>
        /// <Author>Long and Phat</Author>
        /// <Modified>Phat - 12/6/2016: Recheck and clean the code</Modified>
        /// <Startdate>11/5/2016</Startdate>
        [TestMethod]
        public void TC001()
        {
            Console.WriteLine("DA_LOGIN_TC001 - Verify that user can login specific repository successfully via Dashboard login page with correct credentials.");

            
            //1. Navigate to Dashboard login page.
            //2. Enter valid username and password.
            //3. Click on "Login" button

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password);

            bool actualResult = mainPage.IsDashboardMainpageDisplayed();

            //VP: Verify that Dashboard Mainpage appears

            Assert.AreEqual(true, actualResult, "Dashboard Mainpage is not displayed!");

        }

        /// <summary>
        /// Verify that user fails to login specific repository successfully via Dashboard login page with incorrect credentials.
        /// </summary>
        /// <Author>Long and Phat</Author>
        /// <Modified>Phat - 12/6/2016: Recheck and clean the code</Modified>
        /// <Startdate>11/5/2016</Startdate>
        [TestMethod]
        public void TC002()
        {
            Console.WriteLine("DA_LOGIN_TC002 - Verify that user fails to login specific repository successfully via Dashboard login page with incorrect credentials.");

            //1. Navigate to Dashboard login page.
            //2. Enter invalid username and password.
            //3. Click on "Login" button

            LoginPage loginPage = new LoginPage(driver);
            loginPage.Open().Login("aaa", "aaa");

            string expectedMessage = loginPage.GetAlertMessage();
            string actualMessage = "Username or password is invalid";

            //VP: Verify that Dashboard Error message "Username or password is invalid" appears

            Assert.AreEqual(expectedMessage, actualMessage, "\nActual: " + actualMessage + "\nExpected: " + expectedMessage);
        }

        /// <summary>
        /// Verify that user fails to log in specific repository successfully via Dashboard login page with correct username and incorrect password
        /// </summary>
        /// <Author>Long and Phat</Author>
        /// <Modified>Phat - 12/6/2016: Recheck and clean the code</Modified>
        /// <Startdate>11/5/2016</Startdate>
        [TestMethod]
        public void TC003()
        {
            Console.WriteLine("DA_LOGIN_TC003 - Verify that user fails to log in specific repository successfully via Dashboard login page with correct username and incorrect password.");

            //1. Navigate to Dashboard login page.
            //2. Enter valid username and inpassword.
            //3. Click on "Login" button

            LoginPage loginPage = new LoginPage(driver);
            loginPage.Open().Login(Constant.Username, "aaa");

            string expectedMessage = loginPage.GetAlertMessage();
            string actualMessage = "Username or password is invalid";

            //VP: Verify that Dashboard Error message "Username or password is invalid" appears

            Assert.AreEqual(expectedMessage, actualMessage, "\nActual: " + actualMessage + "\nExpected: " + expectedMessage);
        }

        /// <summary>
        /// Verify that user is able to log in different repositories successfully after logging out current repository
        /// </summary>
        /// <Author>Long and Phat</Author>
        /// <Modified>Phat - 12/6/2016: Recheck and clean the code</Modified>
        /// <Startdate>11/5/2016</Startdate>
        [TestMethod]
        public void TC004()
        {
            Console.WriteLine("DA_LOGIN_TC004 - Verify that user is able to log in different repositories successfully after logging out current repository");

            //1. Navigate to Dashboard login page. 
            //2. Enter valid username and password of default repository.
            //3. Click on "Login" button

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password);

            //4. Click on "Logout" button. Select a different repository
            //5. Enter valid username and password of this repository

            mainPage = mainPage.Logout().Login(Constant.Username, Constant.Password, Constant.AdditionalRepo);

            bool actualResult = mainPage.IsDashboardMainpageDisplayed();

            //VP: Verify that Dashboard Mainpage appears

            Assert.AreEqual(true, actualResult, "Dashboard Mainpage is not displayed!");
        }

        /// <summary>
        /// Verify that there is no Login dialog when switching between 2 repositories with the same account
        /// </summary>
        /// <Author>Long and Phat</Author>
        /// <Modified>Phat - 12/6/2016: Recheck and clean the code</Modified>
        /// <Startdate>11/5/2016</Startdate>
        [TestMethod]
        public void TC005()
        {
            Console.WriteLine("DA_LOGIN_TC005 - Verify that there is no Login dialog when switching between 2 repositories with the same account");

            //1. Navigate to Dashboard login page. Login with valid account for the first repository
            //2. Choose another repository in Repository list

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password, Constant.DefaultRepo);
            mainPage.ChooseRepository(Constant.AdditionalRepo);

            string actualRepositoryName = mainPage.GetRepositoryName();
            bool actualAlertDisplayed = mainPage.IsAlertDisplayed();

            //VP: There is no Login Repository dialog
            //VP: The Repository menu displays name of switched repository

            Assert.AreEqual(false, actualAlertDisplayed, "Login Repository dialog displayed!");
            Assert.AreEqual(Constant.AdditionalRepo, actualRepositoryName, "\nActual: " + actualRepositoryName + "\nExpected: " + Constant.AdditionalRepo);
        }

        /// <summary>
        /// Verify that "Password" input is case sensitive
        /// </summary>
        /// <Author>Long and Phat</Author>
        /// <Modified>Phat - 12/6/2016: Recheck and clean the code</Modified>
        /// <Startdate>11/5/2016</Startdate>
        [TestMethod]
        public void TC006()
        {
            Console.WriteLine("DA_LOGIN_TC006 - Verify that \"Password\" input is case sensitive");

            //1. Navigate to Dashboard login page.
            //2. Login with the account has uppercase password

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.OtherUsername, Constant.OtherPassword);

            bool actualMainPageDisplayed = mainPage.IsDashboardMainpageDisplayed();

            //VP: Main page is displayed

            Assert.AreEqual(true, actualMainPageDisplayed, "MainPage is not displayed!");

            //3. Logout TA Dashboard.
            //4. Login with the above account but enter lowercase password

            loginPage = mainPage.Logout();
            loginPage.Login(Constant.OtherUsername,Constant.OtherPassword.ToUpper());

            string actualMessage = loginPage.GetAlertMessage();
            string expectedMessage = "Username or password is invalid";

            //VP: The Repository menu displays name of switched repository

            Assert.AreEqual(expectedMessage, actualMessage, "\nActual: " + actualMessage + "\nExpected: " + expectedMessage);
        }

        /// <summary>Verify that "Username" is not case sensitive
        /// </summary>
        /// <Author>Long and Phat</Author>
        /// <Modified>Phat - 12/6/2016: Recheck and clean the code</Modified>
        /// <Startdate>11/5/2016</Startdate>
        [TestMethod]
        public void TC007()
        {
            Console.WriteLine("DA_LOGIN_TC007 - Verify that \"Username\" is not case sensitive");

            //1. Navigate to Dashboard login page.
            //2. Login with the account has uppercase username

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.OtherUsername.ToUpper(),Constant.OtherPassword);

            bool actualMainPageDisplayed = mainPage.IsDashboardMainpageDisplayed();

            //VP: Main page is displayed

            Assert.AreEqual(true, actualMainPageDisplayed, "MainPage is not displayed!");

            //3. Logout TA Dashboard.
            //4. Login with the above account but enter lowercase username

            mainPage = mainPage.Logout().Login(Constant.OtherUsername, Constant.OtherPassword);

            actualMainPageDisplayed = mainPage.IsDashboardMainpageDisplayed();

            //VP: The Repository menu displays name of switched repository

            Assert.AreEqual(true, actualMainPageDisplayed, "MainPage is not displayed!");
        }

        /// <summary>
        /// Verify that password with special characters is working correctly
        /// </summary>
        /// <Author>Long and Phat</Author>
        /// <Startdate>11/5/2016</Startdate>
        /// <Modified>Phat - 12/6/2016: Recheck and clean the code</Modified>
        [TestMethod]
        public void TC008()
        {
            Console.WriteLine("DA_LOGIN_TC008 - Verify that password with special characters is working correctly");

            //1. Navigate to Dashboard login page.
            //2. Login with account that has special characters password

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login("test1", @"!@#$%^&*()");

            bool actualMainPageDisplayed = mainPage.IsDashboardMainpageDisplayed();

            //VP: Main page is displayed

            Assert.AreEqual(true, actualMainPageDisplayed, "MainPage is not displayed!");
        }

        /// <summary>
        /// Verify that username with special characters is working correctly
        /// </summary>
        /// <Author>Long and Phat</Author>
        /// <Startdate>11/5/2016</Startdate>
        /// <Modified>Phat - 12/6/2016: Recheck and clean the code</Modified>
        [TestMethod]
        public void TC009()
        {
            Console.WriteLine("DA_LOGIN_TC009 - Verify that username with special characters is working correctly");

            //1. Navigate to Dashboard login page.
            //2. Login with account that has special characters username 

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(@"`~!@$^&()',.", "specialCharsUser");

            bool actualMainPageDisplayed = mainPage.IsDashboardMainpageDisplayed();

            //VP: Main page is displayed

            Assert.AreEqual(true, actualMainPageDisplayed, "MainPage is not displayed!");
        }

        /// <summary>
        /// Verify that the page works correctly for the case when no input entered to Password and Username field
        /// </summary>
        /// <Author>Long and Phat</Author>
        /// <Startdate>11/5/2016</Startdate>
        /// <Modified>Phat - 12/6/2016: Recheck and clean the code</Modified>
        [TestMethod]
        public void TC010()
        {
            Console.WriteLine("DA_LOGIN_TC010 - Verify that the page works correctly for the case when no input entered to Password and Username field");

            //1. Navigate to Dashboard login page.
            //2. Click Login button without entering data into Username and Password field 

            LoginPage loginPage = new LoginPage(driver);
            loginPage.Open().BtnLogin.Click();

            string actualMessage = loginPage.GetAlertMessage();
            string expectedMessage = "Please enter username";

            //VP: There is a message "Please enter username"

            Assert.AreEqual(expectedMessage, actualMessage, "\nActual: " + actualMessage + "\nExpected: " + expectedMessage);
        }
    }
}
