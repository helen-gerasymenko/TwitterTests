using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Twitter.UITests.Bases;

namespace Twitter.UITests.Pages
{
    public class LoginPage : PageBase
    {
        private const string PartialUrl = "login";

        public LoginPage(IWebDriver driver) : base(driver){}

        public static LoginPage NavigateToThisPageViaUrl(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(_baseWebsiteUrl + PartialUrl);
            return new LoginPage(driver);
        }

        public HomePage LogIn(string username, string password)
        {
            _loginField.Clear();
            _loginField.SendKeys(username);
            _passwordField.Clear();
            _passwordField.SendKeys(password);
            _loginButton.Click();
            return new HomePage(_driver);
        }

#pragma warning disable 649
        [FindsBy(How = How.XPath, Using = "//input[@class='js-username-field email-input js-initial-focus']")]
        private IWebElement _loginField;

        [FindsBy(How = How.XPath, Using = "//input[@class='js-password-field']")]
        private IWebElement _passwordField;

        [FindsBy(How = How.XPath, Using = "//button[text()='Log in']")]
        private IWebElement _loginButton;
#pragma warning restore 649
    }
}
