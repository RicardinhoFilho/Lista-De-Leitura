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
using Microsoft.AspNetCore.Mvc;

namespace Alura.ListaLeitura.App.Logica
{
    public class LivrosController : Controller
    {
        public IEnumerable<Livro> Livros { get; set; }
        public IActionResult Detalhes(int id)
        {
            //var conteudo = HTMLUtils.CarregaArquivoHTML("detalhesLivro");
            
            var repo = new LivroRepositorioCSV();
            var livro = repo.Todos.First(l => l.Id == id); //analisa dentro da lista na classe 'LivroRepositorioCSV' o Livro.id

            ViewBag.Livros = livro;
            //return context.Response.WriteAsync(livro.Detalhes());//Retorna a função Detalhes presente na classe 'Livro' 

            return View("detalhesLivro");//Retrona nosso arquivo HTML 
        }

        public string NovoLivroParaLer(Livro livro)
        {

            //var livro = new Livro()
            //{
            //    Titulo = Convert.ToString(context.GetRouteValue("nome")),//Pegando no endereço o valor nome e convertendo para string que é o tipo do campo titulo definido na classe Livro
            //    Autor = Convert.ToString(context.GetRouteValue("autor"))//Pegando no endereço o valor nome e convertendo para string que é o tipo do campo autor definido na classe Livro
            //};
            var repo = new LivroRepositorioCSV();

            repo.Incluir(livro);//função incluir livro implementada na classe 'LivroRepositorioCSV'

            return $"Livro {livro.Titulo} adicionado com sucesso!";
        }


        public IActionResult ParaLer()
        {
            var _repo = new LivroRepositorioCSV();
            ViewBag.Livros = _repo.ParaLer.Livros; 
            return View("lista");
        }

        public IActionResult Lidos()
        {
            var _repo = new LivroRepositorioCSV();
            ViewBag.Livros = _repo.Lidos.Livros;

            return View("lista");
        }

        public IActionResult Lendo()
        {
            var _repo = new LivroRepositorioCSV();
            ViewBag.Livros = _repo.Lendo.Livros;

            return View("lista");
        }

        //public string Teste()
        //{
        //    return "nova funcionalidade implementada!";
        //}

    }
}
