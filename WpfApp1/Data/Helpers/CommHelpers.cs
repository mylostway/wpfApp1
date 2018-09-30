using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Data
{
    public class DateTimeHelper
    {
        private DateTimeHelper() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addDay"></param>
        /// <returns></returns>
        public static string GetDayStringOnAddingDays(int addDay = 0)
        {
            if(0 == addDay) return DateTime.Now.ToString("yyyy-MM-dd");
            return DateTime.Now.AddDays(addDay).ToString("yyyy-MM-dd");
        }
    }
}
