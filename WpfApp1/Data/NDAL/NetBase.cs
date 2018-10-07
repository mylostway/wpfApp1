using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfApp1.Data.NDAL
{
    public class NetTaskRequestIDGen
    {
        protected NetTaskRequestIDGen() { }

        private static int s_callerCounter = 1;

        /// <summary>
        /// 最大ID号数
        /// </summary>
        const int MAX_ID_COUNTER = int.MaxValue - 100000;

        public static string Gen()
        {
            var cc = Interlocked.Add(ref s_callerCounter, 1);

            if(cc >= MAX_ID_COUNTER) Interlocked.Exchange(ref s_callerCounter, 1);

            return string.Format("{0}:{1}", NetClientSession.LoginUserName, cc);
        }
    }

    
}
