using MicroserviceNogeco.Repository;

namespace MicroserviceNogeco.Models.StrategySearchFrellancer
{
    //Imprementação da busca normal de frellancers, para enviar a notificação com base nas skills e dia da seamana.
    public class NormalSearchStrategy : IFrellancerSearchStrategy
    {
        private readonly IFrellaRepository _repository;

        public NormalSearchStrategy(IFrellaRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<Frellancer>> Search(Notification notification)
        {
            return await _repository.SearchByFilter(notification.Skills, notification.DiasDaSemana);
        }
    }
}
