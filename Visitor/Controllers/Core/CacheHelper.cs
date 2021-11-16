using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace Visitor.Controllers.Core
{
    /// <summary>
    /// 缓存操作
    /// </summary>
    public class CacheHelper
    {
        /// <summary>
        /// 用于保存缓存名称的缓存
        /// </summary>
        private static readonly string _cacheList = "JKKG_Visitor_CacheList";

        /// <summary>
        /// 缓存名称分隔符
        /// </summary>
        private static readonly string _cacheSeparator = ";;@@,@@;;";

        /// <summary>
        /// 从 System.Web.Caching.Cache 对象检索指定项。
        /// </summary>
        /// <typeparam name="T">缓存项类型</typeparam>
        /// <param name="key">要检索的缓存项的标识符。</param>
        /// <returns>检索到的缓存项，未找到该键时为 null。</returns>
        public static T Get<T>(string key)
        {
            var value = HttpRuntime.Cache[key];
            try
            {
                return (T)value;
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// 将指定项添加到 System.Web.Caching.Cache 对象，该对象具有依赖项、到期和优先级策略以及一个委托（可用于在从 Cache 移除插入项时通知应用程序）。
        /// </summary>
        /// <param name="key">用于引用该项的缓存键。</param>
        /// <param name="value">要添加到缓存的项。</param>
        /// <param name="dependencies">该项的文件依赖项或缓存键依赖项。 当任何依赖项更改时，该对象即无效，并从缓存中移除。 如果没有依赖项，则此参数包含 null。</param>
        /// <param name="absoluteExpiration">所添加对象将到期并被从缓存中移除的时间。 如果使用可调到期，则 absoluteExpiration 参数必须为 System.Web.Caching.Cache.NoAbsoluteExpiration。</param>
        /// <param name="slidingExpiration">最后一次访问所添加对象时与该对象到期时之间的时间间隔。 如果该值等效于 20 分钟，则对象在最后一次被访问 20 分钟之后将到期并从缓存中移除。
        /// 如果使用绝对到期，则 slidingExpiration 参数必须为 System.Web.Caching.Cache.NoSlidingExpiration。</param>
        /// <param name="priority">对象的相对成本，由 System.Web.Caching.CacheItemPriority 枚举表示。 缓存在退出对象时使用该值；具有较低成本的对象在具有较高成本的对象之前被从缓存移除。</param>
        /// <param name="onRemoveCallback">在从缓存中移除对象时所调用的委托（如果提供）。 当从缓存中删除应用程序的对象时，可使用它来通知应用程序。</param>
        /// <returns>如果添加的项之前存储在缓存中，则为表示该项的对象；否则为 null。</returns>
        public static object Add(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration,
            CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
        {
            if (key != _cacheList)
            {
                AddCacheList(key);
            }
            return HttpRuntime.Cache.Add(key, value, dependencies, absoluteExpiration, slidingExpiration, priority, onRemoveCallback);
        }

        /// <summary>
        /// 从应用程序的 System.Web.Caching.Cache 对象移除指定项。
        /// </summary>
        /// <param name="key">要移除的缓存项的 System.String 标识符。</param>
        /// <param name="kind">查找标识符的方式。</param>
        /// <returns>从 Cache 移除的项。 如果未找到键参数中的值，则返回 null。</returns>
        public static IList<object> Remove(string key, SelectKind kind = SelectKind.Exact)
        {
            var objects = new List<object>();
            switch (kind)
            {
                case SelectKind.Fuzzy:
                    var keys = GetCacheList(key, kind);
                    foreach (var myKey in keys)
                    {
                        objects.Add(Remove(myKey));
                    }
                    break;
                default:
                    objects.Add(Remove(key));
                    break;
            }
            return objects;
        }

        /// <summary>
        /// 从应用程序的 System.Web.Caching.Cache 对象移除指定项。
        /// </summary>
        /// <param name="key">要移除的缓存项的 System.String 标识符。</param>
        /// <returns>从 Cache 移除的项。 如果未找到键参数中的值，则返回 null。</returns>
        private static object Remove(string key)
        {
            return HttpRuntime.Cache.Remove(key);
        }

        /// <summary>
        /// 添加缓存的标识符
        /// </summary>
        /// <param name="keys">标识符</param>
        private static void AddCacheList(params string[] keys)
        {
            var cacheList = Get<string>(_cacheList);
            if (cacheList == null)
            {
                cacheList = "";
                Add(_cacheList, cacheList, null, new DateTime(2099, 12, 31), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
            }
            foreach (var key in keys)
            {
                if (!cacheList.Contains(_cacheSeparator + key + _cacheSeparator))
                {
                    cacheList = string.Join(_cacheSeparator, cacheList, key);
                }
            }
            Remove(_cacheList);
            Add(_cacheList, cacheList, null, new DateTime(2099, 12, 31), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
        }

        /// <summary>
        /// 获取缓存的标识符
        /// </summary>
        /// <param name="key">标识符</param>
        /// <param name="kind">查询方式</param>
        /// <returns></returns>
        private static IList<string> GetCacheList(string key, SelectKind kind = SelectKind.Exact)
        {
            var cacheList = Get<string>(_cacheList);
            if (cacheList == null)
            {
                cacheList = "";
                Add(_cacheList, cacheList, null, new DateTime(2099, 12, 31), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
            }
            var list = cacheList.Split(new string[] { _cacheSeparator }, StringSplitOptions.RemoveEmptyEntries).ToList();
            switch (kind)
            {
                case SelectKind.Fuzzy:
                    return list.Where(o => o.Contains(key)).ToList();
                default:
                    return list.Where(o => o == key).ToList();
            }
        }

        /// <summary>
        /// 移除缓存的标识符
        /// </summary>
        /// <param name="keys">标识符</param>
        private static void RemoveCacheList(params string[] keys)
        {
            var cacheList = Get<string>(_cacheList);
            if (cacheList == null)
            {
                cacheList = "";
                Add(_cacheList, cacheList, null, new DateTime(2099, 12, 31), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
            }
            foreach (var key in keys)
            {
                if (cacheList.Contains(_cacheSeparator + key + _cacheSeparator))
                {
                    var list = cacheList.Split(new string[] { _cacheSeparator }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    list.Remove(key);
                    cacheList = string.Join(_cacheSeparator, list);
                }
            }
            Remove(_cacheList);
            Add(_cacheList, cacheList, null, new DateTime(2099, 12, 31), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
        }

        /// <summary>
        /// 查询方式
        /// </summary>
        public enum SelectKind
        {
            /// <summary>
            /// 模糊
            /// </summary>
            Fuzzy,

            /// <summary>
            /// 精确
            /// </summary>
            Exact
        }
    }
}