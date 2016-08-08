using System.Collections.Generic;
using Test.Selenium.Automation.UiControlInterfaces;

namespace Test.Selenium.Automation.ElementLocator
{
    public interface IElementLocator
    {
        T FindElement<T>(string locatortype, string locatorValue) where T : IHtmlControl, new();
        List<T> FindElements<T>(string locatortype, string locatorValue) where T : IHtmlControl, new();
    }
}
