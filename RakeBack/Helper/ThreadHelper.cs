using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RakeBack.Helper
{
    /// <summary>
    /// 线程辅助类
    /// </summary>
    public class ThreadHelper
    {
        /// <summary>
        /// 开启一个新线程执行任务
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Thread StartNew(Action action)
        {
            var th = new Thread(new ThreadStart(action));
            th.IsBackground = true;
            th.Start();
            return th;
        }

        /// <summary>
        /// 开启一个新线程执行任务，若在超时时间内未完成，则直接关闭该线程
        /// </summary>
        /// <param name="action"></param>
        /// <param name="outTime">超时时间(s)</param>
        /// <returns></returns>
        public static Thread StartNew(Action action, int outTime)
        {
            bool isFinish = false;
            var th = new Thread(new ThreadStart(() =>
            {
                action.Invoke();
                isFinish = true;
            }));
            th.IsBackground = true;
            th.Start();

            if (outTime > 0)
            {
                var moniThead = new Thread(new ThreadStart(() =>
                {
                    var last = DateTime.Now;
                    while (isFinish == false)
                    {
                        if ((DateTime.Now - last).TotalSeconds > outTime)
                        {
                            if (th.ThreadState != ThreadState.Stopped)
                                th.Abort();
                            return;
                        }
                        Thread.Sleep(1*1000);
                    }
                }));
                moniThead.IsBackground = true;
                moniThead.Start();
            }
            return th;
        }
    }
}
