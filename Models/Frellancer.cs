using System.Text;

namespace MicroserviceNogeco.Models
{
    public class Frellancer
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string NumeroTelefone { get; set; }
        public List<string> Skills { get; set; }
        public List<string> DiasPreferidos { get; set; }
        public bool IsEmergecy { get; set; }

        public string ValidateFrella()
        {
            var mensagemerro = new StringBuilder();

            if (string.IsNullOrEmpty(Nome)) { mensagemerro.Append("O nome é obrigatório. "); }
            if (string.IsNullOrEmpty(Email)) { mensagemerro.Append("A email é obrigatório. "); }
            if (string.IsNullOrEmpty(NumeroTelefone)) { mensagemerro.Append("O telefone é obrigatório. "); }
            return mensagemerro.ToString();

        }
    }
}


