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

        /// <summary>Verify that all Pre-set Data Profiles are populated correctly
        /// </summary>
        /// <Author>Long</Author>
        /// <Startdate>02/06/2016</Startdate>
        [TestMethod]
        public void TC066()
        {
            Console.WriteLine("DA_LOGIN_TC066 - Verify that all Pre-set Data Profiles are populated correctly");
            int row_number;
            int column_number;

            //1. Navigate to Dashboard login page
            //2. Select a specific repository 
            //3. Enter valid Username and Password
            //4. Click Login

            LoginPage loginPage = new LoginPage(driver);
            loginPage.Open().Login(Constant.Username, Constant.Password);

            //5. Click Administer->Data Profiles

            DataProfilePage dataProfilePage = new DataProfilePage(driver);
            dataProfilePage.SelectMenuItem("Administer", "Data Profiles");

            //7. Check there is no 'Delele' or 'Edit' link appears in Action section of Pre-set Data Profiles

            dataProfilePage.GetIndexOfTableCellValue("Action Implementation By Status", out row_number, out column_number);
            string link = dataProfilePage.GetTableCellValue(row_number, 7);
            Assert.AreEqual(false, link.Contains("Delete"), "Delele link appears in Action section of Action Implementation By Status profile");
            Assert.AreEqual(false, link.Contains("Edit"), "Edit link appears in Action section of Action Implementation By Status profile");

            dataProfilePage.GetIndexOfTableCellValue("Test Case Execution", out row_number, out column_number);
            link = dataProfilePage.GetTableCellValue(row_number, 7);
            Assert.AreEqual(false, link.Contains("Delete"), "Delele link appears in Action section of Test Case Execution profile");
            Assert.AreEqual(false, link.Contains("Edit"), "Edit link appears in Action section of Test Case Execution profile");

            dataProfilePage.GetIndexOfTableCellValue("Test Case Execution Failure Trend", out row_number, out column_number);
            link = dataProfilePage.GetTableCellValue(row_number, 7);
            Assert.AreEqual(false, link.Contains("Delete"), "Delele link appears in Action section of Test Case Execution Failure Trendprofile");
            Assert.AreEqual(false, link.Contains("Edit"), "Edit link appears in Action section of Test Case Execution Failure Trend profile");

            dataProfilePage.GetIndexOfTableCellValue("Test Case Execution History", out row_number, out column_number);
            link = dataProfilePage.GetTableCellValue(row_number, 7);
            Assert.AreEqual(false, link.Contains("Delete"), "Delele link appears in Action section of Test Case Execution History profile");
            Assert.AreEqual(false, link.Contains("Edit"), "Edit link appears in Action section of Test Case Execution History profile");

            dataProfilePage.GetIndexOfTableCellValue("Test Case Execution Results", out row_number, out column_number);
            link = dataProfilePage.GetTableCellValue(row_number, 7);
            Assert.AreEqual(false, link.Contains("Delete"), "Delele link appears in Action section of Test Case Execution Results profile");
            Assert.AreEqual(false, link.Contains("Edit"), "Edit link appears in Action section of Test Case Execution Results profile");

            dataProfilePage.GetIndexOfTableCellValue("Test Case Execution Trend", out row_number, out column_number);
            link = dataProfilePage.GetTableCellValue(row_number, 7);
            Assert.AreEqual(false, link.Contains("Delete"), "Delele link appears in Action section of Test Case Execution Trend profile");
            Assert.AreEqual(false, link.Contains("Edit"), "Edit link appears in Action section of Test Case Execution Trend profile");

            dataProfilePage.GetIndexOfTableCellValue("Test Module Execution", out row_number, out column_number);
            link = dataProfilePage.GetTableCellValue(row_number, 7);
            Assert.AreEqual(false, link.Contains("Delete"), "Delele link appears in Action section of Test Module Execution profile");
            Assert.AreEqual(false, link.Contains("Edit"), "Edit link appears in Action section of Test Module Execution profile");

            dataProfilePage.GetIndexOfTableCellValue("Test Module Execution Failure Trend", out row_number, out column_number);
            link = dataProfilePage.GetTableCellValue(row_number, 7);
            Assert.AreEqual(false, link.Contains("Delete"), "Delele link appears in Action section of Test Module Execution Failure Trend profile");
            Assert.AreEqual(false, link.Contains("Edit"), "Edit link appears in Action section of Test Module Execution Failure Trend profile");

            dataProfilePage.GetIndexOfTableCellValue("Test Module Execution History", out row_number, out column_number);
            link = dataProfilePage.GetTableCellValue(row_number, 7);
            Assert.AreEqual(false, link.Contains("Delete"), "Delele link appears in Action section of Test Module Execution History profile");
            Assert.AreEqual(false, link.Contains("Edit"), "Edit link appears in Action section of Test Module Execution History profile");

            dataProfilePage.GetIndexOfTableCellValue("Test Module Execution Results", out row_number, out column_number);
            link = dataProfilePage.GetTableCellValue(row_number, 7);
            Assert.AreEqual(false, link.Contains("Delete"), "Delele link appears in Action section of Test Module Execution Results profile");
            Assert.AreEqual(false, link.Contains("Edit"), "Edit link appears in Action section of Test Module Execution Results profile");

            dataProfilePage.GetIndexOfTableCellValue("Test Module Execution Results Report", out row_number, out column_number);
            link = dataProfilePage.GetTableCellValue(row_number, 7);
            Assert.AreEqual(false, link.Contains("Delete"), "Delele link appears in Action section of Test Module Execution Results Reports profile");
            Assert.AreEqual(false, link.Contains("Edit"), "Edit link appears in Action section of Test Module Execution Results Report profile");

            dataProfilePage.GetIndexOfTableCellValue("Test Module Execution Trend", out row_number, out column_number);
            link = dataProfilePage.GetTableCellValue(row_number, 7);
            Assert.AreEqual(false, link.Contains("Delete"), "Delele link appears in Action section of Test Module Execution Trend profile");
            Assert.AreEqual(false, link.Contains("Edit"), "Edit link appears in Action section of Test Module Execution Trend profile");

            dataProfilePage.GetIndexOfTableCellValue("Test Module Implementation By Priority", out row_number, out column_number);
            link = dataProfilePage.GetTableCellValue(row_number, 7);
            Assert.AreEqual(false, link.Contains("Delete"), "Delele link appears in Action section of Test Module Implementation By Priority profile");
            Assert.AreEqual(false, link.Contains("Edit"), "Edit link appears in Action section of Test Module Implementation By Priority profile");

            dataProfilePage.GetIndexOfTableCellValue("Test Module Implementation By Status", out row_number, out column_number);
            link = dataProfilePage.GetTableCellValue(row_number, 7);
            Assert.AreEqual(false, link.Contains("Delete"), "Delele link appears in Action section of Test Module Implementation By Status profile");
            Assert.AreEqual(false, link.Contains("Edit"), "Edit link appears in Action section of Test Module Implementation By Status profile");

            dataProfilePage.GetIndexOfTableCellValue("Test Module Status per Assigned Users", out row_number, out column_number);
            link = dataProfilePage.GetTableCellValue(row_number, 7);
            Assert.AreEqual(false, link.Contains("Delete"), "Delele link appears in Action section of Test Module Status per Assigned Users profile");
            Assert.AreEqual(false, link.Contains("Edit"), "Edit link appears in Action section of Test Module Status per Assigned Users profile");

            dataProfilePage.GetIndexOfTableCellValue("Test Objective Execution", out row_number, out column_number);
            link = dataProfilePage.GetTableCellValue(row_number, 7);
            Assert.AreEqual(false, link.Contains("Delete"), "Delele link appears in Action section of Test Objective Execution profile");
            Assert.AreEqual(false, link.Contains("Edit"), "Edit link appears in Action section of Test Objective Execution profile");

            //8. Click on Pre-set Data Profile name
            //9. VP: Check there is no link on Pre-set Data Profile name

            Assert.AreEqual(false, dataProfilePage.IsDataProfileLink("Action Implementation By Status"), "Action Implementation By Status profile is a link");
            Assert.AreEqual(false, dataProfilePage.IsDataProfileLink("Test Case Execution"), "Test Case Execution profile is a link");
            Assert.AreEqual(false, dataProfilePage.IsDataProfileLink("Test Case Execution Failure Trend"), "Test Case Execution Failure Trend profile is a link");
            Assert.AreEqual(false, dataProfilePage.IsDataProfileLink("Test Case Execution History"), "Test Case Execution History profile is a link");
            Assert.AreEqual(false, dataProfilePage.IsDataProfileLink("Test Case Execution Results"), "Test Case Execution Results is a link");
            Assert.AreEqual(false, dataProfilePage.IsDataProfileLink("Test Case Execution Trend"), "Test Case Execution Trend profile is a link");
            Assert.AreEqual(false, dataProfilePage.IsDataProfileLink("Test Module Execution"), "Test Module Execution is a link");
            Assert.AreEqual(false, dataProfilePage.IsDataProfileLink("Test Module Execution Failure Trend"), "Test Module Execution Failure Trend profile is a link");
            Assert.AreEqual(false, dataProfilePage.IsDataProfileLink("Test Module Execution History"), "Test Module Execution History profile is a link");
            Assert.AreEqual(false, dataProfilePage.IsDataProfileLink("Test Module Execution Results"), "Test Module Execution Results profile is a link");
            Assert.AreEqual(false, dataProfilePage.IsDataProfileLink("Test Module Execution Results Report"), "Test Module Execution Results Report profile is a link");
            Assert.AreEqual(false, dataProfilePage.IsDataProfileLink("Test Module Execution Trend"), "Test Module Execution Trend profile is a link");
            Assert.AreEqual(false, dataProfilePage.IsDataProfileLink("Test Module Implementation By Priority"), "Test Module Implementation By Priority profile is a link");
            Assert.AreEqual(false, dataProfilePage.IsDataProfileLink("Test Module Implementation By Status"), "Test Module Implementation By Status profile is a link");
            Assert.AreEqual(false, dataProfilePage.IsDataProfileLink("Test Module Status per Assigned Users"), "Test Module Status per Assigned Users profile is a link");
            Assert.AreEqual(false, dataProfilePage.IsDataProfileLink("Test Objective Execution"), "Test Objective Execution profile is a link");
  
            //10. VP: Check there is no checkbox appears in the left of Pre-set Data Profiles

            Assert.AreEqual(false, dataProfilePage.DoesCheckboxAppearInTheLeftOfDataProfile("Action Implementation By Status"), "Checkbox appears in the left of Action Implementation By Status profile");
            Assert.AreEqual(false, dataProfilePage.DoesCheckboxAppearInTheLeftOfDataProfile("Test Case Execution"), "Checkbox appears in the left of Test Case Execution profile is a link");
            Assert.AreEqual(false, dataProfilePage.DoesCheckboxAppearInTheLeftOfDataProfile("Test Case Execution Failure Trend"), "Checkbox appears in the left of Test Case Execution Failure Trend profile");
            Assert.AreEqual(false, dataProfilePage.DoesCheckboxAppearInTheLeftOfDataProfile("Test Case Execution History"), "Checkbox appears in the left of Test Case Execution History profile");
            Assert.AreEqual(false, dataProfilePage.DoesCheckboxAppearInTheLeftOfDataProfile("Test Case Execution Results"), "Checkbox appears in the left of Test Case Execution Results");
            Assert.AreEqual(false, dataProfilePage.DoesCheckboxAppearInTheLeftOfDataProfile("Test Case Execution Trend"), "Checkbox appears in the left of Test Case Execution Trend profile");
            Assert.AreEqual(false, dataProfilePage.DoesCheckboxAppearInTheLeftOfDataProfile("Test Module Execution"), "Checkbox appears in the left of Test Module Execution");
            Assert.AreEqual(false, dataProfilePage.DoesCheckboxAppearInTheLeftOfDataProfile("Test Module Execution Failure Trend"), "Checkbox appears in the left of Test Module Execution Failure Trend profile");
            Assert.AreEqual(false, dataProfilePage.DoesCheckboxAppearInTheLeftOfDataProfile("Test Module Execution History"), "Checkbox appears in the left of Test Module Execution History profile");
            Assert.AreEqual(false, dataProfilePage.DoesCheckboxAppearInTheLeftOfDataProfile("Test Module Execution Results"), "Checkbox appears in the left of Test Module Execution Results profile");
            Assert.AreEqual(false, dataProfilePage.DoesCheckboxAppearInTheLeftOfDataProfile("Test Module Execution Results Report"), "Checkbox appears in the left of Test Module Execution Results Report profile");
            Assert.AreEqual(false, dataProfilePage.DoesCheckboxAppearInTheLeftOfDataProfile("Test Module Execution Trend"), "Checkbox appears in the left of Test Module Execution Trend profile");
            Assert.AreEqual(false, dataProfilePage.DoesCheckboxAppearInTheLeftOfDataProfile("Test Module Implementation By Priority"), "Checkbox appears in the left of Test Module Implementation By Priority profile");
            Assert.AreEqual(false, dataProfilePage.DoesCheckboxAppearInTheLeftOfDataProfile("Test Module Implementation By Status"), "Checkbox appears in the left of Test Module Implementation By Status profile");
            Assert.AreEqual(false, dataProfilePage.DoesCheckboxAppearInTheLeftOfDataProfile("Test Module Status per Assigned Users"), "Checkbox appears in the left of Test Module Status per Assigned Users profile");
            Assert.AreEqual(false, dataProfilePage.DoesCheckboxAppearInTheLeftOfDataProfile("Test Objective Execution"), "Checkbox appears in the left of Test Objective Execution profile");
        }
    }
}
