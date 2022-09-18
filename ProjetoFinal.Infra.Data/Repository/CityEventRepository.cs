using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProjetoFinal.Core.Interfaces;
using ProjetoFinal.Core.Models;


namespace ProjetoFinal.Infra.Data.Repository
{
    public class CityEventRepository : ICityEventRepository
    {
        private readonly IConfiguration _configuration;

        public CityEventRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<CityEvent> GetEventos()
        {
            var query = "SELECT * FROM CityEvent";
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Query<CityEvent>(query).ToList();
        }

        public List<CityEvent> ConsultarEventosNome(string palavra)
        {
            var query = "SELECT * FROM CityEvent WHERE Title LIKE ('%'+ @palavra + '%');";
            var parameters = new DynamicParameters(new { palavra });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Query<CityEvent>(query, parameters).ToList();
        }

        public List<CityEvent> ConsultarEventosLocalData(string local, DateTime data)
        {
            var query = "SELECT * FROM CityEvent WHERE Local LIKE @Local AND CAST(DateHourEvent as DATE) LIKE @Date;";
            var parameters = new DynamicParameters();
            parameters.Add("Local", local);
            parameters.Add("Date", data.ToString("yyyy-MM-dd"));
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Query<CityEvent>(query, parameters).ToList();
        }

        public List<CityEvent> ConsultarEventosPrecoData(double preco1, double preco2, DateTime data)
        {
            var query = "SELECT * FROM CityEvent WHERE CAST(DateHourEvent as DATE) LIKE @Date AND Price BETWEEN @Preco1 AND @Preco2 ;";
            var parameters = new DynamicParameters();
            parameters.Add("Preco1", preco1);
            parameters.Add("Preco2", preco2);
            parameters.Add("Date", data.ToString("yyyy-MM-dd"));
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Query<CityEvent>(query, parameters).ToList();
        }

        public bool InserirEvento(CityEvent evento)
        {
            var query = $"INSERT INTO CityEvent VALUES ( @Title, @Description, @DateHourEvent, @Local, @Address, @Price, @Status)";
            var parameters = new DynamicParameters();
            
            parameters.Add("Title", evento.Title);
            parameters.Add("Description", evento.Description);
            parameters.Add("DateHourEvent", evento.DateHourEvent);
            parameters.Add("Local", evento.Local);
            parameters.Add("Address", evento.Address);
            parameters.Add("Price", evento.Price);
            parameters.Add("Status", evento.Status);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Execute(query, parameters)==1;
        }

        public bool DeletarEvento(long IdEvent)
        {
            var query = "DELETE FROM CityEvent WHERE IdEvent = @IdEvent";
            var parameters = new DynamicParameters(new { IdEvent });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Execute(query, parameters) == 1;
        }

        public bool EditarEvento(long IdEvent, CityEvent eventoNovo)
        {
            var query = @"UPDATE CityEvent SET Title = @Title, Description = @Description, DateHourEvent = @DateHourEvent, Local = @Local, Address = @Address, Price = @Price
            WHERE IdEvent = @IdEvent";
            var parameters = new DynamicParameters();
            parameters.Add("Title", eventoNovo.Title);
            parameters.Add("Description", eventoNovo.Description);
            parameters.Add("DateHourEvent", eventoNovo.DateHourEvent);
            parameters.Add("Local", eventoNovo.Local);
            parameters.Add("Address", eventoNovo.Address);
            parameters.Add("Price", eventoNovo.Price);
            parameters.Add("Status", eventoNovo.Status);
            parameters.Add("IdEvent", eventoNovo.IdEvent);
                        
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public List<CityEvent> ConsultarEventosId(long IdEvent)
        {
            var query = "SELECT * FROM CityEvent WHERE idEvent = @idEvent";
            var parameters = new DynamicParameters(new { IdEvent });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Query<CityEvent>(query, parameters).ToList();
        }

       


    }
}
