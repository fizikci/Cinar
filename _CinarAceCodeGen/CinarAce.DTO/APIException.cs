using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Membership.DTO
{
    [Serializable]
    public class APIException : Exception
    {
        public ErrorTypes ErrorType { get; set; }
        public ErrorCodes ErrorCode { get; set; }
        public List<string> ExtraMessages { get; set; }

        public APIException(string message)
            : base(message)
        {

        }

        public APIException(string message, ErrorTypes errorType)
            : base(message)
        {
            this.ErrorType = errorType;
        }
        public APIException(string message, ErrorCodes errorCode)
            : base(message)
        {
            this.ErrorCode = errorCode;
        }
        public APIException(string message, ErrorTypes errorType, ErrorCodes errorCode)
            : base(message)
        {
            this.ErrorType = errorType;
            this.ErrorCode = errorCode;
        }
        public APIException(string message, ErrorTypes errorType, ErrorCodes errorCode, List<string> extraMessages)
            : this(message, errorType, errorCode)
        {
            this.ExtraMessages = extraMessages;
        }

        public APIException(string message, ErrorTypes errorType, ErrorCodes errorCode, Exception inner)
            : base(message, inner)
        {
            this.ErrorType = errorType;
            this.ErrorCode = errorCode;
        }

        protected APIException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    public enum ErrorTypes
    {
        SystemError,
        ValidationError
    }

    public enum ErrorCodes
    {
        None,
        UnauthorizedAccess = 100,
        ValidationError = 101,
    }
}
