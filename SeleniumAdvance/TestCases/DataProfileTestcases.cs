using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumAdvance.PageObjects;
using SeleniumAdvance.Common;
using SeleniumAdvance.DataObjects;
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

            DataProfiles dataProfiles = new DataProfiles();
            dataProfiles.DataProfileInfo("Action Implementation By Status", "Action", "");
            bool doesProfileExist = dataProfilePage.DoesPresetDataProfileExist(dataProfiles);
            Assert.AreEqual(true, doesProfileExist, "Action Implementation By Status profile doesn't exist");

            dataProfiles.DataProfileInfo("Test Case Execution", "Test Case", "");
            doesProfileExist = dataProfilePage.DoesPresetDataProfileExist(dataProfiles);
            Assert.AreEqual(true, doesProfileExist, "Test Case Execution profile doesn't exist");

            dataProfiles.DataProfileInfo("Test Case Execution", "Test Case", "");
            doesProfileExist = dataProfilePage.DoesPresetDataProfileExist(dataProfiles);
            Assert.AreEqual(true, doesProfileExist, "Test Case Execution profile doesn't exist");

            dataProfiles.DataProfileInfo("Test Case Execution Failure Trend", "Test Case", "Related Run Results");
            doesProfileExist = dataProfilePage.DoesPresetDataProfileExist(dataProfiles);
            Assert.AreEqual(true, doesProfileExist, "Test Case Execution Failure Trend doesn't exist");

            dataProfiles.DataProfileInfo("Test Case Execution History", "Test Case", "Related Run Results");
            doesProfileExist = dataProfilePage.DoesPresetDataProfileExist(dataProfiles);
            Assert.AreEqual(true, doesProfileExist, "Test Case Execution History doesn't exist");

            dataProfiles.DataProfileInfo("Test Case Execution Results", "Test Case", "Related Run Results");
            doesProfileExist = dataProfilePage.DoesPresetDataProfileExist(dataProfiles);
            Assert.AreEqual(true, doesProfileExist, "Test Case Execution Results doesn't exist");

            dataProfiles.DataProfileInfo("Test Case Execution Trend", "Test Case", "Related Run Results");
            doesProfileExist = dataProfilePage.DoesPresetDataProfileExist(dataProfiles);
            Assert.AreEqual(true, doesProfileExist, "Test Case Execution Trend doesn't exist");

            dataProfiles.DataProfileInfo("Test Module Execution", "Test Module", "");
            doesProfileExist = dataProfilePage.DoesPresetDataProfileExist(dataProfiles);
            Assert.AreEqual(true, doesProfileExist, "Test Module Execution doesn't exist");

            dataProfiles.DataProfileInfo("Test Module Execution Failure Trend", "Test Module", "Related Test Results");
            doesProfileExist = dataProfilePage.DoesPresetDataProfileExist(dataProfiles);
            Assert.AreEqual(true, doesProfileExist, "Test Module Execution Failure Trend doesn't exist");

            dataProfiles.DataProfileInfo("Test Module Execution History", "Test Module", "Related Test Results");
            doesProfileExist = dataProfilePage.DoesPresetDataProfileExist(dataProfiles);
            Assert.AreEqual(true, doesProfileExist, "Test Module Execution History doesn't exist");

            dataProfiles.DataProfileInfo("Test Module Execution Results", "Test Module", "Related Test Results");
            doesProfileExist = dataProfilePage.DoesPresetDataProfileExist(dataProfiles);
            Assert.AreEqual(true, doesProfileExist, "Test Module Execution Results doesn't exist");

            dataProfiles.DataProfileInfo("Test Module Execution Results Report", "Test Module", "Related Test Results");
            doesProfileExist = dataProfilePage.DoesPresetDataProfileExist(dataProfiles);
            Assert.AreEqual(true, doesProfileExist, "Test Module Execution Results Reports doesn't exist");

            dataProfiles.DataProfileInfo("Test Module Execution Trend", "Test Module", "Related Test Results");
            doesProfileExist = dataProfilePage.DoesPresetDataProfileExist(dataProfiles);
            Assert.AreEqual(true, doesProfileExist, "Test Module Execution Trend doesn't exist");

            dataProfiles.DataProfileInfo("Test Module Implementation By Priority", "Test Module", "");
            doesProfileExist = dataProfilePage.DoesPresetDataProfileExist(dataProfiles);
            Assert.AreEqual(true, doesProfileExist, "Test Module Implementation By Priority doesn't exist");

            dataProfiles.DataProfileInfo("Test Module Implementation By Status", "Test Module", "");
            doesProfileExist = dataProfilePage.DoesPresetDataProfileExist(dataProfiles);
            Assert.AreEqual(true, doesProfileExist, "Test Module Implementation By Status doesn't exist");

            dataProfiles.DataProfileInfo("Test Module Status per Assigned Users", "Test Module", "");
            doesProfileExist = dataProfilePage.DoesPresetDataProfileExist(dataProfiles);
            Assert.AreEqual(true, doesProfileExist, "Test Module Status per Assigned Users doesn't exist");

            dataProfiles.DataProfileInfo("Test Objective Execution", "Test Objective", "");
            doesProfileExist = dataProfilePage.DoesPresetDataProfileExist(dataProfiles);
            Assert.AreEqual(true, doesProfileExist, "Test Objective Execution doesn't exist");
        }
    }
}
