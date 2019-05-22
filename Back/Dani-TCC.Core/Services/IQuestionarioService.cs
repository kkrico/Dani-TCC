namespace Dani_TCC.Core.Services
{
    public interface IQuestionarioService
    {
        int QuantidadeQuestoes();
        void RegisterQuestionario(RegisterQuestionarioViewModel model);
    }
}