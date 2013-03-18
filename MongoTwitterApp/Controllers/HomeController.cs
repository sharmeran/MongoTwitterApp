using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoTwitterApp.Constants;
using MongoTwitterApp.Models;

namespace MongoTwitterApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetTweets()
        {
            ActionState actionState = new ActionState();
            TwitterController twitterController = new TwitterController();
            List<TwitterEntity> twitterEntity = new List<TwitterEntity>();
            twitterEntity = twitterController.GetTweets(actionState);
            ViewData[CommonConstants.ActionState] = actionState;
            return View(twitterEntity);
        }
        
        public ActionResult AddTweets(FormCollection  test)
        {

            
                ActionState actionState = new ActionState();
                TwitterController twitterController = new TwitterController();
                List<TwitterEntity> twitterEntity = new List<TwitterEntity>();
                twitterEntity = twitterController.GetTweets(actionState);
                twitterController.AddTweets(twitterEntity, actionState);
                ViewData[CommonConstants.ActionState] = actionState;
                return View();
            
        }

        public ActionResult GetAllStatistics()
        {
            ActionState actionState = new ActionState();
            TwitterController twitterController = new TwitterController();
            List<UserStatistics> userStat = new List<UserStatistics>();
            userStat = twitterController.AllStatistics(actionState);
            ViewData[CommonConstants.ActionState] = actionState;
            return View(userStat);
        }

        public ActionResult GetLastMonthStatistics()
        {
            ActionState actionState = new ActionState();
            TwitterController twitterController = new TwitterController();
            List<UserStatistics> userStat = new List<UserStatistics>();
            userStat = twitterController.GetLastMonthStatistics(actionState);
            ViewData[CommonConstants.ActionState] = actionState;
            return View(userStat);
        }
    }
}
