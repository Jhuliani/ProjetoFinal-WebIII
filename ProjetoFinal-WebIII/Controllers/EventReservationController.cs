using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.Core.Interfaces;
using ProjetoFinal.Core.Models;
using ProjetoFinal_WebIII.Filters;




namespace ProjetoFinal_WebIII.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize]
    public class EventReservationController : ControllerBase
    {
        public IEventReservationService _eventReservationService;

        public EventReservationController(IEventReservationService eventReservationService)
        {

            _eventReservationService = eventReservationService;

        }

        [HttpPost("/reserva/cadastrar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]        
        public ActionResult<EventReservation> InserirEvento(EventReservation reserva)
        {
            if (!_eventReservationService.InserirReserva(reserva))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(InserirEvento), reserva);
        }

        [HttpPut("/reserva/{IdEvent}/alterar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ServiceFilter(typeof(LogGaranteReservaExisteActionFilter))]
        [Authorize(Roles = "admin")]
        public ActionResult<CityEvent> EditarReserva(long IdEvent, EventReservation eventoNovo)
        {

            if (!_eventReservationService.EditarReserva(IdEvent, eventoNovo))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        [HttpDelete("/reserva/{IdEvent}/deletar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ServiceFilter(typeof(LogGaranteReservaExisteActionFilter))]
        [Authorize(Roles = "admin")]
        public IActionResult DeletarReserva(long Idevent)
        {

            if (!_eventReservationService.DeletarReserva(Idevent))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        [HttpGet("/reserva/{nome}/{titulo}/consultar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public IActionResult ConsultarReservaNomeETitulo(string nome, string titulo)
        {

            var eventos = _eventReservationService.ConsultarReservaNomeETitulo(nome, titulo);
            if (eventos == null)
            {
                return NotFound();
            }
            return Ok(eventos);
        }

    }
}
