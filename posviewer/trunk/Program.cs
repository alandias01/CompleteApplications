using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Maple.Utilities;
using Maple.Utilities.DgvHelper;

namespace Maple.Dtc.PositionClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //setup the tracer            
            MapleTracer t = new MapleTracer("PositionViewer", new EmailList("jimp@mapleusa.com"));
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SplashScreen.ShowSplashScreen();
            
            Application.DoEvents();
            SplashScreen.SetStatus("Loading...");

            if (args.Length == 0)
            {
                Application.Run(new MainForm("5239"));
            }
            else
            {
                Application.Run(new MainForm(args[0]));
            }
            
        }
    }

    public interface IDgvForm
    {
        //EventHandler handleCellSelected();
        event CellSelectEventHandler handleCellSelected;
     
    }
}
