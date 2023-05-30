using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjetoGamer.Infra;

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

        //Instância do contexto para acessar o banco de dados e inserir o using para acessar a classe
        Context c = new Context();

        [Route("Listar")] //http://localhost:/Equipe/Listar
        public IActionResult Index()
        {
            // Variável que armazena as equipes lisrtadas do banco de dados
            ViewBag.Equipe = c.Equipes.ToList();

            // Retorna a view de equipe (TELA)
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}