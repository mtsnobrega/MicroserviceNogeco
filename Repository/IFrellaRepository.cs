using MicroserviceNogeco.Models;

namespace MicroserviceNogeco.Repository
{
    public interface IFrellaRepository
    {
     
        //Interfaace definindo os contratos que devem ser implementados
        Task<List<Frellancer>> SearchByFilter(List<string> skills, List<string> diasSemana);

        Task<List<Frellancer>> SearchByFilter_Emergency(bool isEmergency);
        
    }
}
