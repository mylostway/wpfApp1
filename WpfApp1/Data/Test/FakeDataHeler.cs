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
using WL_OA.Data.entity;

namespace WpfApp1.Data.Test
{
    /// <summary>
    /// 虚拟数据提供器，仅供UI单独测试调用
    /// </summary>
    public partial class FakeDataHelper
    {
        protected FakeDataHelper() { }

        public static FakeDataHelper Instance { get; private set; } = new FakeDataHelper();

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


        public Int16 GenRandomInt(int max = Int16.MaxValue - 1)
        {
            var r = new Random(GetRandSeed());
            return (Int16)r.Next(max);
        }

        

        public virtual object GenData(Type type)
        {
            var retObj = Activator.CreateInstance(type);
  
            var fieleds = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var eField in fieleds)
            {
                // 赋值protected字段（按照目前约定，Entity生成的Field为protected）
                var fieldTypeStr = eField.PropertyType.ToString().ToLower();

                var fieldName = eField.Name.ToLower();

                if (fieldTypeStr.IndexOf("string") >= 0)
                {
                    if(fieldName.IndexOf("name") >= 0)
                    {
                        eField.SetValue(retObj, GenRandomName());
                    }
                    else if(fieldName.IndexOf("cert") >= 0)
                    {
                        eField.SetValue(retObj, GenRandomCert());
                    }
                    else if (fieldName.IndexOf("phone") >= 0)
                    {
                        eField.SetValue(retObj, GenRandomPhone());
                    }
                    else if (fieldName.IndexOf("addr") >= 0)
                    {
                        eField.SetValue(retObj, GenRandomCountry());
                    }
                    else
                    {
                        eField.SetValue(retObj, GenRandomString());
                    }
                }
                else if (fieldTypeStr.IndexOf("int") >= 0)
                {
                    eField.SetValue(retObj, GenRandomInt());
                }
                else if (fieldTypeStr.IndexOf("bool") >= 0)
                {
                    eField.SetValue(retObj, GenRandomBool());
                }
                else if(fieldTypeStr.IndexOf("date") >= 0)
                {
                    eField.SetValue(retObj, GenRandomDate());
                }
                else
                {
                    if(eField.PropertyType.IsSubclassOf(typeof(BaseEntity<int>)))
                    {
                        eField.SetValue(retObj, GenData(eField.PropertyType));
                    }
                    if(eField.PropertyType.IsArray)
                    {
                        // TODO：目前不知道数组怎么反射创建。。。
                        eField.SetValue(retObj, null);
                    }
                    else if(eField.PropertyType.IsGenericType)
                    {
                        if (fieldTypeStr.IndexOf("list") > 0)
                        {
                            var genericType = eField.PropertyType.GetGenericArguments()[0];

                            var childElemsCount = GenRandomInt(5) + 1;

                            var genListType = typeof(List<>).MakeGenericType(new System.Type[] { genericType });

                            var list = Activator.CreateInstance(genListType);

                            var addMethod = list.GetType().GetMethod("Add");

                            for (var i = 0; i < childElemsCount; i++)
                            {                                
                                addMethod.Invoke(list, new object[] { GenData(genericType) });
                            }

                            eField.SetValue(retObj, list);
                        }
                    }
                }
            }

            return retObj;
        }
    }
}
