using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Test.Selenium.Automation.Driver;
using Test.Selenium.Automation.UiControlInterfaces;
using Test.Selenium.Automation.Utilities;

namespace Test.Selenium.Automation.ElementLocator
{
    public class ElementLocator
    {
        #region variables

        private readonly TimeSpan _timeOut;
        private readonly IWebDriver _driver;

        #endregion

        public ElementLocator()
        {
            _driver = WebDriver.WebDriverInstance;
            _timeOut = TimeSpan.FromMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["WaitTimeInMinutes"]));
        }

        public T FindElement<T>(string locatortype, string locatorValue) where T : IHtmlControl, new()
        {
            var locator = Common.GetLocator(locatortype, locatorValue);

            var webElement = FindElement(locator);
            if (webElement == null)
                return new T();

            var element = new T { WebElement = webElement };
            return element;
        }

        public List<T> FindElements<T>(string locatorType, string locatorValue) where T : IHtmlControl, new()
        {
            var locator = Common.GetLocator(locatorType, locatorValue);
            var webElements = FindElements(locator);

            return webElements == null ? null : webElements.Select(element => new T { WebElement = element }).ToList();
        }

        #region private Methods

        private IWebElement FindElement(By locator)
        {
            try
            {
                var wait = new WebDriverWait(_driver, _timeOut);
                var webElement = wait.Until(ExpectedConditions.ElementExists(locator));
                return webElement;
            }

            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }

        private ReadOnlyCollection<IWebElement> FindElements(By locator)
        {
            try
            {
                var wait = new WebDriverWait(_driver, _timeOut);
                var webElement = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
                return webElement;
            }

            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }

        }

        #endregion
    }
}
