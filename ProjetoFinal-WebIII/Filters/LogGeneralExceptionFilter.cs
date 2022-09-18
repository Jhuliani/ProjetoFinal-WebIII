using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjetoFinal_WebIII.Filters
{
    public class LogGeneralExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var problem = new ProblemDetails
            {
                Status = 500,
                Title = "Erro inesperado",
                Detail = "Ocorreu um erro inesperado na solicitação",
                Type = context.Exception.GetType().Name
            };

            Console.WriteLine($"Tipo da exceção {context.Exception.GetType().Name}, mensagem {context.Exception.Message}, stack trace {context.Exception.StackTrace}");
            switch (context.Exception)
            {
                case ArgumentNullException:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status501NotImplemented;
                    context.Result = new ObjectResult(problem);
                    break;
                default:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Result = new ObjectResult(problem);
                    break;

            }
        }
    }
}
