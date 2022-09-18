using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjetoFinal.Core.Interfaces;
using ProjetoFinal.Core.Models;
using ProjetoFinal.Core.Services;
using ProjetoFinal.Infra.Data.Repository;
using ProjetoFinal_WebIII.Filters;

namespace ProjetoFinal_WebIII.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize]
    public class CityEventController : ControllerBase
    {
        
        public ICityEventService _cityEventService;

        public CityEventController(ICityEventService cityEventService)
        {
            
            _cityEventService = cityEventService;
            
        }

        
        [HttpGet("/evento/consultar")]
        public ActionResult<List<CityEvent>> GetEventos()
        {                        
            return Ok(_cityEventService.GetEventos());
        }


        [HttpGet("/evento/{palavra}/consultar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public IActionResult ConsultarEventosNome(string palavra)
        {
            
            var eventos = _cityEventService.ConsultarEventosNome(palavra);
            if (eventos == null)
            {
                return NotFound();
            }
            return Ok(eventos);
        }

        [HttpGet("/evento/{local}/{data}/consultar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public IActionResult ConsultarEventosLocalData(string local, DateTime data)
        {

            var eventos = _cityEventService.ConsultarEventosLocalData(local,data.Date);
            if (eventos == null)
            {
                return NotFound();
            }
            return Ok(eventos);
        }

        [HttpGet("/evento/{preco1}/{preco2}/{data}/consultar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public IActionResult ConsultarEventosPrecoData(double preco1, double preco2, DateTime data)
        {

            var eventos = _cityEventService.ConsultarEventosPrecoData(preco1, preco2, data.Date);
            if (eventos == null)
            {
                return NotFound();
            }
            return Ok(eventos);
        }

        [HttpPost("/evento/cadastrar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "admin")]        
        public ActionResult<CityEvent> InserirEvento(CityEvent evento)
        {
            if (!_cityEventService.InserirEvento(evento))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(InserirEvento), evento);
        }

        [HttpPut("/evento/{IdEvent}/alterar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ServiceFilter(typeof(LogGaranteEventoExisteActionFilter))]
        [Authorize(Roles = "admin")]
        public ActionResult<CityEvent> EditarEvento(long IdEvent, CityEvent eventoNovo)
        {

            if (!_cityEventService.EditarEvento(IdEvent, eventoNovo))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        [HttpDelete("/evento/{IdEvent}/deletar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(LogGaranteEventoExisteActionFilter))]
        [Authorize(Roles = "admin")]
        public IActionResult DeletarEvento(long Idevent)
        {

            if (!_cityEventService.DeletarEvento(Idevent))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

    }
}
