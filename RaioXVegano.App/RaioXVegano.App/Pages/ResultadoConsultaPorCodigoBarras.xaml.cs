using NLog;
using RaioXVegano.App.Helpers;
using RaioXVegano.di;
using RaioXVegano.entities;
using RaioXVegano.entities.App;
using RaioXVegano.ibo.Acao;
using RaioXVegano.Util;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RaioXVegano.App.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultadoConsultaPorCodigoBarras : ContentPage
    {
        private readonly Logger _log;
        private Produto Produto { get; set; }
        private bool IsProdutoEncontrado { get; set; }

        private readonly IAtualizaProdutoBO _atualizaProdutoBO;
        private readonly ISalvarInformacoesLogBO _salvarInformacoesLogBO;

        public ResultadoConsultaPorCodigoBarras(bool isProdutoEncontrado, Produto produto)
        {
            InitializeComponent();

            _log = LogManager.GetCurrentClassLogger();

            NavigationPage.SetHasBackButton(this, false);

            ToolbarItems.Add(new ToolbarItem("Home", "Logo_v3.png", () =>
            {
                VoltarParaHome();
            }));
            
            _atualizaProdutoBO = DependencyInjection.Container.GetInstance<IAtualizaProdutoBO>();
            _salvarInformacoesLogBO = DependencyInjection.Container.GetInstance<ISalvarInformacoesLogBO>();

            Produto = produto;
            IsProdutoEncontrado = isProdutoEncontrado;

            if (isProdutoEncontrado)
            {
                string msg = produto.IsVegano.Value ? string.Format(entities.Properties.Resources.produtoVegano, produto.Nome) : string.Format(entities.Properties.Resources.produtoNaoVegano, produto.Nome);
                AlertMessageUtil.InfoMessage(FrameAlert, LabelAlert, msg);
                ResultadoConsultaGif.Source = produto.IsVegano.Value ? ImageSource.FromFile("Logo_Feliz") : ImageSource.FromFile("Logo_Chorando");
                BtnManutencaoProduto.Text = Properties.Labels.BtnAlterarProduto;
            }
            else
            {
                AlertMessageUtil.InfoMessage(FrameAlert, LabelAlert, entities.Properties.Resources.produtoNaoEncontrado);
                ResultadoConsultaGif.Source = ImageSource.FromFile("Logo_Confusa");
                BtnManutencaoProduto.Text = Properties.Labels.BtnCadastrarProduto;
            }
        }

        protected override bool OnBackButtonPressed() 
        {
            VoltarParaHome();

            return true;
        }

        private void ExibeManutencaoProduto(object sender, System.EventArgs e)
        {
            _log.Info("ExibeManutencaoProduto... ");

            if (IsProdutoEncontrado)
            {
                _log.Info("ProdutoEncontrado... ");

                AtualizaProdutoAppRequest requestApp = new AtualizaProdutoAppRequest()
                {
                    ChaveUsuarioLogado = Preferences.Get(Consts.SESSAO_CHAVE_USUARIO_LOGADO, string.Empty),
                    Produto = Produto
                };

                requestApp.Produto.UsuarioEditando = Preferences.Get(Consts.SESSAO_CHAVE_USUARIO_LOGADO, string.Empty);

                _atualizaProdutoBO.Executar(requestApp);
            }

            Navigation.PushAsync(new ManutencaoProduto(IsProdutoEncontrado, Produto));

            _log.Info("ExibeManutencaoProduto... OK");
        }

        private void VoltarParaHome()
        {
            _log.Info("VoltarHome... ");
            Navigation.PopToRootAsync();
            _log.Info("VoltarHome... OK");
        }

        private void EnviarEmail(object sender, EventArgs e)
        {
            EmailHelper.EnviarEmailLogErro(_salvarInformacoesLogBO, FrameAlert, LabelAlert, EmailAlert);
        }
    }
}