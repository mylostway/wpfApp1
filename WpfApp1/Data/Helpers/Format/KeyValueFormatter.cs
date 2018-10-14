﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WpfApp1.Data.Helpers
{
    /// <summary>
    /// 表示默认键值对列化工具
    /// </summary>
    public class KeyValueFormatter 
    {
        /// <summary>
        /// 使用CamelCase的KeyValue属性解析约定
        /// </summary>
        private readonly static PropertyContractResolver useCamelCaseResolver = new PropertyContractResolver(true, FormatScope.KeyValueFormat);

        /// <summary>
        /// 不使用CamelCase的KeyValue属性解析约定
        /// </summary>
        private readonly static PropertyContractResolver noCamelCaseResolver = new PropertyContractResolver(false, FormatScope.KeyValueFormat);

        /// <summary>
        /// 序列化对象为键值对
        /// </summary>
        /// <param name="name">对象名称</param>
        /// <param name="obj">对象实例</param>
        /// <param name="options">选项</param>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<string, string>> Serialize(string name, object obj, FormatOptions options)
        {
            if (obj == null)
            {
                return Enumerable.Empty<KeyValuePair<string, string>>();
            }

            if (options == null)
            {
                options = new FormatOptions();
            }

            var setting = this.CreateSerializerSettings(options);
            var serializer = JsonSerializer.Create(setting);
            var keyValueWriter = new KeyValuePairWriter(name);

            serializer.Serialize(keyValueWriter, obj);
            return keyValueWriter;
        }


        /// <summary>
        /// 创建序列化配置     
        /// </summary>
        /// <param name="options">格式化选项</param>
        /// <returns></returns>
        protected virtual JsonSerializerSettings CreateSerializerSettings(FormatOptions options)
        {
            var useCamelCase = options.UseCamelCase == true;
            var setting = new JsonSerializerSettings
            {
                DateFormatString = options?.DateTimeFormat,
                ContractResolver = useCamelCase ? useCamelCaseResolver : noCamelCaseResolver
            };

            setting.Converters.Add(new KeyValuePairConverter(useCamelCase));
            return setting;
        }
    }
}
