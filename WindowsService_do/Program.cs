using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace WindowsService_do
{

    /// <summary>
    /// 用于线程的委托。显示回调。用于显示线程的处理结果，传入线程类中并在线程类中调用
    /// </summary>
    /// <param name="OutPutHT">需要返回的数据集合</param>
    public delegate void delegateForThread(Hashtable OutPutHT);
    /// <summary>
    /// 用于线程的委托。显示回调。用于显示线程的处理结果，在主线程中配合Invoke使用
    /// </summary>
    /// <param name="OutPutHT">需要返回的数据集合</param>
    public delegate void delegateForThreadShow(Hashtable temp);


    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
