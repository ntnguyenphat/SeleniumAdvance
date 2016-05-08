﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumAdvance.Common;
using SeleniumAdvance.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumAdvance.TestCases.Login
{
    [TestClass]
    public class DA_LOGIN_TC005
    {
        [TestMethod]
        public void TC05()
        {
            Console.WriteLine("TC05 - Verify that there is no Login dialog when switching between 2 repositories with the same account");

            //1. Navigate to Dashboard login page
            HomePage homePage = new HomePage();
            homePage.Open();

            //2. Login with valid account for the first repository
            //3. Choose another repository in Repository list
            LoginPage loginPage = new LoginPage();
            homePage = loginPage.Login(Constant.Username, Constant.Password);

            //Todo: Using method SelectMenuItem to select another repository

            //VP: There is no Login Repository dialog
            //VP: The Repository menu displays name of switched repository
            Assert.AreEqual(true, homePage.LnkAccount.Displayed, "Homepage is not displayed!");
        }
    }
}
