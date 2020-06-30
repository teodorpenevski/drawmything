using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DrawMyThing
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainMenu());
            /*Server1 s = new Server1();
            Task t1 = new Task(() => { Application.Run(new Form1()); });
            Task t2 = new Task(() => { Application.Run(new Form1()); });
            Task t3 = new Task(() => { Application.Run(new Form1()); });
            Task t4 = new Task(() => { Application.Run(new Form1()); });
            s.StartServer();   
            Thread.Sleep(2000);
            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            Thread.Sleep(2000);
            s.StartGame();
            t1.Wait();
            t2.Wait();
            t3.Wait();
            t4.Wait();
            s.Close();*/
        }
    }
}
