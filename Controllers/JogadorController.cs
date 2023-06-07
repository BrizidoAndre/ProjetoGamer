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
    public class JogadorController : Controller
    {
        Context c = new Context();
        private readonly ILogger<JogadorController> _logger;

        public JogadorController(ILogger<JogadorController> logger)
        {
            _logger = logger;
        }

        // Todo //Início da lógica de Listar
        [Route("Listar")]
        public IActionResult Index()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Jogador = c.Jogador.ToList();
            ViewBag.Equipe = c.Equipes.ToList();


            return View();
        }

        // todo //Início da lógica de Cadastrar
        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            Jogador novoJogador = new Jogador();

            novoJogador.IdEquipe = int.Parse(form["IdEquipe"].ToString());
            novoJogador.NomeJogador = form["Nome"].ToString();
            novoJogador.Email = form["Email"].ToString();
            novoJogador.Senha = form["Senha"].ToString();

            c.Jogador.Add(novoJogador);

            c.SaveChanges();
            
            return LocalRedirect("~/Jogador/Listar");
        }

        // todo //Inicio do método excluir
        [Route("Excluir/id")]
        public IActionResult Excluir(int id)
        {
            Jogador jogadorExcluido = c.Jogador.First(z => z.IdJogador == id);

            c.Remove(jogadorExcluido);

            c.SaveChanges();

            return LocalRedirect("~/Jogador/Listar");
        }

        [Route("Editar/id")]
        public IActionResult Editar(int id)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            Jogador jogadorbuscado = c.Jogador.First(z => z.IdJogador == id);

            ViewBag.Jogador = jogadorbuscado;

            return LocalRedirect("~/Jogador/Edit");
        }

        [Route("Atualizar")]
        public IActionResult Atualizar(IFormCollection form)
        {
            Jogador novoJogador = new Jogador();

            novoJogador.IdJogador = int.Parse(form["IdJogador"].ToString());
            novoJogador.IdEquipe = int.Parse(form["IdEquipe"].ToString());

            return LocalRedirect("~/Jogador/Edit");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}