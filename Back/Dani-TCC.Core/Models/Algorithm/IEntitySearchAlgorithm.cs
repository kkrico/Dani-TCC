using System.Collections.Generic;

namespace Dani_TCC.Core.Models.Algorithm
{
    /// <summary>
    ///     Dado determinado pasta, este algoritmo le os arquivos e extrai as entidades
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntitySearchAlgorithm<out T>
    {
        IEnumerable<T> ListEntities(string folder);
    }
}