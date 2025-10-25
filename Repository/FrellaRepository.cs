using MicroserviceNogeco.Models;

namespace MicroserviceNogeco.Repository
{
    //Implementação concreta da busca, baseado em skills\dias da semana e por emergencia
    public class FrellaRepository : IFrellaRepository
    {
        //Sintaxe LINQ(Language Integrated Query)
        //Forma padrão de fazer consultas a coleções de dados (como listas, arrays ou resultados de banco de dados) em C#
        public Task<List<Frellancer>> SearchByFilter(List<string> skills, List<string> diasSemana)
        {
            
            var resultado = _usuarios //Coleção de dados da lista

                //O metodo where filtra os elementos da coleção com base em uma condição especificada e transforma a coleção original em uma nova lista
                .Where(usuario => skills.Any(skill => usuario.Skills.Contains(skill)) && diasSemana.Any(dia => usuario.DiasPreferidos.Contains(dia)))
                .ToList();

            return Task.FromResult(resultado);
        }

        public Task<List<Frellancer>> SearchByFilter_Emergency(bool isEmergency)
        {
            var result = _usuarios
                .Where(usuario => usuario.IsEmergecy == isEmergency)
                .ToList();

            return Task.FromResult(result);
        }














        private readonly List<Frellancer> _usuarios = new()
        {
        new Frellancer { Id = 1, Nome = "João", Email = "matheus.nobregamts@gmail.com", NumeroTelefone = "5511989084846", Skills = new List<string> {"Cozinheiro","Confeiteiro" }, DiasPreferidos = new List<string> { "Sábado", "Domingo" }, IsEmergecy = true },
        new Frellancer   { Id = 2, Nome = "Maria", Email = "maria@email.com",NumeroTelefone = "5511989084846", Skills = new List<string> { "Segurança"  }, DiasPreferidos = new List<string> { "1", "1" }, IsEmergecy = true },
        new Frellancer   { Id = 3, Nome = "Mario", Email = "mario@email.com", Skills = new List<string> { "Cozinheiro" }, DiasPreferidos = new List<string> { "Sexta" }, IsEmergecy = false }
        };


    }
}
