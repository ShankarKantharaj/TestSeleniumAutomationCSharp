using System.Collections.Generic;
using Test.Selenium.Automation.UiControlInterfaces;

namespace Test.Selenium.Automation.ElementLocator
{
    public interface IElementLocator
    {
        T FindElement<T>(string locatorandValue) where T : IHtmlControl, new();
        List<T> FindElements<T>(string locatorandValue) where T : IHtmlControl, new();
    }
}
