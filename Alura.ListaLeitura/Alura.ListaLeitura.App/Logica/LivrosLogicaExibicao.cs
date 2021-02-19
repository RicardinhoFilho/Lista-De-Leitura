using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alura.ListaLeitura.App.HTML;

namespace Alura.ListaLeitura.App.Logica
{
    public class LivrosLogicaExibicao
    {
        public static Task ExibeDetalhes(HttpContext context)
        {
            int id = Convert.ToInt32(context.GetRouteValue("id")); //Pega o id o convertendo para inteiro
            var conteudo = HTMLUtils.CarregaArquivoHTML("detalhesLivro");
            var repo = new LivroRepositorioCSV();
            var livro = repo.Todos.First(l => l.Id == id); //analisa dentro da lista na classe 'LivroRepositorioCSV' o Livro.id

            conteudo = conteudo.Replace("#titulo#", livro.Titulo);
            conteudo = conteudo.Replace("#autor#", livro.Autor);
            conteudo = conteudo.Replace("#lista#", livro.Lista.Titulo);

            //return context.Response.WriteAsync(livro.Detalhes());//Retorna a função Detalhes presente na classe 'Livro' 

            return context.Response.WriteAsync(conteudo);//Retrona nosso arquivo HTML 
        }

        public static Task NovoLivroParaLer(HttpContext context)
        {
            var livro = new Livro()
            {
                Titulo = Convert.ToString(context.GetRouteValue("nome")),//Pegando no endereço o valor nome e convertendo para string que é o tipo do campo titulo definido na classe Livro
                Autor = Convert.ToString(context.GetRouteValue("autor"))//Pegando no endereço o valor nome e convertendo para string que é o tipo do campo autor definido na classe Livro
            };
            var repo = new LivroRepositorioCSV();

            repo.Incluir(livro);//função incluir livro implementada na classe 'LivroRepositorioCSV'

            return context.Response.WriteAsync($"Livro {livro.Titulo} adicionado com sucesso!");
        }


        public static Task LivrosParaLer(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            var conteudo = HTMLUtils.CarregaArquivoHTML("listaDinamicaLivros");

            foreach (var livro in _repo.ParaLer.Livros)
            {
                conteudo = conteudo.Replace("#Novo-Item#", $"<li>{livro.Titulo} - {livro.Autor}</li> #Novo-Item#");
            }

            conteudo = conteudo.Replace("#Novo-Item#", " ");

            return context.Response.WriteAsync(conteudo);
        }

        public static Task LivrosLidos(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            var conteudo = HTMLUtils.CarregaArquivoHTML("listaDinamicaLivros");

            foreach (var livro in _repo.Lidos.Livros)
            {
                conteudo = conteudo.Replace("#Novo-Item#", $"<li>{livro.Titulo} - {livro.Autor}</li> #Novo-Item#");
            }

            conteudo = conteudo.Replace("#Novo-Item#", " ");
            return context.Response.WriteAsync(conteudo);
        }

        public static Task LivrosLendo(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            var conteudo = HTMLUtils.CarregaArquivoHTML("listaDinamicaLivros");

            foreach (var livro in _repo.Lendo.Livros)
            {
                conteudo = conteudo.Replace("#Novo-Item#", $"<li>{livro.Titulo} - {livro.Autor}</li> #Novo-Item#");
            }

            conteudo = conteudo.Replace("#Novo-Item#", " ");

            return context.Response.WriteAsync(conteudo);
        }

    }
}
