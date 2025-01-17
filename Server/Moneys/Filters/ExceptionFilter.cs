using System;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Moneys.Filters;

public class ExceptionFilter(ILogger<ExceptionFilter> logger) : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        BadRequestObjectResult result;

        if (context.Exception is ValidationException validationException)
        {
            var errors = validationException.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

            result = new BadRequestObjectResult(new
            {
                StatusCode = 400,
                Errors = errors
            });
        }
        else
        {
            logger.LogError(context.Exception, context.Exception.Message);

            result = new BadRequestObjectResult(new
            {
                StatusCode = 400,
                Message = "Something went wrong"
            });
        }

        context.Result = result;
        context.ExceptionHandled = true;
    }
}
