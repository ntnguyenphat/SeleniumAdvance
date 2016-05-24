using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumAdvance.PageObjects;
using SeleniumAdvance.Common;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumAdvance.Ultilities;
using System.Collections.Generic;

namespace SeleniumAdvance.TestCases
{
    [TestClass]
    public class PanelTestcases : TestBase
    {
        [TestMethod]
        public void TC027()
        {
            Console.WriteLine("DA_PANEL_TC027 - Verify that when \"Choose panels\" form is expanded all pre-set panels are populated and sorted correctly ");

            string pageName = string.Concat("Page", CommonMethods.GetUniqueString());

            //1. Navigate to Dashboard login page
            //2. Login with valid account
            //3. Go to Global Setting -> Add page
            //4. Enter page name to Page Name field.
            //5. Click OK button
            //6. Go to Global Setting -> Create Panel

            LoginPage loginPage = new LoginPage(driver);
            loginPage.Open().Login(Constant.Username, Constant.Password, Constant.DefaultRepo);

            MainPage mainPage = new MainPage(driver);
            mainPage.AddPage(pageName: pageName);

            mainPage.SelectGeneralSetting("Create Panel");

            PanelPage panelPage = new PanelPage(driver);

            List<string> list = new List<string>();
            list.Add("Action Implementation By Status");
            list.Add("Test Case Execution Failure Trend");
            list.Add("Test Case Execution Results");
            list.Add("Test Case Execution Trend");
            list.Add("Test Module Execution Failure Trend");
            list.Add("Test Module Execution Results");
            list.Add("Test Module Execution Trend");
            list.Add("Test Module Implementation By Priority");
            list.Add("Test Module Implementation By Status");
            list.Add("Test Module Status per Assigned Users");

            //VP: Verify that all pre-set panels are populated and sorted correctly

            for (int i = 0; i < list.Count; i++)
            {
                bool actual = panelPage.IsProfileExist(list[i]);
                Assert.AreEqual(true, actual, "\nItem " + list[i] + " is not exist~!");
            }

            //Post-Condition: Delete the created page
            panelPage.BtnCancel.Click();
            mainPage.DeletePage(pageName);
        }

        [TestMethod]
        public void TC028()
        {
            Console.WriteLine("DA_PANEL_TC028 - Verify that when \"Add New Panel\" form is on focused all other control/form is disabled or locked.");

            //1. Navigate to Dashboard login page
            //2. Login with valid account
            //3. Click Administer link
            //4. Click Panel link
            //5. Click Add New link
            //6. Try to click other controls when Add New Panel dialog is opening

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password, Constant.DefaultRepo);

            mainPage.SelectMenuItem("Administer", "Panels");

            PanelPage panelPage = new PanelPage(driver);
            panelPage.LnkAddNew.Click();

            bool actual = panelPage.IsDashboardLockedByDialog();

            //VP: All control/form are disabled or locked when Add New Panel dialog is opening

            Assert.AreEqual(true, actual, "\nDashboard is not locked by dialog!");
        }

        [TestMethod]
        public void TC029()
        {
            Console.WriteLine("DA_PANEL_TC029 - Verify that user is unable to create new panel when (*) required field is not filled");

            //1. Navigate to Dashboard login page
            //2. Select specific repository
            //3. Enter valid username and password
            //4. Click on Login button
            //5. Click on Administer/Panels link
            //6. Click on "Add new" link
            //7. Enter value into Display Name field with special characters except "@"
            //8. Click on OK button

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password, Constant.DefaultRepo);

            mainPage.SelectMenuItem("Administer", "Panels");

            PanelPage panelPage = new PanelPage(driver);
            panelPage.LnkAddNew.Click();
            panelPage.BtnOK.Click();

            string actual = panelPage.GetAlertMessage();
            string expected = "Display Name is required field";

            //VP: Warning message: "Display Name is required field" show up

            Assert.AreEqual(expected, actual, "\nExpected: " + expected + "\nActual: " + actual);
        }

        [TestMethod]
        public void TC030()
        {
            Console.WriteLine("DA_PANEL_TC030 - Verify that no special character except '@' character is allowed to be inputted into \"Display Name\" field");

            string panelFalse = "Logigear#$%";
            string panelTrue = string.Concat("@", CommonMethods.GetUniqueString());
            string panelSeries = "name";

            //1. Navigate to Dashboard login page
            //2. Select specific repository
            //3. Enter valid username and password
            //4. Click on Login button
            //5. Click on Administer/Panels link
            //6. Click on "Add new" link
            //7. Click on OK button

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password, Constant.DefaultRepo);

            mainPage.SelectMenuItem("Administer", "Panels");

            PanelPage panelPage = new PanelPage(driver);
            panelPage.LnkAddNew.Click();
            panelPage.TxtDisplayName.SendKeys(panelFalse);
            panelPage.BtnOK.Click();

            string actual = panelPage.GetAlertMessage(closeAlert: true);
            string expected = "Invalid display name. The name can't contain high ASCII characters or any of following characters: /:*?<>|\"#{[]{};";

            //VP: Warning message: "Display Name is required field" show up

            Assert.AreEqual(expected, actual, "\nExpected: " + expected + "\nActual: " + actual);

            //8. Close Warning Message box
            //9. Click Add New link
            //10. Enter value into Display Name field with special character is @

            panelPage.TxtDisplayName.Clear();
            panelPage.TxtDisplayName.SendKeys(panelTrue);
            panelPage.ChbSeries.SelectItem(panelSeries, "Value");
            panelPage.BtnOK.Click();

            bool actualCreated = panelPage.IsPanelCreated(panelTrue);

            //VP: The new panel is created

            Assert.AreEqual(true, actualCreated, "\nPanel is not created!");

            //Post-condition: Delete created panel

            panelPage.DeletePanel(panelTrue);
        }

        [TestMethod]
        public void TC031()
        {
            Console.WriteLine("DA_PANEL_TC031 - Verify that correct panel setting form is displayed with corresponding panel type selected");

            //1. Navigate to Dashboard login page
            //2. Login with valid account
            //3. Click on Administer/Panels link
            //4. Click on Add new link

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password, Constant.DefaultRepo);

            mainPage.SelectMenuItem("Administer", "Panels");

            PanelPage panelPage = new PanelPage(driver);
            panelPage.LnkAddNew.Click();

            string actual = panelPage.GetSettingHeader();
            string expected = "Chart Settings";

            //VP: Chart panel setting form is displayed "chart setting" under Display Name field

            Assert.AreEqual(expected, actual, "\nExpected: " + expected + "\nActual: " + actual);

            //5. Select Indicator type

            panelPage.RdIndicator.ChooseAndWait(TimeSpan.FromSeconds(3));

            actual = panelPage.GetSettingHeader();
            expected = "Indicator setting";

            //VP: Chart panel setting form is displayed "Indicator setting" under Display Name field

            Assert.AreEqual(expected, actual, "\nExpected: " + expected + "\nActual: " + actual);

            //6: Select Report type

            panelPage.RdReport.Click();

            //VP:TODO - Report panel setting form is displayed "View mode" under Display Name.

        }

        [TestMethod]
        public void TC032()
        {
            Console.WriteLine("DA_PANEL_TC032 - Verify that user is not allowed to create panel with duplicated \"Display Name\"");

            string panelName = CommonMethods.GetUniqueString();
            string panelSeries = "name";

            //1. Navigate to Dashboard login page
            //2. Login with valid account
            //3. Click on Administer/Panels link
            //4. Click on Add new link

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password, Constant.DefaultRepo);

            mainPage.SelectMenuItem("Administer", "Panels");
            PanelPage panelPage = new PanelPage(driver);
            panelPage.LnkAddNew.Click();

            //5. Enter display name to "Display name" field.
            //6. Click on OK button

            panelPage.TxtDisplayName.SendKeys(panelName);
            panelPage.ChbSeries.SelectItem(panelSeries, "Value");
            panelPage.BtnOK.Click();
            panelPage.WaitForAddingPanel(panelName);

            panelPage.LnkAddNew.Click();
            panelPage.TxtDisplayName.SendKeys(panelName);
            panelPage.ChbSeries.SelectItem(panelSeries, "Value");
            panelPage.BtnOK.Click();

            string actual = panelPage.GetAlertMessage(closeAlert: true);
            string expected = panelName + " already exists. Please enter a different name";

            //VP: Warning message: "Dupicated panel already exists. Please enter a different name" show up

            Assert.AreEqual(expected, actual, "\nExpected: " + expected + "\nActual: " + actual);

            //Post-condtion: Delete created panel

            panelPage.DeletePanel(panelName);
        }

        [TestMethod]
        public void TC033()
        {
            Console.WriteLine("DA_PANEL_TC033 - Verify that \"Data Profile\" listing of \"Add New Panel\" and \"Edit Panel\" control/form are in alphabetical order");

            string panelName = string.Concat("Panel", CommonMethods.GetUniqueString());
            string panelSeries = "name";

            //1. Navigate to Dashboard login page
            //2. Login with valid account
            //3. Click on Administer/Panels link
            //4. Click on Add new link

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password, Constant.DefaultRepo);

            mainPage.SelectMenuItem("Administer", "Panels");
            PanelPage panelPage = new PanelPage(driver);
            panelPage.LnkAddNew.Click();

            bool actual = panelPage.ChbDataProfile.IsItemSorted();

            //VP: Data Profile list is in alphabetical order

            Assert.AreEqual(true, actual, "\nData Profile Combo box is not sorted!");

            //5. Enter display name to Display Name textbox
            //6. Click Ok button to create a panel
            //7. Click on edit link

            panelPage.TxtDisplayName.SendKeys(panelName);
            panelPage.ChbSeries.SelectItem(panelSeries, "Value");
            panelPage.BtnOK.Click();
            panelPage.WaitForAddingPanel(panelName);
            panelPage.ClickEditPanel(panelName);

            actual = panelPage.ChbDataProfile.IsItemSorted();

            //VP: Data Profile list is in alphabetical order

            Assert.AreEqual(true, actual, "\nData Profile Combo box is not sorted!");

            //Post-condtion: Delete created panel

            panelPage.BtnCancel.Click();
            panelPage.DeletePanel(panelName);
        }

        [TestMethod]
        public void TC034()
        {
            Console.WriteLine(@"DA_PANEL_TC034 - Verify that newly created data profiles are populated correctly under the ""Data Profile"" dropped down menu in  ""Add New Panel"" and ""Edit Panel"" control/form");

            string dataName = string.Concat("Data", CommonMethods.GetUniqueString());
            string panelName = string.Concat("Panel", CommonMethods.GetUniqueString());
            string panelSeries = "name";

            //1. Navigate to Dashboard login page
            //2. Login with valid account
            //3. Click on Administer/Data Profiles link

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password, Constant.DefaultRepo);

            mainPage.SelectMenuItem("Administer", "Data Profiles");
            DataProfilePage dataProfile = new DataProfilePage(driver);

            //4. Click on Add new link
            //5. Enter name to Name textbox
            //6. Click on Finish button

            dataProfile.LnkAddNew.Click();
            dataProfile.TxtName.SendKeys(dataName);
            dataProfile.BtnFinish.Click();

            //7. Click on Administer/Panels link
            //8. Click on add new link

            mainPage.SelectMenuItem("Administer", "Panels");
            PanelPage panelPage = new PanelPage(driver);
            panelPage.LnkAddNew.Click();
            bool actual = panelPage.IsProfileExist(dataName);

            //VP: Data profiles are populated correctly under the "Data Profile" dropped down menu.

            Assert.AreEqual(true, actual, "\nProfile: " + dataName + " is not exist!");

            //9. Enter display name to Display Name textbox
            //10. Click Ok button to create a panel
            //11. Click on edit link

            panelPage.TxtDisplayName.SendKeys(panelName);
            panelPage.ChbSeries.SelectItem(panelSeries, "Value");
            panelPage.BtnOK.Click();
            panelPage.WaitForAddingPanel(panelName);
            panelPage.ClickEditPanel(panelName);

            actual = panelPage.IsProfileExist(dataName);

            //VP: Data profiles are populated correctly under the "Data Profile" dropped down menu.

            Assert.AreEqual(true, actual, "\nProfile: " + dataName + " is not exist!");

            //Post-condtion: Delete created panel

            panelPage.BtnCancel.Click();
            panelPage.DeletePanel(panelName);
            dataProfile.DeleteProfile(dataName);
        }

        [TestMethod]
        public void TC035()
        {
            Console.WriteLine("DA_PANEL_TC035 - Verify that no special character except '@' character is allowed to be inputted into \"Chart Title\" field");

            string panelName = string.Concat("Panel@", CommonMethods.GetUniqueString());
            string chartTitleFalse = "Chart#$%";
            string chartTitleTrue = "Chart@";
            string panelSeries = "name";

            //1. Navigate to Dashboard login page
            //2. Login with valid account
            //3. Click Administer link
            //4. Click Panel link
            //5. Click Add New link
            //6. Enter value into Display Name field
            //7. Enter value into Chart Title field with special characters except "@"
            //8. Click Ok button

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password, Constant.DefaultRepo);

            mainPage.SelectMenuItem("Administer", "Panels");
            PanelPage panelPage = new PanelPage(driver);
            panelPage.LnkAddNew.Click();
            panelPage.TxtDisplayName.SendKeys(panelName);
            panelPage.ChbSeries.SelectItem(panelSeries, "Value");
            panelPage.TxtChartTitle.SendKeys(chartTitleFalse);
            panelPage.BtnOK.Click();

            string actual = panelPage.GetAlertMessage(closeAlert: true);
            string expected = "Invalid display name. The name can't contain high ASCII characters or any of following characters: /:*?<>|\"#{[]{};";

            //VP: Message "Invalid display name. The name can't contain high ASCII characters or any of following characters: /:*?<>|"#{[]{};" is displayed

            Assert.AreEqual(expected, actual, "\nExpected: " + expected + "\nActual: " + actual);

            //9. Close Warning Message box
            //10. Click Add New link
            //11. Enter value into Display Name field
            //12. Enter value into Chart Title field with special character is @

            panelPage.TxtChartTitle.Clear();
            panelPage.TxtChartTitle.SendKeys(chartTitleTrue);
            panelPage.BtnOK.Click();
            panelPage.WaitForAddingPanel(panelName);

            bool actualCreated = panelPage.IsPanelCreated(panelName);

            //VP: The new panel is created

            Assert.AreEqual(true, actualCreated, "\nPanel: " + panelName + " is not created!");
        }
    }
}
