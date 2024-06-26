using System.Net;
using DAL.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Controllers.ExceptionFilters;

public class NoSuchApplicationExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is NoSuchApplicationException)
            context.Result = new ContentResult
            {
                Content = context.Exception.Message,
                StatusCode = (int?)HttpStatusCode.NotFound,
            };
    }
}