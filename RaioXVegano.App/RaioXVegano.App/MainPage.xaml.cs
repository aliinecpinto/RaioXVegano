using NLog;
using Plugin.Media;
using RaioXVegano.App.Pages;
using RaioXVegano.di;
using RaioXVegano.entities;
using RaioXVegano.entities.App;
using RaioXVegano.ibo.Acao;
using RaioXVegano.iso.Acao;
using RaioXVegano.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RaioXVegano.App
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private readonly Logger _log;
        private readonly IConsultaProdutoPorCodigoDeBarrasBO _consultaProdutoPorCodigoDeBarrasBO;

        public MainPage()
        {
            InitializeComponent();

            _log = LogManager.GetCurrentClassLogger();

            NavigationPage.SetHasBackButton(this, false);

            _consultaProdutoPorCodigoDeBarrasBO = DependencyInjection.Container.GetInstance<IConsultaProdutoPorCodigoDeBarrasBO>();
        }

        private async void ScannearCodigoBarras(object sender, EventArgs e) => await OpenScan();

        private async Task OpenScan()
        {
            _log.Info("OpenScan... ");

            var scanner = DependencyService.Get<IScannerDeCodigoBarrasSO>();
            var codBarras = await scanner.ScanAsync();
            if (!string.IsNullOrEmpty(codBarras))
            {
                ConsultaCodigoBarras(codBarras);
            }

            _log.Info("OpenScan... OK");
        }

        private void ConsultaCodigoBarras(string codBarras)
        {
            _log.Info("ConsultaCodigoBarras... ");

            string chaveUsuarioLogado = Preferences.Get(Consts.SESSAO_CHAVE_USUARIO_LOGADO, string.Empty);
            ConsultaProdutoPorCodigoDeBarrasAppResponse response = _consultaProdutoPorCodigoDeBarrasBO.Executar(new ConsultaProdutoPorCodigoDeBarrasAppRequest() { ChaveUsuarioLogado = chaveUsuarioLogado, CodigoDeBarras = codBarras });

            MontaTelaRetorno(response, codBarras);
            _log.Info("ConsultaCodigoBarras... OK");
        }

        private void MontaTelaRetorno(ConsultaProdutoPorCodigoDeBarrasAppResponse response, string codBarras)
        {
            _log.Info("MontaTelaRetorno... ");

            bool existeErro = response.Mensagens?.Any() ?? false;
            if (existeErro)
            {
                RetornaErros(response.Mensagens);
            }
            else if (response.IsProdutoSendoEditado)
            {
                RetornaTelaProdutoSendoEditado(codBarras);
            }
            else
            {
                RetornaSucesso(response, codBarras);
            }

            _log.Info("MontaTelaRetorno... OK");
        }

        private void RetornaTelaProdutoSendoEditado(string codBarras)
        {
            _log.Info("RetornaTelaProdutoSendoEditado... ");
            Navigation.PushAsync(new ProdutoSendoEditado(codBarras));
            _log.Info("RetornaTelaProdutoSendoEditado... OK");
        }

        private async void RetornaSucesso(ConsultaProdutoPorCodigoDeBarrasAppResponse response, string codBarras)
        {
            _log.Info("RetornaSucesso... ");

            if (!response.IsProdutoEncontrado)
            {
                _log.Info("CapturaFotoDocumento... ");
                Stream imageStream = await CapturaFotoDocumento();
                _log.Info("CapturaFotoDocumento... OK");

                if (imageStream != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        imageStream.CopyTo(ms);
                        byte[] bytes = ms.ToArray();

                        response.Produto = new Produto() { Base64ImagemProduto = Convert.ToBase64String(bytes), CodigoDeBarras = codBarras };
                    }

                    Navigation.PushAsync(new ResultadoConsultaPorCodigoBarras(response.IsProdutoEncontrado, response.Produto));
                }
                else 
                {
                    Navigation.PushAsync(new MainPage());
                }

            }
            else
            {
                Navigation.PushAsync(new ResultadoConsultaPorCodigoBarras(response.IsProdutoEncontrado, response.Produto));
            }

            _log.Info("RetornaSucesso... OK");
        }

        private static async Task<Stream> CapturaFotoDocumento()
        {
            Stream imageStream = null;

            await CrossMedia.Current.Initialize();

            if (CrossMedia.Current.IsTakePhotoSupported && CrossMedia.Current.IsCameraAvailable)
            {
                var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { SaveToAlbum = false, SaveMetaData = false, CompressionQuality = 80 });

                if (photo != null)
                {
                    imageStream = photo.GetStream();
                }
            }

            return imageStream;
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
    }
}
