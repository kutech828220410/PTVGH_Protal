﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Basic;
namespace 屏東榮總傳送櫃
{
    static class Program
    {
        private static System.Threading.Mutex mutex;

        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            mutex = new System.Threading.Mutex(true, "OnlyRun");
            if (mutex.WaitOne(0, false))
            {
                Application.Run(new Form1());
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
