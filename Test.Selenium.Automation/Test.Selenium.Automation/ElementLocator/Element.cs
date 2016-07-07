using OpenQA.Selenium;

namespace Test.Selenium.Automation.ElementLocator
{
    public interface IElement
    {
        IWebElement WebElement { get; set; }
    }

    public class Element : IElement
    {
        public IWebElement WebElement { get; set; }

        public void Click()
        {
            WebElement.Click();
        }

        public bool IsDisplayed()
        {
            return WebElement.Displayed;
        }

        public string Text
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
