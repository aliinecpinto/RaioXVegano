using NLog;
using RaioXVegano.App.Helpers;
using RaioXVegano.di;
using RaioXVegano.ibo.Acao;
using RaioXVegano.Util;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RaioXVegano.App.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SucessoManutencaoProduto : ContentPage
    {
        private readonly Logger _log;
        private readonly ISalvarInformacoesLogBO _salvarInformacoesLogBO;

        public SucessoManutencaoProduto(bool isProdutoNovo)
        {
            InitializeComponent();

            _salvarInformacoesLogBO = DependencyInjection.Container.GetInstance<ISalvarInformacoesLogBO>();

            _log = LogManager.GetCurrentClassLogger();

            NavigationPage.SetHasBackButton(this, false);

            ToolbarItems.Add(new ToolbarItem("Home", "Logo_v3.png", () =>
            {
                VoltarParaHome();
            }, ToolbarItemOrder.Primary));

            string msg = isProdutoNovo ? entities.Properties.Resources.produtoCadastradoSucesso : entities.Properties.Resources.produtoAlteradoSucesso;
            AlertMessageUtil.SuccessMessage(FrameAlert, LabelAlert, msg);
        }

        protected override bool OnBackButtonPressed()
        {
            VoltarParaHome();

            return true;
        }

        private void VoltarHome(object sender, EventArgs e)
        {
            VoltarParaHome();
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