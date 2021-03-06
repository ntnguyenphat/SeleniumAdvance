﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumAdvance.Common;
using System.Threading;

namespace SeleniumAdvance.Ultilities
{
    public static class IWebElementExtensions
    {
        /// <summary>
        /// Move mouse to a element
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="webDriver">The web driver.</param>
        /// <Author>Phat</Author>
        public static void MouseTo(this IWebElement element, IWebDriver webDriver)
        {
            Actions actions = new Actions(webDriver);
            actions.MoveToElement(element).Build().Perform();
        }

        /// <summary>
        /// Selects an item.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="item">The item.</param>
        /// <param name="selectby">The selectby.</param>
        /// <Author>Phat</Author>
        public static void SelectItem(this IWebElement element, string item, string selectby = "Text")
        {
            SelectElement selector = new SelectElement(element);
            if (selectby == "Value")
                selector.SelectByValue(item);
            else if (selectby == "Index")
                selector.SelectByIndex(int.Parse(item) - 1);
            else
                selector.SelectByText(item);
        }


        /// <summary>
        /// Determines if a element exists
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="item">The item.</param>
        /// <Author>Phat</Author>
        /// <returns></returns>
        public static bool IsItemExist(this IWebElement element, string item)
        {
            SelectElement selector = new SelectElement(element);
            List<IWebElement> list = selector.Options.ToList();
            List<string> listItem = new List<string>();

            for (int i = 0; i < list.Count; i++)
            {
                listItem.Add(list[i].Text.ToString());
            }

            return listItem.Contains(item);
        }

        /// <summary>
        /// Determines if a element sorted.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <Author>Phat</Author>
        /// <returns></returns>
        public static bool IsItemSorted(this IWebElement element)
        {
            SelectElement selector = new SelectElement(element);
            List<IWebElement> list = selector.Options.ToList();
            List<string> listItem = new List<string>();
            bool flag = true;

            for (int i = 0; i < list.Count - 1; i++)
            {
                if (string.Compare(list[i].Text, list[i++].Text) < 0)
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }

        public static int CountItems(this IWebElement element)
        {
            SelectElement selector = new SelectElement(element);
            return selector.Options.Count;
        }

        /// <summary>
        /// Check or uncheck a checkbox
        /// </summary>
        /// <param name="element">The element.</param>
        /// <Author>Phat</Author>
        public static void Check(this IWebElement element)
        {
            bool isChecked = element.Selected;
            if (isChecked == false)
            {
                element.Click();
            }
        }

        /// <summary>
        /// Check or uncheck a checkbox
        /// </summary>
        /// <param name="element">The element.</param>
        /// <Author>Long</Author>
        public static void UnCheck(this IWebElement element)
        {
            bool isChecked = element.Selected;
            if (isChecked == true)
            {
                element.Click();
            }
        }

        /// <summary>
        /// Chooses a element then wait
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="time">The time.</param>
        /// <Author>Phat</Author>
        /// <Startdate>30/5/2016</Startdate>
        public static void ChooseAndWait(this IWebElement element,TimeSpan time)
        {
            element.Click();

            //TODO: Need to handle wait by Driverwait
            //Phat - 02/06/2016: This action is no longer used due to change of MyFindElement method

            Thread.Sleep(time);
        }

        /// <summary>
        /// Inputs the text.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="text">The text.</param>
        /// <Author>Phat</Author>
        /// <Startdate>30/05/2016</Startdate>
        public static void InputText(this IWebElement element, string text)
        {
            element.Clear();
            element.SendKeys(text);
        }

        /// <summary>
        /// Determines whether the element is a link.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="webDriver">The web driver.</param>
        /// <returns></returns>
        /// <Author>Long</Author>
        /// <Startdate>02/06/2016</Startdate>
        public static bool IsLink(this IWebElement element, IWebDriver webDriver)
        {
           if (element.GetAttribute("href") != null)
                return true;
            else
                return false;      
        }
    }
}
