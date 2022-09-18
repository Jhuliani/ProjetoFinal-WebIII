using ProjetoFinal.Core.Models;


namespace ProjetoFinal.Core.Interfaces
{
    public interface ICityEventService
    {
        List<CityEvent> GetEventos();
        List<CityEvent> ConsultarEventosNome(string palavra);
        List<CityEvent> ConsultarEventosLocalData(string local, DateTime data);
        List<CityEvent> ConsultarEventosPrecoData(double preco1, double preco2, DateTime data);
        bool InserirEvento(CityEvent evento);
        bool DeletarEvento(long Idevent);
        bool EditarEvento(long Idevent, CityEvent eventoNovo);
        List<CityEvent> ConsultarEventosId(long IdEvent);

    }
}
