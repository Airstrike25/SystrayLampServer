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


namespace systray_app
{
    public partial class SliderForm : Form
    {
        public SliderForm()
        {
            InitializeComponent();
            this.ControlBox = false;
            this.Text = string.Empty;
            
            
            trackBar1.Scroll += (s,
                                    e) =>
            {
                if (!clicked)
                    return;
               // Console.WriteLine("Detection found (clicked):"+trackBar1.Value);
            };
            trackBar1.MouseDown += (s,
                                    e) =>
            {
                clicked = true;
            };
            trackBar1.MouseUp += (s,
                                    e) =>
            {
                if (!clicked)
                    return;

                clicked = false;
                //Console.WriteLine("Detection found"+trackBar1.Value);
            };

            trackBar1.ValueChanged += new EventHandler(OnValueChanged);
        }
        public bool clicked = false;
        /// <summary>
        /// Hier ... zetten die moeten gebeuren op het moment dat de trackbar veranderd.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        
        private void OnValueChanged(object sender, EventArgs e)
        {
          
            int i = trackBar1.Value;
            //Debug.WriteLine("Value: " + i);
            if (clicked)
            {
                Console.WriteLine("Detection found");
                switch (trackBar1.Value)
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
            }//end if clicked
                        
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
