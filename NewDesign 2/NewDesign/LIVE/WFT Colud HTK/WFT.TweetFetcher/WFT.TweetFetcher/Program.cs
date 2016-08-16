using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using TweetSharp;

namespace WFT.TweetFetcher
{
    class Program
    {
        static void Main(string[] args)
        {  
            try
            {
                TwitterService service = new TwitterService(ConfigurationManager.AppSettings["ConsumerKey"], ConfigurationManager.AppSettings["ConsumerSecret"]);
                OAuthRequestToken requestToken = service.GetRequestToken();
                service.AuthenticateWith(ConfigurationManager.AppSettings["AccessToken"], ConfigurationManager.AppSettings["AccessTokenSecret"]);
                IEnumerable<TwitterStatus> wftTweetsPosted = service.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions());

                cgxwftcloudEntities objDB = new cgxwftcloudEntities();

                foreach (var tweet in wftTweetsPosted)
                {
                    if (!objDB.SocialTweets.Any(o => o.tweetID == tweet.Id))
                    {
                        SocialTweet stweet = new SocialTweet();
                        stweet.tweetID = tweet.Id;
                        stweet.CreatedDate = tweet.CreatedDate;
                        stweet.tweetPlainText = tweet.Text;
                        stweet.tweetHTMLText = tweet.TextAsHtml;

                        objDB.SocialTweets.AddObject(stweet);
                        objDB.SaveChanges();
                    }
                }
            }
            catch (Exception Ex)
            {
                using (cgxwftcloudEntities exContext = new cgxwftcloudEntities())
                {
                    exContext.InsertExceptionLog("WFT.TweetFetcher", Ex.Message, Ex.StackTrace, DateTime.Now, "Main");
                }
            }
        }
    }
}
