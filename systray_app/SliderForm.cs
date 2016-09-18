using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net; // WebRequest
using System.Diagnostics;
using System.Threading;
using System.Timers;


namespace systray_app
{
    public partial class SliderForm : Form
    {
        
        public int valueT;
        public bool timercheck;

        public SliderForm()
        {
            InitializeComponent();////
            this.ControlBox = false;
            this.Text = string.Empty;
            trackBar1.ValueChanged += new EventHandler(OnValueChanged);
            System.Timers.Timer Timera = new System.Timers.Timer();
            Timera.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            Timera.Interval = 1000;
            //Timera.Enabled = true;
            if (timercheck)
            {
                Timera.Enabled = true;
            }
            else
            {
                Timera.Enabled = false;

            }
            

            
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            
            //Debug.WriteLine("Value: " + i);

            Console.WriteLine("Detection found");
            switch (valueT)
            {
                case 1:// alles uit
                    new Thread(() => HttpRequestThread(0, 0)).Start();
                    break;

                case 2:
                    new Thread(() => HttpRequestThread(0, 1)).Start();
                    break;

                case 3:
                    new Thread(() => HttpRequestThread(1, 0)).Start();
                    break;

                case 4:
                    new Thread(() => HttpRequestThread(1, 1)).Start();
                    break;

            }
            timercheck = false;
           
            
        }
        
        /// <summary>
        /// Hier ... zetten die moeten gebeuren op het moment dat de trackbar veranderd.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        
        private void OnValueChanged(object sender, EventArgs e)
        {

            if (!timercheck)
            {
                int i = trackBar1.Value;
                Debug.WriteLine("Value: " + i);
                timercheck = true;
            }

           
            //Console.WriteLine("Detection found"); 
            //switch (trackBar1.Value)
            //{
            //    case 1:// alles uit
            //        new Thread(() => HttpRequestThread(0, 0)).Start();
            //        break;

            //    case 2:
            //        new Thread(() => HttpRequestThread(0, 1)).Start();
            //        break;

            //    case 3:
            //        new Thread(() => HttpRequestThread(1, 0)).Start();
            //        break;

            //    case 4:
            //        new Thread(() => HttpRequestThread(1, 1)).Start();
            //        break;

            //}
        
                        
        }

        public void HttpRequestThread(int sfeer, int hoofd)
        {
            WebRequest request;
            WebResponse response;
            Debug.WriteLine("Starting response   sfeer: " + sfeer +"   hoofd : "+hoofd );
            request = WebRequest.Create("http://192.168.1.113:3232/digitalWrite/56/"+ sfeer); // uit, sfeer
            try
            {
                response = request.GetResponse();
                response.Close();
            }
            catch (WebException ex)
            {
                Debug.WriteLine("Exception caught ");
            }
            request = WebRequest.Create("http://192.168.1.113:3232/digitalWrite/55/"+hoofd); // uit, hoofd
            try
            {
                response = request.GetResponse();
                response.Close();
            }
            catch (WebException ex)
            {
                Debug.WriteLine("Exception caught");
            }
        }

        protected override void OnActivated(EventArgs e)
        {
            Point p = new Point(Cursor.Position.X - 30, Cursor.Position.Y - 260);
            this.Location = p;
            base.OnActivated(e);
        }

        protected override void OnDeactivate(EventArgs e)
        {
            this.Visible = false;
            base.OnDeactivate(e);
        }
    }
}
