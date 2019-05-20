using System.Linq;
using Dani_TCC.Core.Model;

namespace Dani_TCC.Core.Service
{
    public class QuestaoService : IQuestaoService
    {
        private readonly DbContext _dbContext;

        public QuestaoService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int QuantidadeQuestoes()
        {
            return _dbContext.Questao.Count();
        }
    }
}