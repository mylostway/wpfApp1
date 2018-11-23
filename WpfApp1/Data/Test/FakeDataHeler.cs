using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WL_OA.Data;
using WL_OA.Data.dto;
using WL_OA.NET;
using WpfApp1.Data.NDAL;

using Bogus;

namespace WpfApp1.Data.Test
{
    public class FakeDataHeler
    {
        protected FakeDataHeler() { }

        public static FakeDataHeler Instance { get; private set; } = new FakeDataHeler();

        /// <summary>
        /// Bogus 数据制造器的默认locale
        /// </summary>
        const string FAKER_DEFAULT_LOCALE = "zh_CN";

        /// <summary>
        /// 单机测试数据源（随机制作测试数据）
        /// </summary>
        /// <param name="genNum"></param>
        /// <returns></returns>
        public virtual ObservableCollection<object> CreateFakeDataCollection(Type type,int genNum = FakeDataHelerSettings.DEFAULT_GEN_DATA_NUM)
        {
            var retCollection = new ObservableCollection<object>();

            genNum = genNum < 0 ? FakeDataHelerSettings.DEFAULT_GEN_DATA_NUM : genNum;

            for (int i = 0; i < genNum; i++)
            {
                retCollection.Add(GenData(type));
            }

            return retCollection;
        }

        /// <summary>
        /// 单机测试数据源（随机制作测试数据）
        /// </summary>
        /// <param name="genNum"></param>
        /// <returns></returns>
        public virtual HttpResponse CreateFakeDataNetResponse(Type type,int genNum = FakeDataHelerSettings.DEFAULT_GEN_DATA_NUM)
        {
            var genData = CreateFakeDataCollection(type,genNum).ToList();

            var queryResult = new QueryResult<IList<object>>(genData);

            var rsp = new HttpResponse(JsonHelper.SerializeTo(queryResult));

            return rsp;
        }



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

            for (var i = 0; i < maxLen; i++)
            {
                var idx = r.Next(FakeDataHelerSettings.CHARS_ARR.Length);
                sb.Append(FakeDataHelerSettings.CHARS_ARR[idx]);
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

        

        protected virtual object GenData(Type type)
        {
            var retObj = Activator.CreateInstance(type);
  
            var fieleds = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var eField in fieleds)
            {
                // 赋值protected字段（按照目前约定，Entity生成的Field为protected）
                var fieldTypeStr = eField.FieldType.ToString().ToLower();

                var fieldName = eField.Name;

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

            return retObj;
        }
    }
}
