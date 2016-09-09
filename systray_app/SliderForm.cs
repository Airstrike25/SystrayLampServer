using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace systray_app
{
    public partial class SliderForm : Form
    {
        public SliderForm()
        {
            InitializeComponent();
            this.ControlBox = false;
            this.Text = string.Empty;
            trackBar1.ValueChanged += new EventHandler(OnValueChanged);
        }

        /// <summary>
        /// Hier shit zetten die moeten gebeuren op het moment dat de trackbar veranderd.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnValueChanged(object sender, EventArgs e)
        {
            new NotImplementedException();
        }

        protected override void OnDeactivate(EventArgs e)
        {
            this.Visible = false;
            base.OnDeactivate(e);
        }
    }
}
