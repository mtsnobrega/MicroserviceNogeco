using MicroserviceNogeco.Repository;

namespace MicroserviceNogeco.Models.StrategySearchFrellancer
{
    public class EmergencySearchStrategy : IFrellancerSearchStrategy
    {
        //Injeção de dependencia para que a classe EmergencySearchStrategy possa utilizar os métodos definidos na classe  FrellaRepositóry
        private readonly IFrellaRepository _repository;

        //A dependenca se baseia em um objeto que precisa interagir com o banco de dados por exemplo 
        public EmergencySearchStrategy(IFrellaRepository repository)
        {
            _repository = repository;
        }

        //Task representa uma operação assincrona,roda em segundo plano
        //Usada principalmente em operações com recursos externos(banco de dados) 
        public async Task<List<Frellancer>> Search(Notification notification)
        {
            return await _repository.SearchByFilter_Emergency(notification.IsEmergency);
        }
    }
}
