

using RaioXVegano.bo.Acao;
using RaioXVegano.ibo.Acao;
using RaioXVegano.iso.Acao;
using RaioXVegano.so.Acao;
using RaioXVegano.Util;
using SimpleInjector;
using System;
using Xamarin.Essentials;
using Mock = RaioXVegano.so.mock.Acao;

namespace RaioXVegano.di
{
    public static class DependencyInjection
    {
        private static Container _container;
        public static Container Container => _container;

        public static void Configure() 
        {
            _container = new Container();

            bool isLocal;

            try
            {
                isLocal = Consts.TRUE.Equals(Preferences.Get(Consts.RUN_LOCAL, Consts.TRUE).ToString());
            }
            catch (NotImplementedInReferenceAssemblyException)
            {
                isLocal = true;
            }

            if (!isLocal)
            {
                RegistraSOs();
            }
            else
            {
                RegistraSOsMock();
            }

            RegistraBOs();

            _container.Verify();
        }

        private static void RegistraSOs()
        {
            _container.Register<IAtualizaProdutoSO, AtualizaProdutoSO>();
            _container.Register<ICadastraProdutoSO, CadastraProdutoSO>();
            _container.Register<IConsultaProdutoPorCodigoDeBarrasSO, ConsultaProdutoPorCodigoDeBarrasSO>();
            _container.Register<ISalvarInformacoesLogSO, SalvarInformacoesLogSO>();

            Uri uri = new Uri(Preferences.Get(Consts.URL, string.Empty));
            _container.Register<Uri>(() => uri, Lifestyle.Singleton);
        }

        private static void RegistraSOsMock()
        {
            _container.Register<IAtualizaProdutoSO, Mock.AtualizaProdutoSO>();
            _container.Register<ICadastraProdutoSO, Mock.CadastraProdutoSO>();
            _container.Register<IConsultaProdutoPorCodigoDeBarrasSO, Mock.ConsultaProdutoPorCodigoDeBarrasSO>();
            _container.Register<ISalvarInformacoesLogSO, Mock.SalvarInformacoesLogSO>();
        }

        private static void RegistraBOs()
        {
            _container.Register<IAtualizaProdutoBO, AtualizaProdutoBO>();
            _container.Register<ICadastraProdutoBO, CadastraProdutoBO>();
            _container.Register<IConsultaProdutoPorCodigoDeBarrasBO, ConsultaProdutoPorCodigoDeBarrasBO>();
            _container.Register<ISalvarInformacoesLogBO, SalvarInformacoesLogBO>();
        }
    }
}
