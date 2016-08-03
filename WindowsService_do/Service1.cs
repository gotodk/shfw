using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace WindowsService_do
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }
        public Thread trd;
        protected override void OnStart(string[] args)
        {
            Hashtable InPutHT = new Hashtable();
            TongYong UL = new TongYong(InPutHT, null);
            trd = new Thread(new ThreadStart(UL.BeginRun));
            trd.IsBackground = true;
            trd.Start();
        }

        protected override void OnStop()
        {
            try
            {
                trd.Abort();
            }
            catch { }
        }
    }
}
