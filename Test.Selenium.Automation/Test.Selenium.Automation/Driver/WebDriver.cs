using System;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;

namespace Test.Selenium.Automation.Driver
{
    public class WebDriver
    {
        #region Environment Variables

        private static string _browserName;
        private static string _driverPath;
        private static string _waitTimeInMinutes;
        private static string _environment;
        private static string _downloadPath;
        private static string _setPageLoadTimeout;
        private static string _remoteEnvironment;

        //SauceLabs Settings
        private static string _remoteBrowser;
        private static string _remotebrowserVersion;
        private static string _remotePlatform;
        private static string _remotescreenResolution;
        private static string _commandExecutionUri;
        private static string _sauceLabsUserName;
        private static string _sauceLabsKey;
        private static bool _recordScreenshots;
        private static bool _recordVideo;
        private static string _maxDuration;

        #endregion

        private WebDriver()
        {

        }

        public static IWebDriver WebDriverInstance { get; set; }

        #region Singleton Initialization

        private static readonly Lazy<WebDriver> _instance = new Lazy<WebDriver>(() =>
        {
            GetAppSettings();

            switch (_environment)
            {
                case "local":
                    InitializeDriver();
                    break;

                case "remote":
                    InitializeRemoteDriver(_remoteEnvironment);
                    break;

                default:
                    throw new Exception("Please choose a valid environment");
            }

            return new WebDriver();
        });

        public static WebDriver Instance
        {
            get { return _instance.Value; }
        }

        #endregion

        #region Driver Initializations

        private static void InitializeDriver()
        {
            switch (_browserName.ToLower())
            {
                case "firefox":
                    WebDriverInstance = FirefoxInitializations();
                    break;

                case "chrome":
                    WebDriverInstance = ChromeInitializations();
                    break;

                case "ie":
                    WebDriverInstance = IeInitializations();
                    break;

                default:
                    throw new Exception(string.Format("Browser: {0} is not valid. Try changing to Firefox, Chrome or IE ", _browserName));
            }

            WebDriverInstance.Manage().Window.Maximize();
            WebDriverInstance.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromMinutes(5));
            WebDriverInstance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMinutes(Convert.ToInt32(_waitTimeInMinutes)));
        }

        private static void InitializeRemoteDriver(string environment)
        {
            switch (environment.ToLower())
            {
                case "seleniumgrid":
                    WebDriverInstance = InitializeSeleniumGrid();
                    break;

                case "phantomjs":
                    WebDriverInstance = InitializePhantomJsDriver();
                    break;

                case "saucelabs":
                    WebDriverInstance = InitializeSauceLabs();
                    break;
            }
        }

        #endregion

        #region Browser Settings Initialization

        private static IWebDriver IeInitializations()
        {
            var service = InternetExplorerDriverService.CreateDefaultService(_driverPath);
            var options = new InternetExplorerOptions
            {
                IntroduceInstabilityByIgnoringProtectedModeSettings = true
            };

            return new InternetExplorerDriver(service, options);
        }

        private static IWebDriver FirefoxInitializations()
        {
            var fireFoxprofile = new FirefoxProfile();
            fireFoxprofile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            fireFoxprofile.SetPreference("browser.download.folderList", 2);
            fireFoxprofile.SetPreference("browser.download.dir", _downloadPath);
            return new FirefoxDriver(fireFoxprofile);
        }

        private static IWebDriver ChromeInitializations()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("download.default_directory", _downloadPath);
            return new ChromeDriver(_driverPath, chromeOptions);
        }

        #endregion

        #region Cloud Settings Initialization

        private static IWebDriver InitializeSauceLabs()
        {
            var desiredCapabilites = new DesiredCapabilities(_remoteBrowser, _remotebrowserVersion, Platform.CurrentPlatform);

            desiredCapabilites.SetCapability("platform", _remotePlatform);
            desiredCapabilites.SetCapability("username", _sauceLabsUserName);
            desiredCapabilites.SetCapability("accessKey", _sauceLabsKey);
            desiredCapabilites.SetCapability("name", DateTime.Now.ToString("hh:mm:ss tt"));
            desiredCapabilites.SetCapability("screenResolution", _remotescreenResolution);
            desiredCapabilites.SetCapability("recordScreenshots", _recordScreenshots);
            desiredCapabilites.SetCapability("recordVideo", _recordVideo);
            desiredCapabilites.SetCapability("maxDuration", _maxDuration);

            return new RemoteWebDriver(new Uri(_commandExecutionUri), desiredCapabilites, TimeSpan.FromSeconds(120));
        }

        private static IWebDriver InitializeSeleniumGrid()
        {
            var capabilities = DesiredCapabilities.Firefox();
            capabilities.SetCapability(CapabilityType.BrowserName, "firefox");
            capabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Windows));


            var driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), capabilities);
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMinutes(2));
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromMinutes(Convert.ToInt32(_setPageLoadTimeout)));
            return driver;
        }

        private static IWebDriver InitializePhantomJsDriver()
        {
            var service = PhantomJSDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            service.LoadImages = false;
            service.ProxyType = "none";

            var driver = new PhantomJSDriver(service);
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMinutes(Convert.ToInt32(_waitTimeInMinutes)));
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromMinutes(Convert.ToInt32(_setPageLoadTimeout)));

            return driver;
        }

        #endregion

        private static void GetAppSettings()
        {
            #region Environment App settings

            _environment = ConfigurationManager.AppSettings["Environment"];
            _waitTimeInMinutes = ConfigurationManager.AppSettings["WaitTimeInMinutes"];
            _browserName = ConfigurationManager.AppSettings["Browser"];
            _driverPath = ConfigurationManager.AppSettings["DriverPath"];
            _downloadPath = ConfigurationManager.AppSettings["DownloadPath"];
            _setPageLoadTimeout = ConfigurationManager.AppSettings["SetPageLoadTimeout"];

            _remoteEnvironment = ConfigurationManager.AppSettings["RemoteEnvironment"];

            #endregion

            #region Sauce labs Settings

            _commandExecutionUri = ConfigurationManager.AppSettings["CommandExecutionUri"];
            _remoteBrowser = ConfigurationManager.AppSettings["RemoteBrowser"];
            _remotebrowserVersion = ConfigurationManager.AppSettings["RemoteBrowserVersion"];
            _remotePlatform = ConfigurationManager.AppSettings["RemotePlatform"];
            _sauceLabsUserName = ConfigurationManager.AppSettings["SauceLabsUserName"];
            _sauceLabsKey = ConfigurationManager.AppSettings["SauceLabsUserPassword"];
            _recordScreenshots = Convert.ToBoolean(ConfigurationManager.AppSettings["RecordScreenshots"]);
            _recordVideo = Convert.ToBoolean(ConfigurationManager.AppSettings["RecordVideo"]);
            _remotescreenResolution = ConfigurationManager.AppSettings["ScreenResolution"];
            _maxDuration = ConfigurationManager.AppSettings["MaximumDuration"];

            #endregion
        }
    }
}
