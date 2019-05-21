namespace Dani_TCC.Core.Models.Algoritmo
{
    public interface IParseArquivo<out T> where T : class
    {
        T Interpretar(string localFisicoArquivo);
    }
}