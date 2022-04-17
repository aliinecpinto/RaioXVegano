using RaioXVegano.entities.App;
using RaioXVegano.entities.Properties;
using RaioXVegano.ibo.Acao;
using RaioXVegano.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RaioXVegano.App.Helpers
{
    public static class EmailHelper
    {
        public static void EnviarEmailLogErro(ISalvarInformacoesLogBO bo, Frame frameAlert, Label labelAlert, Button emailAlert)
        {
            SalvarInformacoesLogAppRequest request = new SalvarInformacoesLogAppRequest()
            {
                ChaveUsuarioLogado = Preferences.Get(Consts.SESSAO_CHAVE_USUARIO_LOGADO, string.Empty),
                Parametro1 = SecureStorage.GetAsync(Consts.SESSAO_CHAVE_CRIPTOGRAFIA).Result,
                Parametro2 = SecureStorage.GetAsync(Consts.SESSAO_IV_CRIPTOGRAFIA).Result
            };

            SalvarInformacoesLogAppResponse response = bo.Executar(request);

            if (response.Mensagens == null || !response.Mensagens.Any())
            {
                List<string> destinatario = new List<string>() { Resources.emailRaioXVegano };

                string externalStoregePath = DependencyService.Get<IExternalStorage>().GetExternalStorage();
                string logPath = Path.Combine(externalStoregePath, Consts.CAMINHO_LOG);
                string zipName = Consts.NOME_ARQUIVO_LOG_ZIP;

                ZipUtil.GeraZip(logPath, externalStoregePath, zipName);

                string chaveUsuarioLogado = Preferences.Get(Consts.SESSAO_CHAVE_USUARIO_LOGADO, string.Empty);
                string emailBody = String.Format(Resources.emailMensagemLogErro, chaveUsuarioLogado);

                EnviarEmail(destinatario, Resources.emailAssuntoLogErro, emailBody, externalStoregePath, Consts.NOME_ARQUIVO_LOG_ZIP);

                AlertMessageUtil.SuccessMessage(frameAlert, labelAlert, Resources.emailEnviadoComSucesso);
            }
            else
            {
                AlertMessageUtil.DangerMessage(frameAlert, labelAlert, emailAlert, response.Mensagens.First().Value);
            }
                
            emailAlert.IsVisible = false;
        }

        private static void EnviarEmail(List<string> destinatario, string assuntoEmail, string corpoEmail, string caminhoAnexo, string nomeAnexo, List<string> destinatarioEmCopia = null)
        {
            DependencyService.Get<IEmailService>().CreateEmail(destinatario, destinatarioEmCopia, assuntoEmail, corpoEmail, corpoEmail, caminhoAnexo, nomeAnexo);
        }
    }
}
