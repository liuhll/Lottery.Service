namespace Lottery.WebApi.Result.Models
{
    public class ResponseMessage : ResponseMessage<object>
    {
        /// <summary>
        /// Creates an <see cref="ResponseMessage"/> object.
        /// <see cref="ResponseMessage.Success"/> is set as true.
        /// </summary>
        public ResponseMessage()
        {

        }

        /// <summary>
        /// Creates an <see cref="ResponseMessage"/> object with <see cref="ResponseMessage.Success"/> specified.
        /// </summary>
        /// <param name="success">Indicates success status of the result</param>
        public ResponseMessage(bool success)
            : base(success)
        {

        }

        /// <summary>
        /// Creates an <see cref="ResponseMessage"/> object with <see cref="ResponseMessage{TResult}.Result"/> specified.
        /// <see cref="ResponseMessage.Success"/> is set as true.
        /// </summary>
        /// <param name="result">The actual result object</param>
        public ResponseMessage(object result)
            : base(result)
        {

        }

        /// <summary>
        /// Creates an <see cref="ResponseMessage"/> object with <see cref="ResponseMessage.Error"/> specified.
        /// <see cref="ResponseMessage.Success"/> is set as false.
        /// </summary>
        /// <param name="error">Error details</param>
        /// <param name="unAuthorizedRequest">Used to indicate that the current user has no privilege to perform this request</param>
        public ResponseMessage(ErrorInfo error, bool unAuthorizedRequest = false)
            : base(error, unAuthorizedRequest)
        {

        }
    }
}