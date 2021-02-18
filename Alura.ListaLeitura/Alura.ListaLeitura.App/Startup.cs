using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }
        public void Configure(IApplicationBuilder app)
        {
            var builder = new RouteBuilder(app);
            builder.MapRoute("Livros/ParaLer", LivrosParaLer);
            builder.MapRoute("Livros/Lendo", LivrosLendo);
            builder.MapRoute("Livros/Lidos", LivrosLidos);
            builder.MapRoute("Livros/Cadastro/NovoLivro/{nome}/{autor}", NovoLivroParaLer);//Adicionar um novo livro à lista de livros para Ler
            builder.MapRoute("Livros/Detalhes/{id:int}", ExibeDetalhes);//Requisição para exibir de talhes do livro, percebam que restringimos que atenderemos esta rota somente se a requisição for 'int'

            var rotas = builder.Build();//Para não ocorrer erro preciso de um método 'ConfigureServices' com services  

            app.UseRouter(rotas); //Utilizando UseRouter para iniciar nosso rotiamento 
            //app.Run(Roteamento);//Não vamos mais utilizar o nosso Roteamento feito a mão, mas sim utilizamremos 'RouteBuilder'
        }

        private Task ExibeDetalhes(HttpContext context) 
        {
            int id = Convert.ToInt32(context.GetRouteValue("id")); //Pega o id o convertendo para inteiro
            var repo = new LivroRepositorioCSV();
            var livro = repo.Todos.First(l => l.Id == id); //analisa dentro da lista na classe 'LivroRepositorioCSV' o Livro.id
            return context.Response.WriteAsync(livro.Detalhes());//Retorna a função Detalhes presente na classe 'Livro' 
        }

        public Task NovoLivroParaLer(HttpContext context)
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



        //A classe HttpRequest possui várias informações sobre a requisição enviada pelo cliente, a saber:

        //dados do formulário, caso exista
        //dados da query string, caso exista
        //dados do cabeçalho da requisição
        //dados sobre cookies da requisição
        //qual método HTTP foi utilizado
        //se a requisição é segura ou não
        //e outras!

        //Não iremos mais utilizar nosso serviço de rotiamento feito a mão, mas sim o serviço de roteamento nativo do Asp.Net Core implementado pelos dois métodos acima: Configure e ConfigureServices
        //public Task Roteamento(HttpContext context)
        //{  // /Livros/ParaLer
        //   // /Livros/Lendo
        //   // /Livros/lidos
        //    var _repo = new LivroRepositorioCSV();

        //    var caminhosAtendidos = new Dictionary<string, RequestDelegate> //Dicionário de navegação
        //        {
        //            //caminhos possiveis
        //            { "/Livros/ParaLer", LivrosParaLer},
        //            {"/Livros/Lendo",LivrosLendo},
        //            {"/Livros/Lidos", LivrosLidos}
        //        };
        //    //context.Request.Path é responsável por armazenar a solicitação do usuário, o endereço que o usuário está solicitando, exemplo 'livros/home' ou 'livros/livroslidos'
        //    if (caminhosAtendidos.ContainsKey(context.Request.Path))//Se o requerimento feito pelo usuário estiver dentro do Das possibilidades do nosso dicionário, retornaremos o valor que aquele endereço recebe -> _repo.(ParaLer/Lendo/Lidos).ToString(); 
        //    {
        //        var metodoRequerido = caminhosAtendidos[context.Request.Path];
        //        return metodoRequerido.Invoke(context);
        //    }

        //    context.Response.StatusCode = 404;//Alterando o status da requisição para 404, significando que este endereço não existe em nossa aplicação, se checarmos nosso console no momento que é solicitado um endereço inválido ele nos apresenta um erro do tipo 404
        //    return context.Response.WriteAsync("Caminho Inexistente");
        //}
    }
}