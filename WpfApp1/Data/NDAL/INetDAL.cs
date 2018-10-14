using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WL_OA.NET;

namespace WpfApp1.Data.NDAL
{
    public interface INetClientDAL
    {
        void RequestAsync(string url, NetHandler callBack);

        void RequestAsync<T>(string url, T param, NetHandler callBack);
    }
}
