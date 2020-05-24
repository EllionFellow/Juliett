using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hotkeys;

namespace Juliett
{
    public partial class Form1 : Form
    {
        private GlobalHotkey ctrl1;
        public Form1()
        {
            InitializeComponent();
            ShowTheForm(this);
            ContextMenu notifyContextMenu = new ContextMenu();
            notifyContextMenu.MenuItems.Add(0, new MenuItem("About", new EventHandler(AboutButton)));
            notifyContextMenu.MenuItems.Add(1, new MenuItem("Exit", new EventHandler(ClosingTheProgram)));
            notifyIcon1.ContextMenu = notifyContextMenu;
            ctrl1 = new GlobalHotkey(2,Keys.D1,this);
            ctrl1.Register();
        }

        #region SmoothWindows
        private async void ShowTheForm(Form form)
        {
            while (form.Opacity < 0.9)
            {
                form.Opacity += 0.04;
                await Task.Delay(10);
            }

            form.Opacity = 1;
        }

        private void HandleHotkey()
        {
            Close();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
            {
                HandleHotkey();
            }
            base.WndProc(ref m);
        }

        private void UnShowTheForm(Form form)
        {
            while (form.Opacity > 0.1)
            {
                form.Opacity -= 0.04;
                Thread.Sleep(10);
            }

            form.Opacity = 0;
        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            UnShowTheForm(this);
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
            ctrl1.Register();

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnShowTheForm(this);
        }
        #endregion

        private void AboutButton(object sender, EventArgs e)
        {
            MessageBox.Show("Made by EllioN\nEspecially for Nimfea Noctis", "About", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void ClosingTheProgram(object sender, EventArgs e)
        {
            Close();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Normal;
            ShowTheForm(this);
            ShowInTaskbar = true;
        }
    }
}
