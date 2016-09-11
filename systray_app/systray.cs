using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace systray_app
{
    class Systray : ApplicationContext
    {
        SliderForm sliderForm;
        NotifyIcon notifyIcon;
        public Systray()
        {
            sliderForm = new SliderForm();
            sliderForm.Visible = false;
            //MenuItem configMenuItem = new MenuItem("Configuration", new EventHandler(ShowConfig));
            MenuItem exitMenuItem = new MenuItem("Exit", new EventHandler(Exit));

            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = new Icon(SystemIcons.Application, 80, 80);//(SystemIcons.Application, 80, 80); voor icoontje
            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[]
                {/* configMenuItem,*/ exitMenuItem });
            notifyIcon.Visible = true;
            notifyIcon.Click += new EventHandler(OnClick);
        }

        private void OnClick(object sender, EventArgs e)
        {
            sliderForm.Activate();
            sliderForm.Visible = true;
        }

        private void Exit(object sender, EventArgs e)
        {
            sliderForm.Visible = false;
            sliderForm.Dispose();
            notifyIcon.Visible = false;
            notifyIcon.Dispose();
            Application.Exit();
        }

        SliderForm configWindow = new SliderForm();

        

        private void ShowConfig(object sender, EventArgs e)
        {
            // If we are already showing the window, merely focus it.
            if (configWindow.Visible)
            {
                configWindow.Activate();
            }
            else
            {
                configWindow.ShowDialog();
            }
        }

    }
}
