using System.Linq;
using OpenQA.Selenium;
using Twitter.UITests.Bases;
using Twitter.UITests.ExtensionMethods;
using static System.Int32;

namespace Twitter.UITests.Pages
{
    public class ProfilePage : PageBase
    {
        private const string PartialUrl = "Expert_for_Test";

        public ProfilePage(IWebDriver driver) : base(driver)
        {
            WaitForPageToLoad();
        }

        private void WaitForPageToLoad()
        {
            _wait.Until(d =>
                _driver.FindElements(By.XPath(
                    "//li[@class='trend-item js-trend-item  context-trend-item']")).Any());
        }

        public int TweetCount => Parse(_driver.FindElementText(By.ClassName("ProfileNav-value")));

        public static ProfilePage NavigateToThisPageViaUrl(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(_baseWebsiteUrl + PartialUrl);
            return new ProfilePage(driver);
        }

        public string GetTweetContent(int indexInList)
        {
            var tweetList = _driver.FindElementsText(By.XPath("//p[contains(@class, 'tweet-text')]"));
            return tweetList[indexInList];
        }
    }

}
