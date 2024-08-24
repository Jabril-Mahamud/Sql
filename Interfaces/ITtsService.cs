using System.Threading.Tasks;

public interface ITtsService
{
    Task<byte[]> GetTextToSpeechAsync(string text);
}
