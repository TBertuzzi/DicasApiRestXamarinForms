using System;
using MonkeyCache.SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DicasApiRestXamarinForms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Barrel.ApplicationId = "PokemonCache";

            MainPage = new MainPage();
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
