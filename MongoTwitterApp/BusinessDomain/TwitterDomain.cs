using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using LinqToTwitter;
using MongoDB.Bson;
using MongoTwitterApp.Constants;
using MongoTwitterApp.DataAccess;
using MongoTwitterApp.Models;

namespace MongoTwitterApp.BusinessDomain
{
    public class TwitterDomain
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tweetList"></param>
        /// <param name="actionState"></param>
        public void AddTweets(List<TwitterEntity> tweetList, ActionState actionState)
        {

            TwitterRepository twitterRepository = new TwitterRepository();
            for (int i = 0; i < tweetList.Count; i++)
            {
                twitterRepository.Insert(tweetList[i], actionState);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionState"></param>
        /// <returns></returns>
        public List<TwitterEntity> FindAll(ActionState actionState)
        {
            TwitterRepository twitterRepository = new TwitterRepository();
            return twitterRepository.FindAll(actionState);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionState"></param>
        /// <returns></returns>
        public IEnumerable<BsonValue> FindAUsersID(ActionState actionState)
        {
            TwitterRepository twitterRepository = new TwitterRepository();
            return twitterRepository.FindAUsersID(actionState);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionState"></param>
        public void DeleteAll(ActionState actionState)
        {
            TwitterRepository twitterRepository = new TwitterRepository();
            twitterRepository.DeleteAll(actionState);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="actionState"></param>
        /// <returns></returns>
        public int FindTwitterCountByName(string userName, ActionState actionState)
        {
            TwitterRepository twitterRepository = new TwitterRepository();
            return twitterRepository.FindTwitterCountByName(userName, actionState);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionState"></param>
        /// <returns></returns>
        public List<UserStatistics> FindAllUserStatistics(ActionState actionState)
        {
            List<UserStatistics> userStattisticsList = new List<UserStatistics>();
            IEnumerable<BsonValue> userNameList = FindAUsersID(actionState);

            for (int i = 0; i < userNameList.ToArray<BsonValue>().Count(); i++)
            {
                UserStatistics userStatistic = new UserStatistics();
                userStatistic.Name = userNameList.ToArray<BsonValue>()[i].AsString;
                userStatistic.Count = FindTwitterCountByName(userStatistic.Name, actionState);
                userStattisticsList.Add(userStatistic);
            }
            return userStattisticsList;
        }
        /// <summary>
       /// 
       /// </summary>
       /// <param name="start"></param>
       /// <param name="end"></param>
       /// <param name="actionState"></param>
       /// <returns></returns>
        public List<UserStatistics> FindAllUserStatistics(DateTime start, DateTime end, ActionState actionState)
        {
            List<UserStatistics> userStattisticsList = new List<UserStatistics>();
            IEnumerable<BsonValue> userNameList = FindAUsersID(actionState);

            for (int i = 0; i < userNameList.ToArray<BsonValue>().Count(); i++)
            {
                UserStatistics userStatistic = new UserStatistics();
                userStatistic.Name = userNameList.ToArray<BsonValue>()[i].AsString;
                userStatistic.Count = FindTwitterCountByName(start, end, userStatistic.Name, actionState);
                userStattisticsList.Add(userStatistic);
            }
            return userStattisticsList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="actionState"></param>
        /// <returns></returns>
        public List<TwitterEntity> FindAll(DateTime start, DateTime end, ActionState actionState)
        {
            TwitterRepository twitterRepository = new TwitterRepository();
            return twitterRepository.FindAll(start, end, actionState);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="name"></param>
        /// <param name="actionState"></param>
        /// <returns></returns>
        public int FindTwitterCountByName(DateTime start, DateTime end, string name, ActionState actionState)
        {
            TwitterRepository twitterRepository = new TwitterRepository();
            return twitterRepository.FindTwitterCountByName(start, end, name, actionState);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionState"></param>
        /// <returns></returns>
        public List<TwitterEntity> GetTwitter(ActionState actionState)
        {
            List<TwitterEntity> list = new List<TwitterEntity>();
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
                where tweet.Type == StatusType.Home && tweet.Count == Convert.ToInt32(ConfigurationManager.AppSettings[CommonConstants.TwittegGetFieldCount])
                select new TwitterEntity
                {

                    Name = tweet.User.Name,
                    Tweet = tweet.Text,
                    UserID = tweet.User.Identifier.ID,
                    ScreenName = tweet.User.Identifier.ScreenName,
                    CreatedDate = tweet.CreatedAt

                });
                list = friendTweets.ToList<TwitterEntity>();
                actionState.SetSuccess();

            }
            catch (Exception ex)
            {
                actionState.SetFail(Enums.ActionStateEnum.Exception, ex.Message);
            }

            return list;




        }
    }
}