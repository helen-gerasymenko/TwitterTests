using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Twitter.UITests.Bases;
using Twitter.UITests.ExtensionMethods;

namespace Twitter.UITests.Pages.Components
{
    public class TweetDialog : PageBase
    {
        public TweetDialog(IWebDriver driver) : base(driver)
        {
            _wait.Until(d => IsDisplayed);
        }

        private bool IsDisplayed => _driver.IsElementVisible(By.Id("Tweetstorm-dialog"));

        public string Header => _driver.FindElementText(By.ClassName("modal-title"));

        public HomePage AddText(string testInput)
        {
            _tweetBox.Clear();
            _tweetBox.SendKeys(testInput);
            _tweetButton.Click();
            _wait.Until(d => !IsDisplayed);
            return new HomePage(_driver);
        }

        public HomePage AddImage(string imagePath)
        {
            _imageSelectorButton.SendKeys(imagePath);
            _tweetButton.Click();
            _wait.Until(d => !IsDisplayed);
            return new HomePage(_driver);
        }

#pragma warning disable 649
        [FindsBy(How = How.XPath, Using = "//div[@id='Tweetstorm-tweet-box-0']//div[@name='tweet']")]
        private IWebElement _tweetBox;

        [FindsBy(How = How.XPath, Using = "//div[@id='Tweetstorm-tweet-box-0']//input[@data-original-title='Add photos or video']")]
        private IWebElement _imageSelectorButton;

        [FindsBy(How = How.XPath, Using = "//div[@id='Tweetstorm-tweet-box-0']//span[@class='button-text']")]
        private IWebElement _tweetButton;

#pragma warning restore 649
    }
}