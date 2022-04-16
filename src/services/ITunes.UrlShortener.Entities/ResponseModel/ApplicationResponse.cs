using System.Collections.Generic;

namespace ITunes.UrlShortener.Entities.ResponseModel
{
    public class ApplicationResponse<T> : BaseApplicationResponse
    {
        public ApplicationResponse()
        {
            State = ResponseState.Success;
        }

        public ApplicationResponse(T result)
        {
            State = ResponseState.Success;
            Data = result;
        }

        public ApplicationResponse(ResponseState responseState, string message)
        {
            State = responseState;
            Message = message;
        }

        public ApplicationResponse(ResponseState responseState, List<Error> errors)
        {
            State = responseState;
            Errors = errors;
        }

        public ApplicationResponse(ResponseState responseState, T result, string message)
        {
            State = responseState;
            Data = result;
            Message = message;
        }

        public T Data { get; set; }
        public List<Error> Errors { get; set; }
    }


    public class BaseApplicationResponse
    {
        public string Message { get; set; }

        public ResponseState State { get; set; }
    }

    public class Error
    {
        public Error(string message)
        {
            Message = message;
        }
        public string Message { get; set; }
    }


    public enum ResponseState
    {
        None = 0,
        Error = 1,
        Success = 2,
        PageError = 3
    }
}