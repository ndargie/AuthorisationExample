using System.Threading.Tasks;

namespace Zetes.WebGui.Consumers
{
    public interface IRemoteConsumer
    {
        Task<TResponse> GetDate<TResponse>(string url, string accessToken);
        Task<TResponse> PostData<TResponse>(string url, object obj, string accessToken);
    }
}
