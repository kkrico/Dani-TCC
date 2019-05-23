using System;
using System.Collections.Generic;
using System.Linq;
using Dani_TCC.Core.Extensions;
using Dani_TCC.Core.Models;
using Dani_TCC.Core.ViewModels;

namespace Dani_TCC.Core.Services
{
    public class EnumService: IEnumService
    {
        public EnumService(DB_PESQUISA_TCCContext context)
        {
            
        }
        public IEnumerable<EnumViewModel> GetAll<T>() where T : struct
        {
            // Pegar todos os valores
            // Gerar uma lista de enumViewModel
            // retornar para expor os enums na API
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(item => new EnumViewModel(item.GetHashCode(), item.Description()));
        }
    }
}