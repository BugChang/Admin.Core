﻿using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ZhonTai.Admin.Core.Attributes;
using ZhonTai.Admin.Core.Configs;
using ZhonTai.Admin.Core.Dto;
using ZhonTai.DynamicApi;
using ZhonTai.DynamicApi.Attributes;

namespace ZhonTai.Admin.Services.Cache
{
    /// <summary>
    /// 缓存服务
    /// </summary>
    [DynamicApi(Area = "admin")]
    public class CacheService : BaseService, ICacheService, IDynamicApi
    {
        public CacheService()
        {
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        public IResultOutput GetList()
        {
            var list = new List<object>();

            var appConfig = LazyGetRequiredService<AppConfig>();
            Assembly[] assemblies = DependencyContext.Default.RuntimeLibraries
                .Where(a => appConfig.AssemblyNames.Contains(a.Name) || a.Name == "ZhonTai.Admin")
                .Select(o => Assembly.Load(new AssemblyName(o.Name))).ToArray();

            foreach (Assembly assembly in assemblies)
            {
                var types = assembly.GetExportedTypes().Where(a => a.GetCustomAttribute<ScanCacheKeysAttribute>() != null);
                foreach (Type type in types)
                {
                    var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                    foreach (FieldInfo field in fields)
                    {
                        var descriptionAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;

                        list.Add(new
                        {
                            field.Name,
                            Value = field.GetRawConstantValue().ToString(),
                            descriptionAttribute?.Description
                        });
                    }
                }
            }

            return ResultOutput.Ok(list);
        }

        /// <summary>
        /// 清除缓存
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <returns></returns>
        public async Task<IResultOutput> ClearAsync(string cacheKey)
        {
            Logger.LogWarning($"{User.Id}.{User.Name}清除缓存[{cacheKey}]");
            await Cache.DelByPatternAsync(cacheKey);
            return ResultOutput.Ok();
        }
    }
}