using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Juliett.Properties;

namespace Juliett
{
    public partial class Form1 : Form
    {
        string[] settings;
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern uint GetCurrentThreadId();
        [DllImport("user32")]
        public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern bool PostMessage(IntPtr hWnd, int Msg, char wParam, int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetFocus();
        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        globalKeyboardHook gkh1 = new globalKeyboardHook();




        private void functionlOne(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    foreach (char ch in textBox1.Text)
                    {
                        PostMessage(GetFocusedControl(), 0x0102, ch, 1);
                    }
                    break;
                case Keys.F2:
                    foreach (char ch in textBox2.Text)
                    {
                        PostMessage(GetFocusedControl(), 0x0102, ch, 1);
                    }
                    break;
                case Keys.F3:
                    foreach (char ch in textBox3.Text)
                    {
                        PostMessage(GetFocusedControl(), 0x0102, ch, 1);
                    }
                    break;
                    break;
                case Keys.F4:
                    foreach (char ch in textBox4.Text)
                    {
                        PostMessage(GetFocusedControl(), 0x0102, ch, 1);
                    }
                    break;
                case Keys.F5:
                    foreach (char ch in textBox5.Text)
                    {
                        PostMessage(GetFocusedControl(), 0x0102, ch, 1);
                    }
                    break;
                case Keys.F6:
                    foreach (char ch in textBox6.Text)
                    {
                        PostMessage(GetFocusedControl(), 0x0102, ch, 1);
                    }
                    break;
                case Keys.F7:
                    foreach (char ch in textBox7.Text)
                    {
                        PostMessage(GetFocusedControl(), 0x0102, ch, 1);
                    }
                    break;
                case Keys.F8:
                    foreach (char ch in textBox8.Text)
                    {
                        PostMessage(GetFocusedControl(), 0x0102, ch, 1);
                    }
                    break;
                case Keys.F9:
                    foreach (char ch in textBox9.Text)
                    {
                        PostMessage(GetFocusedControl(), 0x0102, ch, 1);
                    }
                    break;
                case Keys.F10:
                    foreach (char ch in textBox10.Text)
                    {
                        PostMessage(GetFocusedControl(), 0x0102, ch, 1);
                    }
                    break;
                case Keys.F11:
                    //----------------//
                    break;
                case Keys.F12:
                    //----------------//
                    break;
                default:
                    break;
            }

            
            e.Handled = true;//запрет получения клавиш другими приложениями
        }

        private IntPtr GetFocusedControl()
        {
            IntPtr hFocus;
            IntPtr hFore;
            uint id = 0;
            //узнаем в каком окне находится фокус ввода
            hFore = GetForegroundWindow();
            //подключаемся к процессу
            AttachThreadInput(GetWindowThreadProcessId(hFore, out id), GetCurrentThreadId(), true);
            //получаем хэндл фокуса
            hFocus = GetFocus();
            //отключаемся от процесса
            AttachThreadInput(GetWindowThreadProcessId(hFore, out id), GetCurrentThreadId(), false);
            return hFocus;
        }
        public Form1()
        {
            StartupFile();

            InitializeComponent();
            ShowTheForm(this);
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            #region HotKeys
            gkh1.HookedKeys.Add(Keys.F1);
            gkh1.HookedKeys.Add(Keys.F2);
            gkh1.HookedKeys.Add(Keys.F3);
            gkh1.HookedKeys.Add(Keys.F4);
            gkh1.HookedKeys.Add(Keys.F5);
            gkh1.HookedKeys.Add(Keys.F6);
            gkh1.HookedKeys.Add(Keys.F7);
            gkh1.HookedKeys.Add(Keys.F8);
            gkh1.HookedKeys.Add(Keys.F9);
            gkh1.HookedKeys.Add(Keys.F10);
            gkh1.HookedKeys.Add(Keys.F11);
            gkh1.HookedKeys.Add(Keys.F12);
            #endregion
            gkh1.KeyDown += new KeyEventHandler(functionlOne);
        }


        private void StartupFile()
        {
            if (!File.Exists("set.ini"))
                File.Create("set.ini");
            else
            {
                settings = File.ReadAllLines("set.ini");
                SettingsFileToProgram();
            }
        }

        private void SettingsFileToProgram()
        {

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
            gkh1.unhook();
            Close();
        }

        private void StopTheProgram(object sender, EventArgs e)
        {
            gkh1.unhook();
        }

        private void StartTheProgramm(object sender, EventArgs e)
        {
            gkh1.hook();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Normal;
            ShowTheForm(this);
            ShowInTaskbar = true;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutButton(this, null);
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartTheProgramm(this, null);
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopTheProgram(this, null);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClosingTheProgram(this, null);
        }
    }
}
