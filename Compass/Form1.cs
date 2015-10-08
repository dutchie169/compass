using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compass
{
    public partial class Form1 : Form
    {
        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        static private int Baud = 9600;
        static private string portName = "COM4";
        private SerialPort Port;
        private int heading = 0;
        private System.Drawing.Pen myPen;
        private System.Drawing.Graphics formGraphics;

        private static int width = 500;
        private static int height = 500;
        
        
        public Form1()
        {
            InitializeComponent();

            Port = new SerialPort(portName, Baud);
            Port.Open();
            
            myPen = new System.Drawing.Pen(System.Drawing.Color.Black, 5);

            formGraphics = this.CreateGraphics();

            myTimer.Tick += new EventHandler(TimerEvent);
            myTimer.Interval = 20;
            myTimer.Start();
        }
    

        private void TimerEvent(Object myObject, EventArgs myEventArgs)
        {
            formGraphics.Clear(Color.Gray);
            formGraphics.DrawLine(myPen, width / 2, height / 2, width / 2 + width / 2 * (float)Math.Sin(heading / 180d * Math.PI), height / 2 + height / 2 * (float)Math.Cos(heading / 180d * Math.PI));
            Thread.Sleep(50);
            heading = Convert.ToInt16(Port.ReadLine());
            labelHeading.Text = Convert.ToString(heading);
        }
    }
}
