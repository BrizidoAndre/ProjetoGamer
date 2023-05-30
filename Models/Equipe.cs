using System.ComponentModel.DataAnnotations;

namespace ProjetoGamer.Models
{
    public class Equipe
    {
        // Determinando que o dado abaixo é uma key primária
        [Key]//DATA ANNOTATION - IdEquipe
        public int IdEquipe { get; private set; }
        //Tornando a propriedade abaixo obrigatória para realizar um cadastro (se não tiver o dado é impossível fazer o cadastro)
        [Required]
        public string Nome { get; private set; }
        public string Imagem { get; private set; }
        // Referencia que a classe equipe vai ter acesso a collection "Jogador"
        public ICollection<Jogador> Jogador { get; private set; }
    }
}