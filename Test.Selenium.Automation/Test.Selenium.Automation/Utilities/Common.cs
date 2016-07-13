using System;
using OpenQA.Selenium;

namespace Test.Selenium.Automation.Utilities
{
    public class Common
    {
        public static By GetLocator(string locatortype, string locatorValue)
        {
            By locatorType;

            switch (locatortype)
            {
                case "id":
                    locatorType = By.Id(locatorValue);
                    break;

                case "xpath":
                    locatorType = By.XPath(locatorValue);
                    break;

                case "name":
                    locatorType = By.Name(locatorValue);
                    break;

                case "classname":
                    locatorType = By.ClassName(locatorValue);
                    break;

                case "tagname":
                    locatorType = By.TagName(locatorValue);
                    break;

                case "cssselector":
                    locatorType = By.CssSelector(locatorValue);
                    break;

                case "linktext":
                    locatorType = By.LinkText(locatorValue);
                    break;

                default:
                    throw new Exception(string.Format("Locator {0} is not valid", locatortype));
            }

            return locatorType;
        }
    }
}
