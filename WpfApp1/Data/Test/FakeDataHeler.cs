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
using System.ComponentModel.DataAnnotations;

namespace WpfApp1.Data
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
        /// 生成数据类型为T的测试数据列表 ObservableCollection<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="genNum"></param>
        /// <returns></returns>
        public virtual ObservableCollection<T> CreateFakeDataCollection<T>(int genNum = FakeDataHelerSettings.DEFAULT_GEN_DATA_NUM)
            where T : class, new()
        {
            var retCollection = new ObservableCollection<T>();
            genNum = genNum < 0 ? FakeDataHelerSettings.DEFAULT_GEN_DATA_NUM : genNum;
            for (int i = 0; i < genNum; i++)
            {
                retCollection.Add(GenData<T>());
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

        
        /// <summary>
        /// 随机布尔值
        /// </summary>
        /// <returns></returns>
        protected bool GenRandomBool()
        {
            var r = new Random(GetRandSeed());
            return r.Next(10) > 5 ? true : false;
        }


        /// <summary>
        /// 随机整数（Int16）
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public Int16 GenRandomInt(int max = Int16.MaxValue - 1)
        {
            var r = new Random(GetRandSeed());
            return (Int16)r.Next(max);
        }



        /// <summary>
        /// 生成类型T的测试数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T GenData<T>()
            where T : class, new()
        {
            return GenData(typeof(T)) as T;
        }


        /// <summary>
        /// 生成类型Type的测试数据
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual object GenData(Type type)
        {
            var retObj = Activator.CreateInstance(type);
  
            var fieleds = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var eField in fieleds)
            {
                // 赋值protected字段（按照目前约定，Entity生成的Field为protected）
                var fieldTypeStr = eField.PropertyType.ToString().ToLower();

                // BaseEntity的字段不生成值
                if (eField.Name == "Fid" || eField.Name == "Fstate") continue;

                var fieldName = eField.Name.ToLower();

                var notGenAttr = eField.GetCustomAttribute<FakeDataNotGenAttribute>(true);

                if (null != notGenAttr) continue;

                if (fieldTypeStr.IndexOf("string") >= 0)
                {
                    var setStringVal = "";
                    if (fieldName.IndexOf("name") >= 0)
                    {
                        setStringVal = GenRandomName();
                    }
                    else if (fieldName.IndexOf("cert") >= 0)
                    {
                        setStringVal = GenRandomCert();
                    }
                    else if (fieldName.IndexOf("phone") >= 0)
                    {
                        setStringVal = GenRandomPhone();
                    }
                    else if (fieldName.IndexOf("addr") >= 0)
                    {
                        setStringVal = GenRandomCountry();
                    }
                    else if (fieldName.IndexOf("_no") >= 0)
                    {
                        var sb = new StringBuilder();
                        for (var i = 0; i < 3; i++)
                        {
                            sb.Append(GenRandomInt().ToString());
                        }
                        setStringVal = sb.ToString(0, 10);
                    }
                    else if (fieldName.IndexOf("num") >= 0)
                    {
                        setStringVal = GenRandomInt().ToString();
                    }
                    else if (fieldName.IndexOf("province") >= 0)
                    {
                        setStringVal = GenRandomCountry();
                    }
                    else if (fieldName.IndexOf("city") >= 0)
                    {
                        setStringVal = GenRandomCountry();
                    }
                    else if (fieldName.IndexOf("area") >= 0)
                    {
                        setStringVal = GenRandomCountry();
                    }
                    else if (fieldName.IndexOf("forshort") >= 0)
                    {
                        setStringVal = GenRandomWord();
                    }
                    else if(fieldName.IndexOf("memo") >= 0)
                    {
                        setStringVal = GenRandomString();
                    }
                    else
                    {
                        setStringVal = GenRandomWord();
                    }

                    var lenAttr = eField.GetCustomAttribute<MaxLengthAttribute>();

                    if(null != lenAttr && setStringVal.Length > lenAttr.Length)
                    {
                        setStringVal = setStringVal.Substring(0, lenAttr.Length);
                    }

                    eField.SetValue(retObj, setStringVal);
                }
                else if (fieldTypeStr.IndexOf("int") >= 0)
                {
                    // 设定有属性的，按照属性限制数值范围
                    int randMax = Int16.MaxValue;

                    var rangeAttr = eField.GetCustomAttribute(typeof(RangeAttribute));

                    if(null != rangeAttr)
                    {
                        randMax = Convert.ToInt16((rangeAttr as RangeAttribute).Maximum);
                    }
                    else
                    {
                        var bitUsageAttr = eField.GetCustomAttribute<BitUsageFieldAttribute>();

                        if(null != bitUsageAttr)
                        {
                            randMax = (int)Math.Pow(2, bitUsageAttr.MaxBit) - 1;                            
                        }
                    }

                    if (randMax <= 0) randMax = 1;

                    eField.SetValue(retObj, GenRandomInt(randMax));
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
