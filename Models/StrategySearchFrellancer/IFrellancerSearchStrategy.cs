namespace MicroserviceNogeco.Models.StrategySearchFrellancer
{
    //Definição do contrato para as estratégias de busca
    public interface IFrellancerSearchStrategy
    {
        Task<List<Frellancer>> Search(Notification notification);
    }
}
