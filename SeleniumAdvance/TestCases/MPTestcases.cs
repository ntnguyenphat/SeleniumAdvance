using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumAdvance.PageObjects;
using SeleniumAdvance.Common;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumAdvance.Ultilities;

namespace SeleniumAdvance.TestCases
{
    [TestClass]
    public class MPTestcases : TestBase
    {

        /// <summary>Verify that user is unable open more than 1 \"New Page\" dialog
        /// </summary>
        /// <Author>Phat</Author>
        [TestMethod]
        public void TC011()
        {
            Console.WriteLine("DA_MP_TC011 - Verify that user is unable open more than 1 \"New Page\" dialog");

            //1. Navigate to Dashboard login page. Login with valid account
            //2. Go to Global Setting -> Add page. Try to go to Global Setting -> Add page again

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password);
            mainPage.SelectGeneralSetting("Add Page");

            bool actualResult = mainPage.IsDashboardLockedByDialog();

            //VP: User cannot go to Global Setting -> Add page while "New Page" dialog appears.

            Assert.AreEqual(true, actualResult, "Dashboard is not locked by dialog!");
        }

        /// <summary>Verify that user is able to add additional pages besides \"Overview\" page successfully
        /// </summary>
        /// <Author>Phat</Author>
        /// <Modified by>Long: Use modified Addpage method. Use Assert to verify</Modified>
        /// <Modified by>Phat: Make the code tidy</Modified>
        [TestMethod]
        public void TC012()
        {
            Console.WriteLine("DA_MP_TC012 - Verify that user is able to add additional pages besides \"Overview\" page successfully");

            string pageName = CommonMethods.GetUniqueString();

            //1. Navigate to Dashboard login page. Login with valid account
            //2. Go to Global Setting -> Add page. Enter Page Name field

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password);

            mainPage.AddPage(pageName);

            //VP: New page is displayed besides "Overview" page

            bool isPageNextToPage = mainPage.IsPageNextToPage("Overview", pageName);
            Assert.AreEqual(true, isPageNextToPage, "\nThe new page isn't displayed besides \"Overview\" page");

            //Post-condition: Delete newly added page
            mainPage.DeletePage(pageName);
        }

        /// <summary>Verify that the newly added main parent page is positioned at the location specified as set with \"Displayed After\" field of \"New Page\" form on the main page bar/\"Parent Page\" dropped down menu
        /// </summary>
        /// <Author>Phat</Author>
        /// <Modified by>Long: Use modified Addpage method. Use Assert to verify </Modified>
        /// <Modified by>Phat: Make the code tidy</Modified>
        [TestMethod]
        public void TC013()
        {
            Console.WriteLine("DA_MP_TC013 - Verify that the newly added main parent page is positioned at the location specified as set with \"Displayed After\" field of \"New Page\" form on the main page bar/\"Parent Page\" dropped down menu");

            string pageName1 = string.Concat("Page1", CommonMethods.GetUniqueString());
            string pageName2 = string.Concat("Page2", CommonMethods.GetUniqueString());

            //1. Navigate to Dashboard login page. Login with valid account
            //2. Go to Global Setting -> Add page. Enter Page Name field

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password);

            mainPage.AddPage(pageName1);
            mainPage.AddPage(pageName: pageName2, displayAfer: pageName1);

            //VP: Page 1 is positioned besides the Page 2

            bool isPageNextToPage = mainPage.IsPageNextToPage(pageName1, pageName2);
            Assert.AreEqual(true, isPageNextToPage, "\n" + pageName2 + " isn't positioned besides" + pageName1);

            //Post-condition: Delete newly added page

            mainPage.DeletePage(pageName2).DeletePage(pageName1);
        }

        /// <summary>Verify that \"Public\" pages can be visible and accessed by all users of working repository
        /// </summary>
        /// <Author>Long</Author>
        /// <Modified by>Phat: Make the code tidy</Modified>
        [TestMethod]
        public void TC014()
        {
            Console.WriteLine("DA_MP_TC014 - Verify that \"Public\" pages can be visible and accessed by all users of working repository");

            string pageName = string.Concat("Page1", CommonMethods.GetUniqueString());

            //1.Navigate to Dashboard login page
            //2.Log in specific repository with valid account

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password, Constant.DefaultRepo);

            //3.Go to Global Setting -> Add page
            //4.Enter Page Name field
            //5.Check Public checkbox
            //6.Click OK button

            mainPage.AddPage(pageName: pageName, publicCheckBox: true);

            //7.Click on Log out link
            //8.Log in with another valid account

            loginPage = mainPage.Logout();
            loginPage.Login(Constant.OtherUsername, Constant.OtherPassword, Constant.DefaultRepo);

            //VP: Check newly added page is visibled

            bool doesPageExist = mainPage.DoesPageExist(pageName);
            Assert.AreEqual(true, doesPageExist, "\n" + pageName + " isn't visibled");

            //Post-condition: Delete newly added page

            loginPage = mainPage.Logout();
            loginPage.Login(Constant.Username, Constant.Password).DeletePage(pageName);
        }

        /// <summary>Verify that non \"Public\" pages can only be accessed and visible to their creators with condition that all parent pages above it are \"Public\
        /// </summary>
        /// <Author>Phat</Author>
        /// <Modified by>Long: Use modified Addpage method. Use Assert to verify</Modified>
        /// <Modified by>Phat: Make the code tidy</Modified>
        [TestMethod]
        public void TC015()
        {
            Console.WriteLine("DA_MP_TC015 - Verify that non \"Public\" pages can only be accessed and visible to their creators with condition that all parent pages above it are \"Public\"");

            string parentPageName = string.Concat("Parent", CommonMethods.GetUniqueString());
            string childPageName = string.Concat("Child", CommonMethods.GetUniqueString());

            //1. Navigate to Dashboard login page. Log in specific repository with valid account
            //2. Go to Global Setting -> Add page. Enter Page Name field. Check Public checkbox. Click OK button

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password, Constant.DefaultRepo);

            mainPage.AddPage(pageName: parentPageName, publicCheckBox: true);

            //3. Go to Global Setting -> Add page. Enter Page Name field. Click on  Select Parent dropdown list
            //4. Select specific page. Click OK button. Click on Log out link. Log in with another valid account

            mainPage.AddPage(pageName: childPageName, parentPage: parentPageName);
            mainPage.Logout();

            loginPage.Login(Constant.OtherUsername, Constant.OtherPassword, Constant.DefaultRepo);

            //VP: Children is invisibled

            bool doesPageExist = mainPage.DoesPageExist(parentPageName + "->" + childPageName);
            Assert.AreEqual(false, doesPageExist, "\n" + childPageName + " is visibled");

            //Post-condition: Delete newly added page

            loginPage = mainPage.Logout();
            loginPage.Login(Constant.Username, Constant.Password, Constant.DefaultRepo).DeletePage(childPageName).DeletePage(parentPageName);
        }

        /// <summary>Verify that user is able to edit the \"Public\" setting of any page successfully
        /// </summary>
        /// <Author>Phat</Author>
        /// <Modified by>Long: Use modified Addpage method. Use Assert to verify</Modified>
        /// <Modified by>Phat: Make the code tidy
        [TestMethod]
        public void TC016()
        {
            Console.WriteLine("DA_MP_TC016 - Verify that user is able to edit the \"Public\" setting of any page successfully");

            string pageName1 = string.Concat("Page1", CommonMethods.GetUniqueString());
            string pageName2 = string.Concat("Page2", CommonMethods.GetUniqueString());

            //1. Navigate to Dashboard login page. Log in specific repository with valid account
            //2. Go to Global Setting -> Add page. Enter Page Name. Click OK button

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password, Constant.DefaultRepo);
            mainPage.AddPage(pageName: pageName1);

            //3. Go to Global Setting -> Add page.  Enter Page Name. Check Public checkbox. Click OK button
            //4. Click on "Test" page. Click on "Edit" link.

            mainPage.AddPage(pageName: pageName2, publicCheckBox: true);
            mainPage.GotoPage(pageName1);
            mainPage.SelectGeneralSetting("Edit");

            //VP: "Edit Page" pop up window is displayed

            bool doesPopupExist1 = mainPage.DoesPopupExist("Edit Page");
            Assert.AreEqual(true, doesPopupExist1, "\nPop up window is not displayed");

            //5. Check Public checkbox. Click OK button
            //6. Click on "Another Test" page. Click on "Edit" link.

            mainPage.EditPageInfomation(publicCheckBox: true).GotoPage(pageName2).SelectGeneralSetting("Edit");

            //VP: "Edit Page" pop up window is displayed

            bool doesPopupExist2 = mainPage.DoesPopupExist("Edit Page");
            Assert.AreEqual(true, doesPopupExist2, "\nPop up window is not displayed");

            //7. Uncheck Public checkbox. Click OK button
            //8. Click Log out link. Log in with another valid account

            mainPage.EditPageInfomation(publicCheckBox: false).Logout();

            loginPage.Login(Constant.OtherUsername, Constant.OtherPassword, Constant.DefaultRepo);

            //VP: Check "Test" Page is visible and can be accessed. Check "Another Test" page is invisible.

            bool doesPageName1Exist = mainPage.DoesPageExist(pageName1);
            Assert.AreEqual(true, doesPageName1Exist, "\n" + pageName1 + " isn't visibled");

            bool doesPageName2Exist = mainPage.DoesPageExist(pageName2);
            Assert.AreEqual(false, doesPageName2Exist, "\n" + pageName2 + " is visibled");
           
            //Post-condition: Delete newly added page

            loginPage = mainPage.Logout();
            loginPage.Login(Constant.Username, Constant.Password, Constant.DefaultRepo).DeletePage(pageName2).DeletePage(pageName1);
        }

        /// <summary>Verify that user can remove any main parent page except \"Overview\" page successfully and the order of pages stays persistent as long as there is not children page under it
        /// </summary>
        /// <Author>Long</Author>
        /// <Modified by>Phat: Make the code tidy</Modified>
        [TestMethod]
        public void TC017()
        {
            Console.WriteLine("DA_MP_TC017 - Verify that user can remove any main parent page except \"Overview\" page successfully and the order of pages stays persistent as long as there is not children page under it");

            string parentPageName = string.Concat("Parent", CommonMethods.GetUniqueString());
            string childPageName = string.Concat("Child", CommonMethods.GetUniqueString());

            //1. Navigate to Dashboard login page
            //2. Log in specific repository with valid account
            //3. Add a new parent page
            //4. Add a children page of newly added page

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password, Constant.DefaultRepo);

            mainPage.AddPage(pageName: parentPageName).AddPage(pageName: childPageName, parentPage: parentPageName);

            //5. Click on parent page
            //6. Click "Delete" link

            mainPage.GotoPage(parentPageName).SelectGeneralSetting("Delete");

            //7. VP: Check confirm message "Are you sure you want to remove this page?" appears

            string expectedMessage1 = "Are you sure you want to remove this page?";
            string actualMessage1 = mainPage.GetAlertMessage();
            Assert.AreEqual(expectedMessage1, actualMessage1, "\nConfirm message doesn't appear.");

            //8. Click OK button

            mainPage.GetAlertMessage(closeAlert: true);

            //9. VP: Check warning message "Can not delete page 'Test' since it has child page(s)" appears

            string expectedMessage2 = "Cannot delete page '" + parentPageName +  "' since it has child page(s).";
            string actualMessage2 = mainPage.GetAlertMessage().Trim();
            Assert.AreEqual(expectedMessage2, actualMessage2, "\nWarning message doesn't appear.");

            //10. Click OK button

            mainPage.GetAlertMessage(closeAlert: true);

            //11. Click on  children page
            //12. Click "Delete" link

            mainPage.GotoPage(parentPageName + "->" + childPageName).SelectGeneralSetting("Delete");

            //13. VP: Check confirm message "Are you sure you want to remove this page?" appears

            string expectedMessage3 = "Are you sure you want to remove this page?";
            string actualMessage3 = mainPage.GetAlertMessage();
            Assert.AreEqual(expectedMessage3, actualMessage3, "\nConfirm message doesn't appear.");

            //14. Click OK button

            mainPage.GetAlertMessage(closeAlert: true);

            //15. VP: Check children page is deleted

            bool doesChildPageExist = mainPage.DoesPageExist(parentPageName + "->" + childPageName);
            Assert.AreEqual(false, doesChildPageExist, "\nChild page isn't deleted");
            
            //16. Click on Parent page
            //17. Click "Delete" link

            mainPage.GotoPage(parentPageName).SelectGeneralSetting("Delete");

            //18. VP: Check confirm message "Are you sure you want to remove this page?" appears

            string expectedMessage4 = "Are you sure you want to remove this page?";
            string actualMessage4 = mainPage.GetAlertMessage();
            Assert.AreEqual(expectedMessage4, actualMessage4, "\nConfirm message doesn't appear.");

            //19. Click OK button

            mainPage.GetAlertMessage(closeAlert: true);

            //20. VP: Check parent page is deleted

            bool doesParentPageExist = mainPage.DoesPageExist(parentPageName);
            Assert.AreEqual(false, doesParentPageExist, "\nParent page isn't deleted");
        }

        /// <summary>Verify that user is able to add additional sibbling pages to the parent page successfully
        /// </summary>
        /// <Author>Long</Author>
        /// <Modified by>Phat: Make the code tidy</Modified>
        [TestMethod]
        public void TC018()
        {
            Console.WriteLine("DA_MP_TC018 - Verify that user is able to add additional sibbling pages to the parent page successfully");

            string parentPageName = string.Concat("Test Parent ", CommonMethods.GetUniqueString());
            string childPageName1 = string.Concat("Test Child 1 ", CommonMethods.GetUniqueString());
            string childPageName2 = string.Concat("Test Child 2 ", CommonMethods.GetUniqueString());

            //1. Navigate to Dashboard login page
            //2. Log in specific repository with valid account
            //3. Go to Global Setting -> Add page
            //4. Enter Page Name
            //5. Click OK button

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password, Constant.DefaultRepo);
            mainPage.AddPage(pageName: parentPageName);

            //6. Go to Global Setting -> Add page
            //7. Enter Page Name
            //8. Click on  Parent Page dropdown list
            //9. Select a parent page
            //10. Click OK button

            mainPage.AddPage(pageName: childPageName1, parentPage: parentPageName);

            //11. Go to Global Setting -> Add page
            //12. Enter Page Name
            //13. Click on  Parent Page dropdown list
            //14. Select a parent page
            //15. Click OK button

            mainPage.AddPage(pageName: childPageName2, parentPage: parentPageName);

            //16. VP: Check "Test Child 2" is added successfully

            bool doesChildPageExist = mainPage.DoesPageExist(parentPageName + "->" + childPageName2);
            Assert.AreEqual(true, doesChildPageExist, "\nTest child 2 isn't added");

            //Post-condtion: Delete all created pages

            mainPage.DeletePage(parentPageName + "->" + childPageName2).DeletePage(parentPageName + "->" + childPageName1).DeletePage(parentPageName);
        }

        /// <summary>Verify that user is able to add additional sibbling page levels to the parent page successfully
        /// </summary>
        /// <Author>Long</Author>
        /// <Modified by>Phat: Make the code tidy</Modified>
        [TestMethod]
        public void TC019()
        {
            Console.WriteLine("DA_MP_TC019 - Verify that user is able to add additional sibbling page levels to the parent page successfully.");

            string childPageName = string.Concat("Child", CommonMethods.GetUniqueString());

            //1. Navigate to Dashboard login page
            //2. Login with valid account
            //3. Go to Global Setting -> Add page
            //4. Enter info into all required fields on New Page dialog

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password, Constant.DefaultRepo);

            mainPage.AddPage(pageName: childPageName, parentPage: "Overview");

            //5. VP: Check that newly added page is the child page of Overview page

            bool doesChildPageExist = mainPage.DoesPageExist("Overview" + "->" + childPageName);
            Assert.AreEqual(true, doesChildPageExist, "\nnewly added page isn't the child page of Overview page");

            //Post-condition: Delete newly added page

            mainPage.DeletePage("Overview" + "->" + childPageName);
        }

        /// <summary>Verify that user is able to delete sibbling page as long as that page has not children page under it
        /// </summary>
        /// <Author>Long</Author>
        /// <Modified by>Phat: Make the code tidy</Modified>
        [TestMethod]
        public void TC020()
        {
            Console.WriteLine("DA_MP_TC020 - Verify that user is able to delete sibbling page as long as that page has not children page under it");

            string pageName1 = string.Concat("Page1", CommonMethods.GetUniqueString());
            string pageName2 = string.Concat("Page2", CommonMethods.GetUniqueString());

            //1. Navigate to Dashboard login page
            //2. Login with valid account
            //3. Go to Global Setting -> Add page
            //4. Enter info into all required fields on New Page dialog

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password, Constant.DefaultRepo);

            mainPage.AddPage(pageName: pageName1, parentPage: "Overview");

            //5. Go to Global Setting -> Add page
            //6. Enter info into all required fields on New Page dialog

            mainPage.AddPage(pageName: pageName2, parentPage: "    " + pageName1);

            //7. Go to the first created page
            //8. Click Delete link
            //9. Click Ok button on Confirmation Delete page

            mainPage.GotoPage("Overview" + "->" + pageName1).SelectGeneralSetting("Delete");
            mainPage.GetAlertMessage(closeAlert: true);

            //10. VP: Check warning message "Can not delete page 'Test' since it has child page(s)" appears

            string expectedMessage = "Cannot delete page '" + pageName1 + "' since it has child page(s).";
            string actualMessage = mainPage.GetAlertMessage().Trim();
            Assert.AreEqual(expectedMessage, actualMessage, "\nWarning message doesn't appear.");
            
            //11. Close confirmation dialog

            mainPage.GetAlertMessage(closeAlert: true);

            //12. Go to the second page
            //13. Click Delete link
            //14. Click Ok button on Confirmation Delete page

            mainPage.GotoPage("Overview" + "->" + pageName1 + "->" + pageName2).SelectGeneralSetting("Delete");
            mainPage.GetAlertMessage(closeAlert: true);
            
            //15. Check that Page 2 is deleted successfully

            bool doesPageExist = mainPage.DoesPageExist("Overview" + "->" + pageName1 + "->" + pageName2);
            Assert.AreEqual(false, doesPageExist, "\nPage 2 isn't deleted");

            //Post-condition: Delete all newly added page

            mainPage.DeletePage("Overview" + "->" + pageName1);
        }

        /// <summary>Verify that user is able to edit the name of the page (Parent/Sibbling) successfully
        /// </summary>
        /// <Author>Long</Author>
        /// <Modified by>Phat: Make the code tidy</Modified>
        [TestMethod]
        public void TC021()
        {
            Console.WriteLine("DA_MP_TC021 - Verify that user is able to edit the name of the page (Parent/Sibbling) successfully");

            string pageName1 = string.Concat("Page1", CommonMethods.GetUniqueString());
            string pageName2 = string.Concat("Page2", CommonMethods.GetUniqueString());
            string pageName3 = string.Concat("Page3", CommonMethods.GetUniqueString());
            string pageName4 = string.Concat("Page4", CommonMethods.GetUniqueString());

            //1. Navigate to Dashboard login page
            //2. Login with valid account
            //3. Go to Global Setting -> Add page
            //4. Enter info into all required fields on New Page dialog

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password, Constant.DefaultRepo);

            mainPage.AddPage(pageName: pageName1, parentPage: "Overview");

            //5. Go to Global Setting -> Add page
            //6. Enter info into all required fields on New Page dialog

            mainPage.AddPage(pageName: pageName2, parentPage: "    " + pageName1);

            //7. Go to the first created page
            //8. Click Edit link
            //9. Enter another name into Page Name field
            //10. Click Ok button on Edit Page dialog

            mainPage.GotoPage("Overview" + "->" + pageName1).SelectGeneralSetting("Edit");
            mainPage.EditPageInfomation(pageName: pageName3);

            //11. VP: User is able to edit the name of parent page successfully

            bool doesEditParentPageExist = mainPage.DoesPageExist("Overview" + "->" + pageName3);
            Assert.AreEqual(true, doesEditParentPageExist, "\nUser isn't able to edit the name of parent page");
            
            //12. Go to the second created page
            //13. Click Edit link
            //14. Enter another name into Page Name field
            //15. Click Ok button on Edit Page dialog

            mainPage.GotoPage("Overview" + "->" + pageName3 + "->" + pageName2).SelectGeneralSetting("Edit");
            mainPage.EditPageInfomation(pageName: pageName4);

            //16. VP: User is able to edit the name of sibbling page successfully

            bool doesEditChildPageExist = mainPage.DoesPageExist("Overview" + "->" + pageName3 + "->" + pageName4);
            Assert.AreEqual(true, doesEditChildPageExist, "User isn't able to edit the name of sibbling page");

            //Post-condition: Delete all newly added page

            mainPage.DeletePage("Overview" + "->" + pageName3 + "->" + pageName4).DeletePage("Overview" + "->" + pageName3);
        }

        /// <summary>Verify that user is unable to duplicate the name of sibbling page under the same parent page
        /// </summary>
        /// <Author>Long</Author>
        /// <Modified by>Phat: Make the code tidy</Modified>
        [TestMethod]
        public void TC022()
        {
            Console.WriteLine("DA_MP_TC022 - Verify that user is unable to duplicate the name of sibbling page under the same parent page");

            string pageName1 = string.Concat("Page1", CommonMethods.GetUniqueString());
            string pageName2 = string.Concat("Page2", CommonMethods.GetUniqueString());

            //1. Navigate to Dashboard login page
            //2. Log in specific repository with valid account
            //3. Add a new page
            //4. Add a sibling page of new page

            LoginPage loginPage = new LoginPage(driver);
            loginPage.Open().Login(Constant.Username, Constant.Password, Constant.DefaultRepo);

            MainPage mainPage = new MainPage(driver);
            mainPage.AddPage(pageName: pageName1);
            mainPage.AddPage(pageName: pageName2, parentPage: pageName1);

            //5. Go to Global Setting -> Add page
            //6. Enter Page Name
            //7. Click on  Parent Page dropdown list
            //8. Select a parent page
            //9. Click OK button

            mainPage.AddPage(pageName: pageName2, parentPage: pageName1);

            //10. Check warning message "Test child already exist. Please enter a different name" appears

            string expectedMessage =  pageName2 + " already exists. Please enter a different name.";
            string actualMessage = mainPage.GetAlertMessage().Trim();
            Assert.AreEqual(expectedMessage, actualMessage, "Warning message doesn't appear.");    
       
            //Post-conmainPage.GetAlertMessage()dition: Delete all added pages

            mainPage.GetAlertMessage(closeAlert: true);
            mainPage.BtnPageCancel.Click();
            mainPage.DeletePage(pageName1 + "->" + pageName2).DeletePage(pageName1);
        }

        /// <summary>Verify that user is able to edit the parent page of the sibbling page
        /// </summary>
        /// <Author>Long</Author>
        /// <Modified by>Phat: Make the code tidy</Modified>
        [TestMethod]
        public void TC023()
         {
             Console.WriteLine("DA_MP_TC023 - Verify that user is able to edit the parent page of the sibbling page");
             
             string pageName1 = string.Concat("Page1", CommonMethods.GetUniqueString());
             string pageName2 = string.Concat("Page2", CommonMethods.GetUniqueString());
             string pageName3 = string.Concat("Page3", CommonMethods.GetUniqueString());

             //1. Navigate to Dashboard login page
             //2. Login with valid account
             //3. Go to Global Setting -> Add page
             //4. Enter info into all required fields on New Page dialog

             LoginPage loginPage = new LoginPage(driver);
             loginPage.Open().Login(Constant.Username, Constant.Password, Constant.DefaultRepo);

             MainPage mainPage = new MainPage(driver);
             mainPage.AddPage(pageName: pageName1, parentPage: "Overview");

             //5. Go to Global Setting -> Add page
             //6. Enter info into all required fields on New Page dialog

             mainPage.AddPage(pageName: pageName2, parentPage: "    " + pageName1);

             //7. Go to the first created page
             //8. Click Edit link
             //9. Enter another name into Page Name field
             //10. Click Ok button on Edit Page dialog

             mainPage.GotoPage("Overview" + "->" + pageName1).SelectGeneralSetting("Edit");
             mainPage.EditPageInfomation(pageName: pageName3);

             //11. VP: User is able to edit the parent page of the sibbling page successfully

             bool doesEditPageExist = mainPage.DoesPageExist("Overview" + "->" + pageName3);
             Assert.AreEqual(true, doesEditPageExist, "User isn't able to edit the parent page of the sibbling page");

             //Post-condition: Delete all created pages

             mainPage.DeletePage("Overview" + "->" + pageName3 + "->" + pageName2).DeletePage("Overview" + "->" + pageName3);
         }

        /// <summary>Verify that \"Bread Crums\" navigation is correct
        /// </summary>
        /// <Author>Long</Author>
        /// <Modified by>Phat: Make the code tidy</Modified>
        [TestMethod]
        public void TC024()
        {
            Console.WriteLine("DA_MP_TC024 - Verify that \"Bread Crums\" navigation is correct");

            string pageName1 = string.Concat("Page1", CommonMethods.GetUniqueString());
            string pageName2 = string.Concat("Page2", CommonMethods.GetUniqueString());
            
            //1. Navigate to Dashboard login page
            //2. Login with valid account
            //3. Go to Global Setting -> Add page
            //4. Enter info into all required fields on New Page dialog

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password, Constant.DefaultRepo);
            mainPage.AddPage(pageName: pageName1, parentPage: "Overview");

            //5. Go to Global Setting -> Add page
            //6. Enter info into all required fields on New Page dialog

            mainPage.AddPage(pageName: pageName2, parentPage: "    " + pageName1);

            //7. Click the first breadcrums

            mainPage.GotoPage("Overview" + "->" + pageName1);

            //8. VP: Check that the first page is navigated

            bool isFirstPageNavigated = mainPage.IsPageNavigated(pageName1);
            Assert.AreEqual(true, isFirstPageNavigated, "The first page isn't navigated");

            //9.Click the second breadcrums

            mainPage.GotoPage("Overview" + "->" + pageName1 + "->" + pageName2);

            //10. VP: Check that the second page is navigated

            bool isSecondPageNavigated = mainPage.IsPageNavigated(pageName2);
            Assert.AreEqual(true, isFirstPageNavigated, "The first page isn't navigated");

            //Post-condition: Delete all created pages.

            mainPage.DeletePage("Overview" + "->" + pageName1 + "->" + pageName2).DeletePage("Overview" + "->" + pageName1);
        }

        /// <summary>Verify that page listing is correct when user edit \"Display After\"  field of a specific page
        /// </summary>
        /// <Author>Long</Author>
        /// <Modified by>Phat: Make the code tidy</Modified>
        [TestMethod]
        public void TC025()
        {
            Console.WriteLine("DA_MP_TC025 - Verify that page listing is correct when user edit \"Display After\"  field of a specific page");

            string pageName1 = string.Concat("Page1", CommonMethods.GetUniqueString());
            string pageName2 = string.Concat("Page2", CommonMethods.GetUniqueString());

            //1. Navigate to Dashboard login page
            //2. Login with valid account
            //3. Go to Global Setting -> Add page
            //4. Enter info into all required fields on New Page dialog

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password, Constant.DefaultRepo);
            mainPage.AddPage(pageName: pageName1);

            //5. Go to Global Setting -> Add page
            //6. Enter info into all required fields on New Page dialog

            mainPage.AddPage(pageName: pageName2);

            //7. Click Edit link for the first created page
            //8. Change value Display After for the second created page to after Overview page
            //9. Click Ok button on Edit Page dialog

            mainPage.GotoPage(pageName1).SelectGeneralSetting("Edit");
            mainPage.EditPageInfomation(displayAfer: "Overview");

            //10. VP: Position of the second page follow Overview page

            bool isPageNextToPage = mainPage.IsPageNextToPage("Overview", pageName1);
            Assert.AreEqual(true, isPageNextToPage, "The position of second page isn't correct");

            //Post-condition: Delee all created pages

            mainPage.DeletePage(pageName1).DeletePage(pageName2);
        }

        /// <summary>Verify that page column is correct when user edit \"Number of Columns\" field of a specific page
        /// </summary>
        /// <Author>Long</Author>
        /// <Modified by>Phat: Make the code tidy</Modified>
        [TestMethod]
        public void TC026()
        {
            Console.WriteLine("DA_MP_TC026 - Verify that page column is correct when user edit \"Number of Columns\" field of a specific page");

            string pageName = string.Concat("Page", CommonMethods.GetUniqueString());

            //1. Navigate to Dashboard login page
            //2. Login with valid account
            //3. Go to Global Setting -> Add page
            //4. Enter info into all required fields on New Page dialog

            LoginPage loginPage = new LoginPage(driver);
            MainPage mainPage = loginPage.Open().Login(Constant.Username, Constant.Password, Constant.DefaultRepo);

            mainPage.AddPage(pageName: pageName, numberOfColumn: 2);

            //5. Go to Global Setting -> Edit link
            //6. Edit Number of Columns for the above created page
            //7. Click Ok button on Edit Page dialog

            mainPage.GotoPage(pageName).SelectGeneralSetting("Edit");
            mainPage.EditPageInfomation(numberOfColumn: 3);

            //8. VP: There are 3 columns on the above created page

            //TO DO : I don't understand the function of the column.
 
            //Post-condition: Delete created page

            mainPage.DeletePage(pageName);
        }
    }
}
