using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;
// Models representa as entidades do sistema, os objetos do sistema 
namespace MicroserviceNogeco.Models
{
    public class Notification
    {
        public string NomeEmpresa { get; set; }
        public string ValorPago { get; set; }
        public string NomeEvento { get; set; }
        public string DescricaoEvento { get; set; }
        public DateTime DataEvento { get; set; }
        public string LocalEvento { get; set; }
        public string HoraEvento { get; set; }
        public string? Skill_1 { get; set; }
        public string? DiaSemana { get; set; }
        public bool IsEmergency { get; set; }

        //public string LinkAprovação;

        public Notification() { }
        public Notification(string nomeEvento, string descricaoEvento, DateTime dataEvento, string localEvento, string skill, string diasemana, bool isEmergency)
        {
            this.NomeEvento = nomeEvento;
            this.DescricaoEvento = descricaoEvento;
            this.DataEvento = dataEvento;
            this.LocalEvento = localEvento;
            this.Skill_1 = skill;
            this.DiaSemana = diasemana;
            this.IsEmergency = isEmergency;
            //this.LinkAprovação = linkaprovação;
        }

        public List<string> Skills
        {
            get => Skill_1?
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .ToList() ?? new List<string>();
        }

        public List<string> DiasDaSemana
        {
            get => DiaSemana?
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .ToList() ?? new List<string>();
        }


        public string Validate()
        {
            var mensagemerro = new StringBuilder();

            if (string.IsNullOrEmpty(NomeEvento)){mensagemerro.Append("O nome do evento é obrigatório. ");}
            if (string.IsNullOrEmpty(DescricaoEvento)){mensagemerro.Append("A descrição do evento é obrigatória. ");}
            if (DataEvento == default(DateTime)){mensagemerro.Append("A data do evento é obrigatória.");}
            if (string.IsNullOrEmpty(LocalEvento)) { mensagemerro.Append("O local do evento é obrigatório. "); }
            if (string.IsNullOrEmpty(Skill_1)){mensagemerro.Append("A Skill é obrigatória. ");}
            if (string.IsNullOrEmpty(DiaSemana)) { mensagemerro.Append("O dia da semanada é obrigatório. "); }
            return mensagemerro.ToString();
            
        }
    }
}
