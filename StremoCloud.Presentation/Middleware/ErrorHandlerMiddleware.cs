using StremoCloud.Application.ExceptionHandler;
using StremoCloud.Application.Wrappers;
using StremoCloud.Shared.Utils;
using System.Net;

namespace StremoCloud.Presentation.Middleware;

public class ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception error)
          {
            Dictionary<string, string[]> errors = new Dictionary<string, string[]>();
            errors.Add("Error", new string[] { error.Message });

            if (error.InnerException != null)
            {
                logger.LogError(error.InnerException.Message);
                errors.Add("Error", new string[] { error.InnerException.Message });
            }

            var response = context.Response;

            response.ContentType = "application/json";

            var responseModel = new Response<string>() { Succeeded = false, Message = "An error occurred!", Errors = errors };

            switch (error)
            {
                case ApiException e:
                    // custom application error
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel = new Response<string>() { Succeeded = false, Message = error.Message, Errors = errors };
                    logger.LogError(error.Message);
                    break;
                case ValidationException e:
                    // custom application error
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel.Errors = e.Errors;
                    logger.LogError(error.Message);
                    break;
                case KeyNotFoundException e:
                    // not found error
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    logger.LogError(error.Message);
                    break;
                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    logger.LogError(error.Message);
                    break;
            }
            var result = System.Text.Json.JsonSerializer.Serialize(responseModel);

            await response.WriteAsync(result);
        }
    }
}
