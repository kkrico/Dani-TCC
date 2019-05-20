namespace Dani_TCC.Core.Model.Algoritmo
{
    public interface IParseArquivo<out T> where T : class
    {
        T Interpretar(string localFisicoArquivo);
    }
}