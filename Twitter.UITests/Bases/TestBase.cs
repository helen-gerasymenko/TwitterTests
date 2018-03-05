using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Twitter.UITests.Bases
{
    /// <summary>
    /// Base Class to define the functionality all test fixtures will share.
    /// </summary>
    public abstract class TestBase
    {
        protected readonly IWebDriver _driver;
        protected static readonly string _baseWebsiteUrl = new Uri(ConfigurationManager.AppSettings["baseWebsiteUrl"]).ToString();
        private readonly string _initialWindowHandle;
        private static readonly string AssemblyLocalPath = SetupAssemblyPath();
        private static readonly string OutputFolderPath = AssemblyLocalPath + @"\..\..\..\Output\Screenshots\";

        protected TestBase()
        {
            Directory.CreateDirectory(OutputFolderPath);
            _driver = CreateChromeDriver();
            _initialWindowHandle = _driver.CurrentWindowHandle;
        }

        [OneTimeSetUp]
        public virtual void BeforeAll() { }

        [SetUp]
        public virtual void BeforeEach() { }

        [TearDown]
        public virtual void AfterEach()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                AfterEachFailed();
            }
        }

        public virtual void AfterEachFailed()
        {
            SaveScreenshot();

            //ensure the driver is set to the original tab opened
            _driver.SwitchTo().Window(_initialWindowHandle);
        }

        [OneTimeTearDown]
        public virtual void AfterAll()
        {
            _driver.Quit();
        }

        private static IWebDriver CreateChromeDriver()
        {
            //get the path to chrome.exe file
            var pathToChromeExecutable = new FileInfo(Path.Combine(AssemblyLocalPath, 
                @"..\..\..\packages\Selenium.WebDriver.ChromeDriver.2.33.0\driver\win32")).FullName;

            //set an option to disable 'Save Password' prompt in the browser
            var options = new ChromeOptions();
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("password_manager_enabled", false);

            var driver= new ChromeDriver(pathToChromeExecutable, options)
            {
                Url = _baseWebsiteUrl 
            }; 

            driver.Manage().Window.Size = new Size(1800, 900);
            return driver;
        }

        private static string SetupAssemblyPath()
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            return new Uri(assemblyPath).LocalPath;
        }

        private void SaveScreenshot()
        {
            if(_driver == null) return;

            //generate a screenshot name
            var testName = TestContext.CurrentContext.Test.Name;
            var fileName = $"{DateTime.Now:yyyy-MM-dd_hh-mm}-{testName}.png";
            var fullPath = Path.Combine(OutputFolderPath, fileName);

            Screenshot screenshot = ((ITakesScreenshot) _driver).GetScreenshot();
            //need to wait until the screenshot is taken
            Thread.Sleep(TimeSpan.FromSeconds(0.5));

            screenshot.SaveAsFile(fullPath, ScreenshotImageFormat.Png);
            
            //need to wait until the screenshot is saved as a file
            Thread.Sleep(TimeSpan.FromSeconds(0.5));
            //TestContext.Out.WriteLine(fileName);
        }
    }
}
