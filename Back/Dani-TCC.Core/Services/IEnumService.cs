using System.Collections.Generic;
using Dani_TCC.Core.ViewModels;

namespace Dani_TCC.Core.Services
{
    public interface IEnumService
    {
        IEnumerable<EnumViewModel> GetAll<T>() where T: struct;
    }
}