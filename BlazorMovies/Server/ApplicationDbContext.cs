using BlazorMovies.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Server
{
    public class ApplicationDbContext : DbContext
    {
        //DBContext tem de ser generica por causa dos bancos
        //Opitions é  a string de conecção, encaminharemos para a classe base
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { 

        
        }
        //Integridade referencias
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Faz de cada uma das tabelasseguindo integridade refencial seguindo a mesma logica 
            //Um ator é uma pessoa, então essa tem de existir
            modelBuilder.Entity<MoviesActors>().HasKey(x => new { x.MovieId, x.PersonId });
            modelBuilder.Entity<MoviesGenres>().HasKey(x => new { x.MovieId, x.GenresId });
            //constinua para todas as tabaleas que tem relacionameto

            base.OnModelCreating(modelBuilder);//Quando tiver criando o modelo,olha aqui e dai continua
        }

        //Cada tabela do banco vem abaixo
        public DbSet<Genre> Genres { get; set; }//A classe define um genero mas a tabela é uma tabela de generos
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<MoviesActors> MoviesActors { get; set; }
        public DbSet<MoviesGenres> MoviesGenres { get; set; }


    }
}
