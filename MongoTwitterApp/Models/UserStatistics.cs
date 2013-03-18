using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoTwitterApp.Models
{
    public class UserStatistics
    {
        string name;
        int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}