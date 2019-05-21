using System.Collections.Generic;

namespace Dani_TCC.Core.Models.Algoritmo
{
    public interface IBuscaArquivoPorPattern
    {
        string Pattern { get; }

        /// <summary>
        ///     Busca, na pasta citada e nas pastas filhas, de maneira recursiva, todas os arquivos do pattern informado
        /// </summary>
        /// <param name="pasta"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        IEnumerable<string> BuscarNaPasta(string pasta);
    }
}