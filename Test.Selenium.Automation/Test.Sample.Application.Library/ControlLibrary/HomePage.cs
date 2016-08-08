using Test.Sample.Application.Library.Constants;
using Test.Sample.Application.Library.DataUtilities;
using Test.Selenium.Automation.ElementLocator;
using Test.Selenium.Automation.UiControls;

namespace Test.Sample.Application.Library.ControlLibrary
{
    public class HomePage
    {
        private readonly IElementLocator _elementLocator;
        private readonly XmlRepositorySingleton _xmlUtility;
        

        public HomePage(IElementLocator elementLocator, XmlRepositorySingleton xmlUtility)
        {
            _elementLocator = elementLocator;
            _xmlUtility = xmlUtility;
        }

        public HomePage()
        {
            
        }

        #region Xml Key Constants

        private const string Selectfile = "Selectfile";
        private const string Convert = "Convert";
        private const string SelectFilePopup = "SelectFilePopup";
        private const string SelectfilePopupOkButton = "SelectfilePopupOkButton";

        #endregion

        public Button SelectfileButton
        {
            get
            {
                var locatorandValue = _xmlUtility.GetLocatorTypeAndValue(XmlConstants.HomePage, Selectfile);
                var button = _elementLocator.FindElement<Button>(locatorandValue[0], locatorandValue[1]);
                return button;
            }
        }

        public Button ConvertButton
        {
            get
            {
                var locatorandValue = _xmlUtility.GetLocatorTypeAndValue(XmlConstants.HomePage, Convert);
                var button = _elementLocator.FindElement<Button>(locatorandValue[0], locatorandValue[1]);
                return button;
            }
        }

        public HtmlControl SelectFilePopUp
        {
            get
            {
                var locatorandValue = _xmlUtility.GetLocatorTypeAndValue(XmlConstants.HomePage, SelectFilePopup);
                var frame = _elementLocator.FindElement<HtmlControl>(locatorandValue[0], locatorandValue[1]);
                return frame;
            }
        }

        public Button SelectFilePopUpOkButton
        {
            get
            {
                var locatorandValue = _xmlUtility.GetLocatorTypeAndValue(XmlConstants.HomePage, SelectfilePopupOkButton);
                var button = _elementLocator.FindElement<Button>(locatorandValue[0], locatorandValue[1]);
                return button;
            }
        }
    }
}
