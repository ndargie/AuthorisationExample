using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFrontEnd.Consumer
{
    public interface IRemoteConsumer
    {
        public T GetData<T>(string url);
        public T GetData<T>(string url, string accessToken);
        public T PostData<T>(string url, string accessToken, object postObject);
    }
}
