using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoGamer.Models
{
    public class Jogador
    {
        [Key]//Data ANNOTARION - Id Jogador
        public int IdJogador { get; set; }

        [ForeignKey("Equipe")] //Data ANNOTATION - IdEquipe
        public int IdEquipe { get; set; }

        public string NomeJogador { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public Equipe Equipe { get; set; }
    }
}