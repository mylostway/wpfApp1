using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLib.Data
{
    public class DataHelper
    {
        /// <summary>
        /// 对浮点数向上取整
        /// </summary>
        /// <param name="dNum"></param>
        /// <returns></returns>
        public static int Ceil(double dNum)
        {
            int nNum = (int)Math.Floor(dNum);
            if (dNum > nNum) return nNum + 1;
            return nNum;
        }
    }
}
