using OpenQA.Selenium;
using SeleniumAdvance.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumAdvance.Ultilities;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace SeleniumAdvance.PageObjects
{
    public class PageBase
    {
        protected IWebDriver _driver;

        public PageBase(IWebDriver driver)
        {
            this._driver = driver;
        }

        /// <summary>
        /// Override FindElement
        /// </summary>
        /// <param name="by">The by.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        /// <author>Long</author>
        /// <startdate>31/05/2016</startdate>
        /// <Modified>Phat - 04/06/2016: Add NoSuchElementException. Wait for element clickable if element is a button</Modified>
        public IWebElement MyFindElement(By by, long timeout = 30)
        {
            IWebElement Ele = null;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (timeout >= 0)
            {
                try
                {
                    WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeout));
                    wait.Until(ExpectedConditions.ElementExists(by));
                    wait.Until(driver => _driver.FindElement(by).Displayed);
                    //if (by.ToString().Contains("input"))
                    //{
                    //    wait.Until(ExpectedConditions.ElementToBeClickable(by));
                    //}
                    Ele = _driver.FindElement(by);
                    break;
                }
                catch(StaleElementReferenceException)
                {
                    timeout = timeout - stopwatch.Elapsed.Ticks;
                    MyFindElement(by, timeout);
                    stopwatch.Stop();
                }
                catch(NullReferenceException)
                {
                    timeout = timeout - stopwatch.Elapsed.Ticks;
                    MyFindElement(by, timeout);
                    stopwatch.Stop();
                }
                catch(WebDriverTimeoutException)
                {
                    timeout = timeout - stopwatch.Elapsed.Ticks;
                    MyFindElement(by, timeout);
                    stopwatch.Stop();
                }
                catch(ArgumentNullException)
                {
                    timeout = timeout - stopwatch.Elapsed.Ticks;
                    MyFindElement(by, timeout);
                    stopwatch.Stop();
                }
                catch (WebDriverException)
                {
                    timeout = timeout - stopwatch.Elapsed.Ticks;
                    MyFindElement(by, timeout);
                    stopwatch.Stop();
                }
            }
            stopwatch.Stop();
            return Ele;
        }

        /// <summary>
        /// Override FindElements
        /// </summary>
        /// <param name="by">The by.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        /// <author>Long</author>
        /// <startdate>31/05/2016</startdate>
        /// <Modified>Phat - 04/06/2016: Add NoSuchElementException</Modified>
        public ReadOnlyCollection<IWebElement> MyFindElements(By by, long timeout = 30)
        {
            ReadOnlyCollection<IWebElement> Eles = null;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (timeout >= 0)
            {
                try
                {
                    WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeout));
                    wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(by));
                    Eles = _driver.FindElements(by);
                    break;
                }
                catch (StaleElementReferenceException)
                {
                    timeout = timeout - stopwatch.Elapsed.Ticks;
                    MyFindElements(by, timeout);
                    stopwatch.Stop();
                }
                catch (NullReferenceException)
                {
                    timeout = timeout - stopwatch.Elapsed.Ticks;
                    MyFindElements(by, timeout);
                    stopwatch.Stop();
                }
                catch (WebDriverTimeoutException)
                {
                    timeout = timeout - stopwatch.Elapsed.Ticks;
                    MyFindElements(by, timeout);
                    stopwatch.Stop();
                }
                catch (WebDriverException)
                {
                    timeout = timeout - stopwatch.Elapsed.Ticks;
                    MyFindElements(by, timeout);
                    stopwatch.Stop();
                }
                catch (ArgumentNullException)
                {
                    timeout = timeout - stopwatch.Elapsed.Ticks;
                    MyFindElements(by, timeout);
                    stopwatch.Stop();
                }
            }
            stopwatch.Stop();
            return Eles;
        }
    }

}
