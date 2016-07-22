using System;
using OpenQA.Selenium;
using Test.Selenium.Automation.Driver;

namespace Test.Selenium.Automation.Utilities
{
    public class BrowserUtilities
    {
        private static readonly IWebDriver Driver;

        static BrowserUtilities()
        {
            Driver = WebDriver.WebDriverInstance;
        }

        public static string ExecuteScript(string script)
        {
            try
            {
                var iJavaexecutor = (IJavaScriptExecutor)WebDriver.WebDriverInstance;
                var result = iJavaexecutor.ExecuteScript(script);
                return result.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static void Scroll()
        {
            var jse = (IJavaScriptExecutor)Driver;
            jse.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
        }

        public static void RefreshBrowser()
        {
            Driver.Navigate().Refresh();
        }

        public static void SwitchTab(int tabIndex)
        {
            var tabs = Driver.WindowHandles;
            Driver.SwitchTo().Window(tabs[1]);
        }

        public static string GetUrl
        {
            get { return Driver.Url; }
        }

        public static void NavigateToUrl(string url)
        {
            Driver.Navigate().GoToUrl(url);
            Driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromMinutes(5));
        }
    }
}
