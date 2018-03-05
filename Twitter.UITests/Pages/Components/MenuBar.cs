using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Twitter.UITests.Bases;

namespace Twitter.UITests.Pages.Components
{
    public class MenuBar : PageBase
    {
        public MenuBar(IWebDriver driver): base(driver) {}

        public TweetDialog OpenTweetDialog()
        {
            _tweetButton.Click();
            _wait.Until(d => _tweetDialog.Displayed);
            return new TweetDialog(_driver);
        }

#pragma warning disable 649
        [FindsBy(How = How.Id, Using = "global-new-tweet-button")]
        private IWebElement _tweetButton;

        [FindsBy(How = How.Id, Using = "Tweetstorm-dialog")]
        private IWebElement _tweetDialog;
#pragma warning restore 649
    }
}
