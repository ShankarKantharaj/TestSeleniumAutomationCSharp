using Test.Sample.Application.Library.ControlLibrary;
using Test.Selenium.Automation.Driver;

namespace Test.Sample.Application.Library.FunctionalLibrary
{
    public class HomePageControlActions
    {
        private readonly HomePage _homePage;

        public HomePageControlActions(HomePage homepage)
        {
            _homePage = homepage;
        }

        public void ClickConvertButton()
        {
            _homePage.ConvertButton.Click();
        }

        public void DismissSelectFilePopup()
        {
            WebDriver.WebDriverInstance.SwitchTo().Frame(_homePage.SelectFilePopUp.WebElement);
            _homePage.SelectFilePopUpOkButton.Click();
        }

    }
}
