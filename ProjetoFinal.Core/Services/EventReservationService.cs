using ProjetoFinal.Core.Interfaces;
using ProjetoFinal.Core.Models;

namespace ProjetoFinal.Core.Services
{
    public class EventReservationService : IEventReservationService
    {
        public IEventReservationRepository _eventReservationRepository;
        public EventReservationService(IEventReservationRepository eventReservationRepository)
        {
            _eventReservationRepository = eventReservationRepository;
        }

       
        public List<EventReservation> ConsultarReservaNomeETitulo(string nome, string titulo)
        {
            return _eventReservationRepository.ConsultarReservaNomeETitulo(nome, titulo);
        }

        public bool InserirReserva(EventReservation evento)
        {
            return _eventReservationRepository.InserirReserva(evento);
        }

        public bool EditarReserva(long IdEvent, EventReservation reserva)
        {
            return _eventReservationRepository.EditarReserva(IdEvent, reserva);
        }

        public bool DeletarReserva(long IdEvent)
        {
            return _eventReservationRepository.DeletarReserva(IdEvent);
        }

        public List<CityEvent> ConsultarReservaId(long IdEvent)
        {
            return _eventReservationRepository.ConsultarReservaId(IdEvent);
        }
    }
}
