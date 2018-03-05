using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Twitter.UITests.Bases;
using Twitter.UITests.ExtensionMethods;
using Twitter.UITests.Pages.Components;
using static System.Int32;

namespace Twitter.UITests.Pages
{
    public class HomePage : PageBase
    {
        public readonly MenuBar MenuBar;

        public HomePage(IWebDriver driver) : base(driver)
        {
            MenuBar = new MenuBar(driver);
            WaitForPageToLoad();
        }

        private void WaitForPageToLoad()
        {
            _wait.Until(d => _whoToFollowItems.Any());
        }

        public int TweetCount => Parse(_driver.FindElementText(By.ClassName("ProfileCardStats-statValue")));

        public string NewTweetNotification =>
            _driver.FindElementText(By.XPath("//button[@class='new-tweets-bar js-new-tweets-bar']"));

        public HomePage NavigateToThisPageViaUrl()
        {
            _driver.Navigate().GoToUrl(_baseWebsiteUrl);
            return new HomePage(_driver);
        }

        public HomePage ViewTweet()
        {
            _driver.WaitUntilElementIsClickable(_viewTweetButton);
            _viewTweetButton.Click();
            _driver.Navigate().Refresh();
            return new HomePage(_driver);
        }

#pragma warning disable 649
        [FindsBy(How = How.XPath, Using = "//button[@class='new-tweets-bar js-new-tweets-bar']")]
        private IWebElement _viewTweetButton;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class, 'UserSmallListItem')]")]
        private IList<IWebElement> _whoToFollowItems;
#pragma warning restore 649
    }
}
