namespace FrasesDoAnoApi
{
    /// <summary>
    /// Classe inicial da API
    /// </summary>
    public class Program
    {

        /// <summary>
        /// M�todo inicial chamado ao iniciar a aplica��o
        /// </summary>
        /// <param name="args">Parametros de inicializa��o da aplica��o</param>
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
        /// <param name="args">Dados para a gera��o do HostBuilder</param>
        /// <returns>Uma implementa��o do IHostBuilder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) => 
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
