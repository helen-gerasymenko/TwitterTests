using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Twitter.UITests.ExtensionMethods
{
    public static class DriverExtensionMethods
    {
        /// <summary>
        /// A class containing custom methods that make Chrome Driver do extra actions
        /// e.g., wait for the element to become displayed and enabled, or return the text value of 
        /// the element
        /// </summary>

        /// <summary>
        /// A method to find a web element matching the selector, and wait until it is displayed and enabled 
        /// </summary>
        public static IWebElement FindElementWait(this IWebDriver driver, By by, int timeout = 30)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            IEnumerable<IWebElement> foundElements = null;
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));

            wait.Until(d =>
            {
                try
                {
                    foundElements = driver.FindElements(by).Where(x => x.Displayed && x.Enabled).ToArray();
                    var isFound = foundElements.Any();

                    //poke the first element's property to make sure it is not stale
                    if (isFound) foundElements.First().Text.ToString(); 
                    return isFound;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            });
            return foundElements.First();

        }

        /// <summary>
        /// A method to find a text value of the found web element which is displayed and enabled 
        /// returns the first
        /// </summary>
        public static string FindElementText(this IWebDriver driver, By by)
        {
            var element = FindElementWait(driver, by);
            return element.Text;
        }

        /// <summary>
        /// A method to find the text values of all web elements found which are displayed and enabled
        /// </summary>
        /// <returns></returns>
        public static List<string> FindElementsText(this IWebDriver driver, By by)
        {
            var elements = driver.FindElementsWait(by);
            return elements.Select(element => element.Text).ToList();
        }

        /// <summary>
        /// A method to find web elements matching the selector, and wait until they are displayed and enabled 
        /// </summary>
        public static IEnumerable<IWebElement> FindElementsWait(this IWebDriver driver, By by, int timeoutSeconds = 20)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            IEnumerable<IWebElement> foundElements = null;

            wait.Until(d =>
            {
                try
                {
                    foundElements = driver.FindElements(by).Where(x => x.Displayed && x.Enabled);
                    return foundElements.Any();
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            });

            return foundElements;
        }

        /// <summary>
        /// A method that pauses the code until a specified element is present and clickable on the page
        /// </summary>
        public static void WaitUntilElementIsClickable(this IWebDriver driver, IWebElement element, int timeout = 15)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.Until(ExpectedConditions.ElementToBeClickable(element));
        }

        /// <summary>
        /// A method that checks if the element has 'display:block' attribute
        /// </summary>
        public static bool IsElementVisible(this IWebDriver driver, By by, int timeout = 60)
        {
            return driver.FindElement(by).GetAttribute("style").Replace(" ", "").Contains("display:block");
        }

    }
}
