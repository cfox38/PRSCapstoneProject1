using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrsWebAppProject.Utility
{
    public class JsonMessage
    {
        public string Result { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public JsonMessage(string Result, string Message, object Data = null)
        {
            this.Result = Result;
            this.Message = Message;
            this.Data = Data;
        }
    }

}