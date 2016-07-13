using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Test.Selenium.Automation.Driver;

namespace Test.Selenium.Automation.ElementLocator
{
    public interface IElement
    {
        IWebElement WebElement { get; set; }

        void Click();
        void DoubleClick(Element element);
    }

    public class Element : IElement
    {
        private readonly Actions _actions;

        public IWebElement WebElement { get; set; }

        public Element()
        {
            _actions = new Actions(WebDriver.WebDriverInstance);
        }

        public void Click()
        {
            WebElement.Click();
        }

        public void DoubleClick(Element element)
        {
            _actions.DoubleClick(element.WebElement);
        }

        public bool IsDisplayed()
        {
            return WebElement.Displayed;
        }

        public string GetTextAttributeValue
        {
            get
            {
                var text = WebElement.Text;
                return text;
            }

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
