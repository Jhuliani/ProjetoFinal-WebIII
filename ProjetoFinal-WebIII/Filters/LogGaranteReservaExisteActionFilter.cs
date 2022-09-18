using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetoFinal.Core.Interfaces;

namespace ProjetoFinal_WebIII.Filters
{
    public class LogGaranteReservaExisteActionFilter : ActionFilterAttribute
    {
        public IEventReservationService _eventReservationService;

        public LogGaranteReservaExisteActionFilter(IEventReservationService eventReservationService)
        {
            _eventReservationService = eventReservationService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            long idProduto = (long)context.ActionArguments["IdEvent"];

            if (_eventReservationService.ConsultarReservaId(idProduto) == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }
    }
}
