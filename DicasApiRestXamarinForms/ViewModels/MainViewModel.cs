using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DicasApiRestXamarinForms.Helpers;
using DicasApiRestXamarinForms.Models;
using DicasApiRestXamarinForms.Services.Base;
using DicasApiRestXamarinForms.Services.Refit;
using DicasApiRestXamarinForms.Services.Tradicional;
using DicasApiRestXamarinForms.Services.XamarinHelpers;
using MonkeyCache.SQLite;
using Refit;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Helpers;

namespace DicasApiRestXamarinForms.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string _key = "PokemonCache";

        public ObservableCollection<Pokemon> Pokemons { get; }
        IApiService _ApiService;
        IRefitApiService _ApiServiceRefit;

        public MainViewModel()
        {
            Titulo = "Consumindo API";

            //Xamarin Essentials
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

            Pokemons = new ObservableCollection<Pokemon>();

             _ApiService = new ApiService();

            //   _ApiService = new ApiXamarinHelpersService();

            //Refit
         //    _ApiServiceRefit = RestService.For<IRefitApiService>(Constantes.ApiBaseUrl);

            var existingList = Barrel.Current.Get<List<Pokemon>>(_key) ?? new List<Pokemon>();

            //if (existingList.Count == 0)
            //{
                //Refit
               // CarregarPaginaRefit();

                CarregarPagina();

            //}
            //else
            //    CarregaCache();
        }

        

        public  async Task CarregarPagina()
        {
            try
            {
                Ocupado = true;

                var pokemons = await _ApiService.GetPokemonsAsync();

                Pokemons.Clear();

                foreach (var pokemon in pokemons)
                {
                    pokemon.Image = GetImageStreamFromUrl(pokemon.Sprites.FrontDefault.AbsoluteUri);
                    Pokemons.Add(pokemon);
                }



            }
            catch (Exception ex)
            {

            }
            finally
            {

                Ocupado = false;
            }
        }

        public async Task CarregarPaginaRefit()
        {
            try
            {
                Ocupado = true;
                Pokemons.Clear();

             //   var existingList = Barrel.Current.Get<List<Pokemon>>(_key) ?? new List<Pokemon>();

                for (int i = 1; i < 20; i++)
                {

                    var pokemon = await _ApiServiceRefit.GetPokemonAsync(i);

                    pokemon.Image = GetImageStreamFromUrl(pokemon.Sprites.FrontDefault.AbsoluteUri);

                    //if (!existingList.Any(e => e.Id == pokemon.Id))
                    //{
                    //    existingList.Add(pokemon);
                    //}

                    Pokemons.Add(pokemon);
                }

                //existingList = existingList.ToList();

                //Barrel.Current.Add(_key, existingList, TimeSpan.FromDays(2));

            }
            catch (Exception ex)
            {

            }
            finally
            {

                Ocupado = false;
            }
        }

        private async Task CarregaCache()
        {
            var pokemonsCache = Barrel.Current.Get<List<Pokemon>>(_key);

            foreach (var pokemon in pokemonsCache)
            {
                Pokemons.Add(pokemon);
            }
        }


        public static byte[] GetImageStreamFromUrl(string url)
        {
            try
            {
                using (var webClient = new HttpClient())
                {
                    var imageBytes = webClient.GetByteArrayAsync(url).Result;

                    return imageBytes;

                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return null;

            }
        }

        async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var IsNotConnected = e.NetworkAccess != NetworkAccess.Internet;

            if (IsNotConnected)
                await Application.Current.MainPage.DisplayAlert("Atenção", "Estamos sem internet :(", "OK");
        }

    }
}

