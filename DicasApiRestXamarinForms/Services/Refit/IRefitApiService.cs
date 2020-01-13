using System;
using System.Threading.Tasks;
using DicasApiRestXamarinForms.Models;
using Refit;

namespace DicasApiRestXamarinForms.Services.Refit
{
    public interface IRefitApiService
    {
        [Get("/pokemon/{id}")]
        Task<Pokemon> GetPokemonAsync(int id);
    }
}
