using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LinqToTwitter;
using MongoTwitterApp.Constants;
using MongoTwitterApp.Models;

namespace MongoTwitterApp.Controllers
{
    public class TwitterController : ApiController
    {


        public void GetTwitter()
        {

            try
            {

                var mvcAuthorizer = new MvcAuthorizer
                {
                    Credentials = new InMemoryCredentials
                    {
                        ConsumerKey = ConfigurationManager.AppSettings[CommonConstants.ConsumerKey],
                        ConsumerSecret = ConfigurationManager.AppSettings[CommonConstants.ConsumerSecret],
                        AccessToken = ConfigurationManager.AppSettings[CommonConstants.AccessToken],
                        OAuthToken = ConfigurationManager.AppSettings[CommonConstants.OAuthToken],
                        ScreenName = ConfigurationManager.AppSettings[CommonConstants.ScreenName]
                    }
                };
                var twitterCtx = new TwitterContext(mvcAuthorizer);
                var friendTweets =
               (from tweet in twitterCtx.Status
                where tweet.Type== StatusType.Home
                select new TwitterEntity
                {

                    Name = tweet.User.Name,
                    Tweet = tweet.Text,
                    UserID = tweet.User.Identifier.ID,
                    ScreenName=tweet.User.Identifier.ScreenName,
                    CreatedDate=tweet.CreatedAt

                });
                List<TwitterEntity> list = friendTweets.ToList<TwitterEntity>();

                }
            catch (Exception ex)
            {

            }






        }
    }
}
