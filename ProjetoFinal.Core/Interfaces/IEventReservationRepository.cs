using ProjetoFinal.Core.Models;


namespace ProjetoFinal.Core.Interfaces
{
    public interface IEventReservationRepository
    {
        List<EventReservation> ConsultarReservaNomeETitulo(string nome, string titulo);
        bool InserirReserva(EventReservation evento);
        bool EditarReserva(long IdEvent, EventReservation reserva);
        bool DeletarReserva(long IdEvent);
        List<CityEvent> ConsultarReservaId(long IdEvent);
    }
}
