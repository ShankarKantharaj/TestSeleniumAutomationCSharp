using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Test.Selenium.Automation.Driver;
using Test.Selenium.Automation.UiControlInterfaces;

namespace Test.Selenium.Automation.UiControls
{
    public class HtmlControl : IHtmlControl
    {
        private readonly Actions _actions;

        public IWebElement WebElement { get; set; }

        public HtmlControl()
        {
            _actions = new Actions(WebDriver.WebDriverInstance);
        }

        public void Click()
        {
            WebElement.Click();
        }

        public void DoubleClick(HtmlControl element)
        {
            _actions.DoubleClick(element.WebElement);
        }

        public bool IsDisplayed()
        {
            return WebElement.Displayed;
        }

        public string GetTextAttributeValue()
        {
            var text = WebElement.Text;
            return text;
        }

        public string GetAttributeValue(string attribute)
        {
            return WebElement.GetAttribute(attribute);
        }

        public string GetCssValue(string cssAttribute)
        {
            return WebElement.GetCssValue(cssAttribute);
        }
    }
}
