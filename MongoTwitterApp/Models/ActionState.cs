using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoTwitterApp.Enums;

namespace MongoTwitterApp.Models
{
    public class ActionState
    {
        ActionStateEnum status;
        string result;


        public string Result
        {
            get { return result; }
            set { result = value; }
        }

        public ActionStateEnum Status
        {
            get { return status; }
            set { status = value; }
        }

        public void SetSuccess()
        {
            status = ActionStateEnum.NoError;
            result = string.Empty;
        }

        public void SetFail(ActionStateEnum errCode, string errorMessage)
        {
            this.status = errCode;
            result = errorMessage;
        }
        public override string ToString()
        {
            return string.Format("  Status = '{1}' ErrMsg = '{2}'", status.ToString(), string.Empty);// result);
        }
    }
}