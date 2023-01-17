namespace FrasesDoAnoApi
{
    /// <summary>
    /// Classe inicial da API
    /// </summary>
    public class Program
    {

        /// <summary>
        /// Método inicial chamado ao iniciar a aplicação
        /// </summary>
        /// <param name="args">Parametros de inicialização da aplicação</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            var host = new WebHostBuilder()
              .UseKestrel()
              .UseContentRoot(Directory.GetCurrentDirectory())
              .UseIISIntegration()
              .UseStartup<Startup>()
              .Build();
        }

        /// <summary>
        /// Host Builder da API
        /// </summary>
        /// <param name="args">Dados para a geração do HostBuilder</param>
        /// <returns>Uma implementação do IHostBuilder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) => 
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
