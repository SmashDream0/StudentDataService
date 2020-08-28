using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Contracts.Attributes
{
    public class LogRequestAttribute : ActionFilterAttribute
    {
        public LogRequestAttribute(ILoggerFactory loggerFactory) : base()
        { _logger = loggerFactory.CreateLogger(nameof(LogRequestAttribute)); }

        private readonly ILogger _logger;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogDebug("Income request: " + context.HttpContext.Request.Path.Value);

            base.OnActionExecuting(context);
        }
    }
}
