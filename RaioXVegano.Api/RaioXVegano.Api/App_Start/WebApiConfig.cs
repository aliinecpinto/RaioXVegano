using RaioXVegano.di;
using RaioXVegano.Util;
using SimpleInjector.Integration.WebApi;
using System;
using System.Configuration;
using System.IO;
using System.Web.Http;

namespace RaioXVegano.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Serviços e configuração da API da Web

            //Injeção de Dependência
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(DependencyInjection.Configure());

            //Configurar log4net.
            log4net.Config.XmlConfigurator.Configure();

            //Setar ambiente para identificar se precisa criptografar informações
            AplicacaoUtil.Ambiente = ConfigurationManager.AppSettings[Consts.AMBIENTE];

            //Setar keys para criptografia
            Criptografia();

            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void Criptografia()
        {
            string chave = string.Empty;
            string IV = string.Empty;

            string caminho = ConfigurationManager.AppSettings[Consts.CAMINHO_CHAVE];
            string nomeArquivo = ConfigurationManager.AppSettings[Consts.NOME_ARQUIVO_CHAVE];
            string caminhoCompleto = Path.Combine(caminho, nomeArquivo);

            if (File.Exists(caminhoCompleto))
            {
                string files = File.ReadAllText(caminhoCompleto);
                if (!string.IsNullOrEmpty(files))
                {
                    string[] chavesDivididas = files.Split(new string[] { Consts.SEPARADOR }, StringSplitOptions.None);
                    chave = chavesDivididas[0];
                    IV = chavesDivididas[1]; 
                }
            }

            if (string.IsNullOrEmpty(chave) && string.IsNullOrEmpty(IV))
            {
                CriptografiaUtil.GerarChaves();

                chave = CriptografiaUtil.AESKey;
                IV = CriptografiaUtil.AESIV;

                using (StreamWriter sw = File.CreateText(caminhoCompleto))
                {
                    sw.WriteLine(string.Concat(chave, Consts.SEPARADOR, IV));
                    sw.Close();
                }
            }
            else 
            {
                CriptografiaUtil.PreencherComChavesExistentes(chave, IV);
            }
        }
    }
}
