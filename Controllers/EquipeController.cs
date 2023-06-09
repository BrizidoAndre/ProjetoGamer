using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjetoGamer.Infra;
using ProjetoGamer.Models;

namespace ProjetoGamer.Controllers
{
    [Route("[controller]")]
    public class EquipeController : Controller
    {
        private readonly ILogger<EquipeController> _logger;

        public EquipeController(ILogger<EquipeController> logger)
        {
            _logger = logger;
        }

        //!Instância do contexto para acessar o banco de dados e inserir o using para acessar a classe
        Context c = new Context();

        // TODO //Criando o método Index que retorna a lista do banco de dados
        [Route("Listar")] //http://localhost:/Equipe/Listar
        public IActionResult Index()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            // Variável que armazena as equipes listadas do banco de dados
            ViewBag.Equipe = c.Equipes.ToList();

            // Retorna a view de equipe (TELA)
            return View();
        }


        //TODO //Criando o método para cadastrar informações de um formulário
        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)//<<<< É uma classe que cadastra formulários
        {
            // Instancia de uma nova equipe
            Equipe novaEquipe = new Equipe();

            // Aqui pegamos os valores recebidos do formulário e adicionamos no novo objeto
            novaEquipe.Nome = form["Nome"].ToString(); //O elemento entre aspas neste exemplo é o nome do input do formulário. Lembrando que devemos definir o tipo de dado que o input nos traz.
            
            //todo //Início da lógica do upload da imagem
            // Se existir um arquivo dentro do formulário ele vai anexar esse arquivo dentro da variável file. Lembrando que por fazermos upload de uma única imagem, a imagem fica dentro do array 0
            if (form.Files.Count > 0)
            {
                var file = form.Files[0];

                // Path.Combine se caso tiver um caminho anterior ele ignora e só visualiza dali pra frente, cria um caminho para criar uma pasta, ele da o ponta pé de inicio do caminho que você deseja. Na realidade ele apenas une nome de pastas colocando uma barra entre elas
                // GetCurrentDirectory = pega a pasta corrente que é a pasta principal do arquivo que no caso a nossa é "PROJETOGAMER"
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");
                // Se a pasta FOLDER não for criadam cria uma nova psata chamada folder
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                // Gera o caminho completo até o caminho do arquivo (imagem - nome com extensão)
                var path = Path.Combine(folder, file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                novaEquipe.Imagem = file.FileName;

            }
            // Caso a imagem não for incluída, uma imagem padrão será selecionada para você
            else
            {
                novaEquipe.Imagem = "padrao.png";
            }
            // todo //Fim da lógica de upload de imagem


            // Adiocionando o objeto no Banco de dados
            c.Equipes.Add(novaEquipe);

            // Salvando as alterações feitas no Banco de dados
            c.SaveChanges();


            return LocalRedirect("~/Equipe/Listar");//Aqui estamos mandando o código recarregar a página no método Listar (o método INDEX)
            //O "~" serve para resumir o endereço Local Host
        }

        // TODO //Inicio do método excluir
        [Route("Excluir/{_id}")] //!O parâmetro deve ser semelhante como está no método e como está dentro da route. lembrando que os parâmentros devem ser colocados após o nome do método, após uma barra e dentro de chaves.
        public IActionResult Excluir(int _id) // O parametro serve para localizar a equipe a ser excluida
        {
            // Aqui, pegamos o objeto de contexto (que representa nosso banco de dados) e utilizamos a função 'FirstOrDefault' para encontrar o primeiro objeto do banco de dados que possui o mesmo Id do Id a ser deletado. Caso o Id não seja encontrado, um valor default é retornado
            Equipe equipeBuscada = c.Equipes.First(x => x.IdEquipe == _id);
            c.Remove(equipeBuscada); //Deletando o produto pequisado pela função
            c.SaveChanges(); //Salvando as alterações
            return LocalRedirect("~/Equipe/Listar");
        }

        // TODO //Inicio do método Editar
        [Route("Editar")]
        public IActionResult Editar(int id)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");

            // A mesma coisa que fizemos no método de Excluir
            Equipe equipeBuscada = c.Equipes.First(x => x.IdEquipe == id);
            // ViewBag = É como se fosse uma mochila temporária que pode ser usada pelas classes Views
            // Lembrando que ela deve estar atribuida a uma classe e nunca sozinha
            ViewBag.Equipe = equipeBuscada;
            
            return View("Edit");
        }
        
        // TODO //Inicio do método Atualizar
        [Route("Atualizar")]
        public IActionResult Atualizar(IFormCollection form)
        {
            Equipe novaEquipe = new Equipe();

            novaEquipe.IdEquipe = int.Parse(form["IdEquipe"].ToString());

            novaEquipe.Nome = form["Nome"].ToString();

            if (form.Files.Count > 0)
            {
                var file = form.Files[0];

                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var path = Path.Combine(folder, file.FileName);
                
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                novaEquipe.Imagem = file.FileName;
            }
            else
            {
                novaEquipe.Imagem = "padrao.png";
            }
            Equipe equipebuscada = c.Equipes.First(x => x.IdEquipe == novaEquipe.IdEquipe);
            equipebuscada.Nome = novaEquipe.Nome;
            equipebuscada.Imagem = novaEquipe.Imagem;
            c.Equipes.Update(equipebuscada);
            c.SaveChanges();

            return LocalRedirect("~/Equipe/Listar");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
} 