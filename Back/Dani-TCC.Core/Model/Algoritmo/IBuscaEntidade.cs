﻿using System.Collections.Generic;

namespace Dani_TCC.Core.Model.Algoritmo
{
    /// <summary>
    ///     Dado determinado pasta, este algoritmo le os arquivos e extrai as entidades
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface IBuscaEntidade<out T>
    {
        IEnumerable<T> ListarEntidades(string pasta);
    }
}