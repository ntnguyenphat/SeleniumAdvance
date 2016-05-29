using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumAdvance.PageObjects;
using SeleniumAdvance.Common;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumAdvance.Ultilities;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;

namespace SeleniumAdvance.TestCases
{
    [TestClass]
    public class PanelTestcases : TestBase
    {

        /// <summary>Verify that when \"Choose panels\" form is expanded all pre-set panels are populated and sorted correctly
        /// </summary>
        /// <Author>Phat</Author>
        /// <Startdate>23/05/2016</Startdate>
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

        /// <summary>Verify that when \"Add New Panel\" form is on focused all other control/form is disabled or locked
        /// </summary>
        /// <Author>Phat</Author>
        /// <Startdate>23/05/2016</Startdate>
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


        /// <summary>Verify that user is unable to create new panel when (*) required field is not filled
        /// </summary>
        /// <Author>Phat</Author>
        /// <Startdate>23/05/2016</Startdate>
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

        /// <summary>Verify that no special character except '@' character is allowed to be inputted into \"Display Name\" field
        /// </summary>
        /// <Author>Phat</Author>
        /// <Startdate>23/05/2016</Startdate>
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
            panelPage.CmbSeries.SelectItem(panelSeries, "Value");
            panelPage.BtnOK.Click();

            bool actualCreated = panelPage.IsPanelCreated(panelTrue);

            //VP: The new panel is created

            Assert.AreEqual(true, actualCreated, "\nPanel is not created!");

            //Post-condition: Delete created panel

            panelPage.DeletePanel(panelTrue);
        }

        /// <summary>Verify that correct panel setting form is displayed with corresponding panel type selected
        /// </summary>
        /// <Author>Phat</Author>
        /// <Startdate>23/05/2016</Startdate>
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

            panelPage.RbIndicator.ChooseAndWait(TimeSpan.FromSeconds(3));

            actual = panelPage.GetSettingHeader();
            expected = "Indicator setting";

            //VP: Chart panel setting form is displayed "Indicator setting" under Display Name field

            Assert.AreEqual(expected, actual, "\nExpected: " + expected + "\nActual: " + actual);

            //6: Select Report type

            panelPage.RbReport.Click();

            //VP:TODO - Report panel setting form is displayed "View mode" under Display Name.

        }

        /// <summary>Verify that user is not allowed to create panel with duplicated \"Display Name\
        /// </summary>
        /// <Author>Phat</Author>
        /// <Startdate>23/05/2016</Startdate>
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
            panelPage.CmbSeries.SelectItem(panelSeries, "Value");
            panelPage.BtnOK.Click();
            panelPage.WaitForAddingPanel(panelName);

            panelPage.LnkAddNew.Click();
            panelPage.TxtDisplayName.SendKeys(panelName);
            panelPage.CmbSeries.SelectItem(panelSeries, "Value");
            panelPage.BtnOK.Click();

            string actual = panelPage.GetAlertMessage(closeAlert: true);
            string expected = panelName + " already exists. Please enter a different name";

            //VP: Warning message: "Dupicated panel already exists. Please enter a different name" show up

            Assert.AreEqual(expected, actual, "\nExpected: " + expected + "\nActual: " + actual);

            //Post-condtion: Delete created panel

            panelPage.DeletePanel(panelName);
        }

        /// <summary>Verify that \"Data Profile\" listing of \"Add New Panel\" and \"Edit Panel\" control/form are in alphabetical order
        /// </summary>
        /// <Author>Phat</Author>
        /// <Startdate>23/05/2016</Startdate>
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

            bool actual = panelPage.CmbDataProfile.IsItemSorted();

            //VP: Data Profile list is in alphabetical order

            Assert.AreEqual(true, actual, "\nData Profile Combo box is not sorted!");

            //5. Enter display name to Display Name textbox
            //6. Click Ok button to create a panel
            //7. Click on edit link

            panelPage.TxtDisplayName.SendKeys(panelName);
            panelPage.CmbSeries.SelectItem(panelSeries, "Value");
            panelPage.BtnOK.Click();
            panelPage.WaitForAddingPanel(panelName);
            panelPage.ClickEditPanel(panelName);

            actual = panelPage.CmbDataProfile.IsItemSorted();

            //VP: Data Profile list is in alphabetical order

            Assert.AreEqual(true, actual, "\nData Profile Combo box is not sorted!");

            //Post-condtion: Delete created panel

            panelPage.BtnCancel.Click();
            panelPage.DeletePanel(panelName);
        }

        /// <summary>Verify that newly created data profiles are populated correctly under the ""Data Profile"" dropped down menu in  ""Add New Panel"" and ""Edit Panel"" control/form
        /// </summary>
        /// <Author>Phat</Author>
        /// <Startdate>23/05/2016</Startdate>
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
            panelPage.CmbSeries.SelectItem(panelSeries, "Value");
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

        /// <summary>Verify that no special character except '@' character is allowed to be inputted into \"Chart Title\" field
        /// </summary>
        /// <Author>Phat</Author>
        /// <Startdate>23/05/2016</Startdate>
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
            panelPage.CmbSeries.SelectItem(panelSeries, "Value");
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

        /// <summary>Verify that all chart types ( Pie, Single Bar, Stacked Bar, Group Bar, Line ) are listed correctly under "Chart Type" dropped down menu.
        /// </summary>
        /// <Author>Long</Author>
        /// <Startdate>26/05/2016</Startdate>
        [TestMethod]
        public void TC036()
        {
            Console.WriteLine("DA_PANEL_TC036 - Verify that all chart types ( Pie, Single Bar, Stacked Bar, Group Bar, Line ) are listed correctly under \"Chart Type\" dropped down menu");

            string pageName = string.Concat("Page ", CommonMethods.GetUniqueString());

            //1. Navigate to Dashboard login page
            //2. Select a specific repository 
            //3. Enter valid Username and Password
            //4. Click 'Login' button
            //5. Click 'Add Page' link
            //6. Enter Page Name
            //7. Click 'OK' button

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password, Constant.DefaultRepo);
            mainPage.AddPage(pageName: pageName);

            //8. Click 'Choose Panels' button
            //9. Click 'Create new panel' button
            //10. Click 'Chart Type' drop-down menu

            PanelPage panelPage = new PanelPage(driver);
            panelPage.UnhideChoosePanelsPage();
            panelPage.BtnCreateNewPanel.Click();

            //11. VP: Check that 'Chart Type' are listed 5 options: 'Pie', 'Single Bar', 'Stacked Bar', 'Group Bar' and 'Line'

            int numberOfListedOptions = panelPage.GetNumberOfItemsInCombobox("Chart Type");
            Assert.AreEqual("5", numberOfListedOptions.ToString(), "There are more/less than 5 options in 'Chart Type' drop-down menu");

            bool IsPieOptionPresent = panelPage.IsItemPresentInCombobox("Chart Type", "Pie");
            Assert.AreEqual(true, IsPieOptionPresent, "Pie option isn't present in 'Chart Type' drop-down menu");

            bool IsSingleBarOptionPresent = panelPage.IsItemPresentInCombobox("Chart Type", "Single Bar");
            Assert.AreEqual(true, IsSingleBarOptionPresent, "Single Bar option isn't present in 'Chart Type' drop-down menu");

            bool IsStackedBarOptionPresent = panelPage.IsItemPresentInCombobox("Chart Type", "Stacked Bar");
            Assert.AreEqual(true, IsStackedBarOptionPresent, "Stacked Bar option isn't present in 'Chart Type' drop-down menu");

            bool IsGroupBarOptionPresent = panelPage.IsItemPresentInCombobox("Chart Type", "Group Bar");
            Assert.AreEqual(true, IsGroupBarOptionPresent, "Group Bar option isn't present in 'Chart Type' drop-down menu");

            bool IsLineOptionPresent = panelPage.IsItemPresentInCombobox("Chart Type", "Line");
            Assert.AreEqual(true, IsLineOptionPresent, "Line option isn't present in 'Chart Type' drop-down menu");

            //Post-condition: Delete created page

            panelPage.BtnCancel.Click();
            mainPage.DeletePage(pageName);
        }

        /// <summary>Verify that "Category", "Series" and "Caption" field are enabled and disabled correctly corresponding to each type of the "Chart Type"
        /// </summary>
        /// <Author>Long</Author>
        /// <Startdate>27/05/2016</Startdate>
        [TestMethod]
        public void TC037()
        {
            Console.WriteLine("DA_PANEL_TC037 - Verify that \"Category\", \"Series\" and \"Caption\" field are enabled and disabled correctly corresponding to each type of the \"Chart Type\"");

            string pageName = string.Concat("Page ", CommonMethods.GetUniqueString());

            //1. Navigate to Dashboard login page
            //2. Select a specific repository 
            //3. Enter valid Username and Password
            //4. Click 'Login' button
            //5. Click 'Add Page' button
            //6. Enter Page Name
            //7. Click 'OK' button

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password, Constant.DefaultRepo);
            mainPage.AddPage(pageName: pageName);

            //8. Click 'Choose Panels' button
            //9. Click 'Create new panel' button
            //10. Click 'Chart Type' drop-down menu
            //11. Select 'Pie' Chart Type

            PanelPage panelPage = new PanelPage(driver);
            panelPage.UnhideChoosePanelsPage();
            panelPage.BtnCreateNewPanel.Click();
            panelPage.CmbChartType.SelectItem(item: "Pie", selectby: "Value");

            //12. VP: Check that 'Category' and 'Caption' are disabled, 'Series' is enabled

            Assert.AreEqual(false, panelPage.CmbCategory.Enabled, "Category is not disabled");
            Assert.AreEqual(false, panelPage.TxtCaptionNextToCategory.Enabled, "Caption next to Category is not disabled");
            Assert.AreEqual(false, panelPage.TxtCaptionNextToSeries.Enabled, "Caption next to Series is not disabled");
            Assert.AreEqual(true, panelPage.CmbSeries.Enabled, "Series is not enabled");

            //13. Click 'Chart Type' drop-down menu
            //14. Select 'Single Bar' Chart Type

            panelPage.CmbChartType.SelectItem(item: "Single Bar", selectby: "Value");

            //15. VP: Check that 'Category' is disabled, 'Series' and 'Caption' are enabled

            Assert.AreEqual(false, panelPage.CmbCategory.Enabled, "Category is not disabled");
            Assert.AreEqual(true, panelPage.TxtCaptionNextToCategory.Enabled, "Caption next to Category is not enabled");
            Assert.AreEqual(true, panelPage.TxtCaptionNextToSeries.Enabled, "Caption next to Series is not enabled");
            Assert.AreEqual(true, panelPage.CmbSeries.Enabled, "Series is not enabled");

            //16. Click 'Chart Type' drop-down menu
            //17. Select 'Stacked Bar' Chart Type

            panelPage.CmbChartType.SelectItem(item: "Stacked Bar", selectby: "Value");

            //18. VP: Check that 'Category' ,'Series' and 'Caption' are enabled

            Assert.AreEqual(true, panelPage.CmbCategory.Enabled, "Category is not enabled");
            Assert.AreEqual(true, panelPage.TxtCaptionNextToCategory.Enabled, "Caption next to Category is not enabled");
            Assert.AreEqual(true, panelPage.TxtCaptionNextToSeries.Enabled, "Caption next to Series is not enabled");
            Assert.AreEqual(true, panelPage.CmbSeries.Enabled, "Series is not enabled");

            //19. Click 'Chart Type' drop-down menu
            //20. Select 'Group Bar' Chart Type

            panelPage.CmbChartType.SelectItem(item: "Group Bar", selectby: "Value");

            //21. VP: Check that 'Category' ,'Series' and 'Caption' are enabled

            Assert.AreEqual(true, panelPage.CmbCategory.Enabled, "Category is not enabled");
            Assert.AreEqual(true, panelPage.TxtCaptionNextToCategory.Enabled, "Caption next to Category is not enabled");
            Assert.AreEqual(true, panelPage.TxtCaptionNextToSeries.Enabled, "Caption next to Series is not enabled");
            Assert.AreEqual(true, panelPage.CmbSeries.Enabled, "Series is not enabled");

            //22. Click 'Chart Type' drop-down menu
            //23. Select 'Line' Chart Type

            panelPage.CmbChartType.SelectItem(item: "Line", selectby: "Value");

            //24. VP: Check that 'Category' ,'Series' and 'Caption' are enabled

            Assert.AreEqual(true, panelPage.CmbCategory.Enabled, "Category is not enabled");
            Assert.AreEqual(true, panelPage.TxtCaptionNextToCategory.Enabled, "Caption next to Category is not enabled");
            Assert.AreEqual(true, panelPage.TxtCaptionNextToSeries.Enabled, "Caption next to Series is not enabled");
            Assert.AreEqual(true, panelPage.CmbSeries.Enabled, "Series is not enabled");

            //Post-condition: Delete created page

            panelPage.BtnCancel.Click();
            mainPage.DeletePage(pageName);
        }

        /// <summary>Verify that all settings within "Add New Panel" and "Edit Panel" form stay unchanged when user switches between "2D" and "3D" radio buttons
        /// </summary>
        /// <Author>Long</Author>
        /// <Startdate>27/05/2016</Startdate>
        [TestMethod]
        public void TC038()
        {
            Console.WriteLine("DA_PANEL_TC038 - Verify that all settings within \"Add New Panel\" and \"Edit Panel\" form stay unchanged when user switches between \"2D\" and \"3D\" radio buttons");

            string pageName = string.Concat("Page ", CommonMethods.GetUniqueString());
            string panelDisplayName = string.Concat("Panel Display ", CommonMethods.GetUniqueString());
            string chartTitle = string.Concat("Chart Title ", CommonMethods.GetUniqueString());

            //1. Navigate to Dashboard login page
            //2. Select a specific repository 
            //3. Enter valid Username and Password
            //4. Click 'Login' button
            //5. Click 'Add Page' button
            //6. Enter Page Name
            //7. Click 'OK' button

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password, Constant.DefaultRepo);
            mainPage.AddPage(pageName: pageName);

            //8. Click 'Choose Panels' button
            //9. Click 'Create new panel' button
            //10. Click 'Chart Type' drop-down menu
            //11. Select a specific Chart Type - Stacked Bar
            //12. Select 'Data Profile' drop-down menu - Test Case Execution
            //13. Enter 'Display Name' and 'Chart Title'
            //14. Select 'Show Title' checkbox - On
            //15. Select 'Legends' radio button - Top
            //16. Select 'Style' radio button - 3D

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PanelPage panelPage = new PanelPage(driver);

            panelPage.UnhideChoosePanelsPage();
            panelPage.BtnCreateNewPanel.Click();
            panelPage.TxtDisplayName.SendKeys(panelDisplayName);
            panelPage.TxtChartTitle.SendKeys(chartTitle);
            panelPage.CmbDataProfile.SelectItem(item: "Test Case Execution", selectby: "Text");

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@id='chkShowTitle']")));

            panelPage.ChbShowTitle.Check();
            panelPage.RbLegendsTop.Check();
            panelPage.CmbChartType.SelectItem(item: "Stacked Bar", selectby: "Value");

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//select[@id='cbbSeriesField']")));

            panelPage.CmbCategory.SelectItem(item: "name", selectby: "Value");
            panelPage.CmbSeries.SelectItem(item: "location", selectby: "Value");
            panelPage.RbStyle3D.Check();

            //17. VP: Check that settings of 'Chart Type', 'Data Profile', 'Display Name', 'Chart Title', 'Show Title' and 'Legends' stay unchanged.

            Assert.AreEqual("Stacked Bar", panelPage.GetSelectedItemOfCombobox("Chart Type"), "Setting of Chart Type combobox has changed");
            Assert.AreEqual("Test Case Execution", panelPage.GetSelectedItemOfCombobox("Profile"), "Setting of Data Profile combobox has changed");
            Assert.AreEqual("Name", panelPage.GetSelectedItemOfCombobox("Category"), "Setting of Category combobox has changed");
            Assert.AreEqual("Location", panelPage.GetSelectedItemOfCombobox("Series"), "Setting of Series combobox has changed");
            Assert.AreEqual(panelDisplayName, panelPage.TxtDisplayName.GetAttribute("value"), "Name input in Display Name textbox has changed");
            Assert.AreEqual(chartTitle, panelPage.TxtChartTitle.GetAttribute("value"), "Title input in Chart Title textbox has changed");
            Assert.AreEqual(true, panelPage.ChbShowTitle.Selected, "Setting of Show Title checkbox has changed");
            Assert.AreEqual(true, panelPage.RbLegendsTop.Selected, "Setting of Legends radio button has changed");

            //18. Select 'Style' radio button - 2D

            panelPage.RbStyle2D.Check();

            //19. VP: Check that settings of 'Chart Type', 'Data Profile', 'Display Name', 'Chart Title', 'Show Title' and 'Legends' stay unchanged.

            Assert.AreEqual("Stacked Bar", panelPage.GetSelectedItemOfCombobox("Chart Type"), "Setting of Chart Type combobox has changed");
            Assert.AreEqual("Test Case Execution", panelPage.GetSelectedItemOfCombobox("Profile"), "Setting of Data Profile combobox has changed");
            Assert.AreEqual("Name", panelPage.GetSelectedItemOfCombobox("Category"), "Setting of Category combobox has changed");
            Assert.AreEqual("Location", panelPage.GetSelectedItemOfCombobox("Series"), "Setting of Series combobox has changed");
            Assert.AreEqual(panelDisplayName, panelPage.TxtDisplayName.GetAttribute("value"), "Name input in Display Name textbox has changed");
            Assert.AreEqual(chartTitle, panelPage.TxtChartTitle.GetAttribute("value"), "Title input in Chart Title textbox has changed");
            Assert.AreEqual(true, panelPage.ChbShowTitle.Selected, "Setting of Show Title checkbox has changed");
            Assert.AreEqual(true, panelPage.RbLegendsTop.Selected, "Setting of Legends radio button has changed");

            //20. Click OK button

            panelPage.BtnOK.Click();

            //21. Select a page in drop-down menu - pagename
            //22. Enter path of Folder
            //23. Click OK button

            panelPage.CmbSelectPage.SelectItem(item: pageName, selectby: "Text");
            panelPage.TxtFolder.Clear();
            panelPage.TxtFolder.SendKeys("/Car Rental/Tests");
            panelPage.BtnOK.Click();

            wait.Until(ExpectedConditions.StalenessOf(panelPage.BtnOK));

            //24. Click 'Edit Panel' button of the created panel
            //25. Select 'Style' radio button - 3D

            panelPage.SelectMenuItem("Administer", "Panels");
            panelPage.ClickEditPanel(panelDisplayName);
            panelPage.RbStyle3D.Check();

            //26. VP: Check that settings of 'Chart Type', 'Data Profile', 'Display Name', 'Chart Title', 'Show Title' and 'Legends' stay unchanged.

            Assert.AreEqual("Stacked Bar", panelPage.GetSelectedItemOfCombobox("Chart Type"), "Setting of Chart Type combobox has changed");
            Assert.AreEqual("Test Case Execution", panelPage.GetSelectedItemOfCombobox("Profile"), "Setting of Data Profile combobox has changed");
            Assert.AreEqual("Name", panelPage.GetSelectedItemOfCombobox("Category"), "Setting of Category combobox has changed");
            Assert.AreEqual("Location", panelPage.GetSelectedItemOfCombobox("Series"), "Setting of Series combobox has changed");
            Assert.AreEqual(panelDisplayName, panelPage.TxtDisplayName.GetAttribute("value"), "Name input in Display Name textbox has changed");
            Assert.AreEqual(chartTitle, panelPage.TxtChartTitle.GetAttribute("value"), "Title input in Chart Title textbox has changed");
            Assert.AreEqual(true, panelPage.ChbShowTitle.Selected, "Setting of Show Title checkbox has changed");
            Assert.AreEqual(true, panelPage.RbLegendsTop.Selected, "Setting of Legends radio button has changed");

            //27. Select 'Style' radio button - 2D

            panelPage.RbStyle2D.Check();

            //28. VP: Check that settings of 'Chart Type', 'Data Profile', 'Display Name', 'Chart Title', 'Show Title' and 'Legends' stay unchanged.

            Assert.AreEqual("Stacked Bar", panelPage.GetSelectedItemOfCombobox("Chart Type"), "Setting of Chart Type combobox has changed");
            Assert.AreEqual("Test Case Execution", panelPage.GetSelectedItemOfCombobox("Profile"), "Setting of Data Profile combobox has changed");
            Assert.AreEqual("Name", panelPage.GetSelectedItemOfCombobox("Category"), "Setting of Category combobox has changed");
            Assert.AreEqual("Location", panelPage.GetSelectedItemOfCombobox("Series"), "Setting of Series combobox has changed");
            Assert.AreEqual(panelDisplayName, panelPage.TxtDisplayName.GetAttribute("value"), "Name input in Display Name textbox has changed");
            Assert.AreEqual(chartTitle, panelPage.TxtChartTitle.GetAttribute("value"), "Title input in Chart Title textbox has changed");
            Assert.AreEqual(true, panelPage.ChbShowTitle.Selected, "Setting of Show Title checkbox has changed");
            Assert.AreEqual(true, panelPage.RbLegendsTop.Selected, "Setting of Legends radio button has changed");

            //Post-condition: Delete created panel and page.

            panelPage.BtnCancel.Click();
            panelPage.DeletePanel(panelDisplayName);
            mainPage.DeletePage(pageName);
        }

        /// <summary>Verify that all settings within "Add New Panel" and "Edit Panel" form stay unchanged when user switches between "Legends" radio buttons
        /// </summary>
        /// <Author>Long</Author>
        /// <Startdate>28/05/2016</Startdate>
        [TestMethod]
        public void TC039()
        {
            Console.WriteLine("DA_PANEL_TC039 - Verify that all settings within \"Add New Panel\" and \"Edit Panel\" form stay unchanged when user switches between \"Legends\" radio buttons");

            string panelDisplayName = string.Concat("Panel Display ", CommonMethods.GetUniqueString());
            string title = string.Concat("Chart Title ", CommonMethods.GetUniqueString());

            string typeOfPanel_before;
            string typeOfPanel_after;
            string dataProfileName_before;
            string dataProfileName_after;
            string panelDisplayName_before;
            string panelDisplayName_after;
            string title_before = "";
            string title_after = "";
            string chartType_before;
            string chartType_after;
            string stasticFieldName_before;
            string stasticFieldName_after;
            string stasticFieldValue_before;
            string stasticFieldValue_after;
            string from_before;
            string from_after;
            string color_before;
            string color_after;
            string captionNextToSeries_before;
            string captionNextToSeries_after;
            string captionNexToCategory_before;
            string captionNexToCategory_after;
            string categoryName_before;
            string categoryName_after;
            string seriesName_before;
            string seriesName_after;
            string seriesValue_before;
            string seriesValue_after;
            bool isLegendsTopChecked_before;
            bool isLegendsTopChecked_after;
            bool isLegendsNoneChecked_before;
            bool isLegendsNoneChecked_after;
            bool isLegendsRightChecked_before;
            bool isLegendsRightChecked_after;
            bool isCaptionNextToCategoryEnabled_before;
            bool isCaptionNextToCategoryEnabled_after;
            bool isCaptionNextToSeriesEnabled_before;
            bool isCaptionNextToSeriesEnabled_after;
            bool isLegendsBottomChecked_before;
            bool isLegendsBottomChecked_after;
            bool isLegendsLeftChecked_before;
            bool isLegendsLeftChecked_after;
            bool isCategoryInChartSettingsEnable_before;
            bool isCategoryInChartSettingsEnable_after;
            bool isDataLabelsSeriesEnables_before;
            bool isDataLabelsSeriesEnables_after;
            bool isDataLabelsCategoriesEnabled_before;
            bool isDataLabelsCategoriesEnabled_after;
            bool isDataLabelsValueEnabled_before;
            bool isDataLabelsValueEnabled_after;
            bool isDataLabelsPercentageEnabled_before;
            bool isDataLabelsPercentageEnabled_after;
            bool isShowTitleChecked_before;
            bool isShowTitleChecked_after;
            bool isStasticOnEnabled_before;
            bool isStasticOnEnabled_after;
            bool isStyle2DChecked_before;
            bool isStyle2DChecked_after;
            bool isStyle3DChecked_before;
            bool isStyle3DChecked_after;
            bool isPercentageChecked_before;
            bool isPercentageChecked_after;
            bool isSetAsHeatValueChecked_before;
            bool isSetAsHeatValueChecked_after;

            //1. Navigate to Dashboard login page
            //2. Login with valid account
            //3. Click Administer link
            //4. Click Panel link
            //5. Click Add New link
            //6. Click None radio button for Legend

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password, Constant.DefaultRepo);

            mainPage.SelectMenuItem("Administer", "Panels");
            PanelPage panelPage = new PanelPage(driver);
            panelPage.LnkAddNew.Click();
            
            panelPage.RbChart.Check();
            panelPage.GetCurrentSettingsInPanelDialog(out typeOfPanel_before, out dataProfileName_before, out panelDisplayName_before, out title_before, out isShowTitleChecked_before,
            out stasticFieldName_before, out stasticFieldValue_before, out isPercentageChecked_before, out from_before, out color_before, out chartType_before,
            out isCaptionNextToCategoryEnabled_before, out isCaptionNextToSeriesEnabled_before, out isCategoryInChartSettingsEnable_before, out isDataLabelsSeriesEnables_before,
            out isDataLabelsCategoriesEnabled_before, out isDataLabelsValueEnabled_before, out isDataLabelsPercentageEnabled_before, out seriesName_before, out isLegendsNoneChecked_before,
            out isLegendsTopChecked_before, out isLegendsRightChecked_before, out isLegendsBottomChecked_before, out isLegendsLeftChecked_before,
            out isStyle2DChecked_before, out isStyle3DChecked_before, out captionNexToCategory_before, out captionNextToSeries_before, out categoryName_before, out isStasticOnEnabled_before,
            out seriesValue_before, out isSetAsHeatValueChecked_before);

            panelPage.RbLegendsNone.Check();

            panelPage.GetCurrentSettingsInPanelDialog(out typeOfPanel_after, out dataProfileName_after, out panelDisplayName_after, out title_after, out isShowTitleChecked_after,
            out stasticFieldName_after, out stasticFieldValue_after, out isPercentageChecked_after, out from_after, out color_after, out chartType_after,
            out isCaptionNextToCategoryEnabled_after, out isCaptionNextToSeriesEnabled_after, out isCategoryInChartSettingsEnable_after, out isDataLabelsSeriesEnables_after,
            out isDataLabelsCategoriesEnabled_after, out isDataLabelsValueEnabled_after, out isDataLabelsPercentageEnabled_after, out seriesName_after, out isLegendsNoneChecked_after,
            out isLegendsTopChecked_after, out isLegendsRightChecked_after, out isLegendsBottomChecked_after, out isLegendsLeftChecked_after,
            out isStyle2DChecked_after, out isStyle3DChecked_after, out captionNexToCategory_after, out captionNextToSeries_after, out categoryName_after, out isStasticOnEnabled_after,
            out seriesValue_after, out isSetAsHeatValueChecked_after);

            //7. VP: All settings are unchange in Add New Panel dialog
            
            Assert.AreEqual(chartType_before,chartType_after, "Setting of Chart Type combobox has changed");
            Assert.AreEqual(dataProfileName_before, dataProfileName_after, "Setting of Data Profile combobox has changed");
            Assert.AreEqual(title_before, title_after, "Title has changed");
            Assert.AreEqual(isShowTitleChecked_before, isShowTitleChecked_after, "Setting of Show Title has changed");
            Assert.AreEqual(isStyle2DChecked_before, isStyle2DChecked_after, "Settings of Style has changed");
            Assert.AreEqual(isStyle3DChecked_before, isStyle3DChecked_after, "Settings of Style has changed");
            Assert.AreEqual(isCaptionNextToCategoryEnabled_before, isCaptionNextToCategoryEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isCaptionNextToSeriesEnabled_before, isCaptionNextToSeriesEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isCaptionNextToSeriesEnabled_before, isCaptionNextToSeriesEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isDataLabelsCategoriesEnabled_before, isDataLabelsCategoriesEnabled_after, "Setting of Data Label has changed");

            //8. Click Top radio button for Legend

            panelPage.RbLegendsTop.Check();

            panelPage.GetCurrentSettingsInPanelDialog(out typeOfPanel_after, out dataProfileName_after, out panelDisplayName_after, out title_after, out isShowTitleChecked_after,
            out stasticFieldName_after, out stasticFieldValue_after, out isPercentageChecked_after, out from_after, out color_after, out chartType_after,
            out isCaptionNextToCategoryEnabled_after, out isCaptionNextToSeriesEnabled_after, out isCategoryInChartSettingsEnable_after, out isDataLabelsSeriesEnables_after,
            out isDataLabelsCategoriesEnabled_after, out isDataLabelsValueEnabled_after, out isDataLabelsPercentageEnabled_after, out seriesName_after, out isLegendsNoneChecked_after,
            out isLegendsTopChecked_after, out isLegendsRightChecked_after, out isLegendsBottomChecked_after, out isLegendsLeftChecked_after,
            out isStyle2DChecked_after, out isStyle3DChecked_after, out captionNexToCategory_after, out captionNextToSeries_after, out categoryName_after, out isStasticOnEnabled_after,
            out seriesValue_after, out isSetAsHeatValueChecked_after);

            //9. VP: All settings are unchange in Add New Panel dialoge

            Assert.AreEqual(chartType_before, chartType_after, "Setting of Chart Type combobox has changed");
            Assert.AreEqual(dataProfileName_before, dataProfileName_after, "Setting of Data Profile combobox has changed");
            Assert.AreEqual(title_before, title_after, "Title has changed");
            Assert.AreEqual(isShowTitleChecked_before, isShowTitleChecked_after, "Setting of Show Title has changed");
            Assert.AreEqual(isStyle2DChecked_before, isStyle2DChecked_after, "Settings of Style has changed");
            Assert.AreEqual(isStyle3DChecked_before, isStyle3DChecked_after, "Settings of Style has changed");
            Assert.AreEqual(isCaptionNextToCategoryEnabled_before, isCaptionNextToCategoryEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isCaptionNextToSeriesEnabled_before, isCaptionNextToSeriesEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isCaptionNextToSeriesEnabled_before, isCaptionNextToSeriesEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isDataLabelsCategoriesEnabled_before, isDataLabelsCategoriesEnabled_after, "Setting of Data Label has changed");

            //10. Click Right radio button for Legend

            panelPage.RbLegendsRight.Check();

            panelPage.GetCurrentSettingsInPanelDialog(out typeOfPanel_after, out dataProfileName_after, out panelDisplayName_after, out title_after, out isShowTitleChecked_after,
            out stasticFieldName_after, out stasticFieldValue_after, out isPercentageChecked_after, out from_after, out color_after, out chartType_after,
            out isCaptionNextToCategoryEnabled_after, out isCaptionNextToSeriesEnabled_after, out isCategoryInChartSettingsEnable_after, out isDataLabelsSeriesEnables_after,
            out isDataLabelsCategoriesEnabled_after, out isDataLabelsValueEnabled_after, out isDataLabelsPercentageEnabled_after, out seriesName_after, out isLegendsNoneChecked_after,
            out isLegendsTopChecked_after, out isLegendsRightChecked_after, out isLegendsBottomChecked_after, out isLegendsLeftChecked_after,
            out isStyle2DChecked_after, out isStyle3DChecked_after, out captionNexToCategory_after, out captionNextToSeries_after, out categoryName_after, out isStasticOnEnabled_after,
            out seriesValue_after, out isSetAsHeatValueChecked_after);

            //11. VP: All settings are unchange in Add New Panel dialog

            Assert.AreEqual(chartType_before, chartType_after, "Setting of Chart Type combobox has changed");
            Assert.AreEqual(dataProfileName_before, dataProfileName_after, "Setting of Data Profile combobox has changed");
            Assert.AreEqual(title_before, title_after, "Title has changed");
            Assert.AreEqual(isShowTitleChecked_before, isShowTitleChecked_after, "Setting of Show Title has changed");
            Assert.AreEqual(isStyle2DChecked_before, isStyle2DChecked_after, "Settings of Style has changed");
            Assert.AreEqual(isStyle3DChecked_before, isStyle3DChecked_after, "Settings of Style has changed");
            Assert.AreEqual(isCaptionNextToCategoryEnabled_before, isCaptionNextToCategoryEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isCaptionNextToSeriesEnabled_before, isCaptionNextToSeriesEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isCaptionNextToSeriesEnabled_before, isCaptionNextToSeriesEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isDataLabelsCategoriesEnabled_before, isDataLabelsCategoriesEnabled_after, "Setting of Data Label has changed");

            //12. Click Bottom radio button for Legend

            panelPage.RbLegendsBottom.Check();

            panelPage.GetCurrentSettingsInPanelDialog(out typeOfPanel_after, out dataProfileName_after, out panelDisplayName_after, out title_after, out isShowTitleChecked_after,
            out stasticFieldName_after, out stasticFieldValue_after, out isPercentageChecked_after, out from_after, out color_after, out chartType_after,
            out isCaptionNextToCategoryEnabled_after, out isCaptionNextToSeriesEnabled_after, out isCategoryInChartSettingsEnable_after, out isDataLabelsSeriesEnables_after,
            out isDataLabelsCategoriesEnabled_after, out isDataLabelsValueEnabled_after, out isDataLabelsPercentageEnabled_after, out seriesName_after, out isLegendsNoneChecked_after,
            out isLegendsTopChecked_after, out isLegendsRightChecked_after, out isLegendsBottomChecked_after, out isLegendsLeftChecked_after,
            out isStyle2DChecked_after, out isStyle3DChecked_after, out captionNexToCategory_after, out captionNextToSeries_after, out categoryName_after, out isStasticOnEnabled_after,
            out seriesValue_after, out isSetAsHeatValueChecked_after);

            //13. VP: All settings are unchange in Add New Panel dialog

            Assert.AreEqual(chartType_before, chartType_after, "Setting of Chart Type combobox has changed");
            Assert.AreEqual(dataProfileName_before, dataProfileName_after, "Setting of Data Profile combobox has changed");
            Assert.AreEqual(title_before, title_after, "Title has changed");
            Assert.AreEqual(isShowTitleChecked_before, isShowTitleChecked_after, "Setting of Show Title has changed");
            Assert.AreEqual(isStyle2DChecked_before, isStyle2DChecked_after, "Settings of Style has changed");
            Assert.AreEqual(isStyle3DChecked_before, isStyle3DChecked_after, "Settings of Style has changed");
            Assert.AreEqual(isCaptionNextToCategoryEnabled_before, isCaptionNextToCategoryEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isCaptionNextToSeriesEnabled_before, isCaptionNextToSeriesEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isCaptionNextToSeriesEnabled_before, isCaptionNextToSeriesEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isDataLabelsCategoriesEnabled_before, isDataLabelsCategoriesEnabled_after, "Setting of Data Label has changed");

            //14. Click Left radio button for Legend

            panelPage.RbLegendsLeft.Check();

            panelPage.GetCurrentSettingsInPanelDialog(out typeOfPanel_after, out dataProfileName_after, out panelDisplayName_after, out title_after, out isShowTitleChecked_after,
            out stasticFieldName_after, out stasticFieldValue_after, out isPercentageChecked_after, out from_after, out color_after, out chartType_after,
            out isCaptionNextToCategoryEnabled_after, out isCaptionNextToSeriesEnabled_after, out isCategoryInChartSettingsEnable_after, out isDataLabelsSeriesEnables_after,
            out isDataLabelsCategoriesEnabled_after, out isDataLabelsValueEnabled_after, out isDataLabelsPercentageEnabled_after, out seriesName_after, out isLegendsNoneChecked_after,
            out isLegendsTopChecked_after, out isLegendsRightChecked_after, out isLegendsBottomChecked_after, out isLegendsLeftChecked_after,
            out isStyle2DChecked_after, out isStyle3DChecked_after, out captionNexToCategory_after, out captionNextToSeries_after, out categoryName_after, out isStasticOnEnabled_after,
            out seriesValue_after, out isSetAsHeatValueChecked_after);

            //15. VP: All settings are unchange in Add New Panel dialog

            Assert.AreEqual(chartType_before, chartType_after, "Setting of Chart Type combobox has changed");
            Assert.AreEqual(dataProfileName_before, dataProfileName_after, "Setting of Data Profile combobox has changed");
            Assert.AreEqual(title_before, title_after, "Title has changed");
            Assert.AreEqual(isShowTitleChecked_before, isShowTitleChecked_after, "Setting of Show Title has changed");
            Assert.AreEqual(isStyle2DChecked_before, isStyle2DChecked_after, "Settings of Style has changed");
            Assert.AreEqual(isStyle3DChecked_before, isStyle3DChecked_after, "Settings of Style has changed");
            Assert.AreEqual(isCaptionNextToCategoryEnabled_before, isCaptionNextToCategoryEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isCaptionNextToSeriesEnabled_before, isCaptionNextToSeriesEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isCaptionNextToSeriesEnabled_before, isCaptionNextToSeriesEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isDataLabelsCategoriesEnabled_before, isDataLabelsCategoriesEnabled_after, "Setting of Data Label has changed");

            //16. Create a new panel

            panelPage.TxtDisplayName.SendKeys(panelDisplayName);
            panelPage.TxtChartTitle.SendKeys(title);
            panelPage.CmbSeries.SelectItem(item: "location", selectby: "Value");
            panelPage.BtnOK.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.StalenessOf(panelPage.BtnOK));

            //17. Click Edit Panel link
            //18. Click None radio button for Legend

            panelPage.ClickEditPanel(panelDisplayName);
            panelPage.RbLegendsNone.Check();

            panelPage.GetCurrentSettingsInPanelDialog(out typeOfPanel_after, out dataProfileName_after, out panelDisplayName_after, out title_after, out isShowTitleChecked_after,
            out stasticFieldName_after, out stasticFieldValue_after, out isPercentageChecked_after, out from_after, out color_after, out chartType_after,
            out isCaptionNextToCategoryEnabled_after, out isCaptionNextToSeriesEnabled_after, out isCategoryInChartSettingsEnable_after, out isDataLabelsSeriesEnables_after,
            out isDataLabelsCategoriesEnabled_after, out isDataLabelsValueEnabled_after, out isDataLabelsPercentageEnabled_after, out seriesName_after, out isLegendsNoneChecked_after,
            out isLegendsTopChecked_after, out isLegendsRightChecked_after, out isLegendsBottomChecked_after, out isLegendsLeftChecked_after,
            out isStyle2DChecked_after, out isStyle3DChecked_after, out captionNexToCategory_after, out captionNextToSeries_after, out categoryName_after, out isStasticOnEnabled_after,
            out seriesValue_after, out isSetAsHeatValueChecked_after);

            //19. VP: All settings are unchange in Add New Panel dialog

            Assert.AreEqual(chartType_before, chartType_after, "Setting of Chart Type combobox has changed");
            Assert.AreEqual(dataProfileName_before, dataProfileName_after, "Setting of Data Profile combobox has changed");
            Assert.AreEqual(title, title_after, "Title has changed");
            Assert.AreEqual(isShowTitleChecked_before, isShowTitleChecked_after, "Setting of Show Title has changed");
            Assert.AreEqual(isStyle2DChecked_before, isStyle2DChecked_after, "Settings of Style has changed");
            Assert.AreEqual(isStyle3DChecked_before, isStyle3DChecked_after, "Settings of Style has changed");
            Assert.AreEqual(isCaptionNextToCategoryEnabled_before, isCaptionNextToCategoryEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isCaptionNextToSeriesEnabled_before, isCaptionNextToSeriesEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isCaptionNextToSeriesEnabled_before, isCaptionNextToSeriesEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isDataLabelsCategoriesEnabled_before, isDataLabelsCategoriesEnabled_after, "Setting of Data Label has changed");

            //20. Click Top radio button for Legend

            panelPage.RbLegendsTop.Check();

            panelPage.GetCurrentSettingsInPanelDialog(out typeOfPanel_after, out dataProfileName_after, out panelDisplayName_after, out title_after, out isShowTitleChecked_after,
            out stasticFieldName_after, out stasticFieldValue_after, out isPercentageChecked_after, out from_after, out color_after, out chartType_after,
            out isCaptionNextToCategoryEnabled_after, out isCaptionNextToSeriesEnabled_after, out isCategoryInChartSettingsEnable_after, out isDataLabelsSeriesEnables_after,
            out isDataLabelsCategoriesEnabled_after, out isDataLabelsValueEnabled_after, out isDataLabelsPercentageEnabled_after, out seriesName_after, out isLegendsNoneChecked_after,
            out isLegendsTopChecked_after, out isLegendsRightChecked_after, out isLegendsBottomChecked_after, out isLegendsLeftChecked_after,
            out isStyle2DChecked_after, out isStyle3DChecked_after, out captionNexToCategory_after, out captionNextToSeries_after, out categoryName_after, out isStasticOnEnabled_after,
            out seriesValue_after, out isSetAsHeatValueChecked_after);

            //21. VP: All settings are unchange in Add New Panel dialog

            Assert.AreEqual(chartType_before, chartType_after, "Setting of Chart Type combobox has changed");
            Assert.AreEqual(dataProfileName_before, dataProfileName_after, "Setting of Data Profile combobox has changed");
            Assert.AreEqual(title, title_after, "Title has changed");
            Assert.AreEqual(isShowTitleChecked_before, isShowTitleChecked_after, "Setting of Show Title has changed");
            Assert.AreEqual(isStyle2DChecked_before, isStyle2DChecked_after, "Settings of Style has changed");
            Assert.AreEqual(isStyle3DChecked_before, isStyle3DChecked_after, "Settings of Style has changed");
            Assert.AreEqual(isCaptionNextToCategoryEnabled_before, isCaptionNextToCategoryEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isCaptionNextToSeriesEnabled_before, isCaptionNextToSeriesEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isCaptionNextToSeriesEnabled_before, isCaptionNextToSeriesEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isDataLabelsCategoriesEnabled_before, isDataLabelsCategoriesEnabled_after, "Setting of Data Label has changed");

            //22. Click Right radio button for Legend

            panelPage.RbLegendsRight.Check();

            panelPage.GetCurrentSettingsInPanelDialog(out typeOfPanel_after, out dataProfileName_after, out panelDisplayName_after, out title_after, out isShowTitleChecked_after,
            out stasticFieldName_after, out stasticFieldValue_after, out isPercentageChecked_after, out from_after, out color_after, out chartType_after,
            out isCaptionNextToCategoryEnabled_after, out isCaptionNextToSeriesEnabled_after, out isCategoryInChartSettingsEnable_after, out isDataLabelsSeriesEnables_after,
            out isDataLabelsCategoriesEnabled_after, out isDataLabelsValueEnabled_after, out isDataLabelsPercentageEnabled_after, out seriesName_after, out isLegendsNoneChecked_after,
            out isLegendsTopChecked_after, out isLegendsRightChecked_after, out isLegendsBottomChecked_after, out isLegendsLeftChecked_after,
            out isStyle2DChecked_after, out isStyle3DChecked_after, out captionNexToCategory_after, out captionNextToSeries_after, out categoryName_after, out isStasticOnEnabled_after,
            out seriesValue_after, out isSetAsHeatValueChecked_after);

            //23. VP: All settings are unchange in Add New Panel dialog

            Assert.AreEqual(chartType_before, chartType_after, "Setting of Chart Type combobox has changed");
            Assert.AreEqual(dataProfileName_before, dataProfileName_after, "Setting of Data Profile combobox has changed");
            Assert.AreEqual(title, title_after, "Title has changed");
            Assert.AreEqual(isShowTitleChecked_before, isShowTitleChecked_after, "Setting of Show Title has changed");
            Assert.AreEqual(isStyle2DChecked_before, isStyle2DChecked_after, "Settings of Style has changed");
            Assert.AreEqual(isStyle3DChecked_before, isStyle3DChecked_after, "Settings of Style has changed");
            Assert.AreEqual(isCaptionNextToCategoryEnabled_before, isCaptionNextToCategoryEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isCaptionNextToSeriesEnabled_before, isCaptionNextToSeriesEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isCaptionNextToSeriesEnabled_before, isCaptionNextToSeriesEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isDataLabelsCategoriesEnabled_before, isDataLabelsCategoriesEnabled_after, "Setting of Data Label has changed");

            //24. Click Bottom radio button for Legend

            panelPage.RbLegendsBottom.Check();

            panelPage.GetCurrentSettingsInPanelDialog(out typeOfPanel_after, out dataProfileName_after, out panelDisplayName_after, out title_after, out isShowTitleChecked_after,
            out stasticFieldName_after, out stasticFieldValue_after, out isPercentageChecked_after, out from_after, out color_after, out chartType_after,
            out isCaptionNextToCategoryEnabled_after, out isCaptionNextToSeriesEnabled_after, out isCategoryInChartSettingsEnable_after, out isDataLabelsSeriesEnables_after,
            out isDataLabelsCategoriesEnabled_after, out isDataLabelsValueEnabled_after, out isDataLabelsPercentageEnabled_after, out seriesName_after, out isLegendsNoneChecked_after,
            out isLegendsTopChecked_after, out isLegendsRightChecked_after, out isLegendsBottomChecked_after, out isLegendsLeftChecked_after,
            out isStyle2DChecked_after, out isStyle3DChecked_after, out captionNexToCategory_after, out captionNextToSeries_after, out categoryName_after, out isStasticOnEnabled_after,
            out seriesValue_after, out isSetAsHeatValueChecked_after);

            //25. VP: All settings are unchange in Add New Panel dialog

            Assert.AreEqual(chartType_before, chartType_after, "Setting of Chart Type combobox has changed");
            Assert.AreEqual(dataProfileName_before, dataProfileName_after, "Setting of Data Profile combobox has changed");
            Assert.AreEqual(title, title_after, "Title has changed");
            Assert.AreEqual(isShowTitleChecked_before, isShowTitleChecked_after, "Setting of Show Title has changed");
            Assert.AreEqual(isStyle2DChecked_before, isStyle2DChecked_after, "Settings of Style has changed");
            Assert.AreEqual(isStyle3DChecked_before, isStyle3DChecked_after, "Settings of Style has changed");
            Assert.AreEqual(isCaptionNextToCategoryEnabled_before, isCaptionNextToCategoryEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isCaptionNextToSeriesEnabled_before, isCaptionNextToSeriesEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isCaptionNextToSeriesEnabled_before, isCaptionNextToSeriesEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isDataLabelsCategoriesEnabled_before, isDataLabelsCategoriesEnabled_after, "Setting of Data Label has changed");

            //26. Click Left radio button for Legend

            panelPage.RbLegendsLeft.Check();

            panelPage.GetCurrentSettingsInPanelDialog(out typeOfPanel_after, out dataProfileName_after, out panelDisplayName_after, out title_after, out isShowTitleChecked_after,
            out stasticFieldName_after, out stasticFieldValue_after, out isPercentageChecked_after, out from_after, out color_after, out chartType_after,
            out isCaptionNextToCategoryEnabled_after, out isCaptionNextToSeriesEnabled_after, out isCategoryInChartSettingsEnable_after, out isDataLabelsSeriesEnables_after,
            out isDataLabelsCategoriesEnabled_after, out isDataLabelsValueEnabled_after, out isDataLabelsPercentageEnabled_after, out seriesName_after, out isLegendsNoneChecked_after,
            out isLegendsTopChecked_after, out isLegendsRightChecked_after, out isLegendsBottomChecked_after, out isLegendsLeftChecked_after,
            out isStyle2DChecked_after, out isStyle3DChecked_after, out captionNexToCategory_after, out captionNextToSeries_after, out categoryName_after, out isStasticOnEnabled_after,
            out seriesValue_after, out isSetAsHeatValueChecked_after);

            //27. VP: All settings are unchange in Add New Panel dialog

            Assert.AreEqual(chartType_before, chartType_after, "Setting of Chart Type combobox has changed");
            Assert.AreEqual(dataProfileName_before, dataProfileName_after, "Setting of Data Profile combobox has changed");
            Assert.AreEqual(title, title_after, "Title has changed");
            Assert.AreEqual(isShowTitleChecked_before, isShowTitleChecked_after, "Setting of Show Title has changed");
            Assert.AreEqual(isStyle2DChecked_before, isStyle2DChecked_after, "Settings of Style has changed");
            Assert.AreEqual(isStyle3DChecked_before, isStyle3DChecked_after, "Settings of Style has changed");
            Assert.AreEqual(isCaptionNextToCategoryEnabled_before, isCaptionNextToCategoryEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isCaptionNextToSeriesEnabled_before, isCaptionNextToSeriesEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isCaptionNextToSeriesEnabled_before, isCaptionNextToSeriesEnabled_after, "Setting of Caption has changed");
            Assert.AreEqual(isDataLabelsCategoriesEnabled_before, isDataLabelsCategoriesEnabled_after, "Setting of Data Label has changed");
        }

        /// <summary>Verify that all "Data Labels" check boxes are enabled and disabled correctly corresponding to each type of "Chart Type"			
        /// </summary>
        /// <Author>Long</Author>
        /// <Startdate>29/05/2016</Startdate>
        [TestMethod]
        public void TC040()
        {
            Console.WriteLine("DA_PANEL_TC040 - Verify that all \"Data Labels\" check boxes are enabled and disabled correctly corresponding to each type of \"Chart Type\"");

            string pageName = string.Concat("Page ", CommonMethods.GetUniqueString());
        
            //1. Navigate to Dashboard login page
            //2. Select a specific repository 
            //3. Enter valid Username and Password
            //4. Click 'Login' button
            //5. Click 'Add Page' button
            //6. Enter Page Name
            //7. Click 'OK' button

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password, Constant.DefaultRepo);
            mainPage.AddPage(pageName: pageName);

            //8. Click 'Choose Panels' button 
            //9. Click 'Create new panel' button

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PanelPage panelPage = new PanelPage(driver);

            panelPage.UnhideChoosePanelsPage();
            panelPage.BtnCreateNewPanel.Click();

            //10. Click 'Chart Type' drop-down menu
            //11. Select 'Pie' Chart Type

            panelPage.Panel(action: "Create", panelType: "Chart", chartType: "Pie");

            //12. VP: Check that 'Categories' checkbox is disabled, 'Series' checkbox, 'Value' checkbox and 'Percentage' checkbox are enabled

            Assert.AreEqual(false, panelPage.ChbDataLabelsCategories.Enabled, "'Categories' checkbox is not disabled");
            Assert.AreEqual(true,panelPage.ChbDataLabelsSeries.Enabled, "'Series' checkbox is not enabled");
            Assert.AreEqual(true, panelPage.ChbDataLabelsValue.Enabled, "'Value' checkbox is not enabled");
            Assert.AreEqual(true, panelPage.ChbDataLabelsPercentage.Enabled, "'Percentage' checkbox is not enabled");

            //13. Click 'Chart Type' drop-down menu
            //14. Select 'Single Bar' Chart Type

            panelPage.Panel(action: "Create", panelType: "Chart", chartType: "Single Bar");

            //15. VP: Check that 'Categories' checkbox is disabled, 'Series' checkbox, 'Value' checkbox and 'Percentage' checkbox are enabled

            Assert.AreEqual(false, panelPage.ChbDataLabelsCategories.Enabled, "'Categories' checkbox is not disabled");
            Assert.AreEqual(true, panelPage.ChbDataLabelsSeries.Enabled, "'Series' checkbox is not enabled");
            Assert.AreEqual(true, panelPage.ChbDataLabelsValue.Enabled, "'Value' checkbox is not enabled");
            Assert.AreEqual(true, panelPage.ChbDataLabelsPercentage.Enabled, "'Percentage' checkbox is not enabled");

            //16. Click 'Chart Type' drop-down menu
            //17. Select 'Stacked Bar' Chart Type

            panelPage.Panel(action: "Create", panelType: "Chart", chartType: "Stacked Bar");

            //18. Check that 'Categories' checkbox, 'Series' checkbox, 'Value' checkbox and 'Percentage' checkbox are enabled

            Assert.AreEqual(true, panelPage.ChbDataLabelsCategories.Enabled, "'Categories' checkbox is not enabled");
            Assert.AreEqual(true, panelPage.ChbDataLabelsSeries.Enabled, "'Series' checkbox is not enabled");
            Assert.AreEqual(true, panelPage.ChbDataLabelsValue.Enabled, "'Value' checkbox is not enabled");
            Assert.AreEqual(true, panelPage.ChbDataLabelsPercentage.Enabled, "'Percentage' checkbox is not enabled");

            //19. Click 'Chart Type' drop-down menu
            //20. Select 'Group Bar' Chart Type

            panelPage.Panel(action: "Create", panelType: "Chart", chartType: "Group Bar");

            //21. VP: Check that 'Categories' checkbox, 'Series' checkbox, 'Value' checkbox and 'Percentage' checkbox are enabled

            Assert.AreEqual(true, panelPage.ChbDataLabelsCategories.Enabled, "'Categories' checkbox is not enabled");
            Assert.AreEqual(true, panelPage.ChbDataLabelsSeries.Enabled, "'Series' checkbox is not enabled");
            Assert.AreEqual(true, panelPage.ChbDataLabelsValue.Enabled, "'Value' checkbox is not enabled");
            Assert.AreEqual(true, panelPage.ChbDataLabelsPercentage.Enabled, "'Percentage' checkbox is not enabled");

            //22. Click 'Chart Type' drop-down menu
            //23. Select 'Line' Chart Type

            panelPage.Panel(action: "Create", panelType: "Chart", chartType: "Line");

            //24. VP: Check that 'Categories' checkbox, 'Series' checkbox, and 'Percentage' checkbox are disabled. 'Value' checkbox is enabled

            Assert.AreEqual(false, panelPage.ChbDataLabelsCategories.Enabled, "'Categories' checkbox is not disabled");
            Assert.AreEqual(false, panelPage.ChbDataLabelsSeries.Enabled, "'Series' checkbox is not disabled");
            Assert.AreEqual(true, panelPage.ChbDataLabelsValue.Enabled, "'Value' checkbox is not enabled");
            Assert.AreEqual(false, panelPage.ChbDataLabelsPercentage.Enabled, "'Percentage' checkbox is not disabled");

            //Post-condition: Delete created page.

            panelPage.BtnCancel.Click();
            mainPage.DeletePage(pageName);
        }
    }
}
