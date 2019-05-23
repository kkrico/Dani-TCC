using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dani_TCC.Core.GuardClause;

namespace Dani_TCC.Core.Models.Algorithm
{
    public class EntitySearchAlgorithmAlgorithm<T> : IEntitySearchAlgorithm<T> where T : class
    {
        private readonly IPatternFileSearchAlgorithm _patternFileSearchAlgorithm;
        private readonly IFileParser<T> _fileParser;

        public EntitySearchAlgorithmAlgorithm(IPatternFileSearchAlgorithm patternFileSearchAlgorithm,
            IFileParser<T> fileParser)
        {
            _patternFileSearchAlgorithm = patternFileSearchAlgorithm;
            _fileParser = fileParser;
        }

        public IEnumerable<T> ListEntities(string folder)
        {
            var resultado = new ConcurrentStack<T>();
            if (string.IsNullOrEmpty(folder))
                return resultado;

            folder = folder.Trim();

            bool pastaExiste = Directory.Exists(folder);
            if (!pastaExiste) return resultado;

            IEnumerable<string> probablyFilesLocation = _patternFileSearchAlgorithm.GetFileContentsOnFolder(folder);
            if (probablyFilesLocation == null)
                return resultado;

            IEnumerable<T> entities = Parse(probablyFilesLocation);
            return entities?.Where(e => e != null) ?? resultado;
        }

        private IEnumerable<T> Parse(IEnumerable<string> localizacaoProvavelEntidade)
        {
            Guard.IsNotNull(localizacaoProvavelEntidade, nameof(localizacaoProvavelEntidade));

            var arquivosEncontrados = new ConcurrentQueue<T>();

            Parallel.ForEach(localizacaoProvavelEntidade,
                TentarExtrairEntidade(arquivosEncontrados));

            return arquivosEncontrados.Where(web => web != null);
        }

        private Action<string> TentarExtrairEntidade(ConcurrentQueue<T> foundFiles)
        {
            return localFisicoEntidadeNoDisco =>
            {
                if (string.IsNullOrEmpty(localFisicoEntidadeNoDisco)) return;

                localFisicoEntidadeNoDisco = localFisicoEntidadeNoDisco.Trim();
                localFisicoEntidadeNoDisco = Path.GetFullPath(localFisicoEntidadeNoDisco);

                T parsedFile =
                    _fileParser.Parse(localFisicoEntidadeNoDisco);

                if (parsedFile != null)
                    foundFiles.Enqueue(parsedFile);
            };
        }
    }
}