using ADPv2.Logger.Interface;
using ADPv2.Logger.Model;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ADPv2.Logger.Filters
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class RequestResponseLoggerErrorFilter : Attribute, IExceptionFilter
    {
        private RequestResponseLogModel GetLogModel(HttpContext context)
        {
            return context.RequestServices.GetService<IRequestResponseLogModelCreator>().LogModel;
        }

        public void OnException(ExceptionContext context)
        {
            var model = GetLogModel(context.HttpContext);
            model.IsExceptionActionLevel = true;
            if(model.ResponseDateTimeUtcActionLevel == null)
                model.ResponseDateTimeUtcActionLevel = DateTime.UtcNow;
        }
    }
}
