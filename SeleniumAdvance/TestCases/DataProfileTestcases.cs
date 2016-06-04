using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumAdvance.PageObjects;
using SeleniumAdvance.Common;
using SeleniumAdvance.DataObjects;
using SeleniumAdvance.Ultilities;
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

        /// <summary>Verify that Data Profiles are listed alphabetically
        /// </summary>
        /// <author>Long</author>
        /// <startdate>04/06/2016</startdate>
        [TestMethod]
        public void TC067()
        {
            Console.WriteLine("DA_LOGIN_TC067 - Verify that Data Profiles are listed alphabetically");

            //1. Navigate to Dashboard login page
            //2. Select a specific repository 
            //3. Enter valid Username and Password
            //4. Click Login
            //5. Click Administer->Data Profiles

            LoginPage loginPage = new LoginPage(driver);
            loginPage.Open().Login(Constant.Username, Constant.Password);

            DataProfilePage dataProfilePage = new DataProfilePage(driver);
            dataProfilePage.SelectMenuItem("Administer", "Data Profiles");

            //6. VP: Check Data Profiles are listed alphabetically

            bool areDataProfileListedAlphabetically = dataProfilePage.AreDataProfilesListedAlphabetically();
            Assert.AreEqual(true, areDataProfileListedAlphabetically, "Data profiles are not listed alphabetically");
        }

        /// <summary>Verify that Check Boxes are only present for non-preset Data Profiles
        /// </summary>
        /// <author>Long</author>
        /// <startdate>04/06/2016</startdate>
        [TestMethod]
        public void TC068()
        {
            Console.WriteLine("DA_LOGIN_TC068 - Verify that Check Boxes are only present for non-preset Data Profiles.");

            string profileName = string.Concat("Profile ", CommonMethods.GetUniqueString());

            //1. Navigate to Dashboard login page
            //2. Select a specific repository 
            //3. Enter valid Username and Password
            //4. Click Login
            //5. Click Administer->Data Profiles

            LoginPage loginPage = new LoginPage(driver);
            loginPage.Open().Login(Constant.Username, Constant.Password);

            DataProfilePage dataProfilePage = new DataProfilePage(driver);
            dataProfilePage.SelectMenuItem("Administer", "Data Profiles");

            //6. Create a new Data Profile

            dataProfilePage.LnkAddNew.Click();
            dataProfilePage.CreateDataProfile(profileName, "test cases", "None");

            //7. Back to Data Profiles page
            //8. VP: Check Check Boxes are only present for non-preset Data Profiles.

            Assert.AreEqual(true, dataProfilePage.DoesCheckboxAppearInTheLeftOfDataProfile(profileName), "Checkbox appears in the left of " + dataProfilePage + " profile");

            //Post-condition: Delete created profile

            dataProfilePage.DeleteDataProfile(profileName);
        }

        /// <summary>Verify that user is unable to proceed to next step or finish creating data profile if  "Name *" field is left empty
        /// </summary>
        /// <author>Long</author>
        /// <startdate>04/06/2016</startdate>
        [TestMethod]
        public void TC069()
        {
            Console.WriteLine("DA_LOGIN_TC069 - Verify that user is unable to proceed to next step or finish creating data profile if  \"Name *\" field is left empty");

            //1. Log in Dashboard
            //2. Navigate to Data Profiles page
            //3. Click on "Add New"
            //4. Click on "Next Button"

            LoginPage loginPage = new LoginPage(driver);
            loginPage.Open().Login(Constant.Username, Constant.Password);

            DataProfilePage dataProfilePage = new DataProfilePage(driver);
            dataProfilePage.SelectMenuItem("Administer", "Data Profiles");
            dataProfilePage.LnkAddNew.Click();
            dataProfilePage.CreateDataProfile("", "test cases", "None", displayFields: true);

            //5. VP: Check dialog message "Please input profile name" appears

            string actualDialogMessage = dataProfilePage.GetAlertMessage(closeAlert: true);
            string expectedDialogMessage = "Please input profile name.";
            Assert.AreEqual(expectedDialogMessage, actualDialogMessage, "Please input profile name message doesn't appear");

            //6. Click on "Finish Button"

            dataProfilePage.CreateDataProfile("", "test cases", "None");

            //7. VP: Check dialog message "Please input profile name" appears

            actualDialogMessage = dataProfilePage.GetAlertMessage(closeAlert: true);
            expectedDialogMessage = "Please input profile name.";
            Assert.AreEqual(expectedDialogMessage, actualDialogMessage, "Please input profile name message doesn't appear");
        }

        /// <summary>Verify that special characters ' /:*?<>|"#[ ]{}=%; 'is not allowed for input to "Name *" field
        /// </summary>
        /// <author>Long</author>
        /// <startdate>04/06/2016</startdate>
        [TestMethod]
        public void TC070()
        {
            Console.WriteLine("DA_LOGIN_TC070 - Verify that special characters ' /:*?<>|\"#[ ]{}=%; 'is not allowed for input to \"Name *\" field");

            //1. Log in Dashboard
            //2. Navigate to Data Profiles page
            //3. Click on "Add New"
            //4. Input special character

            LoginPage loginPage = new LoginPage(driver);
            loginPage.Open().Login(Constant.Username, Constant.Password);

            DataProfilePage dataProfilePage = new DataProfilePage(driver);
            dataProfilePage.SelectMenuItem("Administer", "Data Profiles");
            dataProfilePage.LnkAddNew.Click();
            dataProfilePage.CreateDataProfile("/:*?<>", "test cases", "None");

            //5. VP: Check dialog message indicates invalid characters: /:*?<>|"#[ ]{}=%; is not allowed as input for name field appears

            string actualDialogMessage = dataProfilePage.GetAlertMessage(closeAlert: true);
            string expectedDialogMessage = "Invalid name. The name cannot contain high ASCII characters or any of the following characters: /:*?<>|\"#[]{}=%;";
            Assert.AreEqual(expectedDialogMessage, actualDialogMessage, "Invalid name message doesn't appear");
        }

        /// <summary>Verify that Data Profile names are not case sensitive
        /// </summary>
        /// <author>Long</author>
        /// <startdate>04/06/2016</startdate>
        [TestMethod]
        public void TC071()
        {
            Console.WriteLine("DA_LOGIN_TC070 - Verify that Data Profile names are not case sensitive");

            //1. Log in Dashboard
            //2. Navigate to Data Profiles page
            //3. Click on "Add New"
            //4. Input name of a pre-set profile into "Name *" field with lower case
            //5. Click "Next" button 

            LoginPage loginPage = new LoginPage(driver);
            loginPage.Open().Login(Constant.Username, Constant.Password);

            DataProfilePage dataProfilePage = new DataProfilePage(driver);
            dataProfilePage.SelectMenuItem("Administer", "Data Profiles");
            dataProfilePage.LnkAddNew.Click();
            dataProfilePage.CreateDataProfile("action implementation by status", "test cases", "None", displayFields: true);

            //6. VP: Check dialog message "Data Profile name already exists"

            string actualDialogMessage = dataProfilePage.GetAlertMessage(closeAlert: true);
            string expectedDialogMessage = "Data profile name already exists.";
            Assert.AreEqual(expectedDialogMessage, actualDialogMessage, "Data Profile names are case sensitive");
        }

        /// <summary>Verify that all data profile types are listed under "Item Type" dropped down menu
        /// </summary>
        /// <author>Long</author>
        /// <startdate>04/06/2016</startdate>
        [TestMethod]
        public void TC072()
        {
            Console.WriteLine("DA_LOGIN_TC072 - Verify that all data profile types are listed under \"Item Type\" dropped down menu");

            //1. Navigate to Dashboard login page
            //2. Select a specific repository 
            //3. Enter valid Username and Password
            //4. Click Login
            //5. Click Administer->Data Profiles
            //6. Click 'Add New' link

            LoginPage loginPage = new LoginPage(driver);
            loginPage.Open().Login(Constant.Username, Constant.Password);

            DataProfilePage dataProfilePage = new DataProfilePage(driver);
            dataProfilePage.SelectMenuItem("Administer", "Data Profiles");
            dataProfilePage.LnkAddNew.Click();

            //7. "Check all data profile types are listed under ""Item Type"" dropped down menu in create profile page"

            Assert.AreEqual(true, dataProfilePage.IsItemPresentInCombobox("Item Type", "Test Modules", attribute: "text"), "Test Modules type isn't listed under dropped down menu");
            Assert.AreEqual(true, dataProfilePage.IsItemPresentInCombobox("Item Type", "Test Cases", attribute: "text"), "Test Cases type isn't listed under dropped down menu");
            Assert.AreEqual(true, dataProfilePage.IsItemPresentInCombobox("Item Type", "Test Objectives", attribute: "text"), "Test Objectives type isn't listed under dropped down menu");
            Assert.AreEqual(true, dataProfilePage.IsItemPresentInCombobox("Item Type", "Data Sets", attribute: "text"), "Data Sets type isn't listed under dropped down menu");
            Assert.AreEqual(true, dataProfilePage.IsItemPresentInCombobox("Item Type", "Actions", attribute: "text"), "Actions type isn't listed under dropped down menu");
            Assert.AreEqual(true, dataProfilePage.IsItemPresentInCombobox("Item Type", "Interface Entities", attribute: "text"), "Interface Entities type isn't listed under dropped down menu");
            Assert.AreEqual(true, dataProfilePage.IsItemPresentInCombobox("Item Type", "Test Results", attribute: "text"), "Test Results type isn't listed under dropped down menu");
            Assert.AreEqual(true, dataProfilePage.IsItemPresentInCombobox("Item Type", "Test Case Results", attribute: "text"), "Test Case Results type isn't listed under dropped down menu");
            Assert.AreEqual(true, dataProfilePage.IsItemPresentInCombobox("Item Type", "Test Suites", attribute: "text"), "Test Suites type isn't listed under dropped down menu");
            Assert.AreEqual(true, dataProfilePage.IsItemPresentInCombobox("Item Type", "Bugs", attribute: "text"), "Bugs type isn't listed under dropped down menu");
        }

        /// <summary>Verify that all data profile types are listed in priority order under "Item Type" dropped down menu
        /// </summary>
        /// <author>Long</author>
        /// <startdate>04/06/2016</startdate>
        [TestMethod]
        public void TC073()
        {
            Console.WriteLine("DA_LOGIN_TC073 - Verify that all data profile types are listed in priority order under \"Item Type\" dropped down menu");

            //1. Log in Dashboard
            //2. Navigate to Data Profiles page
            //3. Click on "Add New"

            LoginPage loginPage = new LoginPage(driver);
            loginPage.Open().Login(Constant.Username, Constant.Password);

            DataProfilePage dataProfilePage = new DataProfilePage(driver);
            dataProfilePage.SelectMenuItem("Administer", "Data Profiles");
            dataProfilePage.LnkAddNew.Click();

            //4. Click on "Item Type" dropped down menu
            //5. VP: Check "Item Type" items are listed in priority order

            int position1 = dataProfilePage.GetItemPositionInCombobox("Item Type", "Test Modules");
            int position2 = dataProfilePage.GetItemPositionInCombobox("Item Type", "Test Cases");
            int position3 = dataProfilePage.GetItemPositionInCombobox("Item Type", "Test Objectives");
            int position4 = dataProfilePage.GetItemPositionInCombobox("Item Type", "Data Sets");
            int position5 = dataProfilePage.GetItemPositionInCombobox("Item Type", "Actions");
            int position6 = dataProfilePage.GetItemPositionInCombobox("Item Type", "Interface Entities");
            int position7 = dataProfilePage.GetItemPositionInCombobox("Item Type", "Test Results");
            int position8 = dataProfilePage.GetItemPositionInCombobox("Item Type", "Test Case Results");
            int position9 = dataProfilePage.GetItemPositionInCombobox("Item Type", "Test Suites");
            int position10 = dataProfilePage.GetItemPositionInCombobox("Item Type", "Bugs");
            Assert.AreEqual(true, position1 < position2 && position2 < position3 && position3 < position4 && position4 < position5 &&
                position5 < position6 && position6 < position7 && position7 < position8 && position8 < position9 &&
                position9 < position10, "Item Type items are not listed in priority order");
        }

        /// <summary>Verify that appropriate "Related Data" items are listed correctly corresponding to the "Item Type" items.			
        /// </summary>
        /// <author>Long</author>
        /// <startdate>04/06/2016</startdate>
        [TestMethod]
        public void TC074()
        {
            Console.WriteLine("DA_LOGIN_TC074 - Verify that appropriate \"Related Data\" items are listed correctly corresponding to the \"Item Type\" items.");

            //1. Navigate to Dashboard login page
            //2. Select a specific repository 
            //3. Enter valid Username and Password
            //4. Click Login
            //5. Click Administer->Data Profiles
            //6. Click Add new link

            LoginPage loginPage = new LoginPage(driver);
            loginPage.Open().Login(Constant.Username, Constant.Password);

            DataProfilePage dataProfilePage = new DataProfilePage(driver);
            dataProfilePage.SelectMenuItem("Administer", "Data Profiles");
            dataProfilePage.LnkAddNew.Click();

            //7. Select 'Test Modules' in 'Item Type' drop down list

            dataProfilePage.CmbItemType.SelectItem("test modules");

            //8. VP: Check 'Related Data' items listed correctly - Related Test Results and Related Test Cases

            Assert.AreEqual(true, dataProfilePage.IsItemPresentInCombobox("Related Data", "Related Test Results", attribute: "text"), "Related Test Results isn't listed under dropped down menu");
            Assert.AreEqual(true, dataProfilePage.IsItemPresentInCombobox("Related Data", "Related Test Cases", attribute: "text"), "Related Test Cases isn't listed under dropped down menu");

            //9. Select 'Test Cases' in 'Item Type' drop down list 

            dataProfilePage.CmbItemType.SelectItem("test cases");

            //10. VP: Check 'Related Data' items listed correctly - Related Run Results and Related Objectives

            Assert.AreEqual(true, dataProfilePage.IsItemPresentInCombobox("Related Data", "Related Run Results", attribute: "text"), "Related Run Results isn't listed under dropped down menu");
            Assert.AreEqual(true, dataProfilePage.IsItemPresentInCombobox("Related Data", "Related Objectives", attribute: "text"), "Related Objectives isn't listed under dropped down menu");

            //11. Select 'Test Objectives' in 'Item Type' drop down list

            dataProfilePage.CmbItemType.SelectItem("test objectives");

            //12. Check 'Related Data' items listed correctly - Related Run Results and Related Test Cases
  
            Assert.AreEqual(true, dataProfilePage.IsItemPresentInCombobox("Related Data", "Related Run Results", attribute: "text"), "Related Run Results isn't listed under dropped down menu");
            Assert.AreEqual(true, dataProfilePage.IsItemPresentInCombobox("Related Data", "Related Test Cases", attribute: "text"), "Related Test Cases isn't listed under dropped down menu");

            //13. Select 'Data Sets' in 'Item Type' drop down list

            dataProfilePage.CmbItemType.SelectItem("data sets");

            //14. VP: Check 'Related Data' items listed correctly - No related data appears

            Assert.AreEqual(1, dataProfilePage.GetNumberOfItemsInCombobox("Related Data"), "There is(are) item(s) listed");

            //15. Select 'Actions' in 'Item Type' drop down list

            dataProfilePage.CmbItemType.SelectItem("actions");

            //16. VP: Check 'Related Data' items listed correctly - Action Arguments

            Assert.AreEqual(true, dataProfilePage.IsItemPresentInCombobox("Related Data", "Action Arguments", attribute: "text"), "Action Arguments isn't listed under dropped down menu");

            //17. Select 'Interface Entities' in 'Item Type' drop down list

            dataProfilePage.CmbItemType.SelectItem("interface entities");

            //18. VP: Check 'Related Data' items listed correctly - Interface Elements

            Assert.AreEqual(true, dataProfilePage.IsItemPresentInCombobox("Related Data", "Interface Elements", attribute: "text"), "Interface Elements isn't listed under dropped down menu");

            //19. Select 'Test Results' in 'Item Type' drop down list

            dataProfilePage.CmbItemType.SelectItem("test results");

            //20. VP: Check 'Related Data' items listed correctly - Related Test Modules and Related Test Cases

            Assert.AreEqual(true, dataProfilePage.IsItemPresentInCombobox("Related Data", "Related Test Modules", attribute: "text"), "Related Test Modules isn't listed under dropped down menu");
            Assert.AreEqual(true, dataProfilePage.IsItemPresentInCombobox("Related Data", "Related Test Cases", attribute: "text"), "Related Test Cases isn't listed under dropped down menu");

            //21. Select 'Test Case Results' in 'Item Type' drop down list

            dataProfilePage.CmbItemType.SelectItem("test case results");

            //22. VP: Check 'Related Data' items listed correctly - No related data appears

            Assert.AreEqual(1, dataProfilePage.GetNumberOfItemsInCombobox("Related Data"), "There is(are) item(s) listed");
        }

    }
}
