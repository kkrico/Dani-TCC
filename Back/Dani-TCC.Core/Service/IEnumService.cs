using System.Collections.Generic;
using System.Linq;
using Dani_TCC.Core.ViewModel;
using Google.Protobuf.WellKnownTypes;

namespace Dani_TCC.Core.Service
{
    public interface IEnumService
    {
        IEnumerable<EnumViewModel> GetAll<T>() where T: struct;
    }
    
    public class EnumeService: IEnumService
    {
        public IEnumerable<EnumViewModel> GetAll<T>() where T : struct
        {
            // Pegar todos os valores
            // Gerar uma lista de enumViewModel
            // retornar para expor os enums na API
            return Enumerable.Empty<EnumViewModel>();
        }
    }
}