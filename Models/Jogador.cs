using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoGamer.Models
{
    public class Jogador
    {
        [Key]
        public int IdJogador { get; private set; }

        [ForeignKey("Equipe")] //
        public int IdEquipe { get; private set; }
        
        public string NomeJogador { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
    }
}