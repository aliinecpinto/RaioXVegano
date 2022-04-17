using RaioXVegano.App.Helpers;
using RaioXVegano.di;
using Xamarin.Forms;

namespace RaioXVegano.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //Arquivo de Configuração do App
            ConfigurationManager.Configure();
            
            //Injeção de Dependência
            DependencyInjection.Configure();
            
            MainPage = new NavigationPage(new MainPage()) 
            { 
                BackgroundColor = Color.White
            };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
