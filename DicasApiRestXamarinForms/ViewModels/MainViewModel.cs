using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using DicasApiRestXamarinForms.Helpers;
using DicasApiRestXamarinForms.Models;
using DicasApiRestXamarinForms.Services.Base;
using DicasApiRestXamarinForms.Services.Refit;
using DicasApiRestXamarinForms.Services.Tradicional;
using DicasApiRestXamarinForms.Services.XamarinHelpers;
using Refit;
using Xamarin.Helpers;

namespace DicasApiRestXamarinForms.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Pokemon> Pokemons { get; }
        IApiService _ApiService;
        IRefitApiService _ApiServiceRefit;

        public MainViewModel()
        {
            Titulo = "Consumindo API";

            Pokemons = new ObservableCollection<Pokemon>();

             _ApiService = new ApiService();

            //   _ApiService = new ApiXamarinHelpersService();

            //Refit
            // _ApiServiceRefit = RestService.For<IRefitApiService>(Constantes.ApiBaseUrl);
            //CarregarPaginaRefit();

            CarregarPagina();
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

                for (int i = 1; i < 20; i++)
                {

                    var pokemon = await _ApiServiceRefit.GetPokemonAsync(i);

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

    }
}

