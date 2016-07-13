using OpenQA.Selenium;
using Test.Selenium.Automation.UiControlInterfaces;

namespace Test.Selenium.Automation.UiControls
{
    public class TextBox : HtmlControl, ITextBox
    {
        public void SetText(string value)
        {
            WebElement.SendKeys(value);
        }

        public string GetText()
        {
            return WebElement.Text;
        }

        public void ClearText()
        {
            WebElement.Clear();
            WebElement.SendKeys(Keys.Enter);
        }
    }
}
