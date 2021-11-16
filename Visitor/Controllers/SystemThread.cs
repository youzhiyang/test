using System;
using System.Collections.Generic;
using System.Threading;

namespace Visitor.Controllers
{
    /// <summary>
    /// 系统线程
    /// </summary>
    public class SystemThread
    {
        /// <summary>
        /// 实例化对象
        /// </summary>
        public static readonly SystemThread Instance = new SystemThread();

        /// <summary>
        /// 系统线程字典
        /// </summary>
        private static readonly Dictionary<Enum, Thread> _threadDict = new Dictionary<Enum, Thread>();

        /// <summary>
        /// 运行系统线程
        /// </summary>
        public void RunThreads()
        {
        }
    }
}