using System.Collections.Generic;

namespace Dani_TCC.Core.Models.Algorithm
{
    public interface IPatternFileSearchAlgorithm
    {
        string Pattern { get; }

        /// <summary>
        ///     Busca, na pasta citada e nas pastas filhas, de maneira recursiva, todas os arquivos do pattern informado
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetFileContentsOnFolder(string folder);
    }
}