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
    public class LoginController : Controller
    {
        Context c = new Context();
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [TempData]
        public string Message { get; set; }

        [Route("Login")]
        public IActionResult Index()
        {
            // Esta viewbag foi copiada e colada em todas view INDEX e EDITAR em todos os controller existentes, HOME, JOGADOR E EQUIPE
            // Tudo que retornar uma View tem que inserir este dado para mostrar para todas as views que o jogador esta logado.
            // Em outras palavras precisamos copiar esta linha de código em todos os métodos que nos redirecionarmos em uma nova página
            ViewBag.Username = HttpContext.Session.GetString("Username");
            return View();
        }

        [Route("Logar")]
        public IActionResult Logar(IFormCollection form)
        {
            string email = form["Email"].ToString();
            string senha = form["Senha"].ToString();

            Jogador jogadorBuscado = c.Jogador.FirstOrDefault(z => z.Email == email && z.Senha == senha)!;

            // Aqui precisamos implementar a seção

            if (jogadorBuscado != null)
            {
                HttpContext.Session.SetString("Username", jogadorBuscado.NomeJogador);
                return LocalRedirect("~/");
            }
            else
            {
                Message = "Dados inválidos";
            }

            return LocalRedirect("~/Login/Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}