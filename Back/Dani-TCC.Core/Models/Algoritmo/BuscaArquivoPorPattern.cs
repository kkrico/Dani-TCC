using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dani_TCC.Core.GuardClause;

namespace Dani_TCC.Core.Models.Algoritmo
{
    public class BuscaArquivoPorPattern : IBuscaArquivoPorPattern
    {
        private readonly TipoParse _tipoParse;
        private ConcurrentBag<string> _arquivosEncontrados;

        public BuscaArquivoPorPattern(string pattern, TipoParse tipoParse)
        {
            _tipoParse = tipoParse;
            Guard.IsNotNull(pattern, nameof(pattern));
            Pattern = pattern;
            _arquivosEncontrados = new ConcurrentBag<string>();
        }

        public string Pattern { get; }

        public IEnumerable<string> BuscarNaPasta(string pasta)
        {
            Guard.IsNotNull(pasta, nameof(pasta));
            Guard.IsNotNull(Pattern, nameof(Pattern));

            BuscarNasPastasEArquivos(pasta, Pattern);

            List<string> resultado = _arquivosEncontrados.ToList();
            // Como esta kra é chamado em um contexto de singleton, eu limpo os encontrados
            _arquivosEncontrados = new ConcurrentBag<string>();
            return resultado;
        }

        private void BuscarNasPastasEArquivos(string pasta, string pattern)
        {
            Guard.IsNotNull(pattern, nameof(pattern));
            Guard.IsNotNull(pasta, nameof(pasta));

            try
            {
                pasta = Path.GetFullPath(pasta);
            }
            catch (Exception e) when (e is PathTooLongException || e is UnauthorizedAccessException)
            {
            }

            if (!Directory.Exists(pasta))
                return;
            try
            {
                IEnumerable<string> arquivos = ObterArquivos(pasta);
                Parallel.ForEach(arquivos, arquivo => { VerificarSeArquivoEncontrado(arquivo, pattern); });
            }
            catch (Exception e) when (e is PathTooLongException || e is UnauthorizedAccessException || e is IOException)
            {
            }

            try
            {
                IEnumerable<string> pastas = Directory.EnumerateDirectories(pasta);
                Parallel.ForEach(pastas, p => { BuscarNasPastasEArquivos(p, pattern); });
            }
            catch (Exception e) when (e is PathTooLongException || e is UnauthorizedAccessException || e is IOException)
            {
                // ignored
            }
        }

        private IEnumerable<string> ObterArquivos(string pasta)
        {
            if (_tipoParse == TipoParse.ParseRelativo)
                return Directory.EnumerateFiles(pasta).Where(file =>
                    file.IndexOf(Pattern, StringComparison.InvariantCultureIgnoreCase) >= 0);

            return Directory.EnumerateFiles(pasta, Pattern);
        }

        private void VerificarSeArquivoEncontrado(string arquivo, string pattern)
        {
            string nomeArquivo = Path.GetFileName(arquivo);
            if (IsArquivoEncontrado(pattern, nomeArquivo))
                AdicionarAosEncontrados(arquivo);
        }

        private void AdicionarAosEncontrados(string arquivo)
        {
            _arquivosEncontrados.Add(Path.GetFullPath(arquivo));
        }

        private bool IsArquivoEncontrado(string pattern, string nomeDoArquivo)
        {
            if (_tipoParse != TipoParse.ParseRelativo)
                return nomeDoArquivo.IndexOf(pattern, StringComparison.InvariantCultureIgnoreCase) == 0;

            var match = false;
            int idx = nomeDoArquivo.IndexOf(
                pattern,
                StringComparison.InvariantCultureIgnoreCase);
            if (idx >= 0)
                match = true;

            return match;

        }
    }
}