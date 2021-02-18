using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {

            app.Run(Roteamento);
        }


        //A classe HttpRequest possui várias informações sobre a requisição enviada pelo cliente, a saber:

        //dados do formulário, caso exista
        //dados da query string, caso exista
        //dados do cabeçalho da requisição
        //dados sobre cookies da requisição
        //qual método HTTP foi utilizado
        //se a requisição é segura ou não
        //e outras!
        public Task Roteamento(HttpContext context)
        {  // /Livros/ParaLer
           // /Livros/Lendo
           // /Livros/lidos
            var _repo = new LivroRepositorioCSV();

            var caminhosAtendidos = new Dictionary<string, RequestDelegate> //Dicionário de navegação
                {
                    //caminhos possiveis
                    { "/Livros/ParaLer", LivrosParaLer},
                    {"/Livros/Lendo",LivrosLendo},
                    {"/Livros/Lidos", LivrosLidos}
                };
            //context.Request.Path é responsável por armazenar a solicitação do usuário, o endereço que o usuário está solicitando, exemplo 'livros/home' ou 'livros/livroslidos'
            if (caminhosAtendidos.ContainsKey(context.Request.Path))//Se o requerimento feito pelo usuário estiver dentro do Das possibilidades do nosso dicionário, retornaremos o valor que aquele endereço recebe -> _repo.(ParaLer/Lendo/Lidos).ToString(); 
            {
                var metodoRequerido = caminhosAtendidos[context.Request.Path];
                return metodoRequerido.Invoke(context);
            }

            context.Response.StatusCode = 404;//Alterando o status da requisição para 404, significando que este endereço não existe em nossa aplicação, se checarmos nosso console no momento que é solicitado um endereço inválido ele nos apresenta um erro do tipo 404
            return context.Response.WriteAsync("Caminho Inexistente");
        }

        public Task LivrosParaLer(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();


            return context.Response.WriteAsync(_repo.ParaLer.ToString());
        }

        public Task LivrosLidos(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();


            return context.Response.WriteAsync(_repo.Lidos.ToString());
        }

        public Task LivrosLendo(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();


            return context.Response.WriteAsync(_repo.Lendo.ToString());
        }
    }
}