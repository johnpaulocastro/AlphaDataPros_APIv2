using ADPv2.Logger.Interface;
using ADPv2.Logger.Model;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ADPv2.Logger.Filters
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class RequestResponseLoggerActionFilter : Attribute, IActionFilter
    {
        private RequestResponseLogModel GetLogModel(HttpContext context)
        {
            return context.RequestServices.GetService<IRequestResponseLogModelCreator>().LogModel;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var model = GetLogModel(context.HttpContext);
            model.ResponseDateTimeUtcActionLevel = DateTime.UtcNow; 
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var model = GetLogModel(context.HttpContext);
            model.RequestDateTimeUtcActionLevel = DateTime.UtcNow;
        }
    }
}
