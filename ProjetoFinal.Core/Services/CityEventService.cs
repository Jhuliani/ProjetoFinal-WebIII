using ProjetoFinal.Core.Interfaces;
using ProjetoFinal.Core.Models;


namespace ProjetoFinal.Core.Services
{
    public class CityEventService : ICityEventService
    {
        public ICityEventRepository _cityeventyRepository;
        public CityEventService(ICityEventRepository cityeventyRepository)
        {
            _cityeventyRepository = cityeventyRepository;
        }

        public List<CityEvent> GetEventos()
        {
            return _cityeventyRepository.GetEventos();
        }
        public List<CityEvent> ConsultarEventosNome(string palavra)
        {
            return _cityeventyRepository.ConsultarEventosNome(palavra);
        }
        public List<CityEvent> ConsultarEventosLocalData(string local, DateTime data)
        {
            return _cityeventyRepository.ConsultarEventosLocalData(local, data);
        }
        public List<CityEvent> ConsultarEventosPrecoData(double preco1, double preco2, DateTime data)
        {
            return _cityeventyRepository.ConsultarEventosPrecoData(preco1, preco2, data);
        }
        public bool InserirEvento(CityEvent evento)
        {
            return _cityeventyRepository.InserirEvento(evento);
        }
        public bool DeletarEvento(long Idevent)
        {
            return _cityeventyRepository.DeletarEvento(Idevent);
        }
        public bool EditarEvento(long Idevent, CityEvent eventoNovo)
        {
            return _cityeventyRepository.EditarEvento(Idevent, eventoNovo);
        }

        public List<CityEvent> ConsultarEventosId(long IdEvent)
        {
            return _cityeventyRepository.ConsultarEventosId(IdEvent);
        }

    }
}
