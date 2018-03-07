using System.Configuration;

namespace Twitter.UITests.TestData
{
    public static class LoginData
    {
        public static readonly Account GmailAccount = new Account(
            ConfigurationManager.AppSettings["username"],
            ConfigurationManager.AppSettings["password"]
        );
    }
}
