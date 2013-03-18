using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoTwitterApp.Constants;
using MongoTwitterApp.Enums;
using MongoTwitterApp.Models;

namespace MongoTwitterApp.DataAccess
{
    public class TwitterRepository
    {
        public void Insert(TwitterEntity entity, ActionState actionState)
        {
            MongoServer server = null;
            MongoDatabase database = null;
            try
            {
                server = MongoServer.Create(ConfigurationManager.AppSettings[CommonConstants.ConnictionString]);
                database = server.GetDatabase(CommonConstants.DatabaseName);
                var dataCollection = database.CollectionExists(TwitterConstants.TweetsCollection);
                if (Convert.ToBoolean(dataCollection) == false)
                {
                    database.CreateCollection(TwitterConstants.TweetsCollection);
                    MongoCollection<BsonDocument> tweet = database.GetCollection<BsonDocument>(TwitterConstants.TweetsCollection);
                    BsonDocument tweetEntity = new BsonDocument {
                { TwitterConstants.Name, entity.Name },
                { TwitterConstants.ScreenName, entity.ScreenName },
                { TwitterConstants.Tweets, entity.Tweet },
                { TwitterConstants.UserID, entity.UserID },
                { TwitterConstants.CreatedDate, entity.CreatedDate }
                };
                    tweet.Insert(tweetEntity);
                    actionState.SetSuccess();
                }
                else
                {
                    MongoCollection<BsonDocument> tweet = database.GetCollection<BsonDocument>(TwitterConstants.TweetsCollection);
                    BsonDocument tweetEntity = new BsonDocument {
                { TwitterConstants.Name, entity.Name },
                { TwitterConstants.ScreenName, entity.ScreenName },
                { TwitterConstants.Tweets, entity.Tweet },
                { TwitterConstants.UserID, entity.UserID },
                { TwitterConstants.CreatedDate, entity.CreatedDate }
                };
                    tweet.Insert(tweetEntity);
                    actionState.SetSuccess();

                }
            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStateEnum.Exception, ex.Message);
            }
            finally
            {
                server.Disconnect();
                server = null;
                database = null;

            }
        
        }

        public void DeleteAll( ActionState actionState)
        {
            MongoServer server = null;
            MongoDatabase database = null;
            try
            {
                server = MongoServer.Create(ConfigurationManager.AppSettings[CommonConstants.ConnictionString]);
                database = server.GetDatabase(CommonConstants.DatabaseName);
                MongoCollection<BsonDocument> twiter = database.GetCollection<BsonDocument>(TwitterConstants.TweetsCollection);
                twiter.RemoveAll();
                actionState.SetSuccess();
            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStateEnum.Exception, ex.Message);
            }
            finally
            {
                server.Disconnect();
                server = null;
                database = null;
            }
        }

        public List<TwitterEntity> FindAll(ActionState actionState)
        {
            List<TwitterEntity> twitterEntityList;
            MongoServer server = null;
            MongoDatabase database = null;
            TwitterEntity entity;


            twitterEntityList = new List<TwitterEntity>();
            entity = null;
            try
            {
                server = MongoServer.Create(ConfigurationManager.AppSettings[CommonConstants.ConnictionString]);
                database = server.GetDatabase(CommonConstants.DatabaseName);
                MongoCollection<BsonDocument> tweet = database.GetCollection<BsonDocument>(TwitterConstants.TweetsCollection);
                foreach (BsonDocument tweetEntity in tweet.FindAll())
                {
                    entity = new TwitterEntity();
                    entity.ID = tweetEntity[TwitterConstants.ID].ToString();
                    entity.Name = tweetEntity[TwitterConstants.Name].AsString;
                    entity.ScreenName = tweetEntity[TwitterConstants.ScreenName].AsString;
                    entity.CreatedDate = tweetEntity[TwitterConstants.CreatedDate].AsDateTime;
                    entity.Tweet = tweetEntity[TwitterConstants.Tweets].AsString;
                    entity.UserID = tweetEntity[TwitterConstants.UserID].AsString;
                    twitterEntityList.Add(entity);
                }
                actionState.SetSuccess();
            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStateEnum.Exception, ex.Message);
            }
            finally
            {
                server.Disconnect();
                server = null;
                database = null;
            }
            return twitterEntityList;
        }

        public List<TwitterEntity> FindAll(DateTime start,DateTime end, ActionState actionState)
        {
            List<TwitterEntity> twitterEntityList;
            MongoServer server = null;
            MongoDatabase database = null;
            TwitterEntity entity;


            twitterEntityList = new List<TwitterEntity>();
            entity = null;
            try
            {
                server = MongoServer.Create(ConfigurationManager.AppSettings[CommonConstants.ConnictionString]);
                database = server.GetDatabase(CommonConstants.DatabaseName);
                var query = Query.And(Query.GTE(TwitterConstants.CreatedDate, start), Query.LTE(TwitterConstants.CreatedDate, end));
                MongoCollection<BsonDocument> tweet = database.GetCollection<BsonDocument>(TwitterConstants.TweetsCollection);
                foreach (BsonDocument tweetEntity in tweet.Find(query))
                {
                    entity = new TwitterEntity();
                    entity.ID = tweetEntity[TwitterConstants.ID].ToString();
                    entity.Name = tweetEntity[TwitterConstants.Name].AsString;
                    entity.ScreenName = tweetEntity[TwitterConstants.ScreenName].AsString;
                    entity.CreatedDate = tweetEntity[TwitterConstants.CreatedDate].AsDateTime;
                    entity.Tweet = tweetEntity[TwitterConstants.Tweets].AsString;
                    entity.UserID = tweetEntity[TwitterConstants.UserID].AsString;
                    twitterEntityList.Add(entity);
                }
                actionState.SetSuccess();
            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStateEnum.Exception, ex.Message);
            }
            finally
            {
                server.Disconnect();
                server = null;
                database = null;
            }
            return twitterEntityList;
        }

        public IEnumerable<BsonValue> FindAUsersID(ActionState actionState)
        {
             IEnumerable<BsonValue> tweetList = null;
            MongoServer server = null;
            MongoDatabase database = null;      

            try
            {
                server = MongoServer.Create(ConfigurationManager.AppSettings[CommonConstants.ConnictionString]);
                database = server.GetDatabase(CommonConstants.DatabaseName);
                IEnumerable<BsonValue> tweet = database.GetCollection<BsonDocument>(TwitterConstants.TweetsCollection).Distinct(TwitterConstants.Name);
                tweetList = tweet.ToList();

            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStateEnum.Exception, ex.Message);
            }
            finally
            {
                server.Disconnect();
                server = null;
                database = null;
            }

            return tweetList;
        }

       

        public int FindTwitterCountByName(string name, ActionState actionState)
        {
            int count = 0;
            MongoServer server = null;
            MongoDatabase database = null;
            try
            {
                server = MongoServer.Create(ConfigurationManager.AppSettings[CommonConstants.ConnictionString]);
                database = server.GetDatabase(CommonConstants.DatabaseName);
                MongoCollection<BsonDocument> tweet = database.GetCollection<BsonDocument>(TwitterConstants.TweetsCollection);
                var cursor = tweet.Find(Query.EQ(TwitterConstants.Name , name));
                count =Convert.ToInt32( cursor.Count());
            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStateEnum.Exception, ex.Message);
            }
            finally
            {
                server.Disconnect();
                server = null;
                database = null;
            }
            return count;
        }


        public int FindTwitterCountByName(DateTime start, DateTime end, string name, ActionState actionState)
        {
            int count = 0;
            MongoServer server = null;
            MongoDatabase database = null;
            try
            {
                server = MongoServer.Create(ConfigurationManager.AppSettings[CommonConstants.ConnictionString]);
                database = server.GetDatabase(CommonConstants.DatabaseName);
                MongoCollection<BsonDocument> tweet = database.GetCollection<BsonDocument>(TwitterConstants.TweetsCollection);
                var query = Query.And(Query.EQ(TwitterConstants.Name, name),Query.GTE(TwitterConstants.CreatedDate, start), Query.LTE(TwitterConstants.CreatedDate, end));
                var cursor = tweet.Find(query);
                count = Convert.ToInt32(cursor.Count());
            }
            catch (Exception ex)
            {
                actionState.SetFail(ActionStateEnum.Exception, ex.Message);
            }
            finally
            {
                server.Disconnect();
                server = null;
                database = null;
            }
            return count;
        }
        

    }
}