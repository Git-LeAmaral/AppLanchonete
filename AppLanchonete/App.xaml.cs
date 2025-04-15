using AppLanchonete.Pages;

namespace AppLanchonete
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new InscricaoPage());
        }
    }
}
