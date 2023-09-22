using Service.api.Movie.Entities;

namespace Service.api.Movie.HandleErrors
{
    public class Request
    {
        public ERequest<T> Response<T>(int statusCode, T response, string currentException = "")
        {
            ERequest<T> request = AssembleResponseRequest(statusCode, response, currentException);
            return request;
        }


        protected ERequest<T> AssembleResponseRequest<T>(int statusCode, T response, string currentException)
        {
            return new ERequest<T>
            {
                Response = response,
                StatusCode = statusCode,
                CurrentException = currentException
            };
        }
    }
}
