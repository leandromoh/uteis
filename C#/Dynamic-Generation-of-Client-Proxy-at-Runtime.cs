using dfgd.EW.Entidade.DadosGerais;
using dfgdf.EW.Servico.Bovespa.Cliente;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace benchmark_base_mis
{
    class Program
    {
        static void Main(string[] args)
        {
            var ccis = new[] { 168679, 194405, 300130 };

            var resultados = ccis.Select(cci => 
                                    Proxy<IClienteDadosBasicos>.Consume(x => x.RetornarDadosClienteCorretora(cci)));

            foreach (var x in resultados)
            {
                Console.WriteLine(x.Dados.AssessorNome);
            }

            Console.Read();
        }
    }

    //https://www.codeproject.com/Tips/138388/Dynamic-Generation-of-Client-Proxy-at-Runtime-in
    static class Proxy<T> where T : class
    {
        private static ChannelFactory<T> myChannelFactory;

        static Proxy()
        {
            string url = "asdasdasd.svc";
            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress endpoint = new EndpointAddress(url);
            myChannelFactory = new ChannelFactory<T>(binding, endpoint);
            myChannelFactory.Credentials.UserName.UserName = "werwer";
            myChannelFactory.Credentials.UserName.Password = "erwer";
            binding.SendTimeout = TimeSpan.FromSeconds(25);
            binding.Security.Mode = BasicHttpSecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
        }

        public static TResult Consume<TResult>(Func<T, TResult> func)
        {
            T contrato = null;

            try
            {
                contrato = myChannelFactory.CreateChannel();
                return func(contrato);
            }
            catch (Exception ex)
            {
                (contrato as IClientChannel)?.Abort();
                throw;
            }
            finally
            {
                (contrato as IClientChannel)?.Close();
            }
        }
    }
}
