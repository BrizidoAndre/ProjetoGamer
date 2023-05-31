using System.ComponentModel.DataAnnotations;

namespace ProjetoGamer.Models
{
    public class Equipe
    {
        // Determinando que o dado abaixo é uma key primária
        [Key]//DATA ANNOTATION - IdEquipe
        public int IdEquipe { get; set; }
        //Tornando a propriedade abaixo obrigatória para realizar um cadastro (se não tiver o dado é impossível fazer o cadastro)
        [Required]
        public string Nome { get; set; }
        public string Imagem { get; set; }
        // Referencia que a classe equipe vai ter acesso a collection "Jogador"
        public ICollection<Jogador> Jogador { get; set; }
    }
}