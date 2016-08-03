using System.Collections;
using System.Threading;
using System.Data;
using System.Net;
using System.IO;

namespace WindowsService_do
{
    public class TongYong
    {
        //向线程传递的回调参数
        private delegateForThread DForThread;
        //向线程传递的数据参数
        private Hashtable InPutHT;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="PHT">需要传入线程的参数</param>
        /// <param name="DFT">线程委托</param>
        public TongYong(Hashtable PHT, delegateForThread DFT)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            DForThread = DFT;
            InPutHT = PHT;
        }

        /// <summary>
        /// 开始执行线程
        /// </summary>
        public void BeginRun()
        {
            //防卡死，停100毫秒
            Thread.Sleep(100);
            string url = File.ReadAllText(@"c:\WindowsService_do_url.txt");

            WebClient client = new WebClient();
            //定期访问微信消息发送接口
            while (true)
            {
                client.DownloadString(url);
                Thread.Sleep(1000*60*5); //五分钟跑一次
            }
            


            ////填充传入参数哈希表
            //Hashtable OutPutHT = new Hashtable();
            //OutPutHT["返回值"] = content;
            //if (DForThread != null)
            //{
            //    DForThread(OutPutHT);
            //}


        }


    }
}
