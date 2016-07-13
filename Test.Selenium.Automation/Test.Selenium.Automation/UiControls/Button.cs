using Test.Selenium.Automation.UiControlInterfaces;

namespace Test.Selenium.Automation.UiControls
{
    public class Button : HtmlControl, IButton
    {
        public string GetButtonText()
        {
            var buttonText = WebElement.GetAttribute("value");
            return buttonText;
        }
    }
}
