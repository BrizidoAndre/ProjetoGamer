using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjetoGamer.Models;

namespace ProjetoGamer.Infra
{
    public class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Só fomos capazes de utilizar essas classes através dos Frameworks que baixamos no terminal
                // Data Source = o nome do servidor do gerenciador do banco
                // Initial Catalog: nome do banco de dados

                // Autenticação pelo Windows
                // Integrated Security : Autenticação pelo Windows
                // TrustServerCertificate : Autenticação pelo Windows

                // Autentificação pelo SqlServer
                // user Id = "nome do seu usuario de login"
                // pwd = "Senha do seu usuário"

                optionsBuilder.UseSqlServer("Data Source = DESKTOP; initial catalog = gamerManha; Integrated Security = true; TrustServerCertificate = true"); //Isto é uma string de conexão. Por aqui ele sabe o servidor do banco, o nome e por qual plataforma ele é autenticado
            }
        }
        // Referências de classes e tabelas
        public DbSet<Jogador> Jogador {get; set;}
        public DbSet<Equipe> Equipes { get; set; }
    }
}