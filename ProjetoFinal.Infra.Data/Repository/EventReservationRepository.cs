using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProjetoFinal.Core.Interfaces;
using ProjetoFinal.Core.Models;

namespace ProjetoFinal.Infra.Data.Repository
{
    public class EventReservationRepository : IEventReservationRepository
    {
        private readonly IConfiguration _configuration;

        public EventReservationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public List<EventReservation> ConsultarReservaNomeETitulo(string nome, string titulo)
        {
            var query = @"SELECT * FROM EventReservation 
                           INNER JOIN CityEvent ON
                           EventReservation.PersonName = @nome AND CityEvent.Title LIKE ('%'+ @titulo + '%')
                           AND EventReservation.IdEvent = CityEvent.IdEvent ";
            var parameters = new DynamicParameters();
            parameters.Add("Local", nome);
            parameters.Add("Date", titulo);
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Query<EventReservation>(query, parameters).ToList();
        }

        public bool InserirReserva(EventReservation evento)
        {
            var query = "INSERT INTO EventReservation VALUES ( @IdEvent, @PersonName, @Quantity)";
            var parameters = new DynamicParameters();

            parameters.Add("IdEvent", evento.IdEvent);
            parameters.Add("PersonName", evento.PersonName);
            parameters.Add("Quantity", evento.Quantity);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Execute(query, parameters) == 1;
        }

        public bool EditarReserva(long IdEvent, EventReservation reserva)
        {
            var query = "UPDATE EventReservation SET Title = @Quantity WHERE IdEvent = @IdEvent";
            var parameters = new DynamicParameters();
            parameters.Add("Quantity", reserva.Quantity);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool DeletarReserva(long IdEvent)
        {
            var query = "DELETE FROM EventReservation WHERE IdEvent = @IdEvent";
            var parameters = new DynamicParameters(new { IdEvent });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Execute(query, parameters) == 1;
        }

        public List<CityEvent> ConsultarReservaId(long IdEvent)
        {
            var query = "SELECT * FROM EventReservation WHERE idEvent = @idEvent";
            var parameters = new DynamicParameters(new { IdEvent });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Query<CityEvent>(query, parameters).ToList();
        }

        public bool AtivarEvento(long IdEvent)
        {
            var query = "UPDATE EventReservation SET Status = @Status";
            var parameters = new DynamicParameters();
            parameters.Add("Status", 1);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

    }
}
