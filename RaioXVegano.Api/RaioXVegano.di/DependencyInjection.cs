using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using RaioXVegano.bo.Acao;
using RaioXVegano.entities;
using RaioXVegano.ibo.Acao;
using RaioXVegano.iso.BancoDeDados;
using RaioXVegano.so.BancoDeDados;
using RaioXVegano.Util;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System.Configuration;
using Mock = RaioXVegano.so.mock;

namespace RaioXVegano.di
{
    public static class DependencyInjection
    {
        public static Container Configure()
        {
            Container container = new Container();

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            bool isLocal = Consts.TRUE.Equals(ConfigurationManager.AppSettings[Consts.RUN_LOCAL].ToString());

            if (!isLocal)
            {
                RegistraSOs(container);
                
                RegistraAcessosExternos(container);
            }
            else 
            {
                RegistraSOsMock(container);
            }

            RegistraBOs(container);

            container.Verify();
            
            return container;
        }

        private static void RegistraSOs(Container container)
        {
            container.Register<IAtualizaProdutoSO, AtualizaProdutoSO>();
            container.Register<ICadastraProdutoSO, CadastraProdutoSO>();
            container.Register<IConsultaProdutoPorCodigoDeBarrasSO, ConsultaProdutoPorCodigoDeBarrasSO>();
            container.Register<ISalvarInformacoesLogSO, SalvarInformacoesLogSO>();
        }

        private static void RegistraSOsMock(Container container)
        {
            container.Register<IAtualizaProdutoSO, Mock.BancoDeDados.AtualizaProdutoSO>();
            container.Register<ICadastraProdutoSO, Mock.BancoDeDados.CadastraProdutoSO>();
            container.Register<IConsultaProdutoPorCodigoDeBarrasSO, Mock.BancoDeDados.ConsultaProdutoPorCodigoDeBarrasSO>();
            container.Register<ISalvarInformacoesLogSO, Mock.BancoDeDados.SalvarInformacoesLogSO>();
        }

        private static void RegistraBOs(Container container)
        {
            container.Register<IAtualizaProdutoBO, AtualizaProdutoBO>();
            container.Register<ICadastraProdutoBO, CadastraProdutoBO>();
            container.Register<IConsultaProdutoPorCodigoDeBarrasBO, ConsultaProdutoPorCodigoDeBarrasBO>();
            container.Register<ISalvarInformacoesLogBO, SalvarInformacoesLogBO>();
        }

        private static void RegistraAcessosExternos(Container container)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            bool isInicioProjeto = Consts.TRUE.Equals(ConfigurationManager.AppSettings[Consts.INICIAR_PROJETO]);

            //Instanciar o Fluent do NHibernate com a connection string e o local dos mapeamentos das tabelas.
            FluentConfiguration fluentConfiguration = Fluently.Configure().Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString).ShowSql())
                            .Mappings(c => c.FluentMappings.AddFromAssemblyOf<Produto>());

            //Se for inicio de projeto, limpar database e começar do zero. Se não, apenas atualizar as tabelas pelo mapeamento das classes.
            fluentConfiguration = isInicioProjeto ? fluentConfiguration.ExposeConfiguration(c => new SchemaExport(c).Create(true, true)) : fluentConfiguration.ExposeConfiguration(c => new SchemaUpdate(c).Execute(true, true));

            //Criar session factory
            ISessionFactory sessionFactory = fluentConfiguration.BuildSessionFactory();

            container.Register<ISession>(() => sessionFactory.OpenSession(), Lifestyle.Scoped);
        }
    }
}
