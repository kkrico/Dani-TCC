using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dani_TCC.Core.GuardClause;
using Dani_TCC.Core.Models.Enums;

namespace Dani_TCC.Core.Models.Algorithm
{
    public class PatternFileSearchAlgorithmFileSearchAlgorithm : IPatternFileSearchAlgorithm
    {
        private readonly ParseType _parseType;
        private ConcurrentBag<string> _foundFiles;

        public PatternFileSearchAlgorithmFileSearchAlgorithm(string pattern, ParseType parseType)
        {
            _parseType = parseType;
            Guard.IsNotNull(pattern, nameof(pattern));
            Pattern = pattern;
            _foundFiles = new ConcurrentBag<string>();
        }

        public string Pattern { get; }

        public IEnumerable<string> GetFileContentsOnFolder(string folder)
        {
            Guard.IsNotNull(folder, nameof(folder));
            Guard.IsNotNull(Pattern, nameof(Pattern));

            SearchOnFoldersAndFiles(folder, Pattern);

            List<string> resultado = _foundFiles.ToList();
            // Como esta kra é chamado em um contexto de singleton, eu limpo os encontrados
            _foundFiles = new ConcurrentBag<string>();
            return resultado;
        }

        private void SearchOnFoldersAndFiles(string folder, string pattern)
            {
            Guard.IsNotNull(pattern, nameof(pattern));
            Guard.IsNotNull(folder, nameof(folder));

            try
            {
                folder = Path.GetFullPath(folder);
            }
            catch (Exception e) when (e is PathTooLongException || e is UnauthorizedAccessException)
            {
            }

            if (!Directory.Exists(folder))
                return;
            try
            {
                IEnumerable<string> arquivos = GetFiles(folder);
                Parallel.ForEach(arquivos, files => { CheckIfFileWasFound(files, pattern); });
            }
            catch (Exception e) when (e is PathTooLongException || e is UnauthorizedAccessException || e is IOException)
            {
            }

            try
            {
                IEnumerable<string> pastas = Directory.EnumerateDirectories(folder);
                Parallel.ForEach(pastas, p => { SearchOnFoldersAndFiles(p, pattern); });
            }
            catch (Exception e) when (e is PathTooLongException || e is UnauthorizedAccessException || e is IOException)
            {
                // ignored
            }
        }

        private IEnumerable<string> GetFiles(string pasta)
        {
            if (_parseType == ParseType.Relative)
                return Directory.EnumerateFiles(pasta).Where(file =>
                    file.IndexOf(Pattern, StringComparison.InvariantCultureIgnoreCase) >= 0);

            return Directory.EnumerateFiles(pasta, Pattern);
        }

        private void CheckIfFileWasFound(string arquivo, string pattern)
        {
            string fileName = Path.GetFileName(arquivo);
            if (IsFileFound(pattern, fileName))
                AddToFound(arquivo);
        }

        private void AddToFound(string arquivo)
        {
            _foundFiles.Add(Path.GetFullPath(arquivo));
        }

        private bool IsFileFound(string pattern, string nomeDoArquivo)
        {
            if (_parseType != ParseType.Relative)
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