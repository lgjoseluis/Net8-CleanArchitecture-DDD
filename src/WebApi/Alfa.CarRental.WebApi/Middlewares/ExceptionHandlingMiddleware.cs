using Alfa.CarRental.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Alfa.CarRental.WebApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try 
            { 
                await _next(context);
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, "Error: {Message}", ex.Message);

                ExceptionDetails exceptionDetails = GetExceptionDetails(ex);

                ProblemDetails problemDetails = new ProblemDetails 
                { 
                    Status = exceptionDetails.Status,
                    Type = exceptionDetails.Type,
                    Title = exceptionDetails.Title,
                    Detail = exceptionDetails.Detail                    
                };

                if (exceptionDetails.Errors is not null)
                {
                    problemDetails.Extensions["Errors"] = exceptionDetails.Errors;
                }

                context.Response.StatusCode = exceptionDetails.Status;

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }

        private static ExceptionDetails GetExceptionDetails(Exception ex)
        {
            return ex switch
            {
                ValidationException validationException => new ExceptionDetails(
                        StatusCodes.Status400BadRequest,
                        "ValidationFailure",
                        "Validation errors",
                        "One or more validation errors have occurred",
                        validationException.Errors
                    ),
                _ => new ExceptionDetails(
                        StatusCodes.Status500InternalServerError,
                        "ServerError",
                        "Internal server error",
                        ex.Message, //"Unexpected error on the server",
                        null
                    )
            };
        }

        internal record ExceptionDetails(
            int Status,
            string Type,
            string Title,
            string Detail,
            IEnumerable<object>? Errors
         );
    }
}
