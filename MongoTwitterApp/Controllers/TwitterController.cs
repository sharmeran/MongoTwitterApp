using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LinqToTwitter;
using MongoTwitterApp.BusinessDomain;
using MongoTwitterApp.Constants;
using MongoTwitterApp.Models;

namespace MongoTwitterApp.Controllers
{
    public class TwitterController : ApiController
    {

        public List<TwitterEntity> GetTweets(ActionState actionState)
        {
            TwitterDomain domain = new TwitterDomain();
            return domain.GetTwitter(actionState);
        }

        public void AddTweets(List<TwitterEntity>tweetList, ActionState actionState)
        {
            TwitterDomain domain = new TwitterDomain();
            domain.AddTweets(tweetList, actionState);
        }

        public List<UserStatistics> AllStatistics(ActionState actionState)
        {
            TwitterDomain domain = new TwitterDomain();
            return domain.FindAllUserStatistics(actionState);
        }

        public List<UserStatistics> GetLastMonthStatistics(ActionState actionState)
        {
            TwitterDomain domain = new TwitterDomain();
            return domain.FindAllUserStatistics(DateTime.Now.AddMonths(-1), DateTime.Now, actionState);
        }

       
    }
}
