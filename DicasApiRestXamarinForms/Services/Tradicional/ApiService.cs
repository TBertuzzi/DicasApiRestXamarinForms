using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DicasApiRestXamarinForms.Helpers;
using DicasApiRestXamarinForms.Models;
using DicasApiRestXamarinForms.Services.Base;
using Newtonsoft.Json;

namespace DicasApiRestXamarinForms.Services.Tradicional
{
    public class ApiService : IApiService
    {
       

        public async Task<List<Pokemon>> GetPokemonsAsync()
        {
            List<Pokemon> pokemons = new List<Pokemon>();

           
            try
            {
                var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                for (int i = 1; i < 20; i++)
                {
                    var response = await httpClient.GetAsync($"{Constantes.ApiBaseUrl}/pokemon/{i}").ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        {

                            pokemons.Add(JsonConvert.DeserializeObject<Pokemon>(
                                await new StreamReader(responseStream)
                                    .ReadToEndAsync().ConfigureAwait(false)));

                        }
                    }
                }

                return pokemons;
            }
            catch (Exception ex)
            {
                return new List<Pokemon>();
            }         }      }

}

