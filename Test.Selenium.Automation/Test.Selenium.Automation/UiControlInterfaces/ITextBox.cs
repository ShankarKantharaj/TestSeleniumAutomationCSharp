namespace Test.Selenium.Automation.UiControlInterfaces
{
    public interface ITextBox
    {
        void SetText(string value );
        string GetText();
        void ClearText();
    }
}
