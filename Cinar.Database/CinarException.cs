using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Cinar.Database
{
    public class CinarException : Exception
    {
        public CinarExceptionType Type { get; set; }
        public string Message { get; set; }

        public CinarException(string Message)
            : base(Message, new Exception(Message))
        {
            Type = CinarExceptionType.General;
            this.Message = Message;
        }

        public CinarException(Exception ex) : base(ex.Message, ex)
        {
            if(ex is MySqlException)
            {

                this.Type = CinarExceptionType.General;
                this.Message = ex.Message;

                MySqlException myex = ex as MySqlException;
                if(myex.Number==1062)
                {
                    this.Type = CinarExceptionType.Duplicate;
                    this.Message = ex.Message.Split('\'')[1];
                }
                if (myex.Number == 1406)
                {
                    this.Type = CinarExceptionType.TooLong;
                    this.Message = ex.Message.Split('\'')[1];
                }
                

            }
        }
    }

    public enum CinarExceptionType
    {
        General,
        Duplicate,
        NotNull,
        ForeignKey,
        Check,
        TooLong
    }
}
