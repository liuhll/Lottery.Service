using System;

namespace Lottery.WebApi.Result.Models
{
    /// <summary>
    /// This class is used to create standard responses for AJAX requests.
    /// </summary>
    [Serializable]
    public class ResponseMessage<TResult> : ResponseMessageBase
    {
        /// <summary>
        /// The actual result object of AJAX request.
        /// It is set if <see cref="ResponseMessage.Success"/> is true.
        /// </summary>
        public TResult Result { get; set; }

        /// <summary>
        /// Creates an <see cref="ResponseMessage"/> object with <see cref="Result"/> specified.
        /// <see cref="ResponseMessage.Success"/> is set as true.
        /// </summary>
        /// <param name="result">The actual result object of AJAX request</param>
        public ResponseMessage(TResult result)
        {
            Result = result;
            Success = true;
        }

        /// <summary>
        /// Creates an <see cref="ResponseMessage"/> object.
        /// <see cref="ResponseMessage.Success"/> is set as true.
        /// </summary>
        public ResponseMessage()
        {
            Success = true;
        }

        /// <summary>
        /// Creates an <see cref="ResponseMessage"/> object with <see cref="ResponseMessage.Success"/> specified.
        /// </summary>
        /// <param name="success">Indicates success status of the result</param>
        public ResponseMessage(bool success)
        {
            Success = success;
        }

        /// <summary>
        /// Creates an <see cref="ResponseMessage"/> object with <see cref="ResponseMessage.Error"/> specified.
        /// <see cref="ResponseMessage.Success"/> is set as false.
        /// </summary>
        /// <param name="error">Error details</param>
        /// <param name="unAuthorizedRequest">Used to indicate that the current user has no privilege to perform this request</param>
        public ResponseMessage(ErrorInfo error, bool unAuthorizedRequest = false)
        {
            Error = error;
            UnAuthorizedRequest = unAuthorizedRequest;
            Success = false;
        }
    }
}