namespace Dani_TCC.Core.Services
{
    public class QuestionarioService : IQuestionarioService
    {
        public QuestionarioService()
        {
        }

        public int QuantidadeQuestoes()
        {
            return default(int);
        }

        public void RegisterQuestionario(RegisterQuestionarioViewModel model)
        {
            throw new System.NotImplementedException();
        }
    }
    
    public class RegisterQuestionarioViewModel
    {
        public int? Etnia { get; set; }
        public int? Genero { get; set; }
        public int? FaixaEtaria { get; set; }
        public int? RendaFamiliar { get; set; }
        public int? Sexualidade { get; set; }
    }
}