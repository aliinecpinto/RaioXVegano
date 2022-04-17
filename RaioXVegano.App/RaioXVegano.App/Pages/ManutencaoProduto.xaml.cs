using NLog;
using RaioXVegano.App.Helpers;
using RaioXVegano.di;
using RaioXVegano.entities;
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
    public partial class ManutencaoProduto : ContentPage
    {
        private readonly Logger _log;

        private byte[] _byteArray;

        private readonly ICadastraProdutoBO _cadastrarProdutoBO;
        private readonly IAtualizaProdutoBO _atualizaProdutoBO;
        private readonly ISalvarInformacoesLogBO _salvarInformacoesLogBO;

        public ManutencaoProduto(bool isProdutoEncontrado, Produto produto)
        {
            InitializeComponent();

            _log = LogManager.GetCurrentClassLogger();

            NavigationPage.SetHasBackButton(this, false);

            ToolbarItems.Add(new ToolbarItem("Home", "Logo_v3.png", () =>
            {
                VoltarParaHome();
            }, ToolbarItemOrder.Primary));

            _cadastrarProdutoBO = DependencyInjection.Container.GetInstance<ICadastraProdutoBO>();
            _atualizaProdutoBO = DependencyInjection.Container.GetInstance<IAtualizaProdutoBO>();
            _salvarInformacoesLogBO = DependencyInjection.Container.GetInstance<ISalvarInformacoesLogBO>();

            ExibirFotoProduto(produto);

            PreencheFormulario(isProdutoEncontrado, produto);

            ExibeBotaoCorreto(isProdutoEncontrado);
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

        private void ExibirFotoProduto(Produto produto)
        {
            _log.Info("ExibirFotoProduto... ");
            ImagemProduto.Source = ImagemUtil.ConverteBase64ToImageSource(produto.Base64ImagemProduto, out _byteArray);
            _log.Info("ExibirFotoProduto... OK");
        }

        private void PreencheFormulario(bool isProdutoEncontrado, Produto produto)
        {
            _log.Info("PreencheFormulario... ");

            CodigoDeBarras.Text = produto.CodigoDeBarras;
            IdProduto.Text = produto.Id.ToString();

            if (isProdutoEncontrado)
            {
                _log.Info("ProdutoEncontrado... ");

                NomeProduto.Text = produto.Nome;
                IsProdutoVegano.SelectedItem = produto.IsVegano.Value ? Consts.TRUE : Consts.FALSE;
                Motivo.Text = produto.Motivo;
                Ingredientes.Text = produto.Ingredientes;
            }

            _log.Info("PreencheFormulario... OK");
        }

        private void ExibeBotaoCorreto(bool isProdutoEncontrado)
        {
            _log.Info("ExibeBotaoCorreto... ");

            BtnCadastrarProduto.IsVisible = !isProdutoEncontrado;
            BtnAlterarProduto.IsVisible = isProdutoEncontrado;

            _log.Info("ExibeBotaoCorreto... OK");
        }

        private void CadastrarProduto(object sender, EventArgs e)
        {
            _log.Info("CadastrarProduto... ");

            int.TryParse(IdProduto.Text, out int idProduto);

            CadastraProdutoAppRequest request = new CadastraProdutoAppRequest()
            {
                ChaveUsuarioLogado = Preferences.Get(Consts.SESSAO_CHAVE_USUARIO_LOGADO, string.Empty),
                Produto = new Produto()
                {
                    Base64ImagemProduto = Convert.ToBase64String(_byteArray),
                    CodigoDeBarras = CodigoDeBarras.Text,
                    Id = idProduto,
                    Ingredientes = Ingredientes.Text,
                    IsVegano = IsProdutoVegano.SelectedItem != null ? Consts.TRUE.Equals(IsProdutoVegano.SelectedItem) : (bool?)null,
                    Motivo = Motivo.Text,
                    Nome = NomeProduto.Text
                }
            };

            CadastraProdutoAppResponse response = _cadastrarProdutoBO.Executar(request);

            MontaTelaRetorno(response, CodigoDeBarras.Text, true);

            _log.Info("CadastrarProduto... OK");
        }

        private void AlterarProduto(object sender, EventArgs e)
        {
            _log.Info("AlterarProduto... ");

            int.TryParse(IdProduto.Text, out int idProduto);

            AtualizaProdutoAppRequest request = new AtualizaProdutoAppRequest()
            {
                ChaveUsuarioLogado = Preferences.Get(Consts.SESSAO_CHAVE_USUARIO_LOGADO, string.Empty),
                Produto = new Produto() 
                {
                    Base64ImagemProduto = Convert.ToBase64String(_byteArray),
                    CodigoDeBarras = CodigoDeBarras.Text,
                    Id = idProduto,
                    Ingredientes = Ingredientes.Text,
                    IsVegano = IsProdutoVegano.SelectedItem != null ? Consts.TRUE.Equals(IsProdutoVegano.SelectedItem) : (bool?)null,
                    Motivo = Motivo.Text,
                    Nome = NomeProduto.Text
                }
            };

            AtualizaProdutoAppResponse response = _atualizaProdutoBO.Executar(request);

            MontaTelaRetorno(response, CodigoDeBarras.Text);

            _log.Info("AlterarProduto... OK");
        }

        private void MontaTelaRetorno(IBaseResponseApp response, string codBarras = null, bool isProdutoNovo = false)
        {
            _log.Info("MontaTelaRetorno... ");

            bool existeErro = response.Mensagens?.Any() ?? false;

            if (existeErro)
            {
                RetornaErros(response, codBarras);
            }
            else
            {
                RetornaSucesso(isProdutoNovo);
            }

            _log.Info("MontaTelaRetorno... OK");
        }

        private void RetornaErros(IBaseResponseApp response, string codBarras)
        {
            _log.Info("RetornaErros... ");

            bool existeErroGenerico = response.Mensagens.ContainsKey(Consts.ERRO_GENERICO);

            if (existeErroGenerico)
            {
                _log.Info("ErroGenerico... ");
                Navigation.PushAsync(new Erro(response.Mensagens[Consts.ERRO_GENERICO]));
            }
            else
            {
                _log.Info("ErroEspecifico... ");
                
                LimpaErrosDaTela();

                foreach (KeyValuePair<string, string> erro in response.Mensagens)
                {
                    bool pararLaco = false;

                    switch (erro.Key)
                    {
                        case "":
                            AlertMessageUtil.DangerMessage(FrameAlert, LabelAlert, EmailAlert, erro.Value);
                            break;
                        case Consts.NOME_PRODUTO:
                            NomeProdutoValidacao.Text = erro.Value;
                            NomeProdutoValidacao.IsVisible = true;
                            break;
                        case Consts.TIPO_PRODUTO:
                            TipoProdutoValidacao.Text = erro.Value;
                            TipoProdutoValidacao.IsVisible = true;
                            break;
                        case Consts.MOTIVO:
                            MotivoValidacao.Text = erro.Value;
                            MotivoValidacao.IsVisible = true;
                            break;
                        case Consts.ERRO_PRODUTO_SENDO_EDITADO:
                            pararLaco = true;
                            Navigation.PushAsync(new ProdutoSendoEditado(codBarras));
                            break;
                    }

                    if (pararLaco)
                    {
                        break;
                    }
                }
            }

            _log.Info("RetornaErros... OK");
        }

        private void LimpaErrosDaTela()
        {
            _log.Info("LimpaErrosDaTela... ");

            FrameAlert.IsVisible = false;
            LabelAlert.Text = string.Empty;
            LabelAlert.IsVisible = false;
            NomeProdutoValidacao.Text = string.Empty;
            NomeProdutoValidacao.IsVisible = false;
            TipoProdutoValidacao.Text = string.Empty;
            TipoProdutoValidacao.IsVisible = false;
            MotivoValidacao.Text = string.Empty;
            MotivoValidacao.IsVisible = false;

            _log.Info("LimpaErrosDaTela... OK");
        }

        private void RetornaSucesso(bool isProdutoNovo)
        {
            _log.Info("RetornaSucesso... ");
            Navigation.PushAsync(new SucessoManutencaoProduto(isProdutoNovo));
            _log.Info("RetornaSucesso... OK");
        }

        private void EnviarEmail(object sender, EventArgs e)
        {
            EmailHelper.EnviarEmailLogErro(_salvarInformacoesLogBO, FrameAlert, LabelAlert, EmailAlert);
        }
    }
}