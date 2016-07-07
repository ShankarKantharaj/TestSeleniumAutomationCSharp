using System.Collections.Generic;

namespace Test.Selenium.Automation.ElementLocator
{
    public interface IElementLocator
    {
        T FindElement<T>(string locatorandValue) where T : IElement, new();
        List<T> FindElements<T>(string locatorandValue) where T : IElement, new();
    }
}
