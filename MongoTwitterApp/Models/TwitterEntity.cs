using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoTwitterApp.Models
{
    public class TwitterEntity
    {

        string screenName;
        string tweet;
        string userID;
        string name;
        DateTime createdDate;
        string iD;
        
        public string ID
        {
            get { return iD; }
            set { iD = value; }
        }

        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public string Tweet
        {
            get { return tweet; }
            set { tweet = value; }
        }

        public string ScreenName
        {
            get { return screenName; }
            set { screenName = value; }
        }


    }
}