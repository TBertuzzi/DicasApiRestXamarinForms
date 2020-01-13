using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DicasApiRestXamarinForms.Models;

namespace DicasApiRestXamarinForms.Services.Base
{
    public interface IApiService
    {
        Task<List<Pokemon>> GetPokemonsAsync();
    }
}
