using NUnit.Framework;
using Twitter.UITests.Bases;
using Twitter.UITests.Pages;
using Twitter.UITests.TestAttributes;
using Twitter.UITests.TestData;

namespace Twitter.UITests.Tests
{
    public class TweetTests : TestBase
    {
        private HomePage _homePage;
        private readonly string _testInput = TestDataGenerator.GetRandomString(size: 279);

        public override void BeforeAll()
        {
            var loginPage = LoginPage.NavigateToThisPageViaUrl(_driver);
            _homePage = loginPage.LogIn(LoginData.GmailAccount.Email, LoginData.GmailAccount.Password);
        }

        public override void AfterEach()
        {
            base.AfterEach();
            _homePage = _homePage.NavigateToThisPageViaUrl();
        }

        public override void AfterAll()
        {
            _driver.Manage().Cookies.DeleteAllCookies();
            base.AfterAll();
        }

        [Test, CategorySmoke]
        public void Can_publish_a_279_char_tweet()
        {
            //publish a 279-char tweet
            var initialTweetCount = _homePage.TweetCount;
            var tweetDialog = _homePage.MenuBar.OpenTweetDialog();
            Assert.IsTrue(tweetDialog.Header == "Compose new Tweet", "Tweet dialog header is not as expected.");

            _homePage = tweetDialog.AddText(_testInput);
            Assert.AreEqual("See 1 new Tweet", _homePage.NewTweetNotification, "Tweet notification text is not as expected.");

            //verify tweets total increased by 1 on Home page
            _homePage = _homePage.ViewTweet();
            var updatedTweetCount = _homePage.TweetCount;
            Assert.Greater(updatedTweetCount, initialTweetCount, $"Updated tweet count should be: {initialTweetCount+1}, " +
                                                                 $"but it was {updatedTweetCount}.");
            //verify tweets total increased by 1 on Profile page
            var profilePage = ProfilePage.NavigateToThisPageViaUrl(_driver);
            Assert.AreEqual(initialTweetCount + 1, profilePage.TweetCount, $"Updated tweet count should be: {initialTweetCount + 1}, " +
                                                                           $"but it was {updatedTweetCount}.");
            var publishedTweet = profilePage.GetTweetContent(indexInList: 0);
            Assert.That(publishedTweet.Contains(_testInput), "The tweet does not contain the expected text.");
        }

        [TestCaseSource(typeof(ImageFileManager), nameof(ImageFileManager.ImagePaths))]
        public void Can_publish_a_tweet_with_image(string image)
        {
            //publish a tweet with image
            var initialTweetCount = _homePage.TweetCount;
            var tweetDialog = _homePage.MenuBar.OpenTweetDialog();
            _homePage = tweetDialog.AddImage(image);
            Assert.AreEqual("See 1 new Tweet", _homePage.NewTweetNotification, "Tweet notification text is not as expected.");

            //verify tweets total increased by 1 on Home page
            _homePage = _homePage.ViewTweet();
            var updatedTweetCount = _homePage.TweetCount;
            Assert.Greater(updatedTweetCount, initialTweetCount, $"Updated tweet count should be: {initialTweetCount + 1}, " +
                                                                 $"but it was {updatedTweetCount}.");
            //verify tweets total increased by 1 on Profile page
            var profilePage = ProfilePage.NavigateToThisPageViaUrl(_driver);
            Assert.Greater(profilePage.TweetCount, initialTweetCount, $"Updated tweet count should be: {initialTweetCount + 1}, " +
                                                                      $"but it was {profilePage.TweetCount}.");
        }

    }
}
