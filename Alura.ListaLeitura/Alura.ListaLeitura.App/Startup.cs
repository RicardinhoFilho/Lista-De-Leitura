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
using System.IO;
using Alura.ListaLeitura.App.Logica;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {
        
        //INICIANDO O SERVIÇO DE ROTEAMENTO
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
            services.AddMvc();
        }

        //CONFIGURANDO AS ROTAS
        public void Configure(IApplicationBuilder app)
        {



            app.UseMvcWithDefaultRoute();

            //var builder = new RouteBuilder(app);
            
            ////builder.MapRoute("Livros/ParaLer", LivrosLogicaExibicao.ParaLer);
            ////builder.MapRoute("Livros/Lendo", LivrosLogicaExibicao.Lendo);
            ////builder.MapRoute("Livros/Lidos", LivrosLogicaExibicao.Lidos);
            ////builder.MapRoute("Livros/Detalhes/{id:int}", LivrosLogicaExibicao.Detalhes);//Requisição para exibir de talhes do livro, percebam que restringimos que atenderemos esta rota somente se a requisição for 'int'
            ////builder.MapRoute("Cadastro/NovoLivro/{nome}/{autor}", CadastroLogica.NovoLivro);//Adicionar um novo livro à lista de livros para Ler
            ////builder.MapRoute("Cadastro/ExibeFormulario", CadastroLogica.ExibeFormulario);
            ////builder.MapRoute("Cadastro/Incluir", CadastroLogica.Incluir);

            //var rotas = builder.Build();//Para não ocorrer erro preciso de um método 'ConfigureServices' com services  

            //app.UseRouter(rotas); //Utilizando UseRouter para iniciar nosso rotiamento 
            ////app.Run(Roteamento);//Não vamos mais utilizar o nosso Roteamento feito a mão, mas sim utilizamremos 'RouteBuilder'
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