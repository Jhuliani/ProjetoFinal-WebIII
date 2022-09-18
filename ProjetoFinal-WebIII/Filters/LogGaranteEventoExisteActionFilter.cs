using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetoFinal.Core.Interfaces;

namespace ProjetoFinal_WebIII.Filters
{
    public class LogGaranteEventoExisteActionFilter : ActionFilterAttribute
    {
        public ICityEventService _cityEventService;

        public LogGaranteEventoExisteActionFilter(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            long idProduto = (long)context.ActionArguments["IdEvent"];

            if (_cityEventService.ConsultarEventosId(idProduto) == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }
    }
}
