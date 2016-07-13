using OpenQA.Selenium;
using Test.Selenium.Automation.UiControls;

namespace Test.Selenium.Automation.UiControlInterfaces
{
    public interface IHtmlControl
    {
        IWebElement WebElement { get; set; }

        void Click();
        void DoubleClick(HtmlControl element);
        bool IsDisplayed();
        string GetTextAttributeValue();
        string GetAttributeValue(string attribute);
        string GetCssValue(string cssAttribute);
    }
}
