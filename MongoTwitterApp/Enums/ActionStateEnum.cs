using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoTwitterApp.Enums
{
    public enum ActionStateEnum
    {
        CannotInsert = 0,
        CannotDelete = 1,
        CannotUpdate = 2,
        CannotFind = 3,
        Exception = 4,
        NoError = 5,
        AlreadyExist = 6
    }
}