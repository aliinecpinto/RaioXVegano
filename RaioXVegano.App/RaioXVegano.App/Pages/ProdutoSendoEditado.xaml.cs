using NLog;
using RaioXVegano.App.Helpers;
using RaioXVegano.di;
using RaioXVegano.entities.App;
using RaioXVegano.ibo.Acao;
using RaioXVegano.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RaioXVegano.App.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProdutoSendoEditado : ContentPage
    {
        private readonly Logger _log;

        private readonly IConsultaProdutoPorCodigoDeBarrasBO _consultaProdutoPorCodigoDeBarrasBO;
        private readonly ISalvarInformacoesLogBO _salvarInformacoesLogBO;

        private string CodBarras { get; set; }
        private bool IsProdutoLiberadoParaEdicao { get; set; }
        private int Contador { get; set; }

        public ProdutoSendoEditado(string codBarras)
        {
            InitializeComponent();

            _log = LogManager.GetCurrentClassLogger();

            NavigationPage.SetHasBackButton(this, false);

            ToolbarItems.Add(new ToolbarItem("Home", "Logo_v3.png", () =>
            {
                VoltarParaHome();
            }, ToolbarItemOrder.Primary));

            _consultaProdutoPorCodigoDeBarrasBO = DependencyInjection.Container.GetInstance<IConsultaProdutoPorCodigoDeBarrasBO>();
            _salvarInformacoesLogBO = DependencyInjection.Container.GetInstance<ISalvarInformacoesLogBO>();

            CodBarras = codBarras;

            AlertMessageUtil.WarningMessage(FrameAlert, LabelAlert, entities.Properties.Resources.mensagemProdutoSendoEditado);

            IniciarContador();
        }

        protected override bool OnBackButtonPressed()
        {
            VoltarParaHome();

            return true;
        }

        private void VoltarParaHome()
        {
            _log.Info("VoltarHome... ");
            Navigation.PopToRootAsync();
            _log.Info("VoltarHome... OK");
        }

        private void IniciarContador()
        {
            _log.Info("IniciarContador... ");

            Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            {
                Contador += 1;
                bool continuaConsultandoProduto = !IsProdutoLiberadoParaEdicao;

                if (Contador > 3)
                {
                    continuaConsultandoProduto = false;
                    Navigation.PushAsync(new Erro(entities.Properties.Resources.mensagemErroContadorAtualizacao));
                }
                else if(continuaConsultandoProduto)
                {
                    Device.BeginInvokeOnMainThread(ConsultaCodigoBarras);
                }

                return continuaConsultandoProduto;
            });

            _log.Info("IniciarContador... OK");
        }

        private void ConsultaCodigoBarras()
        {
            _log.Info("ConsultaCodigoBarras... ");

            string chaveUsuarioLogado = Preferences.Get(Consts.SESSAO_CHAVE_USUARIO_LOGADO, string.Empty);
            ConsultaProdutoPorCodigoDeBarrasAppResponse response = _consultaProdutoPorCodigoDeBarrasBO.Executar(new ConsultaProdutoPorCodigoDeBarrasAppRequest() { ChaveUsuarioLogado = chaveUsuarioLogado, CodigoDeBarras = CodBarras });

            MontaTelaRetorno(response);

            _log.Info("ConsultaCodigoBarras... OK");
        }

        private void MontaTelaRetorno(ConsultaProdutoPorCodigoDeBarrasAppResponse response)
        {
            _log.Info("MontaTelaRetorno... ");

            bool existeErro = response.Mensagens?.Any() ?? false;
            if (existeErro)
            {
                RetornaErros(response.Mensagens);
            }
            else if (!response.IsProdutoSendoEditado)
            {
                RetornaSucesso(response);
            }

            _log.Info("MontaTelaRetorno... OK");
        }

        private void RetornaErros(IDictionary<string, string> mensagens)
        {
            _log.Info("RetornaErros... ");

            bool existeErroGenerico = mensagens.ContainsKey(Consts.ERRO_GENERICO);

            if (existeErroGenerico)
            {
                Navigation.PushAsync(new Erro(mensagens[Consts.ERRO_GENERICO]));
            }

            _log.Info("RetornaErros... OK");
        }

        private void RetornaSucesso(ConsultaProdutoPorCodigoDeBarrasAppResponse response)
        {
            _log.Info("RetornaSucesso... ");

            IsProdutoLiberadoParaEdicao = response.IsProdutoEncontrado && !response.IsProdutoSendoEditado;
            Navigation.PushAsync(new ResultadoConsultaPorCodigoBarras(response.IsProdutoEncontrado, response.Produto));

            _log.Info("RetornaSucesso... OK");
        }

        private void EnviarEmail(object sender, EventArgs e)
        {
            EmailHelper.EnviarEmailLogErro(_salvarInformacoesLogBO, FrameAlert, LabelAlert, EmailAlert);
        }
    }
}