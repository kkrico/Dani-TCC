using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dani_TCC.Core.GuardClause;

namespace Dani_TCC.Core.Model.Algoritmo
{
    public class BuscaEntidade<T> : IBuscaEntidade<T> where T : class
    {
        private readonly IBuscaArquivoPorPattern _buscaArquivoPorPattern;
        private readonly IParseArquivo<T> _parseArquivo;

        public BuscaEntidade(IBuscaArquivoPorPattern buscaArquivoPorPattern,
            IParseArquivo<T> parseArquivo)
        {
            _buscaArquivoPorPattern = buscaArquivoPorPattern;
            _parseArquivo = parseArquivo;
        }

        public IEnumerable<T> ListarEntidades(string pasta)
        {
            var resultado = new ConcurrentStack<T>();
            if (string.IsNullOrEmpty(pasta))
                return resultado;

            pasta = pasta.Trim();

            bool pastaExiste = Directory.Exists(pasta);
            if (!pastaExiste) return resultado;

            IEnumerable<string> localizacaoProvaveisEntidades = _buscaArquivoPorPattern.BuscarNaPasta(pasta);
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
                    _parseArquivo.Interpretar(localFisicoEntidadeNoDisco);

                if (arquivoInterpretado != null)
                    arquivosEncontrados.Enqueue(arquivoInterpretado);
            };
        }
    }
}