using JwtOnExample.Entities;
using JwtOnExample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JwtOnExample.ActionFilters
{
    public class ValidationFilterAttribute : IActionFilter
    {


        public void OnActionExecuting(ActionExecutingContext context)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is IEntity);
            if (param.Value == null)
            {
                context.Result = new BadRequestObjectResult("Object is null");
                return;
            }

            if (!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }

           
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var result = context.Result;

            if (result is ObjectResult)
            {
                var objectResult = result as ObjectResult;
                var value = objectResult.Value;

                if (value is Movie)
                {
                    var movie = value as Movie;

                    // Check if the entity has the "Name" property
                    if (movie.GetType().GetProperty("Name") != null)
                    {
                        // Remove the "Name" property dynamically
                        movie.GetType().GetProperty("Name").SetValue(movie, null);
                    }
                }
            }

        }


    }
}
