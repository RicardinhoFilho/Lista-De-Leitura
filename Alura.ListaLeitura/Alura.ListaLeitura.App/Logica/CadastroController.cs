using Alura.ListaLeitura.App.HTML;
using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App.Logica
{
    public class CadastroController
    {
        public string Incluir(Livro livro)
        {
            //var livro = new Livro()
            //{
            //    //Antes recebiamos as informações pelo método get, exibiamos no url as informações dos livros
            //    //Titulo = context.Request.Query["titulo"].First(),
            //    // Autor = context.Request.Query["autor"].First()

            //    //Agora como estamos utilizando o método post, temos q mudar nossa construção:
            //    Titulo = context.Request.Form["titulo"].First(),
            //    Autor = context.Request.Form["autor"].First()
            //};
            var _repo = new LivroRepositorioCSV();
            _repo.Incluir(livro);

            return "O livro foi adicionado com sucesso!";
        }

        public static Task ExibeFormulario(HttpContext context)
        {
            var html = HTMLUtils.CarregaArquivoHTML("cadastrarNovoLivro");

            return context.Response.WriteAsync(html);
        }

    }
}
