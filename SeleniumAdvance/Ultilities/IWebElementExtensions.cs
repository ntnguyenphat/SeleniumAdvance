using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumAdvance.Common;

namespace SeleniumAdvance.Ultilities
{
    public static class IWebElementExtensions
    {
        public static void MouseTo(this IWebElement element, IWebDriver webDriver)
        {
            Actions actions = new Actions(webDriver);
            actions.MoveToElement(element).Build().Perform();
        }

        public static void SelectItem(this IWebElement element, string item)
        {
            SelectElement selector = new SelectElement(element);
            selector.SelectByText(item);
        }

        public static void Check(this IWebElement element)
        {
            bool isChecked = element.Selected;
            if(isChecked == false)
            {
                element.Click();
            }
        }
        public static void UnCheck(this IWebElement element)
        {
            bool isChecked = element.Selected;
            if (isChecked == true)
            {
                element.Click();
            }
        }
    }
}
