using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Data.Test
{
    /// <summary>
    /// 虚拟数据提供器，仅供UI单独测试调用
    /// </summary>
    public class FakeDataHeler<T>
        where T : class,new()
    {
        protected FakeDataHeler() { }

        public static FakeDataHeler<T> Instance { get; private set; } = new FakeDataHeler<T>();

        public virtual ObservableCollection<T> CreateFakeDataCollection(int genNum = 37)
        {
            var retCollection = new ObservableCollection<T>();

            for (int i = 0; i < genNum; i++)
            {
                retCollection.Add(GenData());
            }

            return retCollection;
        }

        static readonly char[] CHARS_ARR = "ab我cd efg@h ijklm nop是qrs tuv w#xy来AB！CDE测试FG HI、据咯JKL啊%MNOP Q?RS TU这些VW都是X数Y".ToArray();

        protected int GetRandSeed()
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            //int seed = (int)DateTime.Now.Ticks;
            int seed = BitConverter.ToInt32(b, 0);
            return seed;
        }

        protected string GenRandomStr(int maxLen = 13)
        {            
            var r = new Random(GetRandSeed());

            StringBuilder sb = new StringBuilder(maxLen + 1);

            for(var i = 0;i < maxLen;i++)
            {
                var idx = r.Next(CHARS_ARR.Length);
                sb.Append(CHARS_ARR[idx]);
            }

            return sb.ToString();
        }

        protected bool GenRandomBool()
        {
            var r = new Random(GetRandSeed());
            return r.Next(10) > 5 ? true : false;
        }


        protected int GenRandomInt(int max = 99999)
        {
            var r = new Random(GetRandSeed());
            return r.Next(max);
        }


        protected virtual T GenData()
        {
            var retObj = new T();

            Type t = typeof(T);

            var fieleds = t.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var eField in fieleds)
            {
                if (eField.IsPrivate)
                {
                    var fieldTypeStr = eField.FieldType.ToString().ToLower();

                    if (fieldTypeStr.IndexOf("string") >= 0)
                    {
                        eField.SetValue(retObj, GenRandomStr());
                    }
                    else if (fieldTypeStr.IndexOf("int") >= 0)
                    {
                        eField.SetValue(retObj, GenRandomInt());
                    }
                    else if (fieldTypeStr.IndexOf("bool") >= 0)
                    {
                        eField.SetValue(retObj, GenRandomBool());
                    }
                }
            }

            return retObj;
        }
    }


}
