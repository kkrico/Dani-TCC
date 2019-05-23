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

        public IEnumerable<T> ListarEntidades(string pasta)
        {
            var resultado = new ConcurrentStack<T>();
            if (string.IsNullOrEmpty(pasta))
                return resultado;

            pasta = pasta.Trim();

            bool pastaExiste = Directory.Exists(pasta);
            if (!pastaExiste) return resultado;

            IEnumerable<string> localizacaoProvaveisEntidades = _patternFileSearchAlgorithm.GetFileContentsOnFolder(pasta);
            if (localizacaoProvaveisEntidades == null)
                return resultado;

            IEnumerable<T> entidades = Interpretar(localizacaoProvaveisEntidades);
            return entidades?.Where(e => e != null) ?? resultado;
        }

        private IEnumerable<T> Interpretar(IEnumerable<string> localizacaoProvavelEntidade)
        {
            Guard.IsNotNull(localizacaoProvavelEntidade, nameof(localizacaoProvavelEntidade));

            var arquivosEncontrados = new ConcurrentQueue<T>();

            Parallel.ForEach(localizacaoProvavelEntidade,
                TentarExtrairEntidade(arquivosEncontrados));

            return arquivosEncontrados.Where(web => web != null);
        }

        private Action<string> TentarExtrairEntidade(ConcurrentQueue<T> arquivosEncontrados)
        {
            return localFisicoEntidadeNoDisco =>
            {
                if (string.IsNullOrEmpty(localFisicoEntidadeNoDisco)) return;

                localFisicoEntidadeNoDisco = localFisicoEntidadeNoDisco.Trim();
                localFisicoEntidadeNoDisco = Path.GetFullPath(localFisicoEntidadeNoDisco);

                T arquivoInterpretado =
                    _fileParser.Parse(localFisicoEntidadeNoDisco);

                if (arquivoInterpretado != null)
                    arquivosEncontrados.Enqueue(arquivoInterpretado);
            };
        }
    }
}